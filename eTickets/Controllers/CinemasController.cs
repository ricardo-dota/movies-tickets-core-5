using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _servicio;

        public CinemasController(ICinemaService servicio)
        {
            _servicio = servicio;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.cinemas = await _servicio.Getall();
            return View();
        }

        //get crear
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Logo,Description")] Cinema cinema)
        {
            if ( !ModelState.IsValid)
            {
                return View(cinema);
            }
            await _servicio.Add(cinema);

            return RedirectToAction( nameof(Index));
        }

        //Detail

        public async Task<IActionResult> Details( int id )
        {
            var cinema = await _servicio.GetById(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _servicio.GetById(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,Description")] Cinema cinema)
        {
            if(!ModelState.IsValid ){

                return View(cinema);
            }

            await _servicio.Update(id,cinema);

            return RedirectToAction( nameof(Index) );
        }


        //Delete

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _servicio.GetById(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _servicio.GetById(id);
            if (cinema == null) return View("NotFound");

            await _servicio.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
