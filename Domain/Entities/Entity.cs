using System;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public Guid Key { get; protected set; } = Guid.NewGuid();

        public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; protected set; }

        public bool IsDeleted => DeletedAt.HasValue;

        public bool Delete()
        {
            if (IsDeleted) return false;

            DeletedAt = DateTime.UtcNow;
            return true;
        }
    }
}
