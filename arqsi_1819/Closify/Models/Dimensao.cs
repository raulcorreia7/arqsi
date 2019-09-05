
using System;
using System.Collections.Generic;
using Closify.Models.DTO;

namespace Closify.Models
{

    public enum TipoValor { SemValor, Discreto, Continuo }

    public class Dimensao
    {
        public const int NR_CONTINUOS = 2;
        public const string CONTINUO = "continuo";
        public const string DISCRETO = "discreto";

        public const string SEM_VALOR = "semvalor";
        public int DimensaoID { get; set; }
        public TipoValor TipoValorComprimento { get; set; }
        public TipoValor TipoValorLargura { get; set; }
        public TipoValor TipoValorAltura { get; set; }
        public virtual List<ValorNumerico> Comprimento { get; set; } = new List<ValorNumerico>();

        public virtual List<ValorNumerico> Largura { get; set; } = new List<ValorNumerico>();

        public virtual List<ValorNumerico> Altura { get; set; } = new List<ValorNumerico>();

        public Dimensao() { }
        public Dimensao(DimensaoDTO dto)
        {
            this.TipoValorComprimento = InterpretarValores(dto.TipoComprimento, dto.Comprimento, Comprimento);
            this.TipoValorLargura = InterpretarValores(dto.TipoLargura, dto.Largura, Largura);
            this.TipoValorAltura = InterpretarValores(dto.TipoAltura, dto.Altura, Altura);

        }

        private TipoValor InterpretarValores(string tipo, List<double> valores, List<ValorNumerico> listaAlvo)
        {
            TipoValor retorno = TipoValor.SemValor;
            if (tipo.ToLower() == DISCRETO)
            {
                retorno = TipoValor.Discreto;
                foreach (double v in valores)
                {
                    listaAlvo.Add(new ValorNumerico(v));
                }
            }
            else
            {
                if (tipo.ToLower() == CONTINUO)
                {
                    retorno = TipoValor.Continuo;
                    if (valores.Count == NR_CONTINUOS)
                    {
                        foreach (double v in valores)
                        {
                            listaAlvo.Add(new ValorNumerico(v));
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Tipo de domínio para contínuos inválido");
                    }
                }
                else
                {
                    throw new ArgumentException("Tipo de valor inválido.");
                }
            }
            return retorno;
        }

        public bool Ocupa(Dimensao dparte, float comprimentoMin, float comprimentoMax, float larguraMin, float larguraMax, float alturaMin, float alturaMax)
        {
            bool ocupa = true;
            ocupa = CompreendidoValor(Altura, dparte.Altura, alturaMin, alturaMax);
            ocupa &= CompreendidoValor(Largura, dparte.Largura, larguraMin, larguraMax);
            ocupa &= CompreendidoValor(Comprimento, dparte.Comprimento, comprimentoMin, comprimentoMax);
            return ocupa;
        }

        private bool CompreendidoValor(List<ValorNumerico> pBase, List<ValorNumerico> pParte, float min, float max)
        {
            bool compreendido = false;
            const float epsilon = 0.0000001f;
            if (min < epsilon && max < epsilon)
            {
                return true;
            }
            //Só tenho limite máximo
            if (min < epsilon && max > epsilon)
            {
                foreach (ValorNumerico vBase in pBase)
                {
                    double OcupacaoMax = vBase.Valor * max;
                    foreach (ValorNumerico vParte in pParte)
                    {
                        if (vParte.Valor <= OcupacaoMax)
                        {
                            compreendido = true;
                        }
                    }
                }

            }
            else
            {
                //Só tenho limite minimo
                if (min > epsilon && max < epsilon)
                {
                    foreach (ValorNumerico vBase in pBase)
                    {
                        double OcupacaoMin = vBase.Valor * min;
                        foreach (ValorNumerico vParte in pParte)
                        {
                            if (vParte.Valor >= OcupacaoMin)
                            {
                                compreendido = true;
                                break;
                            }
                        }
                    }

                }
                //É porque tenho dois limites
                else
                {
                    foreach (ValorNumerico vBase in pBase)
                    {
                        double OcupacaoMax = vBase.Valor * max;
                        double OcupacaoMin = vBase.Valor * min;
                        foreach (ValorNumerico vParte in pParte)
                        {
                            if (vParte.Valor >= OcupacaoMin && vParte.Valor <= OcupacaoMax)
                            {
                                compreendido = true;
                                break;
                            }
                        }
                    }



                }

            }
            return compreendido;
        }

        private bool CompreendidoValorContinuo(List<ValorNumerico> pBase, List<ValorNumerico> pParte, float min, float max)
        {
            bool compreendido = true;

            return compreendido;

        }

        public static String converterTipoValor(TipoValor valor)
        {
            switch (valor)
            {
                case TipoValor.Continuo:
                    return CONTINUO;
                case TipoValor.Discreto:
                    return DISCRETO;
                default:
                    return SEM_VALOR;
            }

        }

        public bool Validar(Dimensao d)
        {
            bool valido = true;
            if(this.TipoValorAltura == TipoValor.Continuo)
            {
                valido &= ValidarContinuo(this.Altura, d.Altura);
            } else
            {
                valido &= ValidarDiscreto(this.Altura, d.Altura);
            }

            if (this.TipoValorLargura == TipoValor.Continuo)
            {
                valido &= ValidarContinuo(this.Largura, d.Largura);
            }
            else
            {
                valido &= ValidarDiscreto(this.Largura, d.Largura);
            }

            if (this.TipoValorComprimento == TipoValor.Continuo)
            {
                valido &= ValidarContinuo(this.Comprimento, d.Comprimento);
            }
            else
            {
                valido &= ValidarDiscreto(this.Comprimento, d.Comprimento);
            }
            return valido;
        }

        private bool ValidarContinuo(List<ValorNumerico> valsCatalogo, List<ValorNumerico> valsItem)
        {
            return valsItem[0].Valor >= valsCatalogo[0].Valor && valsItem[0].Valor <= valsCatalogo[valsCatalogo.Count - 1].Valor;
        }

        private bool ValidarDiscreto(List<ValorNumerico> valsCatalogo, List<ValorNumerico> valsItem)
        {
            return valsCatalogo.Contains(valsItem[0]);
        }
    }
}