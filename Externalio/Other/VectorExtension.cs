using System.Numerics;
using Externalio.Managers;

namespace Externalio.Other
{
	public static class VectorExtension
	{
		public static bool ToScreen(this Vector3 from, out Vector2 worldScreenPos)
		{
			var w = 0.0f;
			var viewMatrix = Structs.Base.ViewMatrix ?? MemoryManager.ReadMatrix<float>((int) Structs.Base.Client + Offsets.dwViewMatrix, 16);

			var booleanVector = new Vector2
			{
				X = viewMatrix[0] * from.X + viewMatrix[1] * from.Y + viewMatrix[2] * from.Z + viewMatrix[3],
				Y = viewMatrix[4] * from.X + viewMatrix[5] * from.Y + viewMatrix[6] * from.Z + viewMatrix[7]
			};

			w = viewMatrix[12] * from.X + viewMatrix[13] * from.Y + viewMatrix[14] * from.Z + viewMatrix[15];

			if (w < 0.01f)
			{
				worldScreenPos = new Vector2(-1f, -1f);
				return false;
			}

			booleanVector.X *= 1.0f / w;
			booleanVector.Y *= 1.0f / w;

			var width = Globals.Proc.Resolution.Width;
			var height = Globals.Proc.Resolution.Height;

			float x = Globals.Proc.Resolution.Width / 2;
			float y = Globals.Proc.Resolution.Height / 2;

			x += 0.5f * booleanVector.X * width + 0.5f;
			y -= 0.5f * booleanVector.Y * height + 0.5f;
			;

			worldScreenPos = new Vector2(x, y);
			return true;
		}
	}
}