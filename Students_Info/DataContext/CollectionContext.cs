using Microsoft.EntityFrameworkCore;
using Students_Info.Models;

namespace Students_Info.DataContext
{
    public class CollectionContext:DbContext
    {
        public CollectionContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Student> students { get; set; }
    }
}
