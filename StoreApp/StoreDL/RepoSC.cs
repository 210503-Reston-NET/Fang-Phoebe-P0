using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreDL
{
    public class RepoSC : IRepository
    {
        public List<Location> GetAllLocations()
        {
            //ToDo: get locations from storage
            return SCStorage.Locations;
        }
    }
}
