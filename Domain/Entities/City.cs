using System;

namespace Domain.Entities
{
    public class City
    {
        public Guid Key { get; private set; } = Guid.NewGuid();

        public string Name { get; private set; }

        public string PostalCode { get; private set; }

        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; private set; }

        public bool IsDeleted => DeletedAt.HasValue;

        public City(string name, string postalCode)
        {
            Name = name;
            PostalCode = postalCode;
        }

        public City(Guid key, string name, string postalCode, DateTime createdOn, DateTime? deletedAt)
        {
            Key = key;
            Name = name;
            PostalCode = postalCode;
            CreatedOn = createdOn;
            DeletedAt = deletedAt;
        }
    }
}
