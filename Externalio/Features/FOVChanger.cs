using System.Threading;
using Externalio.Other;

namespace Externalio.Features
{
	internal class FOVChanger
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (Structs.LocalPlayer.BaseStruct.IsScoped) continue;

				if (Settings.FOVChanger.Enabled)
					Structs.LocalPlayer.Extensions.Fov = Settings.FOVChanger.Fov;

				if (Settings.FOVChanger.ViewModelFov.Enabled)
					Structs.LocalPlayer.Extensions.ViewModelFov = Settings.FOVChanger.ViewModelFov.Fov;
			}
		}

		public static void OnDisableEvent()
		{
			Structs.LocalPlayer.Extensions.Fov = 90;
		}
	}
}