using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSharpUtil
{
    [System.Serializable]
    public struct Optional<T>
    {
        [SerializeField, HideInInspector]
        private bool enabled;

        [SerializeField]
        private T value;

        public bool Enabled => enabled;
        public T Value => value;

        public Optional(T value)
        {
            this.value = value;
            enabled = false;
        }

        public Optional(bool enabled)
        {
            value = default(T);
            this.enabled = enabled;
        }
        public Optional(T value, bool enabled)
        {
            this.value = value;
            this.enabled = enabled;
        }
    }
}
