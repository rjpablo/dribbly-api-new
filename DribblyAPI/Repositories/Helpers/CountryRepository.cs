using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Repositories
{
    public class CountryRepository:BaseRepository<Country>
    {
        public CountryRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }

        public Country AddOrGet(Country country)
        {
            Country tmp = GetAll().SingleOrDefault(c => c.shortName == country.shortName);

            if (tmp == null)
            {
                Add(country);
                Save();
            }else
            {
                country = tmp;
            }
            return country;
        }
    }
}