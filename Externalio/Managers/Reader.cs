using System.Threading;
using Externalio.Other;

namespace Externalio.Managers
{
	internal class Reader
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (Settings.ESP.Enabled)
					Structs.Base.ViewMatrix = MemoryManager.ReadMatrix<float>((int) Structs.Base.Client + Offsets.dwViewMatrix, 16);

				// Read Localplayer struct
				Structs.LocalPlayer.Base = MemoryManager.ReadMemory<int>((int) Structs.Base.Client + Offsets.dwLocalPlayer);
				Structs.LocalPlayer.BaseStruct = MemoryManager.ReadMemory<Structs.LocalPlayer_t>(Structs.LocalPlayer.Base);

				// Read GlobalVars struct 
				Structs.GlobalVars.BaseStruct = MemoryManager.ReadMemory<Structs.GlobalVars_t>((int) Structs.Base.Engine + Offsets.dwGlobalVars);
				Structs.GlobalVars.Extensions.ServerTime = Structs.LocalPlayer.BaseStruct.TickBase * Structs.GlobalVars.BaseStruct.Interval_Per_Tick;

				// Read ClientState struct
				Structs.ClientState.Base = MemoryManager.ReadMemory<int>((int) Structs.Base.Engine + Offsets.dwClientState);
				Structs.ClientState.BaseStruct = MemoryManager.ReadMemory<Structs.ClientState_t>(Structs.ClientState.Base);

				if (Settings.Trigger.Enabled)
				{
					Structs.Enemy_Crosshair.Base = (int) Structs.Base.Client + Offsets.dwEntityList + (Structs.LocalPlayer.BaseStruct.CrosshairID - 1) * 0x10;
					var crosshairStruct = MemoryManager.ReadMemory<Structs.Enemy_Crosshair_t>(MemoryManager.ReadMemory<int>(Structs.Enemy_Crosshair.Base));
					Structs.Enemy_Crosshair.Health = crosshairStruct.Health;
					Structs.Enemy_Crosshair.Dormant = crosshairStruct.Dormant;
					Structs.Enemy_Crosshair.Team = crosshairStruct.Team;
				}
			}
		}
	}
}