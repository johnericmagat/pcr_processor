using System.Diagnostics;
using System.Reflection;

namespace pcr_processor.Helper
{
	public static class GetVersionHelper
	{
		public static string GetVersion()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

			string versionNumber = fileVersionInfo.FileVersion.ToString();
			return versionNumber;
		}
	}
}
