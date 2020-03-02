using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWriteRest
{
	class Program
	{
		static void Main(string[] args)
		{
			RestWorker restWorker = new RestWorker();
			restWorker.Start();
			Console.ReadKey();
		}
	}
}
