using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomiesAPI.Controllers;
using HomiesAPI.DataAccess.Repositories;
using HomiesAPI.Models;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace HomiesAPI.Tests
{
    [TestClass]
    public class HomiesControllerTests
    {
        [TestMethod]
        public void TestGetAll()
        {
            var mockRepo = new Mock<IHomieRepository>();
            var testHomies = GetHomies();
            mockRepo.Setup(x => x.List(It.IsAny<Expression<Func<Homie, object>>[]>())).Returns(testHomies);
            var controller = new HomiesController(mockRepo.Object);

            var result = controller.GetAll();

            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Homie>>));
            Assert.AreEqual(testHomies.Count, result.Value.Count);
        }

        private List<Homie> GetHomies()
        {
            var homies = new List<Homie> 
            {
                new Homie 
                {
                    FirstName = "Logan",
                    LastName = "Fry",
                    NickName = "FrenchFry",
                    Email = "fry@gmail.com",
                    IsHome = false,
                    HasGuest = false
                }, 
                new Homie 
                {
                    FirstName = "Jack",
                    LastName = "Rodman",
                    NickName = "Rod",
                    Email = "rodman@osu.edu",
                    IsHome = true,
                    HasGuest = true
                }
            };

            return homies;
        }
    }
}
