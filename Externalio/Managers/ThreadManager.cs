using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Externalio.Other;

namespace Externalio.Managers
{
	internal class ThreadManager
	{
		public static Dictionary<string, Thread> threads = new Dictionary<string, Thread>();
		public static Dictionary<string, Thread> activeThreads = new Dictionary<string, Thread>();
		public static Dictionary<string, Thread> pausedThreads = new Dictionary<string, Thread>();


		public static void Add(string name, ThreadStart function)
		{
			if (threads.TryGetValue(name, out var temp)) return;

			threads.Add(name, new Thread(function));
		}

		public static void ToggleThread(string name)
		{
			if (activeThreads.TryGetValue(name, out var temp))
			{
#pragma warning disable CS0618 // Typ oder Element ist veraltet
				temp.Suspend();
#pragma warning restore CS0618 // Typ oder Element ist veraltet

				activeThreads.Remove(name);

				pausedThreads.Add(name, temp);


				var t = Type.GetType($"Externalio.Features.{name}");
				if (t != null)
				{
					var method = t.GetMethod("OnDisableEvent", BindingFlags.Static | BindingFlags.Public);

					if (method != null)
					{
						method?.Invoke(null, null);
#if DEBUG
						Console.WriteLine("'OnDisableEvent' method for class '{0}' was executed!", $"Externalio.Features.{name}");
#endif
					}
					else
					{
						Console.WriteLine("\nFailed getting and executing method 'OnDisableEvent'  via reflection from class '" + name + "'");
					}
				}
				else
				{
					Console.WriteLine("\nFailed getting and executing method 'OnDisableEvent'  via reflection from class '" + name + "'");
				}

				Extensions.Information($"[ThreadManager][Paused] {name}", true);

				Console.Beep(300, 100);
				Console.Beep(300, 100);
			}
			else
			{
				if (!threads.TryGetValue(name, out temp))
				{
					Extensions.Error("[ThreadManager][Error] Could not start { name }", 1500, false);
					return;
				}

				if (pausedThreads.TryGetValue(name, out var temp2))
				{
#pragma warning disable CS0618 // Typ oder Element ist veraltet
					temp2.Resume();
#pragma warning restore CS0618 // Typ oder Element ist veraltet

					pausedThreads.Remove(name);

					Extensions.Information($"[ThreadManager][Resumed] {name}", true);

					Console.Beep(300, 100);
				}
				else
				{
					temp.Start();

					Extensions.Information($"[ThreadManager][Started] {name}", true);
				}

				activeThreads.Add(name, temp);
			}
		}
	}
}