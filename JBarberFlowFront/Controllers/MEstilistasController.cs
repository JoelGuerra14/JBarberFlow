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
    public class MEstilistasController : Controller
    {
        private readonly Context _context;

        public MEstilistasController(Context context)
        {
            _context = context;
        }

        // GET: MEstilistas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estilistas.ToListAsync());
        }

        // GET: MEstilistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEstilista = await _context.Estilistas
                .FirstOrDefaultAsync(m => m.ID_Estilista == id);
            if (mEstilista == null)
            {
                return NotFound();
            }

            return View(mEstilista);
        }

        // GET: MEstilistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MEstilistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Estilista,Nombre,Email,ID_Servicio,IsDeleted")] MEstilista mEstilista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mEstilista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mEstilista);
        }

        // GET: MEstilistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEstilista = await _context.Estilistas.FindAsync(id);
            if (mEstilista == null)
            {
                return NotFound();
            }
            return View(mEstilista);
        }

        // POST: MEstilistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Estilista,Nombre,Email,ID_Servicio,IsDeleted")] MEstilista mEstilista)
        {
            if (id != mEstilista.ID_Estilista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEstilista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEstilistaExists(mEstilista.ID_Estilista))
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
            return View(mEstilista);
        }

        // GET: MEstilistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEstilista = await _context.Estilistas
                .FirstOrDefaultAsync(m => m.ID_Estilista == id);
            if (mEstilista == null)
            {
                return NotFound();
            }

            return View(mEstilista);
        }

        // POST: MEstilistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mEstilista = await _context.Estilistas.FindAsync(id);
            if (mEstilista != null)
            {
                _context.Estilistas.Remove(mEstilista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MEstilistaExists(int id)
        {
            return _context.Estilistas.Any(e => e.ID_Estilista == id);
        }
    }
}
