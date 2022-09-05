﻿using AssetRipper.Core.IO;
using System.IO;
using System.Text;

namespace AssetRipper.Library.Exporters
{
	public sealed class ProjectVersionPostExporter : IPostExporter
	{
		public void DoPostExport(Ripper ripper)
		{
			// Although Unity 4 and lower don't have this file, we leave it in anyway for user readibility.
			SaveProjectVersion(ripper.Settings.ProjectSettingsPath, ripper.Settings.Version);
		}

		private static void SaveProjectVersion(string projectSettingsDirectory, UnityVersion version)
		{
			Directory.CreateDirectory(projectSettingsDirectory);
			using Stream fileStream = File.Create(Path.Combine(projectSettingsDirectory, "ProjectVersion.txt"));
			using StreamWriter writer = new InvariantStreamWriter(fileStream, new UTF8Encoding(false));
			writer.Write($"m_EditorVersion: {version}\n");
			if (version.IsEqual(5))
			{
				//Unity 5 has an extra line
				//Even on beta versions, this always seems to be zero.
				writer.Write("m_StandardAssetsVersion: 0");
			}

			//Beginning with 2019.1.0a10, ProjectVersion.txt files have an additional line.
			//m_EditorVersionWithRevision: 2019.4.3f1 (f880dceab6fe)
			//The revision is always 6 bytes.
			//It can be acquired with the FileVersionInfo class in the System.Diagnostics namespace.
			//FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo("path/to/Unity.exe");
			//string productVersion = versionInfo.ProductVersion; //For example: 2019.4.3f1_f880dceab6fe
			//string revision = productVersion.Substring(productVersion.IndexOf('_') + 1);
		}
	}
}
