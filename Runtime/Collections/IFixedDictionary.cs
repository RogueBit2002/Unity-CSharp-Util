using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Collections
{
    public interface IFixedDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        public TValue this[TKey key] {get; set;}

        public bool ContainsKey(TKey key);
    }
}
