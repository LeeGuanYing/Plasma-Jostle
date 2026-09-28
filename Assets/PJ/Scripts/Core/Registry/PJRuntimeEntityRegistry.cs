using System.Collections.Generic;

namespace PokemonJason.Core
{
    /// <summary>
    /// Indexes currently registered entities by RuntimeId.
    /// This registry does not own entity lifetime.
    /// </summary>
    internal sealed class PJRuntimeEntityRegistry
    {
        private readonly Dictionary<RuntimeId, PJEntity> entities = new();

        public int Count => entities.Count;

        public bool Register(RuntimeId id, PJEntity entity)
        {
            if (!id.IsValid || entity == null || entities.ContainsKey(id))
            {
                return false;
            }

            entities.Add(id, entity);
            return true;
        }

        public bool Unregister(RuntimeId id)
        {
            return entities.Remove(id);
        }

        public bool Contains(RuntimeId id)
        {
            return entities.ContainsKey(id);
        }

        public bool TryGet(RuntimeId id, out PJEntity entity)
        {
            return entities.TryGetValue(id, out entity);
        }

        public bool TryGetId(PJEntity entity, out RuntimeId id)
        {
            foreach (KeyValuePair<RuntimeId, PJEntity> pair in entities)
            {
                if (ReferenceEquals(pair.Value, entity))
                {
                    id = pair.Key;
                    return true;
                }
            }

            id = default;
            return false;
        }
    }
}
