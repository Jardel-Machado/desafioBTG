using AutoMapper;
using DesafioBtg.Dominio.Uteis;

namespace DesafioBtg.Aplicacao.PaginacoesConsultas.Profiles;

public class PaginacoesConsultasProfile : Profile
{
    public PaginacoesConsultasProfile()
    {
        CreateMap(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
    }
}
