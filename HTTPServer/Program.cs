using System;
using System.Text;
using SimpleTCP;
using System.Net;
using System.Net.Sockets;
using SimpleLogs4Net;
using System.IO;
namespace HTTPServer
{
	internal class Program
	{
		static SimpleTcpServer server;
		static Log log;
		static IPAddress serverIP = IPAddress.Parse("192.168.10.50");
		static int serverPort = 80;
		static void Main(string[] args)
		{
			log = new Log("Logs\\", true, "HTTPServerLog");
			server = new SimpleTcpServer();
			server.StringEncoder = Encoding.ASCII;
			server.DataReceived += Server_DataReceived;
			server.ClientConnected += Server_ClientConnected;
			server.ClientDisconnected += Server_ClientDisconnected;
			server.Start(serverIP, serverPort);
			Log.AddEvent(new Event("Server Started", Event.Type.Informtion));
            if (!Directory.Exists("Docs"))
            {
                Directory.CreateDirectory("Docs");
            }
			while (true) { }
		}

		private static void Server_ClientDisconnected(object sender, TcpClient e)
		{
			
			Log.AddEvent(new Event("Client Disconnected", Event.Type.Informtion));
			Msg("Client Disconnected",ConsoleColor.Yellow);
		}

		private static void Server_ClientConnected(object sender, TcpClient e)
		{
			Log.AddEvent(new Event("Server Connected", Event.Type.Informtion));
			Msg("Client Connected", ConsoleColor.Green);
		}

		private static void Server_DataReceived(object sender, Message e)
		{
			Log.AddEvent(new Event("Server Data Ricived: \"" + e.MessageString + "\"", Event.Type.Informtion));
			Msg("Server Data Ricived:", ConsoleColor.Blue);
			Msg(e.MessageString, ConsoleColor.Blue);
			string replyline = "";
            string path = e.MessageString.Split(' ')[1];
            path = path.TrimStart('/');
            if (path == "")
            {
				path = "index.html";
            }
			foreach (string item in HttpReply.GenerateReply(path))
			{
                replyline += item + "\r\n";
                e.ReplyLine(item + "\r\n");
            }
			e.ReplyLine(replyline);
            Log.AddEvent(new Event("Server Data Replied: \"" + replyline + "\"", Event.Type.Informtion));
			Msg("Server Data Replied:", ConsoleColor.Blue);
			Msg(replyline, ConsoleColor.Blue);
		}
		public static void Msg(string text, ConsoleColor Color, ConsoleColor ColorAfter = ConsoleColor.White, ConsoleColor BGColor = ConsoleColor.Black, ConsoleColor BGColorAfter = ConsoleColor.Black)
		{
			Console.BackgroundColor = BGColor;
			Console.ForegroundColor = Color;
			Console.WriteLine(text);
			Console.ForegroundColor = ColorAfter;
			Console.BackgroundColor = BGColorAfter;
		}
	}
}
