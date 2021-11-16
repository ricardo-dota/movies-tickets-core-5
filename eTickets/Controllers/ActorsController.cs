using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsServices _service;

        public ActorsController(IActorsServices service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {

            ViewBag.actores = await _service.Getall();

        
            return View();
        }


        //get request Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);

            }
            await _service.Add(actor);
            return RedirectToAction(nameof(Index));
        }

        //GET: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {

            var actorDetails = await _service.GetById(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }



        //get request Actors/Create
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);

            }
            await _service.Update(id,actor);
            return RedirectToAction(nameof(Index));
        }


        //get request Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null) return View("NotFound");

            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
