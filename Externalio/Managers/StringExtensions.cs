using System.Diagnostics;
using Externalio.Other;

namespace Externalio.Managers
{
	public static class StringExtensions
	{
		public static (int RegionStart, int RegionEnd) ModInfo(this string moduleName)
		{
			foreach (ProcessModule pm in Globals.Proc.Process.Modules)
				if (pm.ModuleName.Contains(moduleName.ToLower()) || pm.ModuleName == moduleName)
					return ((int) pm.BaseAddress, (int) pm.BaseAddress + pm.ModuleMemorySize);
			return (-1, -1);
		}
	}
}