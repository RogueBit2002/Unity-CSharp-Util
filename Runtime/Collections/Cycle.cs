using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LaurensKruis.CSharpUtil.Collections
{
	public class Cycle<T> : ICollection<T>, IReadOnlyCollection<T>
	{
		private List<T> items = new List<T>();

		public int Count => items.Count;

		public bool IsReadOnly => false;

		#region Default Behaviour
		public void Add(T item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public void Insert(int index, T item)
		{
			items.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}


		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			foreach (T item in items)
				array[arrayIndex++] = item;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public bool Remove(T item)
		{
			return items.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
		#endregion

		public T this[int index]
		{
			get
			{
				if (items.Count == 0)
					throw new InvalidOperationException();

				while (index < 0)
					index += items.Count;

				index %= items.Count;

				return items[index];
			}

			set
			{
				if (items.Count == 0)
					throw new InvalidOperationException();

				while (index < 0)
					index += items.Count;

				index %= items.Count;

				items[index] = value;
			}
		}

		public void Shift(int amount)
		{
			if (items.Count == 0)
				throw new InvalidOperationException();

			int Solve(int index)
			{
				while (index < 0)
					index += items.Count;

				return index % items.Count;
			}


			T[] arr = new T[items.Count];

			for(int i = 0; i < arr.Length; i ++)
			{
				arr[Solve(i - amount)] = items[i];
			}

			items = arr.ToList();
		}


	}
}
