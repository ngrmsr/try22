using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace try22.Models
{
    public class DemoContext : IdentityDbContext<AppUser>
    {
        public DemoContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
    }
}