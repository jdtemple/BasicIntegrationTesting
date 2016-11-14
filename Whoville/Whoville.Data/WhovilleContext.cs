using System.Data.Entity;
using Whoville.Data.Models;

namespace Whoville.Data
{
  public class WhovilleContext : DbContext
  {
    /// <summary>
    /// default project context
    /// </summary>
    public WhovilleContext() : base ("WhovilleContext")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    /// <summary>
    /// testing constructor, allows us to supply our own connection string
    /// </summary>
    /// <param name="connStr"></param>
    public WhovilleContext(string connStr) : base(connStr)
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Story> Stories { get; set; }
  }
}
