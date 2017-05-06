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
        public CourtRepository(DbContext _ctx) : base(_ctx)
        {

        }

        public Court GetCourtFullDetails(int courtId)
        {
            return ctx.Set<Court>().Include(x => x.photos).FirstOrDefault(x => x.id == courtId);
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
    }
}