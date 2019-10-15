using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {     
    [HttpGet("/animals")]
    public ActionResult Index()
    {
        List<Animal> allAnimals = Animal.GetAll();
        return View(allAnimals);
    }

    [HttpGet("/animals/new")]
    public ActionResult New()
    {
   
      return View();
    }

  }
}