using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ignite.Controllers;

namespace Ignite.Tests.Controllers
{
    [TestClass]
    public class FitnessControllerTest
    {
        [TestMethod]
        public void MyFitnessPlan()
        {
            // Arrange
            var controller = new FitnessController();

            // Act
            ViewResult result = controller.MyFitnessPlan() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExerciseViewer()
        {
            // Arrange
            var controller = new FitnessController();

            // Act
            ViewResult result = controller.ExerciseViewer() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ListEquipment()
        {
            // Arrange
            var controller = new FitnessController();

            // Act
            var result = controller.ListEquipment() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RoutineManager()
        {
            // Arrange
            var controller = new FitnessController();

            // Act
            var result = controller.RoutineManager() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExerciseManager()
        {
            // Arrange
            var controller = new FitnessController();

            // Act
            var result = controller.ExerciseManager() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
