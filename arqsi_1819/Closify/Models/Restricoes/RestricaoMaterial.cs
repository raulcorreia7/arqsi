using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Closify.Models.Restricoes
{
    public class RestricaoMaterial : Restricao
    {
        private static readonly string NOME_R = "Restricao Material";

        public RestricaoMaterial() : base(NOME_R, TipoRestricao.Material) { }

        public override bool Validar(Produto pBase, Produto pParte)
        {
            Boolean exists = false;
            List<MaterialAcabamento> maParte = pParte.GetMateriaisAcabamentos();
            List<MaterialAcabamento> maBase = pBase.GetMateriaisAcabamentos();

            foreach (MaterialAcabamento mb in maBase)
            {
                foreach (MaterialAcabamento mp in maParte)
                {
                    //Tem de ser mesmo material e mesmo acabamento
                    if (mb.Material.Nome.Equals(mp.Material.Nome) && mb.Acabamento.Nome.Equals(mp.Acabamento.Nome))
                    {
                        exists = true;
                        break;
                    }
                }
            }
            return exists;
        }
    }
}
