using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using CapaModelo;

namespace HotelSol.Data
{
    public class FP_059_NAGASystems_Prod2Context : DbContext
    {
        public FP_059_NAGASystems_Prod2Context()
        : base("name=FP_059_NAGASystems_Prod2Context")
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<Disponibilidad> Disponibilidad { get; set; }
        public DbSet<Oferta> Oferta { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<TipoAlojamiento> TipoAlojamiento { get; set; }
        public DbSet<TipoHabitacion> TipoHabitacion { get; set; }
        public DbSet<TipoTemporada> TipoTemporada { get; set; }
        public DbSet<ReservaServicio> ReservaServicio { get; set; }
    }
}
