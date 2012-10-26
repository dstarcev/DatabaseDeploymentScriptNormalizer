using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseDeploymentScriptNormalizer {
	public class Program {
		private static void Main(string[] args) {
			var inputPath = args[0];
			var outputPath = args.Skip(1).FirstOrDefault() ?? inputPath;
			var script = LoadScript(inputPath);
			var normalizedScript = NormalizeScript(script);
			SaveScript(outputPath, normalizedScript);
		}

		private static string NormalizeScript(string script) {
			return Regex.Replace(script, @"N'dbo\.(.*?)'", "N'[dbo].[$1]'");
		}

		private static string LoadScript(string scriptPath) {
			using (var file = new StreamReader(scriptPath, Encoding.UTF8)) {
				return file.ReadToEnd();
			}
		}

		private static void SaveScript(string scriptPath, string normalizedScript) {
			using(var file = new StreamWriter(scriptPath, false, Encoding.UTF8)) {
				file.Write(normalizedScript);
			}
		}
	}
}