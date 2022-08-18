using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace LaurensKruis.CSharpUtil
{
    [Serializable]
    public class SceneReference : ISerializationCallbackReceiver
    {
        /*
         * We have two truths here: "asset", and "path"
         * While in the editor, we assume "asset" to always be true, since this is what we use in the editor
         * While in a build, we assume "path" to be true, since "asset" isn't compiled
         */

#if UNITY_EDITOR
        [SerializeField]
        private SceneAsset asset;

#pragma warning disable 0414
        [HideInInspector, SerializeField,]
        private bool isDirty = false;
#pragma warning restore 0414


#endif

        [HideInInspector, SerializeField]
        private string path;


        public string Path
        {
            get
            {
#if UNITY_EDITOR
                string r = AssetDatabase.GetAssetPath(asset);
                return string.IsNullOrEmpty(r) ? null : r;
#else
                return path;
#endif  
            }

            set
            {
#if UNITY_EDITOR
                asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(value);

                if(asset == null)
                {
                    Debug.LogError($"Trying to set {nameof(SceneReference)}.Path to {value}, but no scene could be located there.");
                }

                path = asset == null ? null : value;

                isDirty = true;
#else
                path = value;
#endif


            }
        }


        public SceneReference()
        {

        }

        public SceneReference(SceneReference sceneReference)
        {
            path = sceneReference.Path;

#if UNITY_EDITOR
            asset = sceneReference.asset;
            isDirty = true;
#endif
        }

        public SceneReference Clone() => new SceneReference(this);

        public void OnBeforeSerialize()
        {
            string r = AssetDatabase.GetAssetPath(asset);
            path = string.IsNullOrEmpty(r) ? null : r;
        }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            EditorApplication.update += UpdateAsset;
#endif
        }

#if UNITY_EDITOR
        private void UpdateAsset()
        {
            EditorApplication.update -= UpdateAsset;

            string r = AssetDatabase.GetAssetPath(asset);
            path = string.IsNullOrEmpty(r) ? null : r;
        }
#endif
    }
}
