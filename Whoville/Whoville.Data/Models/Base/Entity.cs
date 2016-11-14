using System.ComponentModel.DataAnnotations;

namespace Whoville.Data.Models.Base
{
  public class Entity
  {
    [Key]
    public virtual int Id { get; set; }
  }
}
