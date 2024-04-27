using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace efcoreApp.Controllers
{
    
    public class OgrenciController : Controller
    {

        private readonly DataContext _context;

        public OgrenciController(DataContext context)
        {
                _context = context;
        }

      public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }
public IActionResult Create(){


    return View();
}

     public async Task<IActionResult> Edit( int? id)
    {

        if (id == null) {
            return NotFound();
        }
         var ogr = await _context.Ogrenciler.FindAsync(id);
         
         //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o =>o.OgrenciId == id );
         if (ogr == null)
         {
            return NotFound();

         }
         return View(ogr);
         
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Edit (int id,Ogrenci model){


        if (id != model.OgrenciId)
        {
            return NotFound();
        }
        if(ModelState.IsValid){
            try {
_context.Update(model);
await _context.SaveChangesAsync();
            }
            catch (Exception) {
                //herhangi bir kayıt var mı yok mu kontrolü 
        if ( !_context.Ogrenciler.Any(o=>o.OgrenciId == model.OgrenciId))
{
    return NotFound();
} else{
    throw;
}
            }
return RedirectToAction("Index");
        }
        return View(model);

    }
       [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)

        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

       
    }
   
}