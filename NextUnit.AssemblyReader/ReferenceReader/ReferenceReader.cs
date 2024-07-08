using System.Xml;

namespace NextUnit.AssemblyReader.ReferenceReader
{
	public class ReferenceReader
	{
		public static Dictionary<string, Dictionary<string, List<string>>> ReferencesByVersion = new Dictionary<string, Dictionary<string, List<string>>>();

		public static void ProcessReferenceNodes(XmlNodeList nodes, string projectName, Dictionary<string, Dictionary<string, List<string>>> referencesByVersion, bool isPackageReference = false)
		{
			foreach (XmlNode node in nodes)
			{
				string name = isPackageReference ? node.Attributes["Include"]?.Value : node.Attributes["Include"]?.Value.Split(',')[0].Trim();
				string version = isPackageReference ? node.Attributes["Version"]?.Value : ExtractVersionFromReference(node.Attributes["Include"]?.Value);

				if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(version))
				{
					if (!referencesByVersion.ContainsKey(name))
					{
						referencesByVersion[name] = new Dictionary<string, List<string>>();
					}

					if (!referencesByVersion[name].ContainsKey(version))
					{
						referencesByVersion[name][version] = new List<string>();
					}

					if (!referencesByVersion[name][version].Contains(projectName))
					{
						referencesByVersion[name][version].Add(projectName);
					}
				}
			}
		}

		private static string ExtractVersionFromReference(string includeAttribute)
		{
			if (string.IsNullOrEmpty(includeAttribute))
			{
				return string.Empty;
			}

			string version = "";
			foreach (var part in includeAttribute.Split(','))
			{
				if (part.TrimStart().StartsWith("Version=", StringComparison.OrdinalIgnoreCase))
				{
					version = part.Split('=')[1].Trim();
					break;
				}
			}

			return version;
		}


	}
}
