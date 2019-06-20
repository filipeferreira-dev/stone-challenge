using System;

namespace Domain.Entities
{
    public class City : Entity
    {
        public string Name { get; private set; }

        public string PostalCode { get; private set; }

        [Obsolete("Creted for tests purpose", true)]
        public City() { }

        public City(string name, string postalCode)
        {
            Name = name;
            PostalCode = postalCode;
        }

        public City(Guid key, string name, string postalCode, DateTime createdOn) : this(name, postalCode)
        {
            Key = key;
            CreatedOn = createdOn;
        }
    }
}
