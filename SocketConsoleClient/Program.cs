using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
			TcpClient client;
			NetworkStream stream;
			StreamReader sr;
			StreamWriter sw;

			Console.WriteLine("Press Any Key to connect");
			Console.ReadKey();
			client = new TcpClient("127.0.0.1", 12345);
			stream = client.GetStream();
			sr = new StreamReader(stream);
			sw = new StreamWriter(stream);
			Console.WriteLine("Connected.");
			Console.WriteLine("Usage:    dir [path]     subdir [path]");
			while (true)
			{
				Console.WriteLine("Enter your query:");
				string line = Console.ReadLine();
				sw.WriteLine(line);
				sw.Flush();
				Console.WriteLine(sr.ReadLine());
			}
		}
    }
}
