using CliWrap;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Andtech.Jot
{

	[Verb("open", isDefault: true, HelpText = "Open a journal page.")]
	public class OpenPageOptions
	{
		[Value(0, HelpText = "The date of the page", Default = "today", Required = false)]
		public string Date { get; set; }
		[Option(HelpText = "Display extra information.", Required = false)]
		public bool Verbose { get; set; }
		[Option('c', "create", HelpText = "Create the page if one doesn't already exist.", Required = false)]
		public bool CreateIfNoExists { get; set; }
	}

	[Verb("new", HelpText = "Create a new journal page.")]
	public class NewPageOptions
	{
		[Value(0, HelpText = "The date of the journal page", Default = "today", Required = false)]
		public string Date { get; set; }
	}

	public class PageOperations
	{

		public static void NewPage(NewPageOptions options)
		{
			var repository = Repository.Find(Environment.CurrentDirectory);
			var date = DayUtility.ParseDate(options.Date);
			var path = repository.ToPath(date);

			if (File.Exists(path))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"A journal page file already exists for {ToDisplayString(date)}");
				Console.ResetColor();
				return;
			}

			repository.CreatePage(date);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Created journal page for {ToDisplayString(date)}");
			Console.ResetColor();
		}

		public static async Task OpenPage(OpenPageOptions options)
		{
			var repository = Repository.Find(Environment.CurrentDirectory);
			var date = DayUtility.ParseDate(options.Date);
			var path = repository.ToPath(date);

			if (!File.Exists(path))
			{
				if (options.CreateIfNoExists)
				{
					repository.CreatePage(date);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"Created journal page for {ToDisplayString(date)}");
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Journal page not found for {ToDisplayString(date)}");
					Console.ResetColor();
					return;
				}
			}

			var relativePath = Path.GetRelativePath(Environment.CurrentDirectory, path);

			var configuration = repository.GetConfiguration();

			var args = new List<string>();
			string filename;
			if (string.IsNullOrEmpty(configuration.editor))
			{
				filename = relativePath;
			}
			else
			{
				var tokens = configuration.editor.Split(" ");
				filename = tokens[0];

				if (tokens.Length > 1)
				{
					args.AddRange(tokens.Skip(1));
				}

				args.Add(relativePath);
			}

			var argString = string.Join(" ", args.Select(x => $"\"{x}\""));
			if (options.Verbose)
			{
				Console.WriteLine("{0} {1}", filename, argString);
			}

			await Cli.Wrap(filename)
				.WithArguments(argString)
				.WithWorkingDirectory(Environment.CurrentDirectory)
				.ExecuteAsync();
		}

		private static string ToDisplayString(DateTime dateTime)
		{
			return dateTime.ToString("D");
		}
	}
}
