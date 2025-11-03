using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using JBF.Persistence.BD;

namespace JBarberFlowFront.Controllers
{
    public class MDisponibilidadsController : Controller
    {
        private readonly Context _context;

        public MDisponibilidadsController(Context context)
        {
            _context = context;
        }

        // GET: MDisponibilidads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disponibilidades.ToListAsync());
        }

        // GET: MDisponibilidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mDisponibilidad = await _context.Disponibilidades
                .FirstOrDefaultAsync(m => m.ID_Disponibilidad == id);
            if (mDisponibilidad == null)
            {
                return NotFound();
            }

            return View(mDisponibilidad);
        }

        // GET: MDisponibilidads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MDisponibilidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Disponibilidad,ID_Estilista,DiaSemana,HoraInicio,HoraFin")] MDisponibilidad mDisponibilidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mDisponibilidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mDisponibilidad);
        }

        // GET: MDisponibilidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mDisponibilidad = await _context.Disponibilidades.FindAsync(id);
            if (mDisponibilidad == null)
            {
                return NotFound();
            }
            return View(mDisponibilidad);
        }

        // POST: MDisponibilidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Disponibilidad,ID_Estilista,DiaSemana,HoraInicio,HoraFin")] MDisponibilidad mDisponibilidad)
        {
            if (id != mDisponibilidad.ID_Disponibilidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mDisponibilidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MDisponibilidadExists(mDisponibilidad.ID_Disponibilidad))
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
            return View(mDisponibilidad);
        }

        // GET: MDisponibilidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mDisponibilidad = await _context.Disponibilidades
                .FirstOrDefaultAsync(m => m.ID_Disponibilidad == id);
            if (mDisponibilidad == null)
            {
                return NotFound();
            }

            return View(mDisponibilidad);
        }

        // POST: MDisponibilidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mDisponibilidad = await _context.Disponibilidades.FindAsync(id);
            if (mDisponibilidad != null)
            {
                _context.Disponibilidades.Remove(mDisponibilidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MDisponibilidadExists(int id)
        {
            return _context.Disponibilidades.Any(e => e.ID_Disponibilidad == id);
        }
    }
}
