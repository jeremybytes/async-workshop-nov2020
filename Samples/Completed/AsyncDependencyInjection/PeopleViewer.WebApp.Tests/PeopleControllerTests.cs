using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleViewer.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleViewer.WebApp.Tests
{
    [TestClass]
    public class PeopleControllerTests
    {
        private IPersonReader GetGoodReader() => new FakeGoodReader();
        private IPersonReader GetFaultedReader() => new FakeFaultedReader();

        [TestMethod]
        public async Task WithTask_OnSuccess_ReturnsViewWithPeople()
        {
            // Arrange
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            // Act
            var view = await controller.WithTask();
            var result = view.Model as IEnumerable<Person>;

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task WithAwait_OnSuccess_ReturnsViewWithPeople()
        {
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithAwait();
            var result = view.Model as IEnumerable<Person>;

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetPerson_OnSuccess_ReturnsViewWithPerson()
        {
            int testId = 2;
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.GetPerson(testId);
            var result = view.Model as IEnumerable<Person>;

            Assert.AreEqual(testId, result?.First().Id);
        }

        [TestMethod]
        public async Task WithTask_OnFailure_ReturnsErrorView()
        {
            var reader = GetFaultedReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithTask();

            Assert.AreEqual("Error", view.ViewName);
        }

        [TestMethod]
        public async Task WithAwait_OnFailure_ReturnsErrorView()
        {
            var reader = GetFaultedReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithAwait();

            Assert.AreEqual("Error", view.ViewName);
        }

        [TestMethod]
        public async Task GetPerson_WithInvalidID_ReturnsErrorView()
        {
            var testId = -10;
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.GetPerson(testId);

            Assert.AreEqual("Error", view.ViewName);
        }
    }
}
