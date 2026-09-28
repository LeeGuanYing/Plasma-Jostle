namespace PokemonJason.Core
{
    public enum EntityUnregistrationStatus
    {
        Success,
        NullEntity,
        NotRegistered
    }

    /// <summary>
    /// Describes the outcome of unregistering an entity.
    /// </summary>
    public readonly struct EntityUnregistrationResult
    {
        public EntityUnregistrationStatus Status { get; }
        public PJEntity Entity { get; }

        public bool Success => Status == EntityUnregistrationStatus.Success;

        private EntityUnregistrationResult(
            EntityUnregistrationStatus status,
            PJEntity entity)
        {
            Status = status;
            Entity = entity;
        }

        public static EntityUnregistrationResult Succeeded(PJEntity entity)
        {
            return new EntityUnregistrationResult(
                EntityUnregistrationStatus.Success,
                entity);
        }

        public static EntityUnregistrationResult Failed(
            EntityUnregistrationStatus status,
            PJEntity entity = null)
        {
            return new EntityUnregistrationResult(status, entity);
        }
    }
}
