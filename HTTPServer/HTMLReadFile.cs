using System.IO;
using System.Text;
namespace HTTPServer
{
    public class HTMLReadFile
    {
        public static string[] GetHTMLCode(string path)
        {
            path = "Docs\\" + path;
            if (!File.Exists(path))
            {
                return null;
            }
            //check if file is an image
            if (path.Contains(".png") || path.Contains(".jpg") || path.Contains(".jpeg") || path.Contains(".gif"))
            {
                return null;
            }
            else
            {
                string[] file = File.ReadAllLines(path, Encoding.UTF8);
                return file;
            }
        }
        public static string GetHTMLExtention(string path)
        {
            path = "Docs\\" + path;
            if (!File.Exists(path))
            {
                return null;
            }
            string extention = Path.GetExtension(path);
            if (extention != null)
            {
                extention = extention.TrimStart('.');
            }
            return extention;
        }
        public static string[] Get404File(string requestedpath)
        {
            if (File.Exists("Docs\\404.html"))
            {
                string[] file = File.ReadAllLines("Docs\\404.html", Encoding.UTF8);
                string[] repl = new string[file.Length];
                int i = 0;
                foreach (string item in file)
                {
                    repl[i] = item.Replace("{path}", requestedpath);
                    i++;
                }
                return repl;
            }
            else
            {
                string[] file =
                {
                    "<!DOCTYPE html>",
                    "<html>",
                    "<head>",
                    "<title>404 Not Found</title>",
                    "</head>",
                    "<body>",
                    "<h1>404 Not Found</h1>",
                    "<p>The requested URL \"" + requestedpath + "\" was not found on this server.</p>",
                    "<a href=\"index.html\">Home a>",
                    "",
                    "</body>",
                    "</html>"
                };
                return file;
            }
        }
    }
}
