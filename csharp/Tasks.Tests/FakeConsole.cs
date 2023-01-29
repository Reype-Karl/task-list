using NUnit.Framework;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Tasks
{
    public class FakeConsole : IConsole
    {
        private readonly TextReader inputReader;
        private readonly TextWriter inputWriter;

        private readonly TextReader outputReader;
        private readonly TextWriter outputWriter;

        public FakeConsole()
        {
            Stream inputStream = new BlockingStream(new ProducerConsumerStream());
            this.inputReader = new StreamReader(inputStream);
            this.inputWriter = new StreamWriter(inputStream) { AutoFlush = true };

            Stream outputStream = new BlockingStream(new ProducerConsumerStream());
            this.outputReader = new StreamReader(outputStream);
            this.outputWriter = new StreamWriter(outputStream) { AutoFlush = true };
        }

        public string ReadLine()
        {
            return inputReader.ReadLine();
        }

        public void Write(string format)
        {
            outputWriter.Write(format);
        }

        public void WriteLine(string format)
        {
            outputWriter.WriteLine(format);
        }

        public void WriteLine()
        {
            outputWriter.WriteLine();
        }

        public void SendInput(string input)
        {
            inputWriter.Write(input);
        }

        public string RetrieveOutput(int length)
        {
            var buffer = new char[length];
            outputReader.ReadBlock(buffer, 0, length);
            return new string(buffer);
        }
    }
}