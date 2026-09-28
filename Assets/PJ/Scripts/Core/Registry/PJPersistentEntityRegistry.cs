using System.Collections.Generic;

namespace PokemonJason.Core
{
    /// <summary>
    /// Indexes currently loaded persistent entities by PersistentId.
    /// This registry does not own entity lifetime.
    /// </summary>
    internal sealed class PJPersistentEntityRegistry
    {
        private readonly Dictionary<PersistentId, PJEntity> entities = new();

        public int Count => entities.Count;

        public bool Register(PersistentId id, PJEntity entity)
        {
            if (!id.IsValid || entity == null || entities.ContainsKey(id))
            {
                return false;
            }

            entities.Add(id, entity);
            return true;
        }

        public bool Unregister(PersistentId id)
        {
            return entities.Remove(id);
        }

        public bool Contains(PersistentId id)
        {
            return entities.ContainsKey(id);
        }

        public bool TryGet(PersistentId id, out PJEntity entity)
        {
            return entities.TryGetValue(id, out entity);
        }

        public bool TryGetId(PJEntity entity, out PersistentId id)
        {
            foreach (KeyValuePair<PersistentId, PJEntity> pair in entities)
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
