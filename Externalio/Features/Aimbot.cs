using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using Externalio.Managers;
using Externalio.Other;
using Math = Externalio.Other.Math;

namespace Externalio.Features
{
	internal class Aimbot
	{
		// TODO LIST:
		// -Visibility Check (Not that important)

		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(Settings.Aimbot.Smooth == 0f ? 1 : 45);

				if (!Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Keys.LButton) & 0x8000)
				    || Convert.ToBoolean((long) Globals.Imports.GetAsyncKeyState(Settings.Trigger.Key) & 0x8000)
				    || !Checks.IsIngame()
				    || !Structs.LocalPlayer.BaseStruct.Health.IsAlive()
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_INCGRENADE
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_FLASHBANG
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_SMOKEGRENADE
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_HEGRENADE
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_BAYONET
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_BUTTERFLY
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_FALCHION
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_FLIP
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_GUT
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_KARAMBIT
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_M9_BAYONET
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_SURVIVAL_BOWIE
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_T
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_TACTICAL
				    || Structs.LocalPlayer.Extensions.WeaponId == (int) Enums.WeaponIDs.WEAPON_KNIFE_PUSH) continue;

				var maxPlayers = Structs.ClientState.BaseStruct.MaxPlayers;
				var entities = MemoryManager.ReadMemory((int) Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

				var possibleTargets = new Dictionary<float, Vector3>();

				for (var i = 0; i < maxPlayers; i++)
				{
					var cEntity = Math.GetInt(entities, i * 0x10);

					var entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

					if (!entityStruct.Team.HasTeam()
					    || entityStruct.Team.IsMyTeam()
					    || !entityStruct.Health.IsAlive()
					    || entityStruct.Dormant) continue;

					var bonePosition = Extensions.Other.GetBonePos(cEntity, Settings.Aimbot.Bone);

					if (bonePosition == Vector3.Zero) continue;

					var destination = Settings.Aimbot.RecoilControl
						? Math.CalcAngle(Structs.LocalPlayer.BaseStruct.Position, bonePosition, Structs.LocalPlayer.BaseStruct.AimPunch, Structs.LocalPlayer.BaseStruct.VecView, Settings.Aimbot.YawRecoilReductionFactory,
							Settings.Aimbot.PitchRecoilReductionFactory)
						: Math.CalcAngle(Structs.LocalPlayer.BaseStruct.Position, bonePosition, Structs.LocalPlayer.BaseStruct.AimPunch, Structs.LocalPlayer.BaseStruct.VecView, 0f, 0f);

					if (destination == Vector3.Zero) continue;

					var distance = Math.GetDistance3D(destination, Structs.ClientState.BaseStruct.ViewAngles);

					if (!(distance <= Settings.Aimbot.Fov)) continue;

					possibleTargets.Add(distance, destination);
				}

				if (!possibleTargets.Any()) continue;

				var aimAngle = possibleTargets.OrderByDescending(x => x.Key).LastOrDefault().Value;

				if (Settings.Aimbot.Curve)
				{
					var qDelta = aimAngle - Structs.ClientState.BaseStruct.ViewAngles;
					qDelta += new Vector3(qDelta.Y / Settings.Aimbot.CurveY, qDelta.X / Settings.Aimbot.CurveX, qDelta.Z);

					aimAngle = Structs.ClientState.BaseStruct.ViewAngles + qDelta;
				}

				aimAngle = Math.NormalizeAngle(aimAngle);
				aimAngle = Math.ClampAngle(aimAngle);

				MemoryManager.WriteMemory<Vector3>(Structs.ClientState.Base + Offsets.dwClientState_ViewAngles, Settings.Aimbot.Smooth == 0f
					? aimAngle
					: Math.SmoothAim(Structs.ClientState.BaseStruct.ViewAngles, aimAngle, Settings.Aimbot.Smooth));
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}