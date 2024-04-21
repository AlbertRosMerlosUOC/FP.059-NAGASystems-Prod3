using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapaModelo;

namespace FP._059_NAGASystems_Prod3.Data
{
    public class FP_059_NAGASystems_Prod3Context : DbContext
    {
        public FP_059_NAGASystems_Prod3Context (DbContextOptions<FP_059_NAGASystems_Prod3Context> options)
            : base(options)
        {
        }

        public DbSet<CapaModelo.Cliente> Cliente { get; set; } = default!;
        public DbSet<CapaModelo.Habitacion> Habitacion { get; set; } = default!;
        public DbSet<CapaModelo.Disponibilidad> Disponibilidad { get; set; } = default!;
        public DbSet<CapaModelo.Oferta> Oferta { get; set; } = default!;
        public DbSet<CapaModelo.Reserva> Reserva { get; set; } = default!;
        public DbSet<CapaModelo.Servicio> Servicio { get; set; } = default!;
        public DbSet<CapaModelo.TipoAlojamiento> TipoAlojamiento { get; set; } = default!;
        public DbSet<CapaModelo.TipoHabitacion> TipoHabitacion { get; set; } = default!;
        public DbSet<CapaModelo.TipoTemporada> TipoTemporada { get; set; } = default!;
        public DbSet<CapaModelo.ReservaServicio> ReservaServicio { get; set; } = default!;
    }
}
