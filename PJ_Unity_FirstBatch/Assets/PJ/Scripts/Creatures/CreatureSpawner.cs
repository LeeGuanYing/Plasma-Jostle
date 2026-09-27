using UnityEngine;

namespace PokemonJason.Creatures
{
    public sealed class CreatureSpawner : MonoBehaviour
    {
        [SerializeField] private Creature creaturePrefab;
        [SerializeField] private CreatureData creatureData;
        [SerializeField] private int initialSpawnCount = 1;

        private void Start()
        {
            for (int i = 0; i < initialSpawnCount; i++)
            {
                Spawn();
            }
        }

        public Creature Spawn()
        {
            if (creaturePrefab == null)
            {
                Debug.LogError("CreatureSpawner requires a creature prefab.", this);
                return null;
            }

            Creature creature = Instantiate(
                creaturePrefab,
                transform.position,
                transform.rotation);

            return creature;
        }
    }
}
