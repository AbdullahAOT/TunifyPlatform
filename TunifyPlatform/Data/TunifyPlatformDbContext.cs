using Microsoft.EntityFrameworkCore;

namespace TunifyPlatform.Data
{
    public class TunifyPlatformDbContext : DbContext
    {
        public TunifyPlatformDbContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}
