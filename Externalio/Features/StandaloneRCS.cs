using System;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using Externalio.Managers;
using Externalio.Other;
using Math = Externalio.Other.Math;

namespace Externalio.Features
{
	internal class StandaloneRCS
	{
		public static void Run()
		{
			var CurrentViewAngles = Vector3.Zero;
			var NewViewAngles = Vector3.Zero;
			var OldAimPunch = Vector3.Zero;
			var vPunch = Vector3.Zero;

			while (true)
			{
				Thread.Sleep(5);
				if (!Checks.IsIngame()
				    || !Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState((int) Keys.LButton) & 0x8000)
				    || (Settings.Aimbot.RecoilControl && Settings.Aimbot.RecoilControl)
				    || !Settings.StandaloneRCS.Enabled) continue;

				vPunch = Structs.LocalPlayer.BaseStruct.AimPunch;

				if (Structs.LocalPlayer.BaseStruct.ShotsFired > 1)
				{
					CurrentViewAngles = Structs.ClientState.BaseStruct.ViewAngles;

					NewViewAngles.X = CurrentViewAngles.X + OldAimPunch.X - vPunch.X * 2.0f;
					NewViewAngles.Y = CurrentViewAngles.Y + OldAimPunch.Y - vPunch.Y * 2.0f;
					NewViewAngles.Z = 0;

					NewViewAngles = Math.NormalizeAngle(NewViewAngles);
					NewViewAngles = Math.ClampAngle(NewViewAngles);

					OldAimPunch.X = vPunch.X * 2.0f;
					OldAimPunch.Y = vPunch.Y * 2.0f;
					OldAimPunch.Z = 0;

					MemoryManager.WriteMemory<Vector3>(Structs.ClientState.Base + Offsets.dwClientState_ViewAngles, NewViewAngles);
				}
				else
				{
					OldAimPunch.X = OldAimPunch.Y = OldAimPunch.Z = 0;
				}
			}
		}

		public static void OnDisableEvent()
		{
			// Do some disposing here if you'd likew
		}
	}
}