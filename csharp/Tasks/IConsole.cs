using System;

namespace Tasks
{
	public interface IConsole
	{
		string ReadLine();

		void Write(string format);

		void WriteLine(string format);

		void WriteLine();
	}
}
