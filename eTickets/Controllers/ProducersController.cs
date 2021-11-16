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
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.producers = await _service.Getall();
            return View();
        }

        //GET producers/details/1

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View  ("NotFound");
            return View(producerDetails);

        }

        //public async get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {

            if (!ModelState.IsValid)
            {
                return View(producer);

            }
            await _service.Add(producer);
            return RedirectToAction(nameof(Index));
        }


        //public async get
        public async Task<IActionResult> Edit(int id)
        {
            var productor = await _service.GetById(id);
            if (productor == null) return View("NotFound");
            return View(productor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);

            }
            await _service.Update(id, producer);
            return RedirectToAction(nameof(Index));
        }



        //public async get
        public async Task<IActionResult> Delete(int id)
        {
            var productor = await _service.GetById(id);
            if (productor == null) return View("NotFound");
            return View(productor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productor = await _service.GetById(id);
            if (productor == null) return View("NotFound");

            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
