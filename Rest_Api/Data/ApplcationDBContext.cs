using Microsoft.EntityFrameworkCore;
using REST_API.Models;
using System.Drawing;

namespace REST_API.Data
{
    public class ApplcationDBContext : DbContext
    {
        public ApplcationDBContext(DbContextOptions<ApplcationDBContext> options): base(options)
        {

        }

        public DbSet<ProducedOperation> ProducedOperations { get; set; }
    }
}
