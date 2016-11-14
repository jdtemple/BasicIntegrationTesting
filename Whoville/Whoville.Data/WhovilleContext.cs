using System.Data.Entity;
using Whoville.Data.Models;

namespace Whoville.Data
{
  public class WhovilleContext : DbContext
  {
    public WhovilleContext() : base ("WhovilleContext")
    {

    }

    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Story> Stories { get; set; }
  }
}
