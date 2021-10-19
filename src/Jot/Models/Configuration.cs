#nullable enable
using Newtonsoft.Json;
using System.IO;

namespace Andtech.Jot
{

	public interface IConsolidatable<T>
	{

		T Consolidate(T overrideInstance);
	}

	public struct Configuration : IConsolidatable<Configuration>
	{
		public string? editor { get; set; }

		[JsonIgnore]
		public string Path { get; set; }

		public static Configuration GetDefault(string path)
		{
			var configuration = new Configuration
			{
				Path = path
			};

			return configuration;
		}

		public Configuration Consolidate(Configuration overrides)
		{
			var merged = this;
			merged.editor = overrides.editor ?? merged.editor;

			return merged;
		}

		public static Configuration Read(string path)
		{
			var json = File.ReadAllText(path);
			var configuration = JsonConvert.DeserializeObject<Configuration>(json);
			configuration.Path = path;

			return configuration;
		}

		public void Write()
		{
			var directory = System.IO.Path.GetDirectoryName(Path);
			Directory.CreateDirectory(directory);

			var settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			var json = JsonConvert.SerializeObject(this, settings);
			File.WriteAllText(Path, json);
		}
	}
}
