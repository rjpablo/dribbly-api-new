using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class CityRepository: BaseRepository<City>
    {
        private CountryRepository countryRepo;

        public CityRepository(ApplicationDbContext _ctx) : base(_ctx)
        {
            countryRepo = new CountryRepository(new ApplicationDbContext());
        }

        public City AddOrGet(City city)
        {
            try
            {
                Country tmpCountry = countryRepo.GetAll().SingleOrDefault(c => c.longName == city.country.longName && c.shortName == city.country.shortName);
                if (tmpCountry != null)
                {
                    city.countryId = tmpCountry.countryId;
                    City tempCity = GetCity(city.shortName, city.longName, city.countryId);

                    if (tempCity!=null)
                    {
                        city.cityId = tempCity.cityId;
                        return city;
                    }

                    city.country = null;
                }

                Add(city);
                Save();

                if (tmpCountry != null)
                {
                    city.country = tmpCountry;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return city;
        }

        public City GetCity(string shortName, string longName, int countryId)
        {
            City city = ctx.Set<City>().SingleOrDefault(c => c.shortName == shortName && c.longName == longName && c.countryId ==countryId);
            return city;
        }

        public bool Exists(City city)
        {
            return ctx.Set<City>().SingleOrDefault(c => c.shortName == city.shortName && c.longName == city.longName && c.countryId == city.countryId) != null;
        }
    }
}