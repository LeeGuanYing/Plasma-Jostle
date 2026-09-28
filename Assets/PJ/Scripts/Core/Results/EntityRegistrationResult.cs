namespace PokemonJason.Core
{
    public enum EntityRegistrationStatus
    {
        Success,
        NullEntity,
        AlreadyRegistered,
        InvalidRuntimeId,
        DuplicateRuntimeId,
        InvalidPersistentId,
        DuplicatePersistentId
    }

    /// <summary>
    /// Describes the outcome of registering an entity.
    /// </summary>
    public readonly struct EntityRegistrationResult
    {
        public EntityRegistrationStatus Status { get; }
        public PJEntity Entity { get; }
        public PJEntity ConflictingEntity { get; }

        public bool Success => Status == EntityRegistrationStatus.Success;

        private EntityRegistrationResult(
            EntityRegistrationStatus status,
            PJEntity entity,
            PJEntity conflictingEntity)
        {
            Status = status;
            Entity = entity;
            ConflictingEntity = conflictingEntity;
        }

        public static EntityRegistrationResult Succeeded(PJEntity entity)
        {
            return new EntityRegistrationResult(
                EntityRegistrationStatus.Success,
                entity,
                null);
        }

        public static EntityRegistrationResult Failed(
            EntityRegistrationStatus status,
            PJEntity entity = null,
            PJEntity conflictingEntity = null)
        {
            return new EntityRegistrationResult(
                status,
                entity,
                conflictingEntity);
        }
    }
}
