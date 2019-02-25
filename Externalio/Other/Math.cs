using System;
using System.Numerics;

namespace Externalio.Other
{
	internal class Math
	{
		public static float M_PI_F = 180.0f / Convert.ToSingle(System.Math.PI);
		public static float METERS_PER_INCH = 0.0254f;

		public static int GetInt(byte[] bytes, int offset)
		{
			return bytes[offset] | (bytes[++offset] << 8) | (bytes[++offset] << 16) | (bytes[++offset] << 24);
		}

		public static float GetDistance3D(Vector3 playerPosition, Vector3 enemyPosition)
		{
			return Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(enemyPosition.X - playerPosition.X, 2f) + System.Math.Pow(enemyPosition.Y - playerPosition.Y, 2f) +
			                                         System.Math.Pow(enemyPosition.Z - playerPosition.Z, 2f)));
		}

		public static Vector3 SmoothAim(Vector3 viewAngle, Vector3 destination, float smoothAmount)
		{
			var smoothedAngle = destination - viewAngle;

			smoothedAngle /= smoothAmount;
			smoothedAngle += viewAngle;

			smoothedAngle = NormalizeAngle(smoothedAngle);
			smoothedAngle = ClampAngle(smoothedAngle);

			return smoothedAngle;
		}

		public static Vector3 ClampAngle(Vector3 angle)
		{
			while (angle.Y > 180) angle.Y -= 360;
			while (angle.Y < -180) angle.Y += 360;

			if (angle.X > 89.0f) angle.X = 89.0f;
			if (angle.X < -89.0f) angle.X = -89.0f;

			angle.Z = 0f;

			return angle;
		}

		public static Vector3 NormalizeAngle(Vector3 angle)
		{
			while (angle.X < -180.0f) angle.X += 360.0f;
			while (angle.X > 180.0f) angle.X -= 360.0f;

			while (angle.Y < -180.0f) angle.Y += 360.0f;
			while (angle.Y > 180.0f) angle.Y -= 360.0f;

			while (angle.Z < -180.0f) angle.Z += 360.0f;
			while (angle.Z > 180.0f) angle.Z -= 360.0f;

			return angle;
		}

		public static Vector3 CalcAngle(Vector3 playerPosition, Vector3 enemyPosition, Vector3 aimPunch, Vector3 vecView, float yawRecoilReductionFactory, float pitchRecoilReductionFactor)
		{
			var delta = new Vector3(playerPosition.X - enemyPosition.X, playerPosition.Y - enemyPosition.Y, playerPosition.Z + vecView.Z - enemyPosition.Z);

			var tmp = Vector3.Zero;
			tmp.X = Convert.ToSingle(System.Math.Atan(delta.Z / System.Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y))) * 57.295779513082f - aimPunch.X * yawRecoilReductionFactory;
			tmp.Y = Convert.ToSingle(System.Math.Atan(delta.Y / delta.X)) * M_PI_F - aimPunch.Y * pitchRecoilReductionFactor;
			tmp.Z = 0;

			if (delta.X >= 0.0) tmp.Y += 180f;

			tmp = NormalizeAngle(tmp);
			tmp = ClampAngle(tmp);

			return tmp;
		}
	}
}