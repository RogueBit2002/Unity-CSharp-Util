using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSharpUtil
{
    public class IncludeAttribute : PropertyAttribute
    {
        public readonly Type type;
        public readonly IEnumerable<object> values;

        public IncludeAttribute(Type type, IEnumerable<object> values)
        {
            this.type = type;
            this.values = values;
        }

        public IncludeAttribute(Type type, params object[] values)
            : this(type, (IEnumerable<object>) values)
        {
        }
    }
}
