using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Externalio.Other
{
	internal class Offsets
	{
		public const int cs_gamerules_data = 0x0;
		public const int m_ArmorValue = 0xB328;
		public const int m_Collision = 0x31C;
		public const int m_CollisionGroup = 0x474;
		public const int m_Local = 0x2FBC;
		public const int m_MoveType = 0x25C;
		public const int m_OriginalOwnerXuidHigh = 0x31B4;
		public const int m_OriginalOwnerXuidLow = 0x31B0;
		public const int m_SurvivalGameRuleDecisionTypes = 0x1318;
		public const int m_SurvivalRules = 0xCF0;
		public const int m_aimPunchAngle = 0x302C;
		public const int m_aimPunchAngleVel = 0x3038;
		public const int m_angEyeAnglesX = 0xB32C;
		public const int m_angEyeAnglesY = 0xB330;
		public const int m_bBombPlanted = 0x99D;
		public const int m_bFreezePeriod = 0x20;
		public const int m_bGunGameImmunity = 0x3928;
		public const int m_bHasDefuser = 0xB338;
		public const int m_bHasHelmet = 0xB31C;
		public const int m_bInReload = 0x3285;
		public const int m_bIsDefusing = 0x3914;
		public const int m_bIsQueuedMatchmaking = 0x74;
		public const int m_bIsScoped = 0x390A;
		public const int m_bIsValveDS = 0x75;
		public const int m_bSpotted = 0x93D;
		public const int m_bSpottedByMask = 0x980;
		public const int m_clrRender = 0x70;
		public const int m_dwBoneMatrix = 0x26A8;
		public const int m_fAccuracyPenalty = 0x3304;
		public const int m_fFlags = 0x104;
		public const int m_flC4Blow = 0x2990;
		public const int m_flDefuseCountDown = 0x29AC;
		public const int m_flDefuseLength = 0x29A8;
		public const int m_flFallbackWear = 0x31C0;
		public const int m_flFlashDuration = 0xA3E0;
		public const int m_flFlashMaxAlpha = 0xA3DC;

		// Got it my self, will probably need update later?
		public const int m_flFramerate = 0xCEC;

		public const int viewmodel_offset_x = 0xCC28E4;
		public const int viewmodel_offset_y = 0xCC293C;
		public const int viewmodel_offset_z = 0xCC2994;

		public const int flFlashDuration = 0xA3E0;
		// Got it my self, will probably need update later?

		public const int m_flLastBoneSetupTime = 0x2924;
		public const int m_flLowerBodyYawTarget = 0x3A74;
		public const int m_flNextAttack = 0x2D70;
		public const int m_flNextPrimaryAttack = 0x3218;
		public const int m_flSimulationTime = 0x268;
		public const int m_flTimerLength = 0x2994;
		public const int m_hActiveWeapon = 0x2EF8;
		public const int m_hMyWeapons = 0x2DF8;
		public const int m_hObserverTarget = 0x3388;
		public const int m_hOwner = 0x29CC;
		public const int m_hOwnerEntity = 0x14C;
		public const int m_iAccountID = 0x2FC8;
		public const int m_iClip1 = 0x3244;
		public const int m_iCompetitiveRanking = 0x1A84;
		public const int m_iCompetitiveWins = 0x1B88;
		public const int m_iCrosshairId = 0xB394;
		public const int m_iEntityQuality = 0x2FAC;
		public const int m_iFOV = 0x31E4;
		public const int m_iFOVStart = 0x31E8;
		public const int m_iGlowIndex = 0xA3F8;
		public const int m_iHealth = 0x100;
		public const int m_iItemDefinitionIndex = 0x2FAA;
		public const int m_iItemIDHigh = 0x2FC0;
		public const int m_iMostRecentModelBoneCounter = 0x2690;
		public const int m_iObserverMode = 0x3374;
		public const int m_iShotsFired = 0xA370;
		public const int m_iState = 0x3238;
		public const int m_iTeamNum = 0xF4;
		public const int m_lifeState = 0x25F;
		public const int m_nFallbackPaintKit = 0x31B8;
		public const int m_nFallbackSeed = 0x31BC;
		public const int m_nFallbackStatTrak = 0x31C4;
		public const int m_nForceBone = 0x268C;
		public const int m_nTickBase = 0x342C;
		public const int m_rgflCoordinateFrame = 0x444;
		public const int m_szCustomName = 0x303C;
		public const int m_szLastPlaceName = 0x35B0;
		public const int m_thirdPersonViewAngles = 0x31D8;
		public const int m_vecOrigin = 0x138;
		public const int m_vecVelocity = 0x114;
		public const int m_vecViewOffset = 0x108;
		public const int m_viewPunchAngle = 0x3020;
		public const int clientstate_choked_commands = 0x4D28;
		public const int clientstate_delta_ticks = 0x174;
		public const int clientstate_last_outgoing_command = 0x4D24;
		public const int clientstate_net_channel = 0x9C;
		public const int convar_name_hash_table = 0x2F0F8;
		public const int dwClientState_GetLocalPlayer = 0x180;
		public const int dwClientState_IsHLTV = 0x4D40;
		public const int dwClientState_Map = 0x28C;
		public const int dwClientState_MapDirectory = 0x188;
		public const int dwClientState_MaxPlayer = 0x388;
		public const int dwClientState_PlayerInfo = 0x52B8;
		public const int dwClientState_State = 0x108;
		public const int dwClientState_ViewAngles = 0x4D88;
		public const int dwViewmodelFOV = 0xCD5624;
		public const int dwForceAttack = 0x310C710;
		public const int dwForceAttack2 = 0x310C71C;
		public const int dwForceBackward = 0x310C6C8;
		public const int dwForceForward = 0x310C6D4;
		public const int dwForceLeft = 0x310C6EC;
		public const int dwForceRight = 0x310C6E0;
		public const int dwGameDir = 0x631F70;
		public const int dwGameRulesProxy = 0x51F0504;
		public const int dwGetAllClasses = 0xCEE9D4;
		public const int dwGlobalVars = 0x58BA00;
		public const int dwInput = 0x5125DA0;
		public const int dwInterfaceLinkList = 0x8A7E14;
		public const int dwMouseEnable = 0xCD01F0;
		public const int dwMouseEnablePtr = 0xCD01C0;
		public const int dwPlayerResource = 0x310AA6C;
		public const int dwSensitivity = 0xCD008C;
		public const int dwSensitivityPtr = 0xCD0060;
		public const int dwSetClanTag = 0x896A0;
		public const int dwViewMatrix = 0x4CCCA24;
		public const int dwWeaponTable = 0x5126864;
		public const int dwWeaponTableIndex = 0x323C;
		public const int dwYawPtr = 0xCCFE50;
		public const int dwZoomSensitivityRatioPtr = 0xCD5090;
		public const int dwbSendPackets = 0xD230A;
		public const int dwppDirect3DDevice9 = 0xA3FC0;
		public const int force_update_spectator_glow = 0x38D172;
		public const int interface_engine_cvar = 0x3E9EC;
		public const int is_c4_owner = 0x399190;
		public const int m_bDormant = 0xED;
		public const int m_pStudioHdr = 0x294C;
		public const int m_pitchClassPtr = 0x5110000;
		public const int m_yawClassPtr = 0xCCFE50;
		public const int model_ambient_min = 0x58ED1C;
		public const int set_abs_angles = 0x1C7280;
		public const int set_abs_origin = 0x1C70C0;

		public const int haze_dwGlowObjectManager = 0x521AF50;

		public const int haze_dwEntityList = 0x4CDB00C;

		public const int haze_dwClientState = 0x58BCFC;

		public const int haze_dwForceJump = 0x517E1C4;

		public const int haze_dwLocalPlayer = 0xCCA6A4;

		public const int haze_dwRadarBase = 0x510FD4C;
		public static Dictionary<string, int> HazeDumperOffsets = new Dictionary<string, int>();
		public static int dwGlowObjectManager;
		public static int dwEntityList;
		public static int dwClientState;
		public static int dwForceJump;
		public static int dwLocalPlayer;
		public static int dwRadarBase;

		public static void FetchOffsets()
		{
			var offsetDictionary = new Dictionary<string, int>();
			var readContent = "";
			using (var wc = new WebClient())
			{
				readContent = wc.DownloadString("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.cs");
			}

			if (readContent.Length < 1) HazeDumperOffsets = new Dictionary<string, int>();

			var split = readContent.Split('\n');
			var idxBlockStart = Array.FindIndex(split, x => x.Contains("namespace hazedumper"));
			var idxBlockEnd = Array.FindIndex(split, x => x.Contains("} // namespace hazedumper"));

			split = split.Skip(idxBlockStart).Take(idxBlockEnd).ToArray();

			foreach (var line in split)
			{
				var stripped = line.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty);
				if (string.IsNullOrEmpty(stripped)) continue;

				if (stripped.IndexOf('=') == -1) continue;

				var offsetName = stripped.Split('=')[0].Trim(' ');
				if (offsetName.Contains("timestamp")) continue;

				var lastIdxOfWhitespace = offsetName.LastIndexOf(' ');
				offsetName = offsetName.Substring(lastIdxOfWhitespace, offsetName.Length - lastIdxOfWhitespace).Trim(' ');

				var offsetAssociatedWithName = stripped.Split('=')[1].Replace(";", string.Empty).Trim(' ');
				offsetDictionary.Add(offsetName, Convert.ToInt32(offsetAssociatedWithName, 16));
			}

			HazeDumperOffsets = offsetDictionary;
		}

		public static int GetOffset(string offsetName)
		{
			if (HazeDumperOffsets.Count < 1) return -1;

			if (HazeDumperOffsets.TryGetValue(offsetName, out var offset))
				return offset;

			return -1;
		}

		public static void UpdateOffsets()
		{
			if (HazeDumperOffsets.Count < 1)
			{
				// Get them from hardcoded variables
				dwGlowObjectManager = haze_dwGlowObjectManager;

				dwEntityList = haze_dwEntityList;

				dwClientState = haze_dwClientState;

				dwForceJump = haze_dwForceJump;

				dwLocalPlayer = haze_dwLocalPlayer;

				dwRadarBase = haze_dwRadarBase;
			}
			else
			{
				HazeDumperOffsets.TryGetValue("dwGlowObjectManager", out dwGlowObjectManager);
				HazeDumperOffsets.TryGetValue("dwEntityList", out dwEntityList);
				HazeDumperOffsets.TryGetValue("dwClientState", out dwClientState);
				HazeDumperOffsets.TryGetValue("dwForceJump", out dwForceJump);
				HazeDumperOffsets.TryGetValue("dwLocalPlayer", out dwLocalPlayer);
				HazeDumperOffsets.TryGetValue("dwRadarBase", out dwRadarBase);

				Extensions.Information("Updated offsets from hazedumper!", true);
			}
		}

		public class Signatures
		{
			public static string viewmodel_offset_x = "00 00 00 DE 4E 48 6B ? ? ? ? ? ? 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
			public static int viewmodel_offset_x_extraBytes = 3;
		}
	}
}