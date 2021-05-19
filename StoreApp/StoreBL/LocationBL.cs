using System;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
namespace StoreBL
{
    public class LocationBL : ILocationBL
    {
        private IRepository _repo;
        public LocationBL(IRepository repo)
        {
            this._repo = repo;
        }

        public Location AddLocation(Location location)
        {
            if(_repo.GetLocation(location.Name) != null)
            {
                throw new Exception("Branch already exists");
            }
            return _repo.AddLocation(location);
        }

        public List<Location> GetAllLocations()
        {
            //ToDo: get locations from DL
            return _repo.GetAllLocations();
        }

        public Location GetLocation(string branchName)
        {
            return _repo.GetLocation(branchName);
        }
    }
}
