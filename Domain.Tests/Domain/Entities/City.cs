using System;

namespace Domain.Entities
{
    public class City
    {
        public Guid Key { get; private set; }

        public string Name { get; private set; }

        public string PostalCode { get; private set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;

        public City(Guid key, string name, string postalCode)
        {
            Key = key;
            Name = name;
            PostalCode = postalCode;
        }
    }
}
