using System;
using System.Numerics;
using System.Threading;
using Externalio.Managers;
using Externalio.Other;
using Math = Externalio.Other.Math;

namespace Externalio.Features
{
	internal class Trigger
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (!Checks.IsIngame()
				    || !Structs.LocalPlayer.BaseStruct.Health.IsAlive()
				    || !Structs.Enemy_Crosshair.Health.IsAlive()
				    || !Structs.Enemy_Crosshair.Team.HasTeam()
				    || Structs.Enemy_Crosshair.Team.IsMyTeam()
				    || Structs.Enemy_Crosshair.Dormant) continue;

				switch (Settings.Trigger.TriggerbotType)
				{
					case Settings.Trigger.TriggerType.ALWAYS_ON:
						if (Settings.Trigger.Delay > 0) Thread.Sleep(Settings.Trigger.Delay);
						MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceAttack, 6);
						break;

					case Settings.Trigger.TriggerType.HOLD:
						if (!Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.Trigger.Key) & 0x8000)) break;
						if (Settings.Trigger.Delay > 0) Thread.Sleep(Settings.Trigger.Delay);
						MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceAttack, 6);
						break;
				}
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}