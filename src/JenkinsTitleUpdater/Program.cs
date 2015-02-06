using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace JenkinsTitleUpdater
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length == 0)
			{
				Usage();
				return -1;
			}

			HtmlDocument doc = new HtmlDocument();
			if (!File.Exists(args[0])) throw new ArgumentException("File does not exist " + args[0] + "\r\nWe are at " + Environment.CurrentDirectory);
			doc.Load(args[0]);
			HtmlNode root = doc.DocumentNode;
			HtmlNode titleNode = root.ChildNodes["html"].ChildNodes["head"].ChildNodes["title"];
			string buildname = " -- ";
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_DISPLAY_NAME")))
			{
				return -1;
			}
			buildname += Environment.GetEnvironmentVariable("BUILD_DISPLAY_NAME");
			titleNode.InnerHtml += buildname;
			doc.Save(args[0]);
			return 0;
		}

		private static void Usage()
		{
			Console.WriteLine("Usage");
			Console.WriteLine("Usage   : CTTitleUpdater.exe full-path-to-_Layout.cshtml");
			Console.WriteLine(@"Example :  CTTitleUpdater.exe \..\..\src\Web.CTAP.Web\View\Shared\_Layout.cshtml");
		}
	}
}

