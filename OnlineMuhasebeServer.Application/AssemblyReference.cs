using System.Reflection;

namespace OnlineMuhasebeServer.Application
{
	public static class AssemblyReference
	{

		//public static readonly Assembly Assembly=typeof(Assembly).Assembly;

		// DİKKAT: typeof(Assembly) yerine kendi sınıfınızın tipini (AssemblyReference) veriyoruz!
		public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
	}
}
