using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;

namespace LaurensKruis.CSUtil.Extensions
{
    public static class PlayerLoopSystemExt
    {
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
    }
}
