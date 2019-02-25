using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Externalio.Other;

namespace Externalio.Managers
{
	internal class MemoryManager
	{
		public static IntPtr m_pProcessHandle = IntPtr.Zero;

		private static int m_iNumberOfBytesRead;
		private static int m_iNumberOfBytesWritten;

		public static void Initialize(int ProcessID)
		{
			m_pProcessHandle = Globals.Imports.OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, ProcessID);
		}

		public static T ReadMemory<T>(int address) where T : struct
		{
			var ByteSize = Marshal.SizeOf(typeof(T));

			var buffer = new byte[ByteSize];

			Globals.Imports.ReadProcessMemory((int) m_pProcessHandle, address, buffer, buffer.Length, ref m_iNumberOfBytesRead);
			return ByteArrayToStructure<T>(buffer);
		}

		public static string ReadString(int address, int maxLength = 32, bool zeroTerminated = false)
		{
			var buff = new byte[maxLength];
			var numBytesRead = 0;

			Globals.Imports.ReadProcessMemory((int) m_pProcessHandle, address, buff, buff.Length, ref numBytesRead);
			return zeroTerminated ? Encoding.UTF8.GetString(buff).Split('\0')[0] : Encoding.UTF8.GetString(buff);
		}

		public static byte[] ReadMemory(int offset, int size)
		{
			var buffer = new byte[size];

			Globals.Imports.ReadProcessMemory((int) m_pProcessHandle, offset, buffer, size, ref m_iNumberOfBytesRead);

			return buffer;
		}

		public static float[] ReadMatrix<T>(int address, int matrixSize) where T : struct
		{
			var ByteSize = Marshal.SizeOf(typeof(T));

			var buffer = new byte[ByteSize * matrixSize];

			Globals.Imports.ReadProcessMemory((int) m_pProcessHandle, address, buffer, buffer.Length, ref m_iNumberOfBytesRead);

			return ConvertToFloatArray(buffer);
		}

		public static void WriteMemory<T>(int address, object value) where T : struct
		{
			var buffer = StructureToByteArray(value);

			Globals.Imports.WriteProcessMemory((int) m_pProcessHandle, address, buffer, buffer.Length, out m_iNumberOfBytesWritten);
		}

		public static void WriteString(int address, string value)
		{
			if (value.Length < 1) return;

			var memory = new byte[value.Length];
			memory = Encoding.UTF8.GetBytes(value);
			Globals.Imports.WriteProcessMemory((int) m_pProcessHandle, address, memory, memory.Length, out var numBytesRead);
		}

		public static PModule FindModule(string modName)
		{
			var ret = new PModule();
			foreach (ProcessModule pm in Globals.Proc.Process.Modules)
				if (pm.ModuleName.Contains(modName.ToLower()) || pm.ModuleName == modName)
				{
					ret.Name = pm.ModuleName;
					ret.BaseAddress = (int) pm.BaseAddress;
					ret.EndAddress = (int) pm.BaseAddress + pm.ModuleMemorySize;
					return ret;
				}
			return ret;
		}

		public class PModule
		{
			public int BaseAddress = -1;
			public int EndAddress = -1;
			public string Name = "null";
		}

		// Credits to whoever made this, ported it from s10n

		#region SigScanner
		public static int ScanPattern(int dllAddress, string pattern, int extra, int offset, bool modeSubtract)
		{
			var tempOffset = BitConverter.ToInt32(ReadMemory(AobScan(dllAddress, 0x1800000, pattern, 0) + extra, 4), 0) + offset;

			if (modeSubtract) tempOffset -= dllAddress;

			return tempOffset;
		}

		private static int AobScan(int dll, int range, string signature, int instance)
		{
			if (signature == string.Empty) return -1;

			var tempSignature = Regex.Replace(signature.Replace("??", "3F"), "[^a-fA-F0-9]", "");

			var count = -1;

			var searchRange = new byte[tempSignature.Length / 2];

			for (var i = 0; i <= searchRange.Length - 1; i++) searchRange[i] = byte.Parse(tempSignature.Substring(i * 2, 2), NumberStyles.HexNumber);

			var readMemory = ReadMemory(dll, range);

			var temp1 = 0;
			var iEnd = searchRange.Length < 0x20 ? searchRange.Length : 0x20;

			for (var j = 0; j <= iEnd - 1; j++)
				if (searchRange[j] == 0x3f) temp1 = temp1 | (Convert.ToInt32(1) << (iEnd - j - 1));

			var sBytes = new int[0x100];

			if (temp1 != 0)
				for (var k = 0; k <= sBytes.Length - 1; k++) sBytes[k] = temp1;

			temp1 = 1;

			var index = iEnd - 1;

			while (index >= 0)
			{
				sBytes[searchRange[index]] = sBytes[searchRange[index]] | temp1;

				index -= 1;

				temp1 = temp1 << 1;
			}

			var temp2 = 0;

			while (temp2 <= readMemory.Length - searchRange.Length)
			{
				var last = searchRange.Length;

				temp1 = searchRange.Length - 1;

				var temp3 = -1;

				while (temp3 != 0)
				{
					temp3 = temp3 & sBytes[readMemory[temp2 + temp1]];

					if (temp3 != 0)
					{
						if (temp1 == 0)
						{
							count += 1;

							if (count == instance) return dll + temp2;

							temp2 += 2;
						}

						last = temp1;
					}

					temp1 -= 1;

					temp3 = temp3 << 1;
				}

				temp2 += last;
			}

			return -1;
		}

		#endregion

		#region Transformation

		public static float[] ConvertToFloatArray(byte[] bytes)
		{
			if (bytes.Length % 4 != 0) throw new ArgumentException();

			var floats = new float[bytes.Length / 4];

			for (var i = 0; i < floats.Length; i++)
				floats[i] = BitConverter.ToSingle(bytes, i * 4);

			return floats;
		}

		private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
		{
			var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

			try
			{
				return (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			}
			finally
			{
				handle.Free();
			}
		}

		private static byte[] StructureToByteArray(object obj)
		{
			var length = Marshal.SizeOf(obj);

			var array = new byte[length];

			var pointer = Marshal.AllocHGlobal(length);

			Marshal.StructureToPtr(obj, pointer, true);
			Marshal.Copy(pointer, array, 0, length);
			Marshal.FreeHGlobal(pointer);

			return array;
		}

		#endregion

		#region Constants

		private const int PROCESS_VM_OPERATION = 0x0008;
		private const int PROCESS_VM_READ = 0x0010;
		private const int PROCESS_VM_WRITE = 0x0020;

		#endregion
	}
}