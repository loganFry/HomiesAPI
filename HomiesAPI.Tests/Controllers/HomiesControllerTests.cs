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
        private Mock<IHomieRepository> _mockHomieRepo;

        private HomiesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockHomieRepo = new Mock<IHomieRepository>();
            _controller = new HomiesController(_mockHomieRepo.Object);
        }

        [TestMethod]
        public void TestGetAllNonEmpty()
        {
            var testHomies = GetHomies();
            _mockHomieRepo.Setup(x => x.List(It.IsAny<Expression<Func<Homie, object>>[]>())).Returns(testHomies);

            var result = _controller.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Homie>>));
            Assert.AreEqual(testHomies.Count, result.Value.Count);
        }

        [TestMethod]
        public void TestGetAllEmpty()
        {
            _mockHomieRepo.Setup(x => x.List(It.IsAny<Expression<Func<Homie, object>>[]>())).Returns(new List<Homie>());
            var result = _controller.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Homie>>));
            Assert.AreEqual(0, result.Value.Count);
        }

        [TestMethod]
        public void TestGetByIdCorrectId()
        {
            var testHomies = GetHomies();
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(
                    (int id, Expression<Func<Homie, object>>[] includes) => testHomies.FirstOrDefault(x => x.Id == id));

            var validId = 1;
            var result = _controller.GetById(validId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Homie>));

            // make sure the method looked up the correct homie
            Assert.AreEqual(validId, result.Value.Id);
        }

        [TestMethod]
        public void TestGetByIdWrongId()
        {
            // make repo GetById method return null in all cases so that the lookup fails
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns((Homie)null);

            var result = _controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Homie>));

            // the method should return a NotFound result if the homie doesn't exist 
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        private List<Homie> GetHomies()
        {
            var homies = new List<Homie> 
            {
                new Homie 
                {
                    Id = 1,
                    FirstName = "Logan",
                    LastName = "Fry",
                    NickName = "FrenchFry",
                    Email = "fry@gmail.com",
                    IsHome = false,
                    HasGuest = false,
                }, 
                new Homie 
                {
                    Id = 2,
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
