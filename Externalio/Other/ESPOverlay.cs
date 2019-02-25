using System;
using System.Numerics;
using System.Threading;
using Externalio.Managers;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace Externalio.Other
{
	internal class ESPOverlay : IESP
	{
		private static GraphicsWindow _window;

		private SolidBrush _black;
		private SolidBrush _blue;

		private readonly float _debugFontSize = 18f;

		private Font _font;
		private readonly Graphics _graphics;
		private SolidBrush _gray;
		private SolidBrush _green;
		private SolidBrush _red;

		private SolidBrush _transparent;

		public ESPOverlay(IntPtr windowHandle)
		{
			// initialize a new Graphics object
			// GraphicsWindow will do the remaining initialization
			_graphics = new Graphics
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
				UseMultiThreadedFactories = false,
				VSync = true,
				WindowHandle = Globals.Proc.Process.MainWindowHandle
			};

			// it is important to set the window to visible (and topmost) if you want to see it!
			//_window = new GraphicsWindow(GetConsoleWindowHandle(), _graphics)
			_window = new GraphicsWindow(windowHandle, _graphics)
			{
				IsTopmost = true,
				IsVisible = true,
				FPS = 60
			};

			_window.SetupGraphics += _window_SetupGraphics;
			_window.DestroyGraphics += _window_DestroyGraphics;
			_window.DrawGraphics += _window_DrawGraphics;
		}

		public void Initialize()
		{
			Console.WindowWidth = 110;
			Console.WindowHeight = 35;
		}

		public void Run()
		{
			// creates the window and setups the graphics
			_window.StartThread();

			while (true)
				Thread.Sleep(100);
		}

		~ESPOverlay()
		{
			// you do not need to dispose the Graphics surface
			_window.Dispose();
		}

		private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
		{
			var gfx = e.Graphics;

			// creates a simple font with no additional style
			_font = gfx.CreateFont("Arial", 16);

			// colors for brushes will be automatically normalized. 0.0f - 1.0f and 0.0f - 255.0f is accepted!
			_black = gfx.CreateSolidBrush(0, 0, 0);
			_gray = gfx.CreateSolidBrush(0x24, 0x29, 0x2E);

			_transparent = gfx.CreateSolidBrush(Color.Transparent);

			_red = gfx.CreateSolidBrush(Color.Red); // those are the only pre defined Colors
			_green = gfx.CreateSolidBrush(Color.Green);
			_blue = gfx.CreateSolidBrush(Color.Blue);
		}

		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			var gfx = e.Graphics;

			if (!Checks.IsIngame())
			{
				gfx.ClearScene(_transparent); // set the background of the scene (can be transparent)
			}
			else
			{
				// you do not need to call BeginScene() or EndScene()
				gfx.ClearScene(_transparent); // set the background of the scene (can be transparent)

				if (Settings.ESP.DebugMode)
				{
					var startX = 275f;
					var startY = 50f;

					var increaseAmount = 0f;

					for (var strIdx = 0; strIdx < Settings.ESP.DebugStrings.Count; strIdx++)
					{
						gfx.DrawText(_font, _debugFontSize, _red, startX, startY + increaseAmount, Settings.ESP.DebugStrings[strIdx]);
						increaseAmount += _debugFontSize + 5f;
					}
				}

				if (Settings.ESP.DebugMode) return;

				var maxPlayers = Structs.ClientState.BaseStruct.MaxPlayers;
				var entities = MemoryManager.ReadMemory((int) Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

				for (var i = 0; i < maxPlayers; i++)
				{
					var _viewMatrix = Structs.Base.ViewMatrix;
					if (_viewMatrix == null) continue;

					var cEntity = Math.GetInt(entities, i * 0x10);

					var entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

					if (!entityStruct.Team.HasTeam()
					    || entityStruct.Team.IsMyTeam()
					    || !entityStruct.Health.IsAlive()
					    || entityStruct.Dormant
						/*&& (!entityStruct.Spotted || !entityStruct.SpottedByMask)*/) continue;

					if (entityStruct.Position == Vector3.Zero) continue;
					var bonePosition = Extensions.Other.GetBonePos(cEntity, Settings.Aimbot.Bone);
					if (bonePosition == Vector3.Zero) continue;

					if (bonePosition.ToScreen(out var _worldToScreenPos))
					{
						var boneX = _worldToScreenPos.X;
						var boneY = _worldToScreenPos.Y;

						//float flDist = Externalio.Other.Math.GetDistance3D(Structs.LocalPlayer.BaseStruct.Position, entityStruct.Position) * Externalio.Other.Math.METERS_PER_INCH;
						//float flDist = Externalio.Other.Math.GetDistance3D(Structs.LocalPlayer.BaseStruct.Position, entityStruct.Position);

						gfx.DrawCrosshair(_red, boneX, boneY, 9f, 5f, CrosshairStyle.Dot);
					}

					#region old code

					/*
					if (entityStruct.Position.ToScreen(booleanVector, out Vector2 _worldToScreenPos))
					{
						var posX = _worldToScreenPos.X;
						var posY = _worldToScreenPos.Y;

						if (bonePosition.ToScreen(booleanVector, out _worldToScreenPos))
						{
							float boneX = _worldToScreenPos.X;
							//float boneY = _worldToScreenPos.Y - _worldToScreenPos.Y / 64;
							float boneY = _worldToScreenPos.Y;

							float height = posY - boneY;
							float width = height / 2;

							float flDist = Externalio.Other.Math.GetDistance3D(Structs.LocalPlayer.BaseStruct.Position, entityStruct.Position);
							//gfx.DrawText(_font, (100f / flDist) < 13f ? 13f : (100f / flDist), _red, boneX / flDist , boneY / flDist, "XD");

							gfx.DrawCrosshair(_red, boneX, boneY, 9f, 5f, CrosshairStyle.Plus);

							//gfx.DrawLine(_red, new Point(posX - width / 2, posY), new Point(posX - width / 2, boneY), 0.5f);
							//gfx.DrawLine(_red, new Point(posX - width / 2, boneY), new Point(boneX + width / 2, boneY), 0.5f);
							//gfx.DrawLine(_red, new Point(boneX + width / 2, boneY), new Point(posX + width / 2, posY), 0.5f);
							//gfx.DrawLine(_red, new Point(posX + width / 2, posY), new Point(posX - width / 2, posY), 0.5f);
						}
					}
					*/

					#endregion
				}
			}
		}

		private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			// you may want to dispose any brushes, fonts or images
			_red.Dispose();
			_green.Dispose();
			_black.Dispose();
			_gray.Dispose();
			_blue.Dispose();
			_font.Dispose();
			_transparent.Dispose();
		}

		private static IntPtr GetAttachedWindowHandle()
		{
			return _window.ParenWindowHandle;
		}
	}
}