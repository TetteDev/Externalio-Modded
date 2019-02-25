using System;
using System.IO;
using System.Linq;
using Externalio.Model;
using Externalio.Other;

namespace Externalio.Managers
{
	internal class Config
	{
		public static void Load()
		{
			var configs = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ini").Where(x => IsFileUsable(x)).ToList();

			if (!configs.Any())
			{
				Extensions.Error("[Config][Load] No Config found", 0, false);
			}
			else
			{
				if (configs.Count > 1)
				{
					Console.Clear();

					Extensions.Information("> what config do u want to load?", true);

					configs.ForEach(x => Extensions.Information($"[{configs.FindIndex(y => y.Contains(x))}] {Path.GetFileName(x)}", true));

					var input = Console.ReadLine();

					Console.Clear();

					if (!input.All(char.IsDigit))
					{
						Extensions.Error("> thats not a number", 0, false);
						return;
					}

					var index = Convert.ToInt32(input);

					if (configs.ElementAtOrDefault(index) == null)
					{
						Extensions.Error("> index is out of range", 0, false);
						return;
					}

					Load(configs[index]);
				}
				else
				{
					Load(configs.FirstOrDefault());
				}
			}
		}

		public static void Save()
		{
			var configs = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.ini").Where(x => IsFileUsable(x)).ToList();

			Console.Clear();

			if (configs.Any())
			{
				Extensions.Information("> do u want to override a config? type yes or no", true);

				var choice = Console.ReadLine();

				Console.Clear();

				if (string.Equals(choice, "no", StringComparison.CurrentCultureIgnoreCase))
				{
					Extensions.Information("> what should ur config be called?", true);

					var name = Console.ReadLine();

					Console.Clear();

					Save($"{name}.ini");
				}
				else
				{
					Extensions.Information("> which config do you want to override?", true);

					configs.ForEach(x => Extensions.Information($"[{configs.FindIndex(y => y.Contains(x))}] {Path.GetFileName(x)}", true));

					var input = Console.ReadLine();

					Console.Clear();

					if (!input.All(char.IsDigit))
					{
						Extensions.Error("> thats not a number", 0, false);
						return;
					}

					var index = Convert.ToInt32(input);

					if (configs.ElementAtOrDefault(index) == null)
					{
						Extensions.Error("> index is out of range", 0, false);
						return;
					}

					Save(configs[index]);
				}
			}
			else
			{
				Extensions.Information("> what should ur config be called?", true);

				var name = Console.ReadLine();

				Console.Clear();

				Save($"{name}.ini");
			}
		}

		public static bool IsFileUsable(string path)
		{
			using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
			{
				return fileStream.CanWrite;
			}
		}

