using UnityEngine;

namespace PokemonJason.Core
{
    /// <summary>
    /// Base class for runtime entities managed by PJ.
    /// </summary>
    public abstract class PJEntity : MonoBehaviour
    {
        [SerializeField]
        private string entityId;

        public string EntityId => entityId;

        protected virtual void Awake()
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                entityId = System.Guid.NewGuid().ToString("N");
            }
        }
    }
}
