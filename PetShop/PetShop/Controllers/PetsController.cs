using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;
using PetShop.Models.ViewModels;
using PetShop.Services;

namespace PetShop.Controllers
{
    public class PetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private PetService petService;

        public PetsController(ApplicationDbContext db, PetService petService)
        {
            this.db = db;
            this.petService = petService;
        }

        private void Init()
        {
            petService = new PetService(db);
        }

        // GET: Pets
        [AllowAnonymous]
        public ActionResult Index()
        {
            Init();
            IEnumerable<string> breeds = petService.GetBreeds();
            BreedViewModel bvm = new BreedViewModel(petService, breeds);
            return View(bvm);
        }

        [Authorize]
        public ActionResult MyPets()
        {
            return View("MyPets", db.Pets.Include(x => x.Owner)
                        .Where(y => y.Owner.UserName == User.Identity.Name)
                        .ToList());
        }

        // GET: GetPetsByBreed
        public ActionResult GetPetsByBreed(string breed)
        {
            Init();
            return PartialView("_PetListPartial", petService.GetPetsByBreed(breed));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Adopt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adopt(Pet pet)
        {
            var user = db.Users.Include(x => x.Pets)
                .Where(x => x.UserName == User.Identity.Name)
                .First();
            var pet1 = db.Pets.Find(pet.Id);

            var claimUser = (ClaimsPrincipal)User;
            var dateOfBirth = Convert.ToDateTime(
                claimUser.Claims.Where(claim => claim.Type == ClaimTypes.DateOfBirth)
                .First()
                .Value);
            var age = DateTime.Now.Subtract(dateOfBirth);

            if (age.Days >= 365 * 18)
            {
                pet1.Owner = user;
                user.Pets.Add(pet1);
                db.SaveChanges();
            }

            return RedirectToAction("MyPets");
        }

        // GET: Pets/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Init();
            CreatePetViewModel cpvm = new CreatePetViewModel
            {
                DropDownBreed = petService.GetBreedSelectListItems(null)
            };
            return View(cpvm);
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,isMale,Breed,Age,isFixed")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            Init();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = petService.GetPetById(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            EditPetViewModel epvm = new EditPetViewModel
            {
                DropDownBreed = petService.GetBreedSelectListItems(pet.Breed),
                Id = pet.Id,
                Name = pet.Name,
                isMale = pet.isMale,
                Breed = pet.Breed,
                Age = pet.Age
            };
            return View(epvm);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public JsonResult Edit([Bind(Include = "Id,Name,isMale,Breed,Age,isFixed")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { text = "Success!" });
            }
            return Json(new { text = "Something went wrong!" });
        }

        // GET: Pets/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
