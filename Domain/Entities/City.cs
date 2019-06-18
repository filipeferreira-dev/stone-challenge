using System;

namespace Domain.Entities
{
    public class City
    {
        public Guid Key { get; private set; } = Guid.NewGuid();

        public string Name { get; private set; }

        public string PostalCode { get; private set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;

        public DateTime? DeletedAt { get; private set; }

        public bool IsDeleted => DeletedAt.HasValue;

        public City(string name, string postalCode)
        {
            Name = name;
            PostalCode = postalCode;
        }
    }
}
