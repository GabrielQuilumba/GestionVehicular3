﻿using Microsoft.EntityFrameworkCore;

namespace GestionVehicular.Controllers;

[Authorize]
public class TiposMantenimientoController : Controller
{
    private readonly ApplicationDbContext _context;

    public TiposMantenimientoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TiposMantenimiento
    public async Task<IActionResult> Index()
    {
        return View(await _context.TiposMantenimiento.ToListAsync());
    }

    // GET: TiposMantenimiento/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.TiposMantenimiento == null)
        {
            return NotFound();
        }

        var tipoMantenimiento = await _context.TiposMantenimiento
            .FirstOrDefaultAsync(m => m.TipoMantenimientoId == id);
        if (tipoMantenimiento == null)
        {
            return NotFound();
        }

        return View(tipoMantenimiento);
    }

    // GET: TiposMantenimiento/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TiposMantenimiento/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TipoMantenimientoId,Nombre,EsActivo,FechaCreacion")] TipoMantenimiento tipoMantenimiento)
    {
        if (ModelState.IsValid)
        {
            tipoMantenimiento.EsActivo = true;
            tipoMantenimiento.FechaCreacion = DateTime.Now;
            _context.Add(tipoMantenimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tipoMantenimiento);
    }

    // GET: TiposMantenimiento/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.TiposMantenimiento == null)
        {
            return NotFound();
        }

        var tipoMantenimiento = await _context.TiposMantenimiento.FindAsync(id);
        if (tipoMantenimiento == null)
        {
            return NotFound();
        }
        return View(tipoMantenimiento);
    }

    // POST: TiposMantenimiento/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TipoMantenimientoId,Nombre,EsActivo,FechaCreacion")] TipoMantenimiento tipoMantenimiento)
    {
        if (id != tipoMantenimiento.TipoMantenimientoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tipoMantenimiento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMantenimientoExists(tipoMantenimiento.TipoMantenimientoId))
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
        return View(tipoMantenimiento);
    }

    // GET: TiposMantenimiento/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.TiposMantenimiento == null)
        {
            return NotFound();
        }

        var tipoMantenimiento = await _context.TiposMantenimiento
            .FirstOrDefaultAsync(m => m.TipoMantenimientoId == id);
        if (tipoMantenimiento == null)
        {
            return NotFound();
        }

        return View(tipoMantenimiento);
    }

    // POST: TiposMantenimiento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.TiposMantenimiento == null)
        {
            return Problem("Entity set 'ApplicationDbContext.TipoMantenimiento'  is null.");
        }
        var tipoMantenimiento = await _context.TiposMantenimiento.FindAsync(id);
        if (tipoMantenimiento != null)
        {
            _context.TiposMantenimiento.Remove(tipoMantenimiento);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TipoMantenimientoExists(int id)
    {
        return _context.TiposMantenimiento.Any(e => e.TipoMantenimientoId == id);
    }
}
