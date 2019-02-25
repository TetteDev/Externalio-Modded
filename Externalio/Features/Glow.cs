using System.Threading;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class Glow
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(10);

				if (!Checks.IsIngame()) continue;

				var gObject = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwGlowObjectManager);
				var gCount = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwGlowObjectManager + 0x4);

				var gEntities = MemoryManager.ReadMemory(gObject, gCount * 0x38);

				for (var i = 0; i < gCount; i++)
				{
					var gEntity = Math.GetInt(gEntities, i * 0x38);
					if (gEntity == 0) continue;

					var classId = Extensions.Other.GetClassID(gEntity);
					if (classId < 0) continue;

					if (Settings.Glow.Snipers && Checks.IsSniper(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.Snipers_Color_R / 255f;
						currGlowObject.g = Settings.Glow.Snipers_Color_G / 255f;
						currGlowObject.b = Settings.Glow.Snipers_Color_B / 255f;
						currGlowObject.a = Settings.Glow.Snipers_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.Rifles && Checks.IsRifle(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.Rifles_Color_R / 255f;
						currGlowObject.g = Settings.Glow.Rifles_Color_G / 255f;
						currGlowObject.b = Settings.Glow.Rifles_Color_B / 255f;
						currGlowObject.a = Settings.Glow.Rifles_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.MachineGuns && Checks.IsMachineGun(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.MachineGuns_Color_R / 255f;
						currGlowObject.g = Settings.Glow.MachineGuns_Color_G / 255f;
						currGlowObject.b = Settings.Glow.MachineGuns_Color_B / 255f;
						currGlowObject.a = Settings.Glow.MachineGuns_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.Pistols && Checks.IsPistol(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.Pistols_Color_R / 255f;
						currGlowObject.g = Settings.Glow.Pistols_Color_G / 255f;
						currGlowObject.b = Settings.Glow.Pistols_Color_B / 255f;
						currGlowObject.a = Settings.Glow.Pistols_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.Shotguns && Checks.IsShotgun(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.Shotguns_Color_R / 255f;
						currGlowObject.g = Settings.Glow.Shotguns_Color_G / 255f;
						currGlowObject.b = Settings.Glow.Shotguns_Color_B / 255f;
						currGlowObject.a = Settings.Glow.Shotguns_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.MPs && Checks.IsMP(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.MPs_Color_R / 255f;
						currGlowObject.g = Settings.Glow.MPs_Color_G / 255f;
						currGlowObject.b = Settings.Glow.MPs_Color_B / 255f;
						currGlowObject.a = Settings.Glow.MPs_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.C4 && Checks.IsC4(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.C4_Color_R / 255f;
						currGlowObject.g = Settings.Glow.C4_Color_G / 255f;
						currGlowObject.b = Settings.Glow.C4_Color_B / 255f;
						currGlowObject.a = Settings.Glow.C4_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.Grenades && Checks.IsGrenade(classId))
					{
						var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

						currGlowObject.r = Settings.Glow.Grenades_Color_R / 255f;
						currGlowObject.g = Settings.Glow.Grenades_Color_G / 255f;
						currGlowObject.b = Settings.Glow.Grenades_Color_B / 255f;
						currGlowObject.a = Settings.Glow.Grenades_Color_A / 255f;
						currGlowObject.m_bRenderWhenOccluded = true;
						currGlowObject.m_bRenderWhenUnoccluded = false;

						if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

						MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
					}
					else if (Settings.Glow.Allies || Settings.Glow.Enemies && classId == (int) Enums.ClassIDs.CCSPlayer)
					{
						var glowEntity = MemoryManager.ReadMemory<Structs.Enemy_t>(gEntity);

						if (!glowEntity.Health.IsAlive()
						    || glowEntity.Dormant
						    || !glowEntity.Team.HasTeam()
						    || glowEntity.Dormant) continue;

						if (Settings.Glow.PlayerVisCheck && !glowEntity.SpottedByMask) continue;

						if (Settings.Glow.Enemies && !glowEntity.Team.IsMyTeam())
							if (Settings.Glow.PlayerColorMode == 0)
							{
								var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

								var color = Extensions.Colors.FromHealth(glowEntity.Health / 100f);

								currGlowObject.r = color.R / 255f;
								currGlowObject.g = color.G / 255f;
								currGlowObject.b = color.B / 255f;
								currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
								currGlowObject.m_bRenderWhenOccluded = true;
								currGlowObject.m_bRenderWhenUnoccluded = false;

								if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

								MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
							}
							else if (Settings.Glow.PlayerColorMode == 1)
							{
								var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

								currGlowObject.r = Settings.Glow.Enemies_Color_R / 255f;
								currGlowObject.g = Settings.Glow.Enemies_Color_G / 255f;
								currGlowObject.b = Settings.Glow.Enemies_Color_B / 255f;
								currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
								currGlowObject.m_bRenderWhenOccluded = true;
								currGlowObject.m_bRenderWhenUnoccluded = false;

								if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

								MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
							}

						if (Settings.Glow.Allies && glowEntity.Team.IsMyTeam())
							if (Settings.Glow.PlayerColorMode == 0)
							{
								var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

								var color = Extensions.Colors.FromHealth(glowEntity.Health / 100f);

								currGlowObject.r = color.R / 255f;
								currGlowObject.g = color.G / 255f;
								currGlowObject.b = color.B / 255f;
								currGlowObject.a = Settings.Glow.Allies_Color_A / 255f;
								currGlowObject.m_bRenderWhenOccluded = true;
								currGlowObject.m_bRenderWhenUnoccluded = false;

								if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

								MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
							}
							else if (Settings.Glow.PlayerColorMode == 1)
							{
								var currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4);

								currGlowObject.r = Settings.Glow.Allies_Color_R / 255f;
								currGlowObject.g = Settings.Glow.Allies_Color_G / 255f;
								currGlowObject.b = Settings.Glow.Allies_Color_B / 255f;
								currGlowObject.a = Settings.Glow.Allies_Color_A / 255f;
								currGlowObject.m_bRenderWhenOccluded = true;
								currGlowObject.m_bRenderWhenUnoccluded = false;

								if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

								MemoryManager.WriteMemory<Structs.Glow_t>(gObject + i * 0x38 + 0x4, currGlowObject);
							}
					}
				}
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}