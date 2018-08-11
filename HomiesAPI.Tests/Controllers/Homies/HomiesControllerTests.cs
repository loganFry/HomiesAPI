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

namespace HomiesAPI.Tests.Controllers.Homies
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
            _mockHomieRepo
                .Setup(x => x.List(It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(testHomies)
                .Verifiable();

            var result = _controller.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Homie>>));
            Assert.AreEqual(testHomies.Count, result.Value.Count);
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestGetAllEmpty()
        {
            _mockHomieRepo
                .Setup(x => x.List(It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(new List<Homie>())
                .Verifiable();
            var result = _controller.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Homie>>));
            Assert.AreEqual(0, result.Value.Count);
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestGetByIdCorrectId()
        {
            var homie = CreateHomie();
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(homie)
                .Verifiable();

            var result = _controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Homie>));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestGetByIdWrongId()
        {
            // make repo GetById method return null in all cases so that the lookup fails
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns((Homie)null)
                .Verifiable();

            var result = _controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Homie>));

            // the method should return a NotFound result if the homie doesn't exist 
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestCreateValidHomie()
        {
            var homie = CreateHomie(3);
            _mockHomieRepo
                .Setup(x => x.Add(It.IsAny<Homie>()))
                .Verifiable();

            var result = _controller.Create(homie);

            Assert.IsNotNull(result);

            // the result should be a CreatedAtRouteResult
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestCreateInvalidHomie()
        {
            var initialHomies = GetHomies();
            var newHomie = CreateHomie(3);

            _controller.ModelState.AddModelError("Email", "Required");

            var result = _controller.Create(newHomie);

            Assert.IsNotNull(result);

            // the result should be of the BadRequestObjectResult type
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestFullUpdateValidHomie()
        {
            var newHomie = CreateHomie();
            _mockHomieRepo
                .Setup(x => x.Edit(It.IsAny<Homie>()))
                .Verifiable();

            var result = _controller.FullUpdate(newHomie);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestFullUpdateInvalidHomie()
        {
            var newHomie = CreateHomie();
            _controller.ModelState.AddModelError("Email", "Required");

            var result = _controller.FullUpdate(newHomie);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestCheckInInvalidId()
        {
            _mockHomieRepo
                .Setup(x => x.GetById(
                    It.IsAny<int>(), 
                    It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns((Homie) null)
                .Verifiable();

            var result = _controller.CheckIn(1, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestCheckInValidId()
        {
            var homie = CreateHomie();
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(homie)
                .Verifiable();
            _mockHomieRepo
                .Setup(x => x.CheckIn(It.IsAny<Homie>(), It.IsAny<bool>(), It.IsAny<DateTime>()))
                .Verifiable();

            var result = _controller.CheckIn(1, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestCheckOutInvalidId()
        {
            _mockHomieRepo
                .Setup(x => x.GetById(
                    It.IsAny<int>(), 
                    It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns((Homie) null)
                .Verifiable();

            var result = _controller.CheckOut(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            _mockHomieRepo.Verify();
        }

        [TestMethod]
        public void TestCheckOutValidId()
        {
            var homie = CreateHomie();
            _mockHomieRepo
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Homie, object>>[]>()))
                .Returns(homie)
                .Verifiable();
            _mockHomieRepo
                .Setup(x => x.CheckOut(It.IsAny<Homie>(), It.IsAny<DateTime>()))
                .Verifiable();

            var result = _controller.CheckOut(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _mockHomieRepo.Verify();
        }

        private List<Homie> GetHomies()
        {
            var homies = new List<Homie>
            {
                CreateHomie(1),
                CreateHomie(2)
            };

            return homies;
        }

        private Homie CreateHomie(int id = 1)
        {
            var homie = new Homie
            {
                Id = id,
                FirstName = "Logan",
                LastName = "Fry",
                NickName = "FrenchFry",
                Email = "fry@gmail.com",
                IsHome = false,
                HasGuest = false,
            };

            return homie;
        }
    }
}
