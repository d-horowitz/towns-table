﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using towns.Data;
using towns.Models;

namespace towns.Controllers
{
    public class TownsController : Controller
    {
        private readonly townsContext _context;

        public TownsController(townsContext context)
        {
            _context = context;
        }

        // GET: Towns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Town.ToListAsync());
        }

        // GET: Towns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Town
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // GET: Towns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Towns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Town town)
        {
            if (ModelState.IsValid)
            {
                _context.Add(town);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException!.Message.Contains("duplicate")) {
                        @ViewData["ErrorMessage"] = "This town already exists. Perhaps you mistyped the name of the town?";
                    }
                    return View(town);
                }
            }
            return View(town);
        }

        // GET: Towns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Town.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }
            return View(town);
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Town town)
        {
            if (id != town.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(town);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TownExists(town.Id))
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
            return View(town);
        }

        // GET: Towns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Town
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var town = await _context.Town.FindAsync(id);
            if (town != null)
            {
                _context.Town.Remove(town);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownExists(int id)
        {
            return _context.Town.Any(e => e.Id == id);
        }
    }
}
