using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;

namespace LaurensKruis.CSUtil.Extensions
{
    public static class PlayerLoopSystemExt
    {
		public static bool HasSubsystem<T>(this PlayerLoopSystem root)
		{
			PlayerLoopSystem[] list = root.subSystemList;

			foreach(PlayerLoopSystem system in root.subSystemList)
				if (typeof(T) == system.type || system.HasSubsystem<T>())
					return true;

			return false;
		}
		public static PlayerLoopSystem RemoveSubsystem<T>(this PlayerLoopSystem root)
		{
			List<PlayerLoopSystem> list = root.subSystemList.ToList();

			for(int i = list.Count - 1; i >= 0; i--)
			{
				if(list[i].type == typeof(T))
				{
					list.RemoveAt(i);
					break;
				}

				list[i] = list[i].RemoveSubsystem<T>();
			}

			root.subSystemList = list.ToArray();

			return root;
		}

		public static PlayerLoopSystem InsertBefore<T>(this PlayerLoopSystem root, PlayerLoopSystem system)
		{
			List<PlayerLoopSystem> list = root.subSystemList.ToList();

			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].type == typeof(T))
				{
					list.Insert(i, system);
					break;
				}

				list[i] = list[i].InsertBefore<T>(system);
			}

			root.subSystemList = list.ToArray();

			return root;
		}

		public static PlayerLoopSystem InsertAfter<T>(this PlayerLoopSystem root, PlayerLoopSystem system)
		{
			List<PlayerLoopSystem> list = root.subSystemList.ToList();

			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].type == typeof(T))
				{
					list.Insert(i+1, system);
					break;
				}

				list[i] = list[i].InsertAfter<T>(system);
			}

			root.subSystemList = list.ToArray();

			return root;
		}
    }
}
