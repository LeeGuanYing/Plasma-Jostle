#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using PokemonJason.Player;

namespace PokemonJason.Editor
{
    public static class PlayerFrameworkBuilder
    {
        private const string InputActionsPath = "Assets/InputSystem_Actions.inputactions";
        private const string PrefabPath = "Assets/PJ/Prefabs/Player.prefab";
        private const string ScenePath = "Assets/PJ/Scenes/PlayerFrameworkTest.unity";

        [MenuItem("PJ/Build Player Framework Test")]
        public static void Build()
        {
            InputActionAsset inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(InputActionsPath);
            if (inputActions == null)
            {
                Debug.LogError($"Cannot find InputActionAsset at {InputActionsPath}.");
                return;
            }
            GameObject player = CreatePlayer(inputActions);
            PrefabUtility.SaveAsPrefabAsset(player, PrefabPath);
            Object.DestroyImmediate(player);
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            CreateTestEnvironment();
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.position = new Vector3(0f, 0.05f, 0f);
            EditorSceneManager.SaveScene(scene, ScenePath);
            Selection.activeGameObject = instance;
            Debug.Log($"Player framework created. Open {ScenePath} and press Play.");
        }

        private static GameObject CreatePlayer(InputActionAsset inputActions)
        {
            GameObject player = new GameObject("Player");
            player.tag = "Player";
            CharacterController controller = player.AddComponent<CharacterController>();
            controller.height = 1.8f;
            controller.radius = 0.35f;
            controller.center = new Vector3(0f, 0.9f, 0f);
            controller.stepOffset = 0.3f;
            controller.slopeLimit = 45f;

            Transform cameraRoot = new GameObject("CameraRoot").transform;
            cameraRoot.SetParent(player.transform, false);
            cameraRoot.localPosition = new Vector3(0f, 1.62f, 0f);
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.SetParent(cameraRoot, false);
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.fieldOfView = 75f;
            camera.nearClipPlane = 0.05f;
            cameraObject.AddComponent<AudioListener>();

            PlayerController controllerScript = player.AddComponent<PlayerController>();
            SerializedObject controllerSerialized = new SerializedObject(controllerScript);
            controllerSerialized.FindProperty("inputActions").objectReferenceValue = inputActions;
            controllerSerialized.FindProperty("cameraRoot").objectReferenceValue = cameraRoot;
            controllerSerialized.ApplyModifiedPropertiesWithoutUndo();

            PlayerCamera cameraScript = cameraRoot.gameObject.AddComponent<PlayerCamera>();
            SerializedObject cameraSerialized = new SerializedObject(cameraScript);
            cameraSerialized.FindProperty("inputActions").objectReferenceValue = inputActions;
            cameraSerialized.ApplyModifiedPropertiesWithoutUndo();

            PlayerInteractor interactor = player.AddComponent<PlayerInteractor>();
            SerializedObject interactorSerialized = new SerializedObject(interactor);
            interactorSerialized.FindProperty("inputActions").objectReferenceValue = inputActions;
            interactorSerialized.FindProperty("playerCamera").objectReferenceValue = camera;
            interactorSerialized.ApplyModifiedPropertiesWithoutUndo();
            return player;
        }

        private static void CreateTestEnvironment()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Test Ground";
            ground.transform.localScale = new Vector3(10f, 1f, 10f);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "Interaction Test Object";
            cube.transform.position = new Vector3(0f, 0.5f, 5f);
            cube.transform.localScale = new Vector3(1.5f, 1f, 1.5f);
            GameObject lightObject = new GameObject("Directional Light");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }
    }
}
#endif
