using DesafioBtg.Dominio.Uteis.Enumeradores;
using System.Diagnostics.CodeAnalysis;

namespace DesafioBtg.Dominio.Uteis;

[ExcludeFromCodeCoverage]

public class PaginacaoFiltro
{
    private int qt;

    public int Qt
    {
        get
        {
            return qt;
        }
        set
        {
            qt = ((value < 100) ? value : 100);
        }
    }

    public int Pg { get; set; }

    public TipoOrdenacaoEnum TpOrd { get; set; }

    public string CpOrd { get; set; }

    public PaginacaoFiltro(string cpOrd, TipoOrdenacaoEnum tpOrd)
    {
        Qt = 10;
        Pg = 1;
        CpOrd = cpOrd;
        TpOrd = tpOrd;
    }

    public string ObterSqlOrdenacao()
    {
        if (string.IsNullOrWhiteSpace(CpOrd))
            return string.Empty;        

        return $" ORDER BY {CpOrd} {TpOrd}";
    }
}

