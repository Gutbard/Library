using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

	public class MenuItem : IPrintable, IProcessable
	{
		private Action _processHandler;

		public int Num { get; }
		public string Title { get; }

		public MenuItem(int Num, string title, Action processHandler)
		{
			Title = title;
			this.Num = Num;
			_processHandler = processHandler;
		}

		public void Print()
		{
			Console.WriteLine($"{Num}.{Title}");
		}

		public void Process()
		{
			_processHandler?.Invoke();
		}
	}
}
