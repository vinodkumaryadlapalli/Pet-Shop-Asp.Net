using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Interfaces
{
    internal interface IPetService
    {

        bool OldEnoughToAdopt(DateTime dateTime);

        IEnumerable<Pet> GetAllPets();

        Pet GetPetById(int? id);

        IEnumerable<Pet> GetPetsByBreed(string breed);
    }
}
