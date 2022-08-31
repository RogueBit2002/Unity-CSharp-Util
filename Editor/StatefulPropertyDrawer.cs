using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Editor
{

    public class StatefulPropertyDrawer<T> : PropertyDrawer where T : class, StatefulPropertyDrawer<T>.IState, new()
    {
        public interface IState
        {
            public void Setup(SerializedProperty property);
        }

        private struct Identifier
        {
            public int instanceID;
            public string propertyPath;

            public Identifier(SerializedProperty property)
            {
                instanceID = property.serializedObject.targetObject.GetInstanceID();
                propertyPath = property.propertyPath;
            }
        }

        private class Entry
        {
            public T state;
            public bool accessed = true;

            public Entry(T state)
            {
                this.state = state;
            }
        }

        //TODO: Add manual garbage collection
        private static Dictionary<Identifier, Entry> states = new Dictionary<Identifier, Entry>();


        protected T EnsureState(SerializedProperty property)
        {
            Identifier id = new Identifier(property);

            if(!states.ContainsKey(id))
            {
                T state = new T();
                state.Setup(property);

                states.Add(id, new Entry(state));
            }

            return states[id].state;
        }

        protected void SetState(SerializedProperty property, T state)
        {
            Identifier id = new Identifier(property);

            if (!states.ContainsKey(id))
            {
                states.Add(id, new Entry(state));
                return;
            }

            states[id].state = state;
            states[id].accessed = true;
        }
    }
}
