using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Waiting for connection...");
			TcpListener listener = new TcpListener(IPAddress.Any, 12345);
			listener.Start();
			Socket client = listener.AcceptSocket();
			Console.WriteLine("Client connected");
			NetworkStream stream = new NetworkStream(client);
			StreamReader sr = new StreamReader(stream);
			StreamWriter sw = new StreamWriter(stream);
			while (true)
			{
				string request = sr.ReadLine();
				Console.WriteLine("got request: "+request);
				string[] split = request.Split(' ');
				DirectoryInfo info;
				switch (split[0])
				{
					case "dir":
						info = new DirectoryInfo(split[1]);
						if (info.Exists)
						{
							sw.WriteLine(split[1] + " exists, and was created on " + info.CreationTime);
						}
						else
						{
							sw.WriteLine(split[1]+" was not found. Please try again.");
						}
						sw.Flush();
						break;
					case "subdir":
						info = new DirectoryInfo(split[1]);
						if (info.Exists)
						{
							string response = split[1] + " exists, and was created on " + info.CreationTime;
							DirectoryInfo[] dirs = info.GetDirectories();
							if(dirs.Length > 0)
							{
								response += " The subdirectories are: ";
								foreach (var dir in dirs)
								{
									response += dir.Name+", ";
								}
								response = response.Trim(' ').Trim(',');
							}
							else
							{
								response += " There are no subdirectories";
							}
							sw.WriteLine(response);
						}
						else
						{
							sw.WriteLine(split[1] + " was not found. Please try again.");
						}
						sw.Flush();
						break;
					default:
						break;
				}
			}
        }
    }
}
