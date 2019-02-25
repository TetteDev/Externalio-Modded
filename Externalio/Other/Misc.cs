using System;
using System.Linq;
using Externalio.Windoof;

namespace Externalio.Other
{
	public class Misc
	{
		private static readonly Random RNG = new Random();

		public static string RandomString(int length = 128)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[RNG.Next(s.Length)]).ToArray());
		}

		public static (int Width, int Height) GetResolution()
		{
			try
			{
				WinAPI.GetClientRect(Globals.Proc.Process.MainWindowHandle, out var result);

				return (result.Width, result.Height);
			}
			catch
			{
				return (-1, -1);
			}
		}
	}
}