using System.Collections.Generic;
using StoreModels;
namespace StoreDL
{
    public class SCStorage
    {
        // A static method to get a list of hard coded locations
        static Address KirklandStore = new Address("219 Kirkland Ave", "#102,", "Kirkland", "WA", 98033);
        static Address BellevueStore = new Address("909 112th Ave NE", "#106", "Bellevue", "WA", 98004);
        public static List<Location> Locations = new List<Location>() 
        {
            new Location("Kirkland Store", KirklandStore),
            new Location("Bellevue Store", BellevueStore)
        };
    }
}