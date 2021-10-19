using CommandLine;
using System;
using System.IO;

namespace Andtech.Jot
{

	[Verb("config", HelpText = "Configure Jot settings.")]
	public class ConfigOptions
	{
		[Option('g', "global", Required = false, HelpText = "Configure global settings.")]
		public bool Global { get; set; }
		[Value(0, Required = true, HelpText = "Name of the variable in the settings.")]
		public string Name { get; set; }
		[Value(1, Required = false, HelpText = "Value of the variable in the settings.")]
		public string Value { get; set; }
	}

	[Verb("init", HelpText = "Initialize the repository.")]
	public class InitOptions { }

	public class AdminOperations
	{

		public static void Initalize(InitOptions options)
		{
			if (Repository.Exists(Environment.CurrentDirectory))
			{
				throw new IOException("The directory is already a Jot repository");
			}

			var repository = Repository.Initialize(Environment.CurrentDirectory);

			Console.WriteLine($"Initialized empty Jot repository in {repository.JotDirectory}");
		}

		public static void Configure(ConfigOptions options)
		{
			var repository = Repository.Find(Environment.CurrentDirectory);
			var configurationHelper = ConfigurationHelper.New(repository.Root);

			var hasName = options.Name != null;
			var hasValue = options.Value != null;

			if (hasName && hasValue)
			{
				if (configurationHelper.TryGet(options.Global ? ConfigurationType.Global : ConfigurationType.Local, out var config))
				{
					config.editor = options.Value;
					config.Write();
				}
			}
			else if (hasName)
			{
				if (configurationHelper.TryGet(options.Global ? ConfigurationType.Global : ConfigurationType.Local, out var config))
				{
					if (options.Name == nameof(Configuration.editor))
					{
						Console.WriteLine(config.editor);
					}
				}
			}
		}
	}
}
