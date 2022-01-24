using System;
using System.IO;

namespace Andtech.Jot
{

	/// <summary>
	/// A Jot repository.
	/// </summary>
	public class Repository
	{
		/// <summary>
		/// The root directory.
		/// </summary>
		public string Root { get; set; }
		public string JotDirectory => Path.Combine(Root, ".jot");

		public static bool Exists(string path)
		{
			var jotDirectory = Path.Combine(path, ".jot");

			return Directory.Exists(jotDirectory);
		}

		public Configuration GetConfiguration()
		{
			var configurationHelper = ConfigurationHelper.New(Root);

			configurationHelper.TryGetConsolidated(out var configuration);

			return configuration;
		}

		public static Repository Initialize(string path)
		{
			var jotDirectory = Path.Combine(path, ".jot", "settings.json");

			var config = Configuration.GetDefault(jotDirectory);
			config.Write();

			return new Repository { Root = path };
		}

		public void CreatePage(DateTime date)
		{
			var path = ToPath(date);

			var directory = Path.GetDirectoryName(path);
			Directory.CreateDirectory(directory);

			File.Create(path).Close();
		}

		/// <summary>
		/// Returns the path to the journal page file.
		/// </summary>
		/// <param name="date">The date of the journal page.</param>
		/// <returns>The absolute path to the file.</returns>
		public string ToPath(DateTime date) => Path.Combine(Root, ToRelativePath(date));

		/// <summary>
		/// Returns the path to the journal page file.
		/// </summary>
		/// <param name="date">The date of the journal page.</param>
		/// <returns>The relative path to the file.</returns>
		public static string ToRelativePath(DateTime date)
		{
			var month = Constants.GetMonth(date.Month - 1);

			var path = Path.Combine(
				date.Year.ToString(),
				string.Format("{0:00}-{1}", date.Month, Constants.GetDisplayMonthAbbreviated(month).ToLower()),
				string.Format("{0:00}-{1}.md", date.Day, Constants.GetDisplayDayOfWeekAbbreviated(date.DayOfWeek).ToLower())
			);

			return path;
		}

		public static Repository Find(string searchDirectory)
		{
			var directoryInfo = new DirectoryInfo(searchDirectory);
			while (directoryInfo != null)
			{
				var path = directoryInfo.FullName;

				var expectedPath = Path.Combine(path, ".jot");
				if (Directory.Exists(expectedPath))
				{
					break;
				}

				var parent = Directory.GetParent(path);
				if (parent is null)
				{
					throw new Exception($"No Jot repository. (or any parent up to mount point {directoryInfo.FullName})");
				}

				directoryInfo = parent;
			}

			return new Repository { Root = directoryInfo.FullName };
		}
	}
}
