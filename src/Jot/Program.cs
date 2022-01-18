using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Jot
{

	[Verb("repl", HelpText = "REPL interpreter", Hidden = true)]
	public class ReplOptions
	{
	}

	class Program
	{

		static async Task Main(string[] args)
		{
			var result = Parser.Default.ParseArguments<ReplOptions, InitOptions, ConfigOptions, NewPageOptions, OpenPageOptions>(args);
			result
				.WithParsed<ReplOptions>(Repl)
				.WithParsed<InitOptions>(AdminOperations.Initalize)
				.WithParsed<ConfigOptions>(AdminOperations.Configure)
				.WithParsed<NewPageOptions>(PageOperations.NewPage)
			;
			await result
				.WithParsedAsync<OpenPageOptions>(OpenPageWithExceptionLogging);
		}

		static async Task OpenPageWithExceptionLogging(OpenPageOptions options)
		{
			try
			{
				await PageOperations.OpenPage(options);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex);
			}
		}

		static void Repl(ReplOptions options)
		{
			var commas = new char[] { ',' };
			var spaces = new char[] { ' ' };

			while (true)
			{
				try
				{
					Console.Write("$ ");
					var line = Console.ReadLine();

					var delimiters = line.Contains(',') ? commas : spaces;
					var args = line.Split(delimiters);

					Main(args);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(ex.Message);
				}
			}
		}
	}
}
