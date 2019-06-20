using System;

namespace Domain.Entities
{
    public class CityTemperature : Entity
    {
        public int Temperature { get; private set; }

        public Guid CityKey { get; private set; }

        public CityTemperature(Guid cityKey, int temperature)
        {
            Temperature = temperature;
            CityKey = cityKey;
        }
    }
}
