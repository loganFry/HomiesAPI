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
    public class HomiesControllerCheckInCheckOutTests
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
            var homie = HomiesTestHelpers.CreateHomie();
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
            var homie = HomiesTestHelpers.CreateHomie();
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
    }
}