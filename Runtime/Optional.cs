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
    }
}
