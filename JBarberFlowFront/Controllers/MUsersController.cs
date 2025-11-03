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
    public class MUsersController : Controller
    {
        private readonly Context _context;

        public MUsersController(Context context)
        {
            _context = context;
        }

        // GET: MUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: MUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUsers = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.ID_User == id);
            if (mUsers == null)
            {
                return NotFound();
            }

            return View(mUsers);
        }

        // GET: MUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_User,NombreUsuario,PasswordHash,Correo,TipoUsuario,IsDeleted")] MUsers mUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mUsers);
        }

        // GET: MUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUsers = await _context.Usuarios.FindAsync(id);
            if (mUsers == null)
            {
                return NotFound();
            }
            return View(mUsers);
        }

        // POST: MUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_User,NombreUsuario,PasswordHash,Correo,TipoUsuario,IsDeleted")] MUsers mUsers)
        {
            if (id != mUsers.ID_User)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MUsersExists(mUsers.ID_User))
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
            return View(mUsers);
        }

        // GET: MUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mUsers = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.ID_User == id);
            if (mUsers == null)
            {
                return NotFound();
            }

            return View(mUsers);
        }

        // POST: MUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mUsers = await _context.Usuarios.FindAsync(id);
            if (mUsers != null)
            {
                _context.Usuarios.Remove(mUsers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MUsersExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID_User == id);
        }
    }
}
