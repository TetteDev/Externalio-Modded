using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Externalio.Windoof
{
	public class WinAPI
	{
		#region DLL IMPORTs

		public delegate bool VirtualProtectExDelegate(IntPtr hProcess, IntPtr lpAddress, int nSize, uint flNewProtect, out uint lpflOldProtect);
		public static VirtualProtectExDelegate VirtualProtectEx = Externalio.Other.Globals.DynApi.CreateAPI<VirtualProtectExDelegate>("kernel32.dll", "VirtualProtectEx");

		public delegate IntPtr CreateRemoteThreadDelegate(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
		public static CreateRemoteThreadDelegate CreateRemoteThread = Externalio.Other.Globals.DynApi.CreateAPI<CreateRemoteThreadDelegate>("kernel32.dll", "CreateRemoteThread");

		public delegate bool VirtualFreeExDelegate(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);
		public static VirtualFreeExDelegate VirtualFreeEx = Externalio.Other.Globals.DynApi.CreateAPI<VirtualFreeExDelegate>("kernel32.dll", "VirtualFreeEx");

		public delegate bool GetClientRectDelegate(IntPtr hWnd, out RECT lpRect);
		public static GetClientRectDelegate GetClientRect = Externalio.Other.Globals.DynApi.CreateAPI<GetClientRectDelegate>("user32.dll", "GetClientRect");


		//[DllImport("kernel32.dll")]
		//public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int nSize, uint flNewProtect, out uint lpflOldProtect);

		//[DllImport("kernel32.dll", SetLastError = true)]
		//public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		//[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		//public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

		//[DllImport("user32.dll")]
		//public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("kernel32.dll")]
		internal static extern uint WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize,
			uint flAllocationType, uint flProtect);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		[DllImport("kernel32.dll")]
		internal static extern bool CloseHandle(IntPtr hProcess);

		public struct RECT
		{
			public int Left, Top, Right, Bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
			{
			}

			public int X
			{
				get => Left;
				set
				{
					Right -= Left - value;
					Left = value;
				}
			}

			public int Y
			{
				get => Top;
				set
				{
					Bottom -= Top - value;
					Top = value;
				}
			}

			public int Height
			{
				get => Bottom - Top;
				set => Bottom = value + Top;
			}

			public int Width
			{
				get => Right - Left;
				set => Right = value + Left;
			}

			public Point Location
			{
				get => new Point(Left, Top);
				set
				{
					X = value.X;
					Y = value.Y;
				}
			}

			public Size Size
			{
				get => new Size(Width, Height);
				set
				{
					Width = value.Width;
					Height = value.Height;
				}
			}

			public static implicit operator Rectangle(RECT r)
			{
				return new Rectangle(r.Left, r.Top, r.Width, r.Height);
			}

			public static implicit operator RECT(Rectangle r)
			{
				return new RECT(r);
			}

			public static bool operator ==(RECT r1, RECT r2)
			{
				return r1.Equals(r2);
			}

			public static bool operator !=(RECT r1, RECT r2)
			{
				return !r1.Equals(r2);
			}

			public bool Equals(RECT r)
			{
				return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
			}

			public override bool Equals(object obj)
			{
				if (obj is RECT)
					return Equals((RECT) obj);
				if (obj is Rectangle)
					return Equals(new RECT((Rectangle) obj));
				return false;
			}

			public override int GetHashCode()
			{
				return ((Rectangle) this).GetHashCode();
			}

			public override string ToString()
			{
				return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
			}
		}

		#endregion
	}
}