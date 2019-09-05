using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Closify.Models.DTO;

namespace Closify.Models.Restricoes
{
    public class RestricaoOcupacao : Restricao
    {
        private static readonly string NOME_R = "Restricao Ocupacao";

        /*
            Todos estes valores são percentagens [0-1]
         */
        public float LarguraMin { get; set; }
        public float LarguraMax { get; set; }

        public float AlturaMin { get; set; }
        public float AlturaMax { get; set; }

        public float ComprimentoMax { get; set; }
        public float ComprimentoMin { get; set; }

        public RestricaoOcupacao() : base(NOME_R,TipoRestricao.Ocupacao){}
        public RestricaoOcupacao(RestricaoOcupacaoDTO dto) : base(NOME_R, TipoRestricao.Ocupacao)
        {
            parseDTO(dto);
        }

        private void parseDTO(RestricaoOcupacaoDTO dto)
        {
            LarguraMin = avaliar(dto.LarguraMin);
            LarguraMax = avaliar(dto.LarguraMax);

            AlturaMin = avaliar(dto.AlturaMin);
            AlturaMax = avaliar(dto.AlturaMax);

            ComprimentoMin = avaliar(dto.AlturaMin);
            ComprimentoMax = avaliar(dto.ComprimentoMax);
        }

        private float avaliar(float? valorDto)
        {
            if (valorDto.HasValue)
            {
                if (valorDto.Value >= 0.0f && valorDto <= 1.0f)
                    return valorDto.Value;
                else
                    throw new ArgumentException("Valor para Percentagem inválida:" + valorDto.Value);
            }
            else
                return 0.0f;
        }
        public override bool Validar(Produto pBase, Produto pParte)
        {
            Dimensao dbase = pBase.Dimensao;
            Dimensao dparte = pParte.Dimensao;
            return dbase.Ocupa(dparte,ComprimentoMin,ComprimentoMax,LarguraMin,LarguraMax,AlturaMin,AlturaMax);
        }
    }
}
