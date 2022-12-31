using PetShop.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [NoDigits]
        public string Name { get; set; }    
        public bool isMale { get; set; }
        public string Breed { get; set; }
        public ApplicationUser Owner { get; set; }
        [NonNegative]
        public int Age { get; set; }
    }
}