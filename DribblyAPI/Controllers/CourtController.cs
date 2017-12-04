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
using DribblyAPI.Repositories;
using DribblyAPI.Models;
using DribblyAPI.Interfaces;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/court")]
    public class CourtController : BaseController
    {
        private List<Court> courts = new List<Court>();
        private FileRepository fileRepo;
        private CourtRepository repo;

        public CourtController()
        {
            fileRepo = new FileRepository();
            repo = new CourtRepository(new ApplicationDbContext());
        }

        [HttpGet]
        [Route("FindCourtsByName/{courtName}")]
        public IHttpActionResult FindCourtsByName(string courtName)
        {
            try
            {
                return Ok(repo.FindBy(c=> c.name.Contains(courtName)));
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to retrieve court list.";
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("delete/{courtId:int?}")]
        public IHttpActionResult Delete(int courtId = -1)
        {
            try
            {
                ApplicationDbContext DbContext = new ApplicationDbContext();

                var court = DbContext.Courts.Include(c => c.photos).FirstOrDefault(c => c.id == courtId);

                if(court != null)
                {
                    foreach (CourtPhoto photo in court.photos)
                    {
                        fileRepo.delete(photo.fileName, court.userId);
                    }

                    DbContext.Courts.Remove(court);
                    DbContext.SaveChanges();

                }

                return Ok();
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Failed to delete court. Please try again later.";
                Console.WriteLine(ex.Message);
                return InternalServerError(ex);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("setPrimaryPhoto")]
        public IHttpActionResult setPrimaryPhoto([FromUri] int courtId,[FromUri] string fileName)
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    Court court = db.Courts.SingleOrDefault(c => c.id == courtId);

                    if (court == null)
                    {
                        return BadRequest("Could not find court record.");
                    }

                    court.imagePath = fileName;

                    db.SaveChanges();

                    return Ok(court);

                }
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Unable to update court's primary photo. Please try again later.";
                return InternalServerError(ex);
            }
        }


        [Route("update")]
        public IHttpActionResult PUT(Court CourtDetails)
        {
            try
            {
                var result = repo.EditCourt(CourtDetails);

                return handleRepoMethodResult(result);
            }
            catch (Exception ex)
            {
                return handleRepoMethodException(ex, "Failed to update court details. Please try again later.");
            }
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Court CourtDetails)
        {
            try
            {
                repo.Add(CourtDetails);
                repo.Save();

                return Ok(CourtDetails);
            }
            catch (DribblyException ex)
            {
                ex.UserMessage = "Court registration failed. Please try again later.";
                return InternalServerError(ex);
            }
        }

        [Route("{courtId:int?}")]
        public IHttpActionResult GetCourtsFullDetails(int courtId = -1)
        {
            try
            {
                using (ApplicationDbContext DbContext = new ApplicationDbContext())
                {
                    RepoMethodResult result = repo.GetCourtFullDetails(courtId);
                    return handleRepoMethodResult(result);
                }
            }
            catch (Exception ex)
            {
                return handleRepoMethodException(ex, "Failed to retrieve court details");
            }

        }

        [HttpPost]
        [Route("~/api/courts")]
        public IHttpActionResult GetCourts(CourtSearchCriteria criteria)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    IEnumerable<Court> courts;

                    if (criteria != null)
                    {
                        var result = repo.Search(criteria);
                        return handleRepoMethodResult(result);
                    }
                    else
                    {
                        courts = repo.GetAll().OrderBy(x => x.dateRegistered).ToList<Court>();
                        return Ok(courts);
                    }
                }
            }
            catch (Exception ex)
            {
                return handleRepoMethodException(ex, "Failed to retrieve list of courts. Please try again later.");
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
                            photos = new List<CourtPhoto>() {
                                new CourtPhoto() {
                                    fileName="1.jpg"
                                },new CourtPhoto() {
                                    fileName="2.jpg"
                                },new CourtPhoto() {
                                    fileName="3.jpg"
                                }
                            }
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
                            photos = new List<CourtPhoto>() {
                                new CourtPhoto() {
                                    fileName="2.jpg"
                                },new CourtPhoto() {
                                    fileName="4.jpg"
                                },new CourtPhoto() {
                                    fileName="5.jpg"
                                }
                            }
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
                            photos = new List<CourtPhoto>() {
                                new CourtPhoto() {
                                    fileName="1.jpg"
                                },new CourtPhoto() {
                                    fileName="3.jpg"
                                },new CourtPhoto() {
                                    fileName="5.jpg"
                                }
                            }
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