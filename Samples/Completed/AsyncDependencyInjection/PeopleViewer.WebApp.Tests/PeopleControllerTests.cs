using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleViewer.WebApp.Controllers;
using System;
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
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithTask();
            var result = view.Model as IEnumerable<Person>;

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task WithTask_OnFailure_ReturnsErrorView()
        {
            var reader = GetFaultedReader();
            var controller = new PeopleController(reader);

            try
            {
                var view = await controller.WithTask();
                Assert.Fail("Expected Exception not thrown");
            }
            catch (NotImplementedException)
            {
                // This is the passing state.
                // Note: Other frameworks have an "Assert.Pass"
                // that we can put here to make it more obvious
                // that this is the desired state.
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expecting: {typeof(NotImplementedException)} \n Actual: {ex.GetType()}");
            }
        }

        [TestMethod]
        public async Task WithAwait_OnSuccess_ReturnsViewWithPeople()
        {
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithAwait() as ViewResult;
            var result = view.Model as IEnumerable<Person>;

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task WithAwait_OnFailure_ReturnsErrorView()
        {
            var reader = GetFaultedReader();
            var controller = new PeopleController(reader);

            var view = await controller.WithAwait() as ViewResult;
            var result = view.Model as IEnumerable<Exception>;

            Assert.AreEqual(typeof(NotImplementedException), result.First().GetType());
        }

        [TestMethod]
        public async Task GetPerson_OnSuccess_ReturnsViewWithPerson()
        {
            var testId = 2;
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.GetPerson(testId) as ViewResult;
            var result = view.Model as IEnumerable<Person>;

            Assert.AreEqual(testId, result.First()?.Id);
        }

        [TestMethod]
        public async Task GetPerson_WithInvalidID_ReturnsErrorView()
        {
            var testId = -1;
            var reader = GetGoodReader();
            var controller = new PeopleController(reader);

            var view = await controller.GetPerson(testId) as ViewResult;
            var result = view.Model as IEnumerable<Exception>;

            Assert.AreEqual(typeof(KeyNotFoundException), result.First().GetType());
        }
    }
}
