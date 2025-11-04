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
    public class MServiciosController : Controller
    {
        private readonly Context _context;

        public MServiciosController(Context context)
        {
            _context = context;
        }

        // GET: MServicios
        public async Task<IActionResult> Index()
        {

            var serviciosActivos = await _context.Servicios
                                        .Where(s => s.IsDeleted == false) 
                                        .ToListAsync();

            return View(serviciosActivos);
        }

        // GET: MServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mServicio = await _context.Servicios
                .Where(s => s.IsDeleted == false) 
                .FirstOrDefaultAsync(m => m.ID_Servicio == id);

            if (mServicio == null)
            {
                return NotFound(); 
            }

            return View(mServicio);
        }

        // GET: MServicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MServicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Servicio,Nombre,Descripcion,Precio,DuracionMinutos,IsDeleted")] MServicio mServicio)
        {
            if (ModelState.IsValid)
            {

                mServicio.IsDeleted = false; 
                _context.Add(mServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mServicio);
        }

        // GET: MServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mServicio = await _context.Servicios
                                 .Where(s => s.IsDeleted == false) 
                                 .FirstOrDefaultAsync(s => s.ID_Servicio == id);

            if (mServicio == null)
            {
                return NotFound();
            }
            return View(mServicio);
        }

        // POST: MServicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Servicio,Nombre,Descripcion,Precio,DuracionMinutos,IsDeleted")] MServicio mServicio)
        {
            if (id != mServicio.ID_Servicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    mServicio.IsDeleted = false; 

                    _context.Update(mServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MServicioExists(mServicio.ID_Servicio))
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
            return View(mServicio);
        }

        // GET: MServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mServicio = await _context.Servicios
                .Where(s => s.IsDeleted == false) 
                .FirstOrDefaultAsync(m => m.ID_Servicio == id);

            if (mServicio == null)
            {
                return NotFound();
            }

            return View(mServicio);
        }

        // POST: MServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var mServicio = await _context.Servicios.FindAsync(id);

            if (mServicio != null)
            {

                mServicio.IsDeleted = true;

                _context.Update(mServicio);

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ID_Servicio == id && e.IsDeleted == false);
        }
    }
}