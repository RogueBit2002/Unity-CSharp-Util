using System.Collections;
using System.Collections.Generic;
using System;

namespace LaurensKruis.CSharpUtil.Collections
{
    public class Flow<T> : IEnumerable<T>
    {
		private T[] items;

		public int Size
		{
			get
			{
				return items.Length;
			}

			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException();
				T[] newItems = new T[value];

				Array.Copy(items, 0, newItems, 0, value);

				items = newItems;
			}
		}

		public T this[int index]
		{
			get
			{
				return items[index];
			}

			set
			{
				items[index] = value;
			}
		}

		public Flow(int size)
		{
			items = new T[size];
		}


		public void Push(T item)
		{
			Array.Copy(items, 0, items, 1, items.Length - 1);
			items[0] = item;
		}

		public void Clear()
		{
			//items = new T[items.Length];
			for (int i = 0; i < items.Length; i++)
				items[i] = default(T);
		}

		public bool Contains(T item)
		{
			if(item == null)
			{
				foreach (T i in items)
					if (i == null)
						return true;

				return false;
			}

			foreach (T i in items)
				if (EqualityComparer<T>.Default.Equals(i, item))
					return true;

			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(items, 0, array, arrayIndex, items.Length);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)items).GetEnumerator();
		}

		public bool Remove(T item)
		{
			for(int i = 0; i < items.Length; i ++)
			{
				if(EqualityComparer<T>.Default.Equals(items[i], item))
				{
					RemoveAt(i);
					return true;
				}
			}

			return false;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index > items.Length - 1)
				throw new ArgumentOutOfRangeException();

			if (index < items.Length - 1)
				Array.Copy(items, index + 1, items, index, items.Length - 1 - index);
			
			items[items.Length - 1] = default(T);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
