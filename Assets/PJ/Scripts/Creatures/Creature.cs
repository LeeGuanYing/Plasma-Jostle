using PokemonJason.Core;
using UnityEngine;

namespace PokemonJason.Creatures
{
    [RequireComponent(typeof(CreatureHealth))]
    [RequireComponent(typeof(CreatureAI))]
    public sealed class Creature : PJEntity
    {
        [SerializeField]
        private CreatureData data;

        public CreatureData Data => data;
        public CreatureHealth Health { get; private set; }
        public CreatureAI AI { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Health = GetComponent<CreatureHealth>();
            AI = GetComponent<CreatureAI>();

            Health.Configure(data);
            AI.Configure(this, data);
        }
    }
}
