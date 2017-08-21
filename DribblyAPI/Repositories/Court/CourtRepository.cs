using DribblyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DribblyAPI.Models;

namespace DribblyAPI.Repositories
{
    public class CourtRepository : BaseRepository<Court>
    {
        public CourtRepository(ApplicationDbContext _ctx) : base(_ctx)
        {

        }

        public Court GetCourtFullDetails(int courtId)
        {
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
            }
            return court;
        }

        public IEnumerable<Court> Search(CourtSearchCriteria criteria)
        {
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

            return courts.ToList<Court>();

        }

        public bool Exists(string userId)
        {
            return ctx.Set<Court>().Count(u => u.userId == userId) > 0;
        }
    }
}