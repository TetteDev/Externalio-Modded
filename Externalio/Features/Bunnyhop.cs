using System;
using System.Numerics;
using System.Threading;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class Bunnyhop
	{
		public static void Run()
		{
			var prevViewAngle = Vector3.Zero;

			while (true)
			{
				Thread.Sleep(1);

				if (!Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.Bunnyhop.Key) & 0x8000)
				    || !Checks.IsIngame()
				    || !Structs.LocalPlayer.BaseStruct.Health.IsAlive()
					/* || !Checks.CanBunnyhop*/)
					if (Settings.Bunnyhop.AutoStrafing)
						//prevViewAngle = Vector3.Zero;
						continue;

				if (Checks.CanBunnyhop)
					MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceJump, 6);

				if (Settings.Bunnyhop.AutoStrafing)
				{
					if (Structs.ClientState.BaseStruct.ViewAngles.Y > prevViewAngle.Y)
						MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceLeft, 6);
					else if (Structs.ClientState.BaseStruct.ViewAngles.Y < prevViewAngle.Y)
						MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceRight, 6);

					prevViewAngle = Structs.ClientState.BaseStruct.ViewAngles;
				}
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}