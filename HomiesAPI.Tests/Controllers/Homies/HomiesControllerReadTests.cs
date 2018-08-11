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
    public class HomiesControllerReadTests
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
            var testHomies = HomiesTestHelpers.GetHomies();
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
            var homie = HomiesTestHelpers.CreateHomie();
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
    }
}