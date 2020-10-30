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
        [TestMethod]
        public void WithTask_OnSuccess_ReturnsViewWithPeople()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void WithTask_OnFailure_ReturnsErrorView()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void WithAwait_OnSuccess_ReturnsViewWithPeople()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void WithAwait_OnFailure_ReturnsErrorView()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPerson_OnSuccess_ReturnsViewWithPerson()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPerson_WithInvalidID_ReturnsErrorView()
        {
            Assert.Inconclusive();
        }
    }
}
