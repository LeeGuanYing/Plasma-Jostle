using UnityEngine;

namespace PokemonJason.Creatures
{
    /// <summary>
    /// Data definition shared by creature instances.
    /// </summary>
    [CreateAssetMenu(
        fileName = "CreatureData",
        menuName = "Pokemon Jason/Creature Data")]
    public sealed class CreatureData : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string creatureId;
        [SerializeField] private string displayName;

        [Header("Movement")]
        [Min(0f)]
        [SerializeField] private float movementSpeed = 3f;

        [Min(0f)]
        [SerializeField] private float detectionRange = 10f;

        [Header("Combat")]
        [Min(1)]
        [SerializeField] private int maxHealth = 100;

        [Min(0f)]
        [SerializeField] private float attackRange = 2f;

        [Min(0)]
        [SerializeField] private int attackPower = 10;

        public string CreatureId => creatureId;
        public string DisplayName => displayName;
        public float MovementSpeed => movementSpeed;
        public float DetectionRange => detectionRange;
        public int MaxHealth => maxHealth;
        public float AttackRange => attackRange;
        public int AttackPower => attackPower;
    }
}
