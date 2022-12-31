using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Services
{
    public class PetService
    {
        private ApplicationDbContext context;

        public PetService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool OldEnoughToAdopt(DateTime dateTime)
        {
            TimeSpan difference = DateTime.Now.Subtract(dateTime);
            if (difference.Days > 365 * 18) return true;
            return false;
        }

        public IEnumerable<Pet> GetAllItems()
        {
            return context.Pets.ToList();
        }

        public Pet GetPetById(int? id)
        {
            return context.Pets.Where(x => x.Id == id).First();
        }

        public IEnumerable<Pet> GetPetsByBreed(string breed)
        {
            return context.Pets.Where(x => x.Breed == breed);
        }

        public IEnumerable<string> GetBreeds()
        {
            List<string> result = new List<String>();
            foreach (Pet item in context.Pets)
            {
                if (!result.Contains(item.Breed)) result.Add(item.Breed);
            }
            result.Sort();
            return result;
        }

        public List<SelectListItem> GetBreedSelectListItems(string SelectedBreed)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (string breed in GetBreeds())
            {
                result.Add(new SelectListItem { Text = breed, Value = breed, Selected = (breed == SelectedBreed) });
            }
            return result;
        }

    }
}