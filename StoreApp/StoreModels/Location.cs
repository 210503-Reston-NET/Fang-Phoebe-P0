using System;

namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a store location
    /// </summary>
    public class Location
    {
        public Location(string locationName, Address address)
        {
            this.LocationName = locationName;
            this.Address = address;
        }
        public string LocationName { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"\t Location Name: {LocationName} \n\t Address: {Address.ToString()}";
        }
    }
}
