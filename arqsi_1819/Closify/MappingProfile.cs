using Closify.Models;
using AutoMapper;
using Closify.Models.DTO;
using Closify.Models.Restricoes;

namespace Closify
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDTOID>().ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CategoriaID)).ReverseMap();
            CreateMap<Acabamento, AcabamentoDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ReverseMap();
            CreateMap<ProdutoMaterialAcabamento, MaterialAcabamentoDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.MaterialAcabamento.MaterialAcabamentoID))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.MaterialAcabamento.Material))
            .ForMember(dest => dest.Acabamento, opt => opt.MapFrom(src => src.MaterialAcabamento.Acabamento));

            CreateMap<Material, MaterialDTO>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ReverseMap();

            CreateMap<Produto, ProdutoDTO>()
                    .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
                    .ForMember(dest => dest.MaterialAcabamentoDTO, opt => opt.MapFrom(src => src.ProdutoMaterialAcabamentos))
                    .ForMember(dest => dest.DimensaoDTO, opt => opt.MapFrom(src => src.Dimensao));

            CreateMap<Categoria, CategoriaDTO>()
            .ForMember(dest => dest.SuperCategoria, opt => opt.MapFrom(src => src.SuperCatID))
            .ForMember(dest => dest.CategoriaDTOID, opt => opt.MapFrom(src => src.CategoriaID));

            CreateMap<Categoria, CategoriaDTOID>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CategoriaID))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ValorNumerico, double>().ConvertUsing(src => src.Valor);
            CreateMap<TipoValor, string>().ConvertUsing(src => Dimensao.converterTipoValor(src));

            CreateMap<Dimensao, DimensaoDTO>()
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Comprimento, opt => opt.MapFrom(src => src.Comprimento))
                .ForMember(dest => dest.Largura, opt => opt.MapFrom(src => src.Largura))
                .ForMember(dest => dest.TipoAltura, opt => opt.MapFrom(src => src.TipoValorAltura))
                .ForMember(dest => dest.TipoComprimento, opt => opt.MapFrom(src => src.TipoValorComprimento))
                .ForMember(dest => dest.TipoLargura, opt => opt.MapFrom(src => src.TipoValorLargura));

            CreateMap<Restricao, RestricaoDTO>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeRestricao))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.RestricaoID))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoRestricao));

            CreateMap<RestricaoOcupacao, RestricaoOcupacaoDTO>()
            .IncludeBase<Restricao, RestricaoDTO>()
            .ForMember(dest => dest.AlturaMin, opt => opt.MapFrom(src => src.AlturaMin))
            .ForMember(dest => dest.AlturaMax, opt => opt.MapFrom(src => src.AlturaMax))
            .ForMember(dest => dest.ComprimentoMin, opt => opt.MapFrom(src => src.ComprimentoMin))
            .ForMember(dest => dest.ComprimentoMax, opt => opt.MapFrom(src => src.ComprimentoMax))
            .ForMember(dest => dest.LarguraMin, opt => opt.MapFrom(src => src.LarguraMin))
            .ForMember(dest => dest.LarguraMax, opt => opt.MapFrom(src => src.LarguraMax));

            CreateMap<Agregacao, AgregacaoDTO>()
            .ForMember(dest => dest.ProdutoBase, opt => opt.MapFrom(src => src.Base))
            .ForMember(dest => dest.ProdutoParte, opt => opt.MapFrom(src => src.Parte))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.AgregacaoID));

            CreateMap<MaterialAcabamento, MaterialAcabamentoDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.MaterialAcabamentoID))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
            .ForMember(dest => dest.Acabamento, opt => opt.MapFrom(src => src.Acabamento))
            .ReverseMap();
        }
    }
}