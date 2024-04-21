using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CapaModelo
{
    public class Habitacion
    {
        [Key]
        public int Numero { get; set; }
        public int TipoHabitacion { get; set; }
        public int Estado { get; set; }
    }
}
