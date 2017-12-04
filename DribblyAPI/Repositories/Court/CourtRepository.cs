using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DribblyAPI.Models;
using NLog;
using Newtonsoft.Json;

namespace DribblyAPI.Repositories
{
    public class CourtRepository : BaseRepository<Court>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CourtRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }

        public RepoMethodResult GetCourtFullDetails(int courtId)
        {
            try
            {
                logger.Info(FormatLogMessage("Requested details for court with court id = " + courtId));

                RepoMethodResult result = new RepoMethodResult();
                Court court = ctx.Set<Court>().Include(x => x.photos).Include(x => x.owner).FirstOrDefault(x => x.id == courtId);
                if (court != null && court.owner != null)
                {
                    //remove sensitive data
                    court.owner.Claims.Clear();
                    court.owner.PasswordHash = "";
                    court.owner.SecurityStamp = "";
                    court.owner.Logins.Clear();
                    court.owner.PhoneNumber = "";
                    court.owner.Email = "";
                    court.owner.Roles.Clear();

                    result.Content = court;

                }
                else
                {
                    result.ResultType = RepoMethodResultType.Failed;
                    result.UserMessage = "Court details not found";
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, FormatLogMessage("Failed to get court details, court id = " + courtId));
                throw ex;
            }
            
        }

        public RepoMethodResult Search(CourtSearchCriteria criteria)
        {
            try
            {
                RepoMethodResult result = new RepoMethodResult();
                IQueryable<Court> courts = ctx.Set<Court>();

                if (criteria.courtName != null && criteria.courtName.Trim() != "")
                {
                    courts = courts.Where(c => c.name.Contains(criteria.courtName));
                }

                if (criteria.address != null && criteria.address.Trim() != "")
                {
                    courts = courts.Where(c => c.address.Contains(criteria.address));
                }

                courts = courts.Where(c => c.rate >= criteria.rangeMin && c.rate <= criteria.rangeMax);
                result.Content = courts.ToList<Court>();
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, FormatLogMessage("Failed to retrieve seach result, criteria: " + JsonConvert.SerializeObject(criteria)));
                throw ex;
            }           

        }

        public bool Exists(int courtId)
        {
            try
            {
                return ctx.Set<Court>().Count(u => u.id == courtId) > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RepoMethodResult EditCourt(Court entity)
        {
            try
            {
                RepoMethodResult result = new RepoMethodResult();
                if (Exists(entity.id))
                {
                    Edit(entity);
                    Save();
                }
                else
                {
                    result.SetResult(RepoMethodResultType.Failed, "Court details not found");
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, FormatLogMessage("Failed to update court, court: " + JsonConvert.SerializeObject(entity)));
                throw ex;
            }
            
        }
    }
}