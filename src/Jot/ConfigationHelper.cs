using Newtonsoft.Json;
using System;
using System.IO;

namespace Andtech.Jot
{

	public enum ConfigurationType
	{
		Local,
		Global,
		System
	}

	public class ConfigurationHelper
	{
		private string globalConfigurationPath;
		private string localConfigurationPath;

		public static ConfigurationHelper New(string repositoryRoot)
		{
			var local = Path.Combine(repositoryRoot, ".jot/settings.json");
			var global = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/jot/settings.json");

			return new ConfigurationHelper
			{
				localConfigurationPath = local,
				globalConfigurationPath = global
			};
		}

		public bool TryGet(ConfigurationType configurationType, out Configuration configuration)
		{
			switch (configurationType)
			{
				case ConfigurationType.Local:
					return TryGet(out configuration, false);
				case ConfigurationType.Global:
					if (!File.Exists(globalConfigurationPath))
					{
						configuration = Configuration.GetDefault(globalConfigurationPath);
						return true;
					}

					return TryGet(out configuration, true);
			};

			configuration = default;
			return false;
		}

		/// <summary>
		/// Merges local and global configurations.
		/// </summary>
		/// <param name="configuration">A configuration object.</param>
		/// <returns>The merged configuration info.</returns>
		public bool TryGetConsolidated(out Configuration configuration)
		{
			var hasGlobal = TryGet(out var global, true);
			var hasLocal = TryGet(out var local, false);

			configuration = global.Consolidate(local);

			return hasLocal || hasGlobal;
		}

		private bool TryGet(out Configuration configuration, bool global = false)
		{
			var path = global ? globalConfigurationPath : localConfigurationPath;
			if (!File.Exists(path))
			{
				configuration = default;
				return false;
			}

			configuration = Configuration.Read(path);
			return true;
		}

		public string GetFullPath(ConfigurationType type) => Path.GetFullPath(type == ConfigurationType.Global ? globalConfigurationPath : localConfigurationPath);

		public void Write(Configuration configuration, bool global = false)
		{
			var path = global ? globalConfigurationPath : localConfigurationPath;

			Directory.CreateDirectory(Path.GetDirectoryName(path));
			var settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Formatting = Formatting.Indented
			};
			var json = JsonConvert.SerializeObject(configuration, settings);
			File.WriteAllText(path, json);
		}
	}
}
