using System.Threading;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class Radar
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(50);

				if (!Checks.IsIngame()) continue;

				var maxPlayers = Structs.ClientState.BaseStruct.MaxPlayers;

				var entities = MemoryManager.ReadMemory((int) Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

				for (var i = 0; i < maxPlayers; i++)
				{
					var cEntity = Math.GetInt(entities, i * 0x10);

					var cEntityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

					if (cEntityStruct.Spotted
					    || !cEntityStruct.Health.IsAlive()
					    || cEntityStruct.Team.IsMyTeam()
					    || cEntityStruct.Dormant) continue;

					MemoryManager.WriteMemory<int>(cEntity + Offsets.m_bSpotted, 1);

					Thread.Sleep(150);
				}
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}