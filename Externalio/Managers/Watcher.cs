using System;
using System.Threading;
using Externalio.Other;

namespace Externalio.Managers
{
	internal class Watcher
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(75);

				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.LoadConfig) & 0x8000)) Config.Load();
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.SaveConfig) & 0x8000)) Config.Save();

				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleBunnyhop) & 0x8000)) ThreadManager.ToggleThread("Bunnyhop");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleTrigger) & 0x8000)) ThreadManager.ToggleThread("Trigger");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleGlow) & 0x8000)) ThreadManager.ToggleThread("Glow");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleRadar) & 0x8000)) ThreadManager.ToggleThread("Radar");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleAimbot) & 0x8000)) ThreadManager.ToggleThread("Aimbot");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleChams) & 0x8000)) ThreadManager.ToggleThread("Chams");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleEsp) & 0x8000)) ThreadManager.ToggleThread("ESP");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleFov) & 0x8000)) ThreadManager.ToggleThread("FOVChanger");
				if (Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.OtherControls.ToggleStandaloneRCS) & 0x8000)) ThreadManager.ToggleThread("StandaloneRCS");
			}
		}
	}
}