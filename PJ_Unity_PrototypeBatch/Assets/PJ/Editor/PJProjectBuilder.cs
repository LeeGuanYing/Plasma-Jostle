#if UNITY_EDITOR
using System.IO;
using PokemonJason.Core;
using PokemonJason.Creatures;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PokemonJason.Editor
{
    public static class PJProjectBuilder
    {
        private const string RootPath = "Assets/PJ";
        private const string DataPath = RootPath + "/Data/Creatures";
        private const string PrefabPath = RootPath + "/Prefabs/Creatures";
        private const string ScenePath = RootPath + "/Scenes/Prototype.unity";

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
        }

        [MenuItem("Pokemon Jason/Create Prototype Scene")]
        private static void CreatePrototypeScene()
        {
            EnsureDirectories();
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            CreateCoreRootInScene();
            CreateGround();
            CreatePlayer();
            CreatureData data = CreateCreatureData();
            Creature prefab = CreateCreaturePrefab(data);
            CreateCreatureSpawner(prefab, data);
            CreateCamera();
            CreateLight();
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"PJ Prototype Scene created at {ScenePath}");
        }

        private static void EnsureDirectories()
        {
            CreateDirectoryIfMissing(RootPath + "/Data");
            CreateDirectoryIfMissing(DataPath);
            CreateDirectoryIfMissing(RootPath + "/Prefabs");
            CreateDirectoryIfMissing(PrefabPath);
            CreateDirectoryIfMissing(RootPath + "/Scenes");
        }

        private static void CreateDirectoryIfMissing(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            string parent = Path.GetDirectoryName(path)?.Replace("\\", "/");
            string folder = Path.GetFileName(path);
            if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent)) CreateDirectoryIfMissing(parent);
            AssetDatabase.CreateFolder(parent, folder);
        }

        private static void CreateCoreRootInScene()
        {
            GameObject root = new GameObject("PJ_Game");
            root.AddComponent<PJGame>();
            root.AddComponent<PJEntityManager>();
        }

        private static void CreateGround()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.localScale = new Vector3(5f, 1f, 5f);
        }

        private static void CreatePlayer()
        {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 1f, 5f);
        }

        private static CreatureData CreateCreatureData()
        {
            const string assetPath = DataPath + "/PrototypeCreature.asset";
            CreatureData existing = AssetDatabase.LoadAssetAtPath<CreatureData>(assetPath);
            if (existing != null) return existing;

            CreatureData data = ScriptableObject.CreateInstance<CreatureData>();
            SerializedObject so = new SerializedObject(data);
            so.FindProperty("creatureId").stringValue = "prototype_creature";
            so.FindProperty("displayName").stringValue = "Prototype Creature";
            so.FindProperty("movementSpeed").floatValue = 2f;
            so.FindProperty("detectionRange").floatValue = 10f;
            so.FindProperty("maxHealth").intValue = 100;
            so.FindProperty("attackRange").floatValue = 2f;
            so.FindProperty("attackPower").intValue = 10;
            so.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.CreateAsset(data, assetPath);
            return data;
        }

        private static Creature CreateCreaturePrefab(CreatureData data)
        {
            const string assetPath = PrefabPath + "/PrototypeCreature.prefab";
            Creature existing = AssetDatabase.LoadAssetAtPath<Creature>(assetPath);
            if (existing != null) return existing;

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            obj.name = "PrototypeCreature";
            obj.transform.position = new Vector3(0f, 1f, 0f);
            Creature creature = obj.AddComponent<Creature>();
            creature.ConfigureData(data);
            Creature prefab = PrefabUtility.SaveAsPrefabAsset(obj, assetPath);
            Object.DestroyImmediate(obj);
            return prefab;
        }

        private static void CreateCreatureSpawner(Creature prefab, CreatureData data)
        {
            GameObject obj = new GameObject("CreatureSpawner");
            CreatureSpawner spawner = obj.AddComponent<CreatureSpawner>();
            SerializedObject so = new SerializedObject(spawner);
            so.FindProperty("creaturePrefab").objectReferenceValue = prefab;
            so.FindProperty("creatureData").objectReferenceValue = data;
            so.FindProperty("initialSpawnCount").intValue = 1;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void CreateCamera()
        {
            GameObject obj = new GameObject("Main Camera");
            Camera camera = obj.AddComponent<Camera>();
            obj.tag = "MainCamera";
            obj.transform.position = new Vector3(0f, 8f, 12f);
            obj.transform.rotation = Quaternion.Euler(30f, 180f, 0f);
            camera.fieldOfView = 60f;
        }

        private static void CreateLight()
        {
            GameObject obj = new GameObject("Directional Light");
            Light light = obj.AddComponent<Light>();
            light.type = LightType.Directional;
            obj.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }
    }
}
#endif
