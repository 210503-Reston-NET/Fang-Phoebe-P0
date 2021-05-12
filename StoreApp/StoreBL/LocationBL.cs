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
        public List<Location> GetAllLocations()
        {
            //ToDo: get locations from DL
            return _repo.GetAllLocations();
        }
    }
}
