using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaModelo;
using FP._059_NAGASystems_Prod3.Data;

namespace FP._059_NAGASystems_Prod3
{
    public class ReservaController : Controller
    {
        private readonly FP_059_NAGASystems_Prod3Context _context;

        public ReservaController(FP_059_NAGASystems_Prod3Context context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserva.ToListAsync());
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/CheckOut/5
        [HttpGet]
        public async Task<IActionResult> CheckOut(int id)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Habitacion)
                    .ThenInclude(h => h.TipoHabitacion)
                .Include(r => r.TipoAlojamiento)
                .Include(r => r.TipoTemporada)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null || reserva.Cancelado == 1)
            {
                return NotFound("Reserva no encontrada o ya está cancelada.");
            }

            // Detalles de cálculo
            double precioHabitacion = reserva.Habitacion.TipoHabitacion.Precio;
            double precioTipoAlojamiento = reserva.TipoAlojamiento.Precio;
            double coeficiente = reserva.TipoTemporada.Coeficiente / 100.0; // Convertir a decimal
            int totalDias = (reserva.FechaFin - reserva.FechaInicio).Days;
            if (totalDias == 0) totalDias = 1;

            double costoBase = (precioHabitacion + precioTipoAlojamiento) * coeficiente;
            double totalCosto = costoBase * totalDias;

            // Pasar a la vista
            ViewBag.PrecioHabitacion = precioHabitacion;
            ViewBag.PrecioTipoAlojamiento = precioTipoAlojamiento;
            ViewBag.Coeficiente = reserva.TipoTemporada.Coeficiente; // Mostrar como porcentaje
            ViewBag.TotalDias = totalDias;
            ViewBag.TotalCosto = totalCosto;

            return View(reserva);
        }

        // POST: Reserva/ConfirmCheckOut/5
        [HttpPost, ActionName("ConfirmCheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCheckOut(int id)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Habitacion)
                    .ThenInclude(h => h.TipoHabitacion)
                .Include(r => r.TipoAlojamiento)
                .Include(r => r.TipoTemporada)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }
            // Calcula el total de días.
            int totalDias = (reserva.FechaFin - reserva.FechaInicio).Days;
            if (totalDias == 0) totalDias = 1;  // Asegura que al menos un día es cobrado

            // Convertir el coeficiente de porcentaje a formato decimal
            double coeficienteDecimal = reserva.TipoTemporada.Coeficiente / 100.0;

            // Calcular el costo base aplicando el coeficiente
            double costoBase = (reserva.TipoAlojamiento.Precio + reserva.Habitacion.TipoHabitacion.Precio) * coeficienteDecimal;

            // Calcular el total multiplicando por los días
            double totalCosto = costoBase * totalDias;

            // Actualizar la factura de la reserva
            reserva.Factura = (decimal)totalCosto;

            // Guarda los cambios en la base de datos.
            _context.Update(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = reserva.Id });
        }


        // GET: Reserva/Create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateSelectLists();  // Usar el método auxiliar para configurar los SelectList
            return View(new Reserva { Factura = 0, Cancelado = 0, CheckIn = 0 }); // Establece valores predeterminados
        }

        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DNI,HabitacionId,TipoAlojamientoId,TipoTemporadaId,FechaInicio,FechaFin,Factura,Referido,CheckIn,Cancelado,OfertaId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(reserva);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al intentar crear la reserva: {ex.Message}");
                }
            }

            // Recargar SelectList en caso de modelo no válido, manteniendo selecciones del usuario
            PopulateSelectLists(reserva.TipoAlojamientoId, reserva.TipoTemporadaId, reserva.OfertaId, reserva.HabitacionId);
            return View(reserva);
        }


        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            PopulateSelectLists(reserva.TipoAlojamientoId, reserva.TipoTemporadaId, reserva.OfertaId, reserva.HabitacionId);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DNI,HabitacionId,TipoAlojamientoId,TipoTemporadaId,FechaInicio,FechaFin,Referido,CheckIn,Cancelado,OfertaId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateSelectLists(reserva.TipoAlojamientoId, reserva.TipoTemporadaId, reserva.OfertaId, reserva.HabitacionId);
            return View(reserva);
        }

        private void PopulateSelectLists(int? tipoAlojamientoId = null, int? tipoTemporadaId = null, int? ofertaId = null, int? habitacionId = null)
        {
            ViewBag.TipoAlojamientoId = new SelectList(_context.TipoAlojamiento, "Id", "Descripcion", tipoAlojamientoId);
            ViewBag.TipoTemporadaId = new SelectList(_context.TipoTemporada, "Id", "Descripcion", tipoTemporadaId);
            ViewBag.OfertaId = new SelectList(_context.Oferta, "Id", "Descripcion", ofertaId);
            ViewBag.HabitacionId = new SelectList(_context.Habitacion, "Id", "Nombre", habitacionId);
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
        // POST: Reserva/CheckIn/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            if (reserva.CheckIn == 0)
            {
                reserva.CheckIn = 1;
                _context.Update(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ListaPendientesCheckIns()
        {
            var currentDate = DateTime.Now;
            var reservasPendientes = await _context.Reserva
                .Where(r => r.FechaInicio < currentDate && r.CheckIn == 0 && r.Cancelado != 1)
                .ToListAsync();

            return View(reservasPendientes);
        }

        // POST: Reserva/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Cancelado = 1;  // Anula la reserva
            _context.Update(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListaPendientesCheckIns));
        }
        // POST: Reserva/CancelReservation/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Cancelado = 1;  // Marca la reserva como cancelada
            _context.Update(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
