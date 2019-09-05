
using Closify.Models;
using Closify.Models.DTO;

namespace Closify.Models.Restricoes
{

    public enum TipoRestricao
    {
        Generica = 0,
        Caber,//1
        Material,//2
        Obrigatoria,//3
        Opcional,//4
        Ocupacao//5
    };
    public class RestricaoFactory
    {

        public RestricaoFactory()
        {

        }

        /*  
             Fix Me:
             Raúl
             É preciso mesmo pedir o nome da restrição? É redundante
             Vai ter que se passar por parâmetro as opções de construção para a RestricaoDimensao

             Nota : A restrição opcional é simplesmentne remover a obrigatória numa agregacao
          */
        public static Restricao RestricaoGenerica()
        {
            return new RestricaoGenerica();
        }
        public static Restricao RestricaoCaber()
        {
            return new RestricaoCaber();
        }

        public static Restricao RestricaoMaterial()
        {
            return new RestricaoMaterial();
        }

        public static Restricao RestricaoObrigatoria()
        {
            return new RestricaoObrigatoria();
        }

        public static Restricao RestricaoOpcional()
        {
            return new RestricaoOpcional();
        }

        public static Restricao RestricaoOcupacao(RestricaoOcupacaoDTO dto)
        {
            return new RestricaoOcupacao(dto);
        }

        public static bool Valid(TipoRestricao tipo)
        {
            switch (tipo)
            {
                case TipoRestricao.Generica:
                    return true;
                case TipoRestricao.Caber:
                    return true;
                case TipoRestricao.Material:
                    return true;
                case TipoRestricao.Obrigatoria:
                    return true;
                case TipoRestricao.Opcional:
                    return true;
                case TipoRestricao.Ocupacao:
                    return true;
                default:
                    return false;
            }
        }

        public static Restricao Create(TipoRestricao tipo, NovaRestricaoDTO dto)
        {
            switch (tipo)
            {
                case TipoRestricao.Generica:
                    return RestricaoGenerica();
                case TipoRestricao.Caber:
                    return RestricaoCaber();
                case TipoRestricao.Material:
                    return RestricaoMaterial();
                case TipoRestricao.Obrigatoria:
                    return RestricaoObrigatoria();
                case TipoRestricao.Opcional:
                    return RestricaoOpcional();
                case TipoRestricao.Ocupacao:
                    return RestricaoOcupacao(dto.Ocupacao);
                default:
                    return null;
            }
        }

    }
}