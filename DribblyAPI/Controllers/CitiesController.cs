using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DribblyAPI;
using DribblyAPI.Entities;
using DribblyAPI.Repositories;
using DribblyAPI.Models;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/Cities")]
    public class CitiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CityRepository _repo = new CityRepository(new ApplicationDbContext());
        private CountryRepository countryRepo = new CountryRepository(new ApplicationDbContext());

        // GET: api/Cities
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.cityId)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cities
        [Route("Add")]
        public IHttpActionResult PostCity(City city)
        {
            try
            {

                if (city.longName == "" || city.shortName == "")
                {
                    return BadRequest("city is not valid");
                }

                if (city.country.longName == "" || city.country.shortName == "")
                {
                    return BadRequest("city has invalid country details");
                }

                Country tmpCountry = countryRepo.GetAll().SingleOrDefault(c => c.longName == city.country.longName && c.shortName == city.country.shortName);
                if (tmpCountry != null)
                {
                    city.countryId = tmpCountry.countryId;
                    city.country = null;

                    if (_repo.Exists(city))
                    {
                        return BadRequest("city already exists");
                    }

                    _repo.Add(city);
                    _repo.Save();

                    city.country = tmpCountry;

                }
                else
                {
                    _repo.Add(city);
                    _repo.Save();
                }

                return Ok(city);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage= "An error occurred while saving city details.";
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.cityId == id) > 0;
        }
    }
}