using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JBF.Persistence.BD;
using ReservaCitasBackend.Modelos;

namespace JBarberFlowFront.Controllers
{
    public class MCitasController : Controller
    {
        private readonly Context _context;

        public MCitasController(Context context)
        {
            _context = context;
        }

        // GET: MCitas
        public async Task<IActionResult> Index()
        {

            var citasActivas = await _context.Citas
                                    .Where(c => c.IsCanceled == false)
                                    .ToListAsync();

            return View(citasActivas);
        }

        // GET: MCitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCitas = await _context.Citas
                .Where(c => c.IsCanceled == false) 
                .FirstOrDefaultAsync(m => m.ID_Citas == id);

            if (mCitas == null)
            {
                return NotFound();
            }

            return View(mCitas);
        }

        // GET: MCitas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MCitas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Citas,ID_Cliente,ID_Estilista,ID_Servicio,FechaInicio,FechaFin,IsCanceled")] MCitas mCitas)
        {
            if (ModelState.IsValid)
            {
                
                mCitas.IsCanceled = false; 
                _context.Add(mCitas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mCitas);
        }

        // GET: MCitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCitas = await _context.Citas
                                .Where(c => c.IsCanceled == false)
                                .FirstOrDefaultAsync(c => c.ID_Citas == id);

            if (mCitas == null)
            {
                return NotFound();
            }
            return View(mCitas);
        }

        // POST: MCitas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Citas,ID_Cliente,ID_Estilista,ID_Servicio,FechaInicio,FechaFin,IsCanceled")] MCitas mCitas)
        {
            if (id != mCitas.ID_Citas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    mCitas.IsCanceled = false;

                    _context.Update(mCitas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MCitasExists(mCitas.ID_Citas))
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
            return View(mCitas);
        }

        // GET: MCitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCitas = await _context.Citas
                .Where(c => c.IsCanceled == false)
                .FirstOrDefaultAsync(m => m.ID_Citas == id);

            if (mCitas == null)
            {
                return NotFound();
            }

            return View(mCitas);
        }

        // POST: MCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mCitas = await _context.Citas.FindAsync(id);
            if (mCitas != null)
            {

                mCitas.IsCanceled = true;

                _context.Update(mCitas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MCitasExists(int id)
        {

            return _context.Citas.Any(e => e.ID_Citas == id && e.IsCanceled == false);
        }
    }
}