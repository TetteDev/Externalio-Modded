using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Externalio.Managers;

namespace Externalio.Other
{
	internal class Structs
	{
		public struct clrRender_t
		{
			public byte r;
			public byte g;
			public byte b;
			public byte a;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Glow_t
		{
			public float r; //0x4
			public float g; //0x8
			public float b; //0xC
			public float a; //0x10

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] junk1;

			public bool m_bRenderWhenOccluded; //0x24
			public bool m_bRenderWhenUnoccluded; //0x25
			public bool m_bFullBloom; //0x26
			public int GlowStyle;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] junk2;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct GlobalVars_t
		{
			[FieldOffset(0x0000)] public float RealTime; // 0x00

			[FieldOffset(0x0004)] public int FrameCount;

			[FieldOffset(0x0008)] public float AbsoluteFrametime;

			[FieldOffset(0x000C)] public float AbsoluteFrameStartTimestddev;

			[FieldOffset(0x0010)] public float Curtime;

			[FieldOffset(0x0014)] public float Frametime;

			[FieldOffset(0x0018)] public int MaxClients;

			[FieldOffset(0x001c)] public int TickCount;

			[FieldOffset(0x0020)] public float Interval_Per_Tick;

			[FieldOffset(0x0024)] public float Interpolation_Amount;

			[FieldOffset(0x0028)] public int SimTicksThisFrame;

			[FieldOffset(0x002c)] public int Network_Protocol;

			[FieldOffset(0x0030)] public IntPtr pSaveData;

			[FieldOffset(0x0031)] public bool m_bClient;

			[FieldOffset(0x0032)] public bool m_bRemoteClient;

			[FieldOffset(0x0036)] public int nTimestampNetworkingBase;

			[FieldOffset(0x003A)] public int nTimestampRandomizeWindow;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct LocalPlayer_t
		{
			[FieldOffset(Offsets.m_lifeState)] public int LifeState;

			[FieldOffset(Offsets.m_iHealth)] public int Health;

			[FieldOffset(Offsets.m_fFlags)] public int Flags;

			[FieldOffset(Offsets.m_iTeamNum)] public int Team;

			[FieldOffset(Offsets.m_iShotsFired)] public int ShotsFired;

			[FieldOffset(Offsets.m_iCrosshairId)] public int CrosshairID;

			[FieldOffset(Offsets.m_bDormant)] public bool Dormant;

			[FieldOffset(Offsets.m_MoveType)] public int MoveType;

			[FieldOffset(Offsets.m_vecOrigin)] public Vector3 Position;

			[FieldOffset(Offsets.m_aimPunchAngle)] public Vector3 AimPunch;

			[FieldOffset(Offsets.m_vecViewOffset)] public Vector3 VecView;

			[FieldOffset(Offsets.m_bIsScoped)] public bool IsScoped;

			[FieldOffset(Offsets.m_nTickBase)] public int TickBase;

			[FieldOffset(Offsets.m_iFOV)] public int FOV;

			[FieldOffset(Offsets.m_iObserverMode)] public int ShittyThirdperson;

			[FieldOffset(Offsets.m_hActiveWeapon)] public int ActiveWeapon;

			[FieldOffset(Offsets.flFlashDuration)] public float FlashDuration;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct Enemy_Crosshair_t
		{
			[FieldOffset(Offsets.m_iHealth)] public int Health;

			[FieldOffset(Offsets.m_iTeamNum)] public int Team;

			[FieldOffset(Offsets.m_bDormant)] public bool Dormant;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct Enemy_t
		{
			[FieldOffset(Offsets.m_iHealth)] public int Health;

			[FieldOffset(Offsets.m_iTeamNum)] public int Team;

			[FieldOffset(Offsets.m_bDormant)] public bool Dormant;

			[FieldOffset(Offsets.m_bSpotted)] public bool Spotted;

			[FieldOffset(Offsets.m_bSpottedByMask)] public bool SpottedByMask;

			[FieldOffset(Offsets.m_vecOrigin)] public Vector3 Position;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct ClientState_t
		{
			[FieldOffset(Offsets.dwClientState_State)] public int GameState;

			[FieldOffset(Offsets.dwClientState_MaxPlayer)] public int MaxPlayers;

			[FieldOffset(Offsets.dwClientState_ViewAngles)] public Vector3 ViewAngles;

			[FieldOffset(Offsets.dwClientState_IsHLTV)] public int IsHLTV;

			[FieldOffset(Offsets.dwClientState_Map)] public int Map;

			[FieldOffset(Offsets.dwClientState_MapDirectory)] public int MapDirectory;

			[FieldOffset(Offsets.dwClientState_PlayerInfo)] public int PlayerInfo;
		}

		public struct Base
		{
			public static IntPtr Client { get; set; }
			public static IntPtr Engine { get; set; }
			public static IntPtr Vstdlib { get; set; }

			public static float[] ViewMatrix { get; set; } = null;
		}

		public struct LocalPlayer
		{
			public static int Base { get; set; }
			public static LocalPlayer_t BaseStruct { get; set; }

			public class Extensions
			{
				public static float flNextPrimaryAttack
				{
					get
					{
						var localplayerActiveWeapon = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwEntityList + ((MemoryManager.ReadMemory<int>(Base + Offsets.m_hActiveWeapon) & 0xFFF) - 1) * 16);
						return MemoryManager.ReadMemory<float>(localplayerActiveWeapon + Offsets.m_flNextPrimaryAttack);
					}
				}

				public static int WeaponId
				{
					get
					{
						var it = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwEntityList + ((MemoryManager.ReadMemory<int>(Base + Offsets.m_hActiveWeapon) & 0xFFF) - 1) * 16);
						return MemoryManager.ReadMemory<int>(it + Offsets.m_iItemDefinitionIndex);
					}
				}

				public static float ViewModelFov
				{
					get => MemoryManager.ReadMemory<int>(Offsets.dwViewmodelFOV);
					set
					{
						var baValue = BitConverter.GetBytes(value);
						var intValue = BitConverter.ToInt32(baValue, 0);
						MemoryManager.WriteMemory<int>(
							(int) Structs.Base.Client + Offsets.dwViewmodelFOV,
							intValue ^ ((int) Structs.Base.Client + Offsets.dwViewmodelFOV - 0x2c));
					}
				}

				public static int Fov
				{
					set => MemoryManager.WriteMemory<int>(Base + Offsets.m_iFOV, value);
				}

				public static class ViewModelOffsets
				{
					public static float X
					{
						get => X;
						set
						{
							var baValue = BitConverter.GetBytes(value);
							var intValue = BitConverter.ToInt32(baValue, 0);
							MemoryManager.WriteMemory<int>(
								(int) Structs.Base.Client + Offsets.viewmodel_offset_x,
								intValue ^ ((int) Structs.Base.Client + Offsets.viewmodel_offset_x - 0x2c));
							X = value;
						}
					}

					public static float Y
					{
						get => Y;
						set
						{
							var baValue = BitConverter.GetBytes(value);
							var intValue = BitConverter.ToInt32(baValue, 0);
							MemoryManager.WriteMemory<int>(
								(int) Structs.Base.Client + Offsets.viewmodel_offset_y,
								intValue ^ ((int) Structs.Base.Client + Offsets.viewmodel_offset_y - 0x2c));
							Y = value;
						}
					}

					public static float Z
					{
						private get => Z;
						set
						{
							var baValue = BitConverter.GetBytes(value);
							var intValue = BitConverter.ToInt32(baValue, 0);
							MemoryManager.WriteMemory<int>(
								(int) Structs.Base.Client + Offsets.viewmodel_offset_z,
								intValue ^ ((int) Structs.Base.Client + Offsets.viewmodel_offset_z - 0x2c));
							Z = value;
						}
					}
				}
			}
		}

		public struct GlobalVars
		{
			public static int Base { get; set; }
			public static GlobalVars_t BaseStruct { get; set; }

			public static class Extensions
			{
				public static float ServerTime { get; set; }
			}
		}

		public struct Enemy_Crosshair
		{
			public static int Base { get; set; }

			public static int Health { get; set; }
			public static int Team { get; set; }
			public static bool Dormant { get; set; }
		}

		public struct Enemy
		{
			public int Base { get; set; }
			public int Health { get; set; }
			public int Team { get; set; }
			public bool Dormant { get; set; }
			public bool Spotted { get; set; }
			public bool SpottedByMask { get; set; }

			public Vector3 Position { get; set; }

			public static string Name(int cEntityIndex)
			{
				var radarBase = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwRadarBase);
				var radar = MemoryManager.ReadMemory<int>(radarBase + 0x20);
				return MemoryManager.ReadString(radar + 0x1ec * cEntityIndex + 0x3c, 32, false);
			}
		}

		public struct ClientState
		{
			public static int Base { get; set; }
			public static ClientState_t BaseStruct { get; set; }

			public class Extensions
			{
				public static int Template { get; set; }
			}
		}
	}
}