		public static void Load(string path)
		{
			if (!IsFileUsable(path))
			{
				Extensions.Error("[Config][Load][Error] Config File is not usable", 0, false);
				return;
			}

			var configParser = new FileIniDataParser();
			var configData = configParser.ReadFile(path);

			try
			{
				Settings.Radar.Enabled = bool.Parse(configData["Radar"]["Enabled"]);

				Settings.Bunnyhop.Enabled = bool.Parse(configData["Bunnyhop"]["Enabled"]);
				Settings.Bunnyhop.Key = int.Parse(configData["Bunnyhop"]["Key"]);

				Settings.Trigger.Enabled = bool.Parse(configData["Trigger"]["Enabled"]);
				Settings.Trigger.Key = int.Parse(configData["Trigger"]["Key"]);
				Settings.Trigger.Delay = int.Parse(configData["Trigger"]["Delay"]);

				Settings.Chams.Enabled = bool.Parse(configData["Chams"]["Enabled"]);

				Settings.Chams.Allies = bool.Parse(configData["Glow"]["Allies"]);
				Settings.Chams.Allies_Color_R = float.Parse(configData["Chams"]["Allies_Color_R"]);
				Settings.Chams.Allies_Color_G = float.Parse(configData["Chams"]["Allies_Color_G"]);
				Settings.Chams.Allies_Color_B = float.Parse(configData["Chams"]["Allies_Color_B"]);
				Settings.Chams.Allies_Color_A = float.Parse(configData["Chams"]["Allies_Color_A"]);

				Settings.Chams.Enemies = bool.Parse(configData["Glow"]["Enemies"]);
				Settings.Chams.Enemies_Color_R = float.Parse(configData["Chams"]["Enemies_Color_R"]);
				Settings.Chams.Enemies_Color_G = float.Parse(configData["Chams"]["Enemies_Color_G"]);
				Settings.Chams.Enemies_Color_B = float.Parse(configData["Chams"]["Enemies_Color_B"]);
				Settings.Chams.Enemies_Color_A = float.Parse(configData["Chams"]["Enemies_Color_A"]);

				Settings.Glow.Enabled = bool.Parse(configData["Glow"]["Enabled"]);
				Settings.Glow.PlayerColorMode = int.Parse(configData["Glow"]["PlayerColorMode"]);
				Settings.Glow.FullBloom = bool.Parse(configData["Glow"]["FullBloom"]);

				Settings.Glow.Allies = bool.Parse(configData["Glow"]["Allies"]);
				Settings.Glow.Allies_Color_R = float.Parse(configData["Glow"]["Allies_Color_R"]);
				Settings.Glow.Allies_Color_G = float.Parse(configData["Glow"]["Allies_Color_G"]);
				Settings.Glow.Allies_Color_B = float.Parse(configData["Glow"]["Allies_Color_B"]);
				Settings.Glow.Allies_Color_A = float.Parse(configData["Glow"]["Allies_Color_A"]);

				Settings.Glow.Enemies = bool.Parse(configData["Glow"]["Enemies"]);
				Settings.Glow.Enemies_Color_R = float.Parse(configData["Glow"]["Enemies_Color_R"]);
				Settings.Glow.Enemies_Color_G = float.Parse(configData["Glow"]["Enemies_Color_G"]);
				Settings.Glow.Enemies_Color_B = float.Parse(configData["Glow"]["Enemies_Color_B"]);
				Settings.Glow.Enemies_Color_A = float.Parse(configData["Glow"]["Enemies_Color_A"]);

				Settings.Glow.Snipers = bool.Parse(configData["Glow"]["Snipers"]);
				Settings.Glow.Snipers_Color_R = float.Parse(configData["Glow"]["Snipers_Color_R"]);
				Settings.Glow.Snipers_Color_G = float.Parse(configData["Glow"]["Snipers_Color_G"]);
				Settings.Glow.Snipers_Color_B = float.Parse(configData["Glow"]["Snipers_Color_B"]);
				Settings.Glow.Snipers_Color_A = float.Parse(configData["Glow"]["Snipers_Color_A"]);

				Settings.Glow.Rifles = bool.Parse(configData["Glow"]["Rifles"]);
				Settings.Glow.Rifles_Color_R = float.Parse(configData["Glow"]["Rifles_Color_R"]);
				Settings.Glow.Rifles_Color_G = float.Parse(configData["Glow"]["Rifles_Color_G"]);
				Settings.Glow.Rifles_Color_B = float.Parse(configData["Glow"]["Rifles_Color_B"]);
				Settings.Glow.Rifles_Color_A = float.Parse(configData["Glow"]["Rifles_Color_A"]);

				Settings.Glow.MachineGuns = bool.Parse(configData["Glow"]["MachineGuns"]);
				Settings.Glow.MachineGuns_Color_R = float.Parse(configData["Glow"]["MachineGuns_Color_R"]);
				Settings.Glow.MachineGuns_Color_G = float.Parse(configData["Glow"]["MachineGuns_Color_G"]);
				Settings.Glow.MachineGuns_Color_B = float.Parse(configData["Glow"]["MachineGuns_Color_B"]);
				Settings.Glow.MachineGuns_Color_A = float.Parse(configData["Glow"]["MachineGuns_Color_A"]);

				Settings.Glow.MPs = bool.Parse(configData["Glow"]["MPs"]);
				Settings.Glow.MPs_Color_R = float.Parse(configData["Glow"]["MPs_Color_R"]);
				Settings.Glow.MPs_Color_G = float.Parse(configData["Glow"]["MPs_Color_G"]);
				Settings.Glow.MPs_Color_B = float.Parse(configData["Glow"]["MPs_Color_B"]);
				Settings.Glow.MPs_Color_A = float.Parse(configData["Glow"]["MPs_Color_A"]);

				Settings.Glow.Pistols = bool.Parse(configData["Glow"]["Pistols"]);
				Settings.Glow.Pistols_Color_R = float.Parse(configData["Glow"]["Pistols_Color_R"]);
				Settings.Glow.Pistols_Color_G = float.Parse(configData["Glow"]["Pistols_Color_G"]);
				Settings.Glow.Pistols_Color_B = float.Parse(configData["Glow"]["Pistols_Color_B"]);
				Settings.Glow.Pistols_Color_A = float.Parse(configData["Glow"]["Pistols_Color_A"]);

				Settings.Glow.Shotguns = bool.Parse(configData["Glow"]["Shotguns"]);
				Settings.Glow.Shotguns_Color_R = float.Parse(configData["Glow"]["Shotguns_Color_R"]);
				Settings.Glow.Shotguns_Color_G = float.Parse(configData["Glow"]["Shotguns_Color_G"]);
				Settings.Glow.Shotguns_Color_B = float.Parse(configData["Glow"]["Shotguns_Color_B"]);
				Settings.Glow.Shotguns_Color_A = float.Parse(configData["Glow"]["Shotguns_Color_A"]);

				Settings.Glow.C4 = bool.Parse(configData["Glow"]["C4"]);
				Settings.Glow.C4_Color_R = float.Parse(configData["Glow"]["C4_Color_R"]);
				Settings.Glow.C4_Color_G = float.Parse(configData["Glow"]["C4_Color_G"]);
				Settings.Glow.C4_Color_B = float.Parse(configData["Glow"]["C4_Color_B"]);
				Settings.Glow.C4_Color_A = float.Parse(configData["Glow"]["C4_Color_A"]);

				Settings.Glow.Grenades = bool.Parse(configData["Glow"]["Grenades"]);
				Settings.Glow.Grenades_Color_R = float.Parse(configData["Glow"]["Grenades_Color_R"]);
				Settings.Glow.Grenades_Color_G = float.Parse(configData["Glow"]["Grenades_Color_G"]);
				Settings.Glow.Grenades_Color_B = float.Parse(configData["Glow"]["Grenades_Color_B"]);
				Settings.Glow.Grenades_Color_A = float.Parse(configData["Glow"]["Grenades_Color_A"]);

				Settings.Aimbot.Enabled = bool.Parse(configData["Aimbot"]["Enabled"]);
				Settings.Aimbot.Fov = float.Parse(configData["Aimbot"]["Fov"]);
				Settings.Aimbot.Bone = int.Parse(configData["Aimbot"]["Bone"]);
				Settings.Aimbot.Smooth = float.Parse(configData["Aimbot"]["Smooth"]);
				Settings.Aimbot.RecoilControl = bool.Parse(configData["Aimbot"]["RecoilControl"]);
				Settings.Aimbot.YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["YawRecoilReductionFactory"]);
				Settings.Aimbot.PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["PitchRecoilReductionFactory"]);
				Settings.Aimbot.Curve = bool.Parse(configData["Aimbot"]["Curve"]);
				Settings.Aimbot.CurveX = float.Parse(configData["Aimbot"]["CurveX"]);
				Settings.Aimbot.CurveY = float.Parse(configData["Aimbot"]["CurveY"]);

				Settings.FOVChanger.Enabled = bool.Parse(configData["FOVChanger"]["Enabled"]);
				Settings.FOVChanger.Fov = int.Parse(configData["FOVChanger"]["Fov"]);

				Settings.FOVChanger.ViewModelFov.Enabled = bool.Parse(configData["ViewModelFov"]["Enabled"]);
				Settings.FOVChanger.ViewModelFov.Fov = float.Parse(configData["ViewModelFov"]["Fov"]);

				Settings.FOVChanger.ViewModelFov.Enabled =
					Settings.StandaloneRCS.Enabled = bool.Parse(configData["StandaloneRCS"]["Enabled"]);
				Settings.AutoPistol.Enabled = bool.Parse(configData["AutoPistol"]["Enabled"]);
				Settings.No_Flash.Enabled = bool.Parse(configData["No_Flash"]["Enabled"]);
			}
			catch
			{
				Extensions.Error("[Config][Error] Could not load Config, try to delete your old one", 0, false);
			}

			Extensions.Information("[Config][Load] Loaded", true);
		}

		public static void Save(string path)
		{
			#region Base

			if (File.Exists(path))
				if (!IsFileUsable(path))
				{
					Extensions.Error("[Config][Save][Error] Config File is not usable", 0, false);
					return;
				}

			var configParser = new FileIniDataParser();
			var configData = new IniData();

			configData["Radar"]["Enabled"] = Settings.Radar.Enabled.ToString();

			configData["Bunnyhop"]["Enabled"] = Settings.Bunnyhop.Enabled.ToString();
			configData["Bunnyhop"]["Key"] = Settings.Bunnyhop.Key.ToString();

			configData["Trigger"]["Enabled"] = Settings.Trigger.Enabled.ToString();
			configData["Trigger"]["Key"] = Settings.Trigger.Key.ToString();
			configData["Trigger"]["Delay"] = Settings.Trigger.Delay.ToString();

			configData["Chams"]["Enabled"] = Settings.Chams.Enabled.ToString();

			configData["Chams"]["Allies"] = Settings.Chams.Allies.ToString();
			configData["Chams"]["Allies_Color_R"] = Settings.Chams.Allies_Color_R.ToString();
			configData["Chams"]["Allies_Color_G"] = Settings.Chams.Allies_Color_G.ToString();
			configData["Chams"]["Allies_Color_B"] = Settings.Chams.Allies_Color_B.ToString();
			configData["Chams"]["Allies_Color_A"] = Settings.Chams.Allies_Color_A.ToString();

			configData["Chams"]["Enemies"] = Settings.Chams.Enemies.ToString();
			configData["Chams"]["Enemies_Color_R"] = Settings.Chams.Enemies_Color_R.ToString();
			configData["Chams"]["Enemies_Color_G"] = Settings.Chams.Enemies_Color_G.ToString();
			configData["Chams"]["Enemies_Color_B"] = Settings.Chams.Enemies_Color_B.ToString();
			configData["Chams"]["Enemies_Color_A"] = Settings.Chams.Enemies_Color_A.ToString();

			configData["Glow"]["Enabled"] = Settings.Glow.Enabled.ToString();
			configData["Glow"]["PlayerColorMode"] = Settings.Glow.PlayerColorMode.ToString();
			configData["Glow"]["FullBloom"] = Settings.Glow.FullBloom.ToString();

			configData["Glow"]["Allies"] = Settings.Glow.Allies.ToString();
			configData["Glow"]["Allies_Color_R"] = Settings.Glow.Allies_Color_R.ToString();
			configData["Glow"]["Allies_Color_G"] = Settings.Glow.Allies_Color_G.ToString();
			configData["Glow"]["Allies_Color_B"] = Settings.Glow.Allies_Color_B.ToString();
			configData["Glow"]["Allies_Color_A"] = Settings.Glow.Allies_Color_A.ToString();

			configData["Glow"]["Enemies"] = Settings.Glow.Enemies.ToString();
			configData["Glow"]["Enemies_Color_R"] = Settings.Glow.Enemies_Color_R.ToString();
			configData["Glow"]["Enemies_Color_G"] = Settings.Glow.Enemies_Color_G.ToString();
			configData["Glow"]["Enemies_Color_B"] = Settings.Glow.Enemies_Color_B.ToString();
			configData["Glow"]["Enemies_Color_A"] = Settings.Glow.Enemies_Color_A.ToString();

			configData["Glow"]["Snipers"] = Settings.Glow.Snipers.ToString();
			configData["Glow"]["Snipers_Color_R"] = Settings.Glow.Snipers_Color_R.ToString();
			configData["Glow"]["Snipers_Color_G"] = Settings.Glow.Snipers_Color_G.ToString();
			configData["Glow"]["Snipers_Color_B"] = Settings.Glow.Snipers_Color_B.ToString();
			configData["Glow"]["Snipers_Color_A"] = Settings.Glow.Snipers_Color_A.ToString();

			configData["Glow"]["Rifles"] = Settings.Glow.Rifles.ToString();
			configData["Glow"]["Rifles_Color_R"] = Settings.Glow.Rifles_Color_R.ToString();
			configData["Glow"]["Rifles_Color_G"] = Settings.Glow.Rifles_Color_G.ToString();
			configData["Glow"]["Rifles_Color_B"] = Settings.Glow.Rifles_Color_B.ToString();
			configData["Glow"]["Rifles_Color_A"] = Settings.Glow.Rifles_Color_A.ToString();

			configData["Glow"]["MachineGuns"] = Settings.Glow.MachineGuns.ToString();
			configData["Glow"]["MachineGuns_Color_R"] = Settings.Glow.MachineGuns_Color_R.ToString();
			configData["Glow"]["MachineGuns_Color_G"] = Settings.Glow.MachineGuns_Color_G.ToString();
			configData["Glow"]["MachineGuns_Color_B"] = Settings.Glow.MachineGuns_Color_B.ToString();
			configData["Glow"]["MachineGuns_Color_A"] = Settings.Glow.MachineGuns_Color_A.ToString();

			configData["Glow"]["MPs"] = Settings.Glow.MPs.ToString();
			configData["Glow"]["MPs_Color_R"] = Settings.Glow.MPs_Color_R.ToString();
			configData["Glow"]["MPs_Color_G"] = Settings.Glow.MPs_Color_G.ToString();
			configData["Glow"]["MPs_Color_B"] = Settings.Glow.MPs_Color_B.ToString();
			configData["Glow"]["MPs_Color_A"] = Settings.Glow.MPs_Color_A.ToString();

			configData["Glow"]["Pistols"] = Settings.Glow.Pistols.ToString();
			configData["Glow"]["Pistols_Color_R"] = Settings.Glow.Pistols_Color_R.ToString();
			configData["Glow"]["Pistols_Color_G"] = Settings.Glow.Pistols_Color_G.ToString();
			configData["Glow"]["Pistols_Color_B"] = Settings.Glow.Pistols_Color_B.ToString();
			configData["Glow"]["Pistols_Color_A"] = Settings.Glow.Pistols_Color_A.ToString();

			configData["Glow"]["Shotguns"] = Settings.Glow.Shotguns.ToString();
			configData["Glow"]["Shotguns_Color_R"] = Settings.Glow.Shotguns_Color_R.ToString();
			configData["Glow"]["Shotguns_Color_G"] = Settings.Glow.Shotguns_Color_G.ToString();
			configData["Glow"]["Shotguns_Color_B"] = Settings.Glow.Shotguns_Color_B.ToString();
			configData["Glow"]["Shotguns_Color_A"] = Settings.Glow.Shotguns_Color_A.ToString();

			configData["Glow"]["C4"] = Settings.Glow.C4.ToString();
			configData["Glow"]["C4_Color_R"] = Settings.Glow.C4_Color_R.ToString();
			configData["Glow"]["C4_Color_G"] = Settings.Glow.C4_Color_G.ToString();
			configData["Glow"]["C4_Color_B"] = Settings.Glow.C4_Color_B.ToString();
			configData["Glow"]["C4_Color_A"] = Settings.Glow.C4_Color_A.ToString();

			configData["Glow"]["Grenades"] = Settings.Glow.Grenades.ToString();
			configData["Glow"]["Grenades_Color_R"] = Settings.Glow.Grenades_Color_R.ToString();
			configData["Glow"]["Grenades_Color_G"] = Settings.Glow.Grenades_Color_G.ToString();
			configData["Glow"]["Grenades_Color_B"] = Settings.Glow.Grenades_Color_B.ToString();
			configData["Glow"]["Grenades_Color_A"] = Settings.Glow.Grenades_Color_A.ToString();

			configData["Aimbot"]["Enabled"] = Settings.Aimbot.Enabled.ToString();
			configData["Aimbot"]["Fov"] = Settings.Aimbot.Fov.ToString();
			configData["Aimbot"]["Bone"] = Settings.Aimbot.Bone.ToString();
			configData["Aimbot"]["Smooth"] = Settings.Aimbot.Smooth.ToString();
			configData["Aimbot"]["RecoilControl"] = Settings.Aimbot.RecoilControl.ToString();
			configData["Aimbot"]["YawRecoilReductionFactory"] = Settings.Aimbot.YawRecoilReductionFactory.ToString();
			configData["Aimbot"]["PitchRecoilReductionFactory"] = Settings.Aimbot.PitchRecoilReductionFactory.ToString();
			configData["Aimbot"]["Curve"] = Settings.Aimbot.Curve.ToString();
			configData["Aimbot"]["CurveX"] = Settings.Aimbot.CurveX.ToString();
			configData["Aimbot"]["CurveY"] = Settings.Aimbot.CurveY.ToString();

			#endregion

			configData["FOVChanger"]["Enabled"] = Settings.FOVChanger.Enabled.ToString();
			configData["FOVChanger"]["Fov"] = Settings.FOVChanger.Fov.ToString();
			configData["ViewModelFov"]["Enabled"] = Settings.FOVChanger.ViewModelFov.Enabled.ToString();
			configData["ViewModelFov"]["Fov"] = Settings.FOVChanger.ViewModelFov.Fov.ToString();

			configData["No_Flash"]["Enabled"] = Settings.No_Flash.Enabled.ToString();
			configData["AutoPistol"]["Enabled"] = Settings.AutoPistol.Enabled.ToString();
			configData["StandaloneRCS"]["Enabled"] = Settings.StandaloneRCS.Enabled.ToString();

			configParser.WriteFile(path, configData);

			Extensions.Information("[Config][Save] Saved", true);
		}
	}
}