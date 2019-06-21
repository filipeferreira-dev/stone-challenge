using System;

namespace Domain.Entities
{
    public class CityTemperature : Entity
    {
        public int Temperature { get; private set; }

        public Guid CityKey { get; private set; }

        [Obsolete("Creted for tests purpose", true)]
        public CityTemperature() { }

        public CityTemperature(Guid cityKey, int temperature)
        {
            Temperature = temperature;
            CityKey = cityKey;
        }

        public CityTemperature(Guid key, Guid cityKey, int temperature, DateTime createdOn) : this(cityKey, temperature)
        {
            Key = key;
            CreatedOn = createdOn;
        }
    }
}
