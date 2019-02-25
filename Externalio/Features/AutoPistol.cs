using System;
using System.Threading;
using System.Windows.Forms;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class AutoPistol
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(1);

				if (!Settings.AutoPistol.Enabled
				    || !Checks.IsIngame()
				    || !Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState((int) Keys.LButton) & 0x8000)
				    || Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_DEAGLE
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_ELITE
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_FIVESEVEN
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_GLOCK
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_HKP2000
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_P250
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_USP_SILENCER
				    && Structs.LocalPlayer.Extensions.WeaponId != (int) Enums.WeaponIDs.WEAPON_TEC9) continue;

				if (Structs.LocalPlayer.Extensions.flNextPrimaryAttack < Structs.GlobalVars.Extensions.ServerTime)
				{
					MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceAttack, 5);
					Thread.Sleep(1);
					MemoryManager.WriteMemory<int>((int) Structs.Base.Client + Offsets.dwForceAttack, 4);
				}
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}