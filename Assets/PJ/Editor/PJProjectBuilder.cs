#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using PokemonJason.Core;

namespace PokemonJason.Editor
{
    public static class PJProjectBuilder
    {
        [MenuItem("Pokemon Jason/Create Core Root")]
        private static void CreateCoreRoot()
        {
            if (Object.FindFirstObjectByType<PJGame>() != null)
            {
                Debug.Log("PJ core root already exists in the active scene.");
                return;
            }

            GameObject root = new GameObject("PJ_Game");
            root.AddComponent<PJGame>();
            root.AddComponent<PJEntityManager>();

            Selection.activeGameObject = root;
            EditorGUIUtility.PingObject(root);

            Debug.Log("Created PJ core root.");
        }
    }
}
#endif
