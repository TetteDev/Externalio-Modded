using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Externalio.Managers;
using Microsoft.VisualBasic.CompilerServices;

namespace Externalio.Other
{
	internal static class Globals
	{
		internal class Imports
		{
			public delegate IntPtr OpenProcessDelegate(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
			public static OpenProcessDelegate OpenProcess = DynApi.CreateAPI<OpenProcessDelegate>("kernel32.dll", "OpenProcess");

			public delegate bool ReadProcessMemoryDelegate(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);
			public static ReadProcessMemoryDelegate ReadProcessMemory = DynApi.CreateAPI<ReadProcessMemoryDelegate>("kernel32.dll", "ReadProcessMemory");

			public delegate bool WriteProcessMemoryDelegate(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);
			public static WriteProcessMemoryDelegate WriteProcessMemory = DynApi.CreateAPI<WriteProcessMemoryDelegate>("kernel32.dll", "WriteProcessMemory");

			public delegate void mouse_eventDelegate(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
			public static mouse_eventDelegate mouse_event = DynApi.CreateAPI<mouse_eventDelegate>("user32.dll", "mouse_event");

			public delegate int GetWindowThreadProcessIdDelegate(IntPtr handle, out int processId);
			public static GetWindowThreadProcessIdDelegate GetWindowThreadProcessId = DynApi.CreateAPI<GetWindowThreadProcessIdDelegate>("user32.dll", "GetWindowThreadProcessId");

			public delegate IntPtr GetForegroundWindowDelegate();
			public static GetForegroundWindowDelegate GetForegroundWindow = DynApi.CreateAPI<GetForegroundWindowDelegate>("user32.dll", "GetForegroundWindow");

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
			[DllImport("User32.dll")]
			public static extern short GetAsyncKeyState(Keys vKey);

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
			[DllImport("User32.dll")]
			public static extern short GetAsyncKeyState(int vKey);

			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
			[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
			public static extern long GetAsyncKeyState(long vKey);
		}

		internal class DynApi
		{
			public static T CreateAPI<T>(string containingDll, string methodName)
			{
				var asmb = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(Assembly.GetExecutingAssembly().FullName), AssemblyBuilderAccess.RunAndSave);
				var modb = asmb.DefineDynamicModule(MethodBase.GetCurrentMethod().Name);
				var declaringType = MethodBase.GetCurrentMethod().DeclaringType;
				if (declaringType == null) throw new InvalidOperationException();
				var tb = modb.DefineType(declaringType.Name, TypeAttributes.Public);
				var mi = typeof(T).GetMethods()[0];

				var mb = tb.DefinePInvokeMethod(methodName,
					containingDll,
					MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
					CallingConventions.Standard,
					mi.ReturnType, mi.GetParameters().Select(pI => pI.ParameterType).ToArray(),
					CallingConvention.Winapi,
					CharSet.Ansi);

				mb.SetImplementationFlags(mb.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);

				return Conversions.ToGenericParameter<T>(Delegate.CreateDelegate(typeof(T), tb.CreateType().GetMethod(methodName) ?? throw new InvalidOperationException()));
			}
		}

		internal class Proc
		{
			public static string Name = "csgo";

			public static string[] Modules =
			{
				"client_panorama.dll",
				"engine.dll",
				"vstdlib.dll"
			};

			public static Process Process { get; set; }

			public static (int Width, int Height) Resolution { get; set; }

			public static IntPtr GetOpenHandle => MemoryManager.m_pProcessHandle;

			public static void UpdateResolution()
			{
				Resolution = Misc.GetResolution();
			}
		}
	}
}