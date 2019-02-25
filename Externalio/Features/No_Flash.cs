using System.Threading;
using Externalio.Managers;
using Externalio.Other;

namespace Externalio.Features
{
	internal class No_Flash
	{
		public static void Run()
		{
			while (true)
			{
				Thread.Sleep(5);

				if (!Checks.IsIngame()
				    || !Settings.No_Flash.Enabled) continue;

				if (Structs.LocalPlayer.BaseStruct.FlashDuration > 0f)
					MemoryManager.WriteMemory<float>(Structs.LocalPlayer.Base + Offsets.flFlashDuration, 0);
			}
		}

		public static void OnDisableEvent()
		{
		}
	}
}