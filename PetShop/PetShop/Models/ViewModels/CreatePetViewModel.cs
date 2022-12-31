using PetShop.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Models.ViewModels
{
    public class CreatePetViewModel
    {
        [NoDigits]
        public string Name { get; set; }
        public bool isMale { get; set; }
        public string Breed { get; set; }

        [NonNegative]
        public int Age { get; set; }

        public bool isFixed { get; set; }

        public List<SelectListItem> DropDownBreed { get; set; }
    }
}