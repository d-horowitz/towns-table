using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using towns.Models;

namespace towns.Data
{
    public class townsContext : DbContext
    {
        public townsContext (DbContextOptions<townsContext> options)
            : base(options)
        {
        }

        public DbSet<towns.Models.Town> Town { get; set; } = default!;
    }
}
