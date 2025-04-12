using System.ComponentModel;

namespace DesafioBtg.Dominio.Uteis.Enumeradores;

public enum DiaSemanaEnum
{
    [Description("Domingo")]
    Domingo = 0,

    [Description("Segunda-feira")]
    SegundaFeira = 1,

    [Description("Terça-feira")]
    TercaFeira = 2,

    [Description("Quarta-feira")]
    QuartaFeira = 3,

    [Description("Quinta-feira")]
    QuintaFeira = 4,

    [Description("Sexta-feira")]
    SextaFeira = 5,

    [Description("Sábado")]
    Sabado = 6
}
