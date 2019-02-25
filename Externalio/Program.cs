using System;
using System.Numerics;
using Externalio.Features;
using Externalio.Managers;
using Externalio.Other;
using Math = Externalio.Other.Math;

namespace Externalio
{
	internal class Program
	{
		public static ESPOverlay ESP;

		[STAThread]
		private static void Main(string[] args)
		{
			Offsets.FetchOffsets();
			Offsets.UpdateOffsets();

			Console.Title = Misc.RandomString(64);
			Globals.Proc.Process = Extensions.Proc.WaitForProcess(Globals.Proc.Name);
			Extensions.Proc.WaitForModules(Globals.Proc.Modules, Globals.Proc.Name);

			MemoryManager.Initialize(Globals.Proc.Process.Id);
			Globals.Proc.UpdateResolution();

			Config.Load();

			//Settings.ESP.DebugStrings.Add("Debug String 1");
			//Settings.ESP.DebugStrings.Add("Debug String 1");
			//Settings.ESP.DebugStrings.Add("Debug String 1");
			//Settings.ESP.DebugStrings.Add("Debug String 1");

			Extensions.Information("-----------------------------------------------]", true);
			Extensions.Information("[TempMessage] Config Save:         F4", true);
			Extensions.Information("[TempMessage] Config Load:         F5", true);
			Extensions.Information("-----------------------------------------------]", true);
			Extensions.Information("[FEATURE] Toggle Bunnyhop:         F6", true);
			Extensions.Information("[FEATURE] Toggle Trigger:          F7", true);
			Extensions.Information("[FEATURE] Toggle Glow:             F8", true);
			Extensions.Information("[FEATURE] Toggle Radar:            F9", true);
			Extensions.Information("[FEATURE] Toggle Aimbot:           F10", true);
			Extensions.Information("[FEATURE] Toggle Chams:            F11", true);
			Extensions.Information("[FEATURE] Toggle ESP:              F3", true);
			Extensions.Information("[FEATURE] Toggle FOV Changer:      F2", true);
			Extensions.Information("[FEATURE] Toggle Standalone RCS:   F1", true);
			Extensions.Information("[FEATURE] Toggle AutoPistol:       <NO KEY YET>", true);
			Extensions.Information("[FEATURE] Toggle No_Flash:         <NO KEY YET>", true);
			Extensions.Information("-----------------------------------------------]", true);

			ThreadManager.Add("Watcher", Watcher.Run);
			ThreadManager.Add("Reader", Reader.Run);

			ThreadManager.Add("Bunnyhop", Bunnyhop.Run);
			ThreadManager.Add("Trigger", Trigger.Run);
			ThreadManager.Add("Glow", Glow.Run);
			ThreadManager.Add("Radar", Radar.Run);
			ThreadManager.Add("Aimbot", Aimbot.Run);
			ThreadManager.Add("FOVChanger", FOVChanger.Run);
			ThreadManager.Add("StandaloneRCS", StandaloneRCS.Run);
			ThreadManager.Add("AutoPistol", AutoPistol.Run);
			ThreadManager.Add("No_Flash", No_Flash.Run);
			ThreadManager.Add("SkinChanger", SkinChanger.Run);

			ThreadManager.ToggleThread("Watcher");
			ThreadManager.ToggleThread("Reader");

			if (Settings.Bunnyhop.Enabled) ThreadManager.ToggleThread("Bunnyhop");
			if (Settings.Trigger.Enabled) ThreadManager.ToggleThread("Trigger");
			if (Settings.Glow.Enabled) ThreadManager.ToggleThread("Glow");
			if (Settings.Radar.Enabled) ThreadManager.ToggleThread("Radar");
			if (Settings.Aimbot.Enabled) ThreadManager.ToggleThread("Aimbot");
			if (Settings.Chams.Enabled) ThreadManager.ToggleThread("Chams");
			if (Settings.FOVChanger.Enabled || Settings.FOVChanger.ViewModelFov.Enabled) ThreadManager.ToggleThread("FOVChanger");
			if (Settings.No_Flash.Enabled) ThreadManager.ToggleThread("No_Flash");
			if (Settings.StandaloneRCS.Enabled) ThreadManager.ToggleThread("StandaloneRCS");
			if (Settings.AutoPistol.Enabled) ThreadManager.ToggleThread("AutoPistol");
			if (Settings.SkinChanger.Enabled) ThreadManager.ToggleThread("SkinChanger");

			if (ESP == null && Settings.ESP.Enabled)
			{
				Extensions.Information("[ThreadManager][Started] ESP", true);
				ESP = new ESPOverlay(Globals.Proc.Process.MainWindowHandle); // Attach it to CSGO main window handle
				ESP.Initialize();
				ESP.Run();
			}
		}
	}
}