using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = _context.SalesRecord.Select(x => x);
            if (minDate.HasValue)
                result = result.Where(sr => sr.Date >= minDate);
            if (maxDate.HasValue)
                result = result.Where(sr => sr.Date <= maxDate);

            return await result
                         .Include(sr => sr.Seller)
                         .Include(sr => sr.Seller.Department)
                         .OrderByDescending(sr => sr.Date)
                         .ToListAsync();
        }
    }
}
