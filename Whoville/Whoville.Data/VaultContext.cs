using System.Data.Entity;
using Whoville.Data.Models;

namespace Whoville.Data
{
  public class VaultContext : DbContext
  {
    /// <summary>
    /// default project context
    /// </summary>
    public VaultContext() : base ("VaultContext")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    /// <summary>
    /// testing constructor, allows us to supply our own connection string
    /// </summary>
    /// <param name="connStr"></param>
    public VaultContext(string connStr) : base(connStr)
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<File> Files { get; set; }
  }
}
