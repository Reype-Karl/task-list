using System;

namespace Tasks
{
	public class RealConsole : IConsole
	{
		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public void Write(string format)
		{
            Console.Write(format);
		}

		public void WriteLine(string format)
		{
            Console.WriteLine(format);
		}

		public void WriteLine()
		{
            Console.WriteLine();
		}
	}
}
