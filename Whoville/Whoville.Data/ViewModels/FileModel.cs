using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whoville.Data.ViewModels
{
  public class FileModel
  {
    public string Name { get; set; }

    public int FoldertId { get; set; }

    public FolderModel Folder { get; set; }
  }
}
