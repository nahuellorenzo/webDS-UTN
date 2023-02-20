using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query;
using web.Models;

namespace web.ViewModels
{
    public class ContratoViewModel
    {
        public string Filtro { get; set; }
        public IIncludableQueryable<Contrato, Proveedor> Contratos { get; set; }
    }
    public class ContratoCrearViewModel
    {
        public Models.Contrato Contrato { get; set; }
        public List<Indicador> Indicadores { get; set; }
        public SelectList IndicadoresNombres { get; set; }

    }

    public class Indicador
    {
        public string Nombre { get; set; }
        public int Valor { get; set; }
    }

    public class ValorIndicadorCalidadContratoViewModel
    {
        public IndicadorCalidadesContrato? IndicadoresCalidadContrato { get; set; }
    }
}
