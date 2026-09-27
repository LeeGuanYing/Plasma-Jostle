using UnityEngine;

namespace PokemonJason.Core
{
    /// <summary>
    /// Entry point for the PJ runtime.
    /// </summary>
    public sealed class PJGame : MonoBehaviour
    {
        public static PJGame Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
