using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        public string DNI { get; set; }
        public int Habitacion { get; set; }
        public int TipoAlojamiento { get; set; }
        public int TipoTemporada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Factura { get; set; }
        public string Referido { get; set; }
        public int CheckIn { get; set; }
        public int Cancelado { get; set; }
        public int Oferta { get; set; }
    }
}
