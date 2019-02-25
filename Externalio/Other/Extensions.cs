using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading;
using Externalio.Managers;

namespace Externalio.Other
{
	internal class Extensions
	{
		public static DateTime AssemblyCreationDate()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;
			return new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.MinorRevision * 2);
		}

		public static void Information(string text, bool newLine)
		{
			Console.ForegroundColor = ConsoleColor.Green;

			if (newLine)
				Console.WriteLine(text);
			else
				Console.Write(text);

			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void Error(string text, int sleep, bool closeProc)
		{
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine(text);

			Thread.Sleep(sleep);

			if (closeProc) Environment.Exit(0);
		}

		internal class Other
		{
			public static Vector3 GetBonePos(int entity, int targetBone)
			{
				var boneMatrix = MemoryManager.ReadMemory<int>(entity + Offsets.m_dwBoneMatrix);

				if (boneMatrix == 0) return Vector3.Zero;

				var position = MemoryManager.ReadMatrix<float>(boneMatrix + 0x30 * targetBone + 0x0C, 9);

				if (!position.Any()) return Vector3.Zero;

				return new Vector3(position[0], position[4], position[8]);
			}

			public static int GetClassID(int id)
			{
				var classID = MemoryManager.ReadMemory<int>(id + 0x8);
				var classID2 = MemoryManager.ReadMemory<int>(classID + 2 * 0x4);
				var classID3 = MemoryManager.ReadMemory<int>(classID2 + 1);
				return MemoryManager.ReadMemory<int>(classID3 + 20);
			}
		}

		internal class Colors
		{
			public static Color FromHealth(float percent)
			{
				if (percent < 0 || percent > 1) return Color.Black;

				int red, green;

				red = percent < 0.5 ? 255 : 255 - (int) (255 * (percent - 0.5) / 0.5);
				green = percent < 0.5 ? (int) (255 * percent) : 255;

				return Color.FromArgb(red, green, 0);
			}
		}

		internal class Proc
		{
			public static bool IsWindowFocused(Process procName)
			{
				var activatedHandle = Globals.Imports.GetForegroundWindow();

				if (activatedHandle == IntPtr.Zero) return false;

				Globals.Imports.GetWindowThreadProcessId(activatedHandle, out var activeProcId);

				return activeProcId == procName.Id;
			}

			public static Process WaitForProcess(string procName)
			{
				var process = Process.GetProcessesByName(procName);

				Information($"> waiting for {procName} to show up", false);

				while (process.Length < 1)
				{
					Information(".", false);

					process = Process.GetProcessesByName(procName);

					Thread.Sleep(250);
				}

				Console.Clear();

				return process[0];
			}

			public static void WaitForModules(string[] modules, string procName)
			{
				var loadedModules = new List<string>(modules.Length);

				Information($"> waiting for {modules.Count()} module(s) to load", false);

				Process[] process;

				while (loadedModules.Count < modules.Length)
				{
					process = Process.GetProcessesByName(procName);

					if (process.Length < 1) continue;

					foreach (ProcessModule module in process[0].Modules)
						if (modules.Contains(module.ModuleName) && !loadedModules.Contains(module.ModuleName))
						{
							loadedModules.Add(module.ModuleName);

							switch (module.ModuleName)
							{
								case "client_panorama.dll":
									Structs.Base.Client = module.BaseAddress;
									//Information("\nFound client_panorama.dll: 0x" + module.BaseAddress.ToString("x"), true);
									break;
								case "engine.dll":
									Structs.Base.Engine = module.BaseAddress;
									//Information("\nFound engine.dll: 0x" + module.BaseAddress.ToString("x"), true);
									break;
								case "vstdlib.dll":
									Structs.Base.Vstdlib = module.BaseAddress;
									break;
								default:
									Console.WriteLine("\nOther Module '{0}' : 0x" + module.BaseAddress.ToString("X"));
									break;
							}
						}

					Thread.Sleep(250);
				}

				Structs.GlobalVars.Base = (int) Structs.Base.Engine + Offsets.dwGlobalVars;
				Console.Clear();
			}
		}
	}
}