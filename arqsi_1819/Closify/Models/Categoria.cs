using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        
        public int? SuperCatID { get; set; }

        public string Nome { get; set; }

        [ForeignKey("SuperCatID")]
        public virtual Categoria SuperCategoria { get; set; }
        public virtual IList<Categoria> SubCategorias { get; set; } = new List<Categoria>();

        public Boolean AdicionarSubCategoria(Categoria subCat)
        {
            if (SubCategorias.Contains(subCat))
            {
                return false;
            }
            else
            {
                subCat.SuperCatID=CategoriaID;
                SubCategorias.Add(subCat);
                return true;
            }
        }
    }

}
