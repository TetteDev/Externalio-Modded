using System.Threading;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class SkinChanger
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(500);

				var CurrentWeaponIndex = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_hActiveWeapon) & 0xFFF;
				var CurrentWeaponEntity = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwEntityList + (CurrentWeaponIndex - 1) * 0x10);
				var CurrentWeaponId = MemoryManager.ReadMemory<int>(CurrentWeaponEntity + Offsets.m_iItemDefinitionIndex);
				var MyXuid = MemoryManager.ReadMemory<int>(CurrentWeaponEntity + Offsets.m_OriginalOwnerXuidLow);

				if (CurrentWeaponId != (int) Enums.WeaponIDs.WEAPON_AK47) continue;

				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + 0x2FA4, -1);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_iItemIDHigh, -1);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_OriginalOwnerXuidLow, 0);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_OriginalOwnerXuidHigh, 0);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_nFallbackPaintKit, Settings.SkinChanger.AK_PAINTKIT);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_nFallbackSeed, 125);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_flFallbackWear, 0.0f);
				//MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_nFallbackStatTrak, 1337);
				MemoryManager.WriteMemory<int>(CurrentWeaponEntity + Offsets.m_iAccountID, MyXuid);

				// Figure out a way to force full update in panorama
			}
		}


		public static void OnDisableEvent()
		{
		}
	}
}