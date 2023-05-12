using Microsoft.EntityFrameworkCore;
using Unistream.TestTask.Db.Domain;

namespace Unistream.TestTask.Db;

public class EntitiesContext : DbContext
{
    public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options) { }
    
    public DbSet<Entity> Entities { get; set; }
}