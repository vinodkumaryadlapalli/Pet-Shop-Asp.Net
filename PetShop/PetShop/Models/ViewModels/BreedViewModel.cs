using PetShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Models.ViewModels
{
    public class BreedViewModel
    {
        public PetService PetService { get; set; }
        public IEnumerable<string> Breeds { get; set; }

        public BreedViewModel(PetService petService, IEnumerable<string> breeds)
        {
            PetService = petService;
            Breeds = breeds;
        }
    }
}