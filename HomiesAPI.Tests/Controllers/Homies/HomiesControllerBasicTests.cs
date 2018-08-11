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
    public class HomiesControllerBasicTests
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
        public void TestCreateValidHomie()
        {
            var homie = HomiesTestHelpers.CreateHomie(3);
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
            var newHomie = HomiesTestHelpers.CreateHomie(3);

            _controller.ModelState.AddModelError("Email", "Required");

            var result = _controller.Create(newHomie);

            Assert.IsNotNull(result);

            // the result should be of the BadRequestObjectResult type
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void TestFullUpdateValidHomie()
        {
            var newHomie = HomiesTestHelpers.CreateHomie();
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
            var newHomie = HomiesTestHelpers.CreateHomie();
            _controller.ModelState.AddModelError("Email", "Required");

            var result = _controller.FullUpdate(newHomie);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

    }
}
