/// <reference path="../typings/tsd.d.ts" />
///

import nodeunit = require('nodeunit');
import stream = require('stream');
import task_list = require('../src/task_list');

class Expectation {
    constructor(public ctxt: TestCtxt) {
        this.ctxt.expectations.push(this)
    }

    test(): boolean {
        this.ctxt.test.ok(false, "Override me");
        return true;
    }
}

class TestCtxt
{
    input = new stream.PassThrough();
    output = new stream.PassThrough();
    expectations : Expectation[] = [];
    tl = new task_list.TaskList(this.input, this.output);

    constructor(public test: nodeunit.Test){
        var count = 0;
        this.output.on('readable', () => {
            if(count >= this.expectations.length) {
                this.test.ok(false, "Got output than expected proabably didn't quit")
                this.test.done();
            } else if(this.expectations[count].test()) {
                count += 1;
            }
        });
        this.output.on('end', () => {
            this.test.equal(count, this.expectations.length);
            this.test.done();
        });
    }

    read(expected) {
        var data = this.output.read(expected.length);
        if (data != null) {
//            console.log("read" + data);
            this.test.equal(data.toString(), expected);
            return true;
        }
        return false;
    }

    run() {
        this.test.expect(this.expectations.length + 1);
        this.tl.run();
    }
}

class ExecuteExpectation extends Expectation {
    prompt = '> ';

    constructor(ctxt: TestCtxt, public cmd:string) {
        super(ctxt)
    }

    test() {
        if (!this.ctxt.read(this.prompt))
            return false;
        this.ctxt.input.write(this.cmd + '\n');
        return true;
    }
}

class OutputExpectation extends Expectation {

    constructor(ctxt: TestCtxt, public out:string) {
        super(ctxt)
    }

    test() {
        return this.ctxt.read(this.out)
    }
}

export function application_test(test: nodeunit.Test) {
    var ctxt = new TestCtxt(test);

    function execute(cmd: string) {
        new ExecuteExpectation(ctxt, cmd);
    }

    execute('show');

    execute("add project secrets");
    execute("add task secrets Eat more donuts.");
    execute("add task secrets Destroy all humans.");

    execute('quit');

    ctxt.run();
}
