using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSUtil.Collections
{
	public class JengaStack<T> : IEnumerable<T>, IReadOnlyCollection<T>
	{
		private List<T> items = new List<T>();
		//private T[] items = new T[0];

		public int Count => items.Count;

		public IEnumerator<T> GetEnumerator()
		{
			//return ((IEnumerable<T>)items).GetEnumerator();
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public void Push(T value)
		{
			items.Add(value);
		}

		public T Pop()
		{
			if (items.Count == 0)
				throw new InvalidOperationException();

			T item = items[items.Count - 1];

			items.RemoveAt(items.Count - 1);

			return item;
		}

		public T Peek(int index)
		{
			if (items.Count == 0)
				throw new InvalidOperationException();

			return items[items.Count - 1 - index];
		}

		public T Peek()
		{
			if (items.Count == 0)
				throw new InvalidOperationException();

			return items[items.Count - 1];
		}

		public bool Remove(T value)
		{
			return items.Remove(value);
		}
	}
}
