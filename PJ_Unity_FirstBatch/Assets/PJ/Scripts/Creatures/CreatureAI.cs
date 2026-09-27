using UnityEngine;

namespace PokemonJason.Creatures
{
    public sealed class CreatureAI : MonoBehaviour
    {
        private Creature creature;
        private CreatureData data;
        private Transform target;

        public Transform Target => target;

        public void Configure(Creature owner, CreatureData creatureData)
        {
            creature = owner;
            data = creatureData;
        }

        private void Update()
        {
            if (creature == null || data == null || creature.Health.IsDead)
            {
                return;
            }

            AcquireTarget();

            if (target == null)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > data.AttackRange)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    data.MovementSpeed * Time.deltaTime);
            }
        }

        private void AcquireTarget()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance <= data.DetectionRange)
                {
                    return;
                }

                target = null;
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                return;
            }

            float distanceToPlayer =
                Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= data.DetectionRange)
            {
                target = player.transform;
            }
        }
    }
}
