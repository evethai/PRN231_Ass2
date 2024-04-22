using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Ass2PRN231.Data
{
    public class Ass2PRN231Context : DbContext
    {
        public Ass2PRN231Context (DbContextOptions<Ass2PRN231Context> options)
            : base(options)
        {
        }

        public DbSet<Repository.Entity.Role> Role { get; set; } = default!;
    }
}
