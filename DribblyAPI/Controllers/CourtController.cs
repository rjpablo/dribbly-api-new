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
using DribblyAPI.Entities;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/court")]
    public class CourtController : ApiController
    {
        private List<Court> courts = new List<Court>();

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Court CourtDetails)
        {
            try
            {
                ApplicationDbContext DbContext = new ApplicationDbContext();

                DbContext.Courts.Add(CourtDetails);

                DbContext.SaveChanges();

                return Ok(CourtDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return InternalServerError();
            }
        }

        [Route("{courtId:int?}")]
        public IHttpActionResult GetCourtsFullDetails(int courtId = -1)
        {
            try
            {
                using (ApplicationDbContext DbContext = new ApplicationDbContext())
                {
                    DbContext.Configuration.LazyLoadingEnabled = false;
                    DbContext.Configuration.ProxyCreationEnabled = false;

                    DbContext.Configuration.LazyLoadingEnabled = false;
                    DbContext.Configuration.ProxyCreationEnabled = false;

                    if (courtId > -1)
                    {
                        try
                        {
                            //Court court = DbContext.Courts.Include(c => c.photos).Single(c => c.id == courtId);
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return BadRequest("Could not find requested court details.");
                        }
                    }
                    else
                    {
                        //List<Court> courts = DbContext.Courts.Include(c => c.photos).ToList<Court>();
                        return Ok(courts);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("~/api/courts/{useTestData:bool?}")]
        public IHttpActionResult GetCourts(bool useTestData = false)
        {
            try
            {
                if (!useTestData)
                {
                    using (ApplicationDbContext dbContext = new ApplicationDbContext())
                    {
                        List<Court> courts = dbContext.Courts.ToList<Court>();

                        return Ok(courts);
                    }
                }
                else
                {
                    string imageUploadPath = WebConfigurationManager.AppSettings["imageUploadPath"];

                    courts.Add(new Court()
                    {
                        id = 1,
                        name = "ABC Basketball Court",
                        description = "Just some long long long description.",
                        address = "#123 Kanto St. Sampaloc Manila",
                        contactNo = "0923-765-9876",
                        rate = 100.00,
                        imagePath = "1.jpg"
                    });

                    courts.Add(new Court()
                    {
                        id = 2,
                        name = "Monster Ballers Basketball Court",
                        description = "Just some long long long description.",
                        address = "#12 Ben Harrison St. Brgy. Pio del Pilar, Makati City, Metro Manila",
                        contactNo = "0923-765-9876",
                        rate = 100.00,
                        imagePath = "2.jpg"
                    });

                    courts.Add(new Court()
                    {
                        id = 3,
                        name = "Monster Ballers Basketball Court",
                        description = "Just some long long long description.",
                        address = "#12 Don Juan St. Brgy. Palanan, Makati City, Metro Manila",
                        contactNo = "0923-765-9854",
                        rate = 100.00,
                        imagePath = "3.jpg"
                    });

                    courts.Add(new Court()
                    {
                        id = 4,
                        name = "Monster Ballers Basketball Court",
                        description = "Just some long long long description.",
                        address = "#12 Ben Harrison St. Brgy. Pio del Pilar, Makati City, Metro Manila",
                        contactNo = "0923-765-9876",
                        rate = 100.00,
                        imagePath = "4.jpg"
                    });

                    courts.Add(new Court()
                    {
                        id = 5,
                        name = "Monster Ballers Basketball Court",
                        description = "Just some long long long description.",
                        address = "#12 Ben Harrison St. Brgy. Pio del Pilar, Makati City, Metro Manila",
                        contactNo = "0923-765-9876",
                        rate = 100.00,
                        imagePath = "5.jpg"
                    });

                    return Ok(courts);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("createTestData")]
        public IHttpActionResult createTestData()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Courts.Add(
                        new Court()
                        {
                            name = "",
                            rate = 250,
                            address = "#34 Pinaglabanan San Juan Manila",
                            contactNo = "0932-324-1234",
                            dateRegistered = DateTime.Now,
                            //photos = new List<CourtPhoto>() {
                            //new CourtPhoto() {
                            //    fileName="1.jpg"
                            //},new CourtPhoto() {
                            //    fileName="2.jpg"
                            //},new CourtPhoto() {
                            //    fileName="3.jpg"
                            //},
                            //}
                        }
                    );

                    db.Courts.Add(
                        new Court()
                        {
                            name = "",
                            rate = 250,
                            address = "#23 Pancho Villa San Juan Manila",
                            contactNo = "0932-334-1234",
                            dateRegistered = DateTime.Now,
                            //photos = new List<CourtPhoto>() {
                            //new CourtPhoto() {
                            //    fileName="2.jpg"
                            //},new CourtPhoto() {
                            //    fileName="4.jpg"
                            //},new CourtPhoto() {
                            //    fileName="5.jpg"
                            //},
                            //}
                        }
                    );

                    db.Courts.Add(
                        new Court()
                        {
                            name = "",
                            rate = 200,
                            address = "#87 Ben Harrison St. Pio del Pilar Makati City",
                            contactNo = "0932-334-1234",
                            dateRegistered = DateTime.Now,
                            //photos = new List<CourtPhoto>() {
                            //new CourtPhoto() {
                            //    fileName="1.jpg"
                            //},new CourtPhoto() {
                            //    fileName="3.jpg"
                            //},new CourtPhoto() {
                            //    fileName="5.jpg"
                            //},
                            //}
                        }
                    );

                    db.SaveChanges();

                    return Ok("Test generated successfully!");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return InternalServerError(ex);
            }

        }
    }
}