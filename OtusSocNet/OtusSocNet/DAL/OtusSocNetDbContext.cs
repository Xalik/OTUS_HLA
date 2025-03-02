using Microsoft.EntityFrameworkCore;
using OtusSocNet.DAL.Entities;

namespace OtusSocNet.DAL;

public class OtusSocNetDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null;

    public OtusSocNetDbContext()
    {
    }

    public OtusSocNetDbContext(DbContextOptions<OtusSocNetDbContext> options) : base(options)
    {
    }
}