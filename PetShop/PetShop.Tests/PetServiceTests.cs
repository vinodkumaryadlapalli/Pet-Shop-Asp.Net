using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Models;
using PetShop.Services;
using System;

namespace PetShop.Tests
{
    [TestClass]
    public class PetServiceTests
    {
        [TestMethod]
        public void PetService_OldEnoughToAdopt_OldEnough()
        {
            // Arrange
            PetService petService = new PetService(new ApplicationDbContext());
            DateTime oldEnough = DateTime.Now.AddYears(-20);
            bool expectedResult = true;

            // Act
            bool actualResult = petService.OldEnoughToAdopt(oldEnough);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GroceryService_OldEnoughToAdopt_NotOldEnough()
        {
            // Arrange
            PetService petService = new PetService(new ApplicationDbContext());
            DateTime notOldEnough = DateTime.Now.AddYears(-4);
            bool expectedResult = false;

            // Act
            bool actualResult = petService.OldEnoughToAdopt(notOldEnough);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
