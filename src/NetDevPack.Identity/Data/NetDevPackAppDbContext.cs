using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetDevPack.Identity.Data
{
    public class NetDevPackAppDbContext : IdentityDbContext
    {
        public NetDevPackAppDbContext(DbContextOptions<NetDevPackAppDbContext> options) : base(options) { }
    }
}