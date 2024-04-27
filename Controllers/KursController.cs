using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {

        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }
        public IActionResult Create()
        {

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {

            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);

            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o =>o.OgrenciId == id );
            if (kurs == null)
            {
                return NotFound();

            }
            return View(kurs);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Kurs model)
        {
            if (id != model.KursId)
            {

                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!_context.Kurslar.Any(k => k.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}