using System.Data.Entity;
using CapaModelo;
using HotelSol.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class HabitacionServicio
{
    private readonly FP_059_NAGASystems_Prod2Context _context;

    public HabitacionServicio(FP_059_NAGASystems_Prod2Context context)
    {
        _context = context;
    }

    public async Task<List<Habitacion>> ObtenerHabitacionesDisponibles(DateTime fechaInicio, DateTime fechaFin)
    {
        var habitacionesOcupadas = await _context.Reserva
            .Where(r => r.FechaInicio < fechaFin && r.FechaFin > fechaInicio && r.Cancelado == 0)
            .Select(r => r.Habitacion)
            .Distinct()
            .ToListAsync();

        var habitacionesDisponibles = await _context.Habitacion
            .Where(h => !habitacionesOcupadas.Contains(h.Numero))
            .ToListAsync();

        return habitacionesDisponibles;
    }
}
