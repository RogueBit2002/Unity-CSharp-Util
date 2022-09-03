using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Collections
{
    [System.Serializable]
    public class FixedDictionary<TKey, TValue> : IFixedDictionary<TKey, TValue>
    {
        [System.Serializable]
        private struct SerializablePair
        {
            public TKey key;
            public TValue value;

            public SerializablePair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }

        [SerializeField]
        private SerializablePair[] items = new SerializablePair[0];

        public TValue this[TKey key]
        {
            get
            {
                foreach (SerializablePair pair in items)
                    if (pair.key.Equals(key))
                        return pair.value;

                throw new KeyNotFoundException();
            }
            set
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if(items[i].key.Equals(key))
                    {
                        items[i].value = value;
                    }
                }
            }
        }

        public int Count => items.Length;

        public FixedDictionary(IEnumerable<TKey> keys)
            : this(keys, new TValue[keys.Count()])
        {
        }

        public FixedDictionary(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            int keyCount = keys.Count();

            if (keyCount != values.Count())
                throw new System.ArgumentException("Keys and values length mismatch");


            if (keys.Distinct().Count() != keyCount)
                throw new System.ArgumentException("Not all keys are unique!");

            items = new SerializablePair[keyCount];

            IEnumerator<TValue> valueIterator = values.GetEnumerator();
            valueIterator.MoveNext();

            int index = 0;
            foreach (TKey key in keys)
            {
                items[index++] = new SerializablePair(key, valueIterator.Current);
                valueIterator.MoveNext();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return items.Select((SerializablePair pair, int index) => new KeyValuePair<TKey, TValue>(pair.key, pair.value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKey(TKey key)
        {
            foreach (SerializablePair pair in items)
                if (pair.key.Equals(key))
                    return true;

            return false;
        }
    }
}
