using System.Collections.Generic;
using UnityEngine;

namespace PokemonJason.Core
{
    /// <summary>
    /// Tracks active PJ entities.
    /// </summary>
    public sealed class PJEntityManager : MonoBehaviour
    {
        public static PJEntityManager Instance { get; private set; }

        private readonly HashSet<PJEntity> entities = new();

        public IReadOnlyCollection<PJEntity> Entities => entities;

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

        public void Register(PJEntity entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        public void Unregister(PJEntity entity)
        {
            if (entity != null)
            {
                entities.Remove(entity);
            }
        }
    }
}
