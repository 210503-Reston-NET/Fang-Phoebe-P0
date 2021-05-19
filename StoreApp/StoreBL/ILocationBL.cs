using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface ILocationBL
    {
        Location AddLocation(Location location);
        Location GetLocation(string branchName);
        List<Location> GetAllLocations();
    }
}