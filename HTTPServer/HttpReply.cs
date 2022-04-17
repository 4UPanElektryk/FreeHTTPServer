using System;

namespace HTTPServer
{
	public class HttpReply
	{
		public static string[] GenerateReply(string path)
		{
			string[] html = HTMLReadFile.GetHTMLCode(path);
			string[] reply;
			int l = 0;
			string responseln = "HTTP/1.1 200 OK";
			string cotenttype = "Content-Type: text/html";
			if (html == null)
			{
				responseln = "HTTP/1.1 404";
			}
			string filetype = HTMLReadFile.GetHTMLExtention(path);
			Console.WriteLine(path);
			switch (filetype)
			{
				case "htm":
				case "html":
					cotenttype = "Content-Type: text/html";
					break;
				case "css":
					cotenttype = "Content-Type: text/css";
					break;
				case "js":
					cotenttype = "Content-Type: text/javascript";
					break;
				case "json":
					cotenttype = "Content-Type: application/json";
					break;
				case "pdf":
					cotenttype = "Content-Type: application/pdf";
					break;
				case "zip":
					cotenttype = "Content-Type: application/zip";
					break;
				case "rar":
					cotenttype = "Content-Type: application/rar";
					break;
				case "7z":
					cotenttype = "Content-Type: application/7z";
					break;
				case "txt":
					cotenttype = "Content-Type: text/plain";
					break;

				default:
					break;
			}
			if (html == null)
			{
				html = HTMLReadFile.Get404File(path);
			}
			foreach (string item in html)
			{
				l += item.Length;
			}
			string[] header = {
				responseln,
				cotenttype + "; charset=utf-8",
				"Content-Language: en-US",
				"Server: Maciek_OS_Beta_HTML_Server/0.6.1",
				"Accept-Ranges: bytes",
				"Vary: Accept-Encoding",
				"Content-Length: " + l.ToString(),
				"Connection: Keep-Alive",
				""
			};
			reply = new string[header.Length + html.Length];
			int i = 0;
			foreach (string item in header)
			{
				reply[i] = item;
				i++;
			}
			foreach (string item in html)
			{
				reply[i] = item;
				i++;
			}
			return reply;
		}
	}
}
