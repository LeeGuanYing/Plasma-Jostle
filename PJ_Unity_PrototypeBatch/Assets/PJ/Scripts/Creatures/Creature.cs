using PokemonJason.Core;
using UnityEngine;

namespace PokemonJason.Creatures
{
    [RequireComponent(typeof(CreatureHealth))]
    [RequireComponent(typeof(CreatureAI))]
    public sealed class Creature : PJEntity
    {
        [SerializeField] private CreatureData data;
        public CreatureData Data => data;
        public CreatureHealth Health { get; private set; }
        public CreatureAI AI { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Health = GetComponent<CreatureHealth>();
            AI = GetComponent<CreatureAI>();
            ConfigureComponents();
        }

        public void ConfigureData(CreatureData creatureData)
        {
            data = creatureData;
            if (Health == null) Health = GetComponent<CreatureHealth>();
            if (AI == null) AI = GetComponent<CreatureAI>();
            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            if (Health == null || AI == null) return;
            Health.Configure(data);
            AI.Configure(this, data);
        }
    }
}
