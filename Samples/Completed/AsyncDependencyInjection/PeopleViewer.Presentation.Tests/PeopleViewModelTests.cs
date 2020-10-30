using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleViewer.Presentation.Tests
{
    [TestClass]
    public class PeopleViewModelTests
    {
        private IPersonReader GetTestReader()
        {
            return new FakeReader();
        }

        [TestMethod]
        public async Task RefreshPeople_OnExecute_PeopleIsPopulated()
        {
            // Arrange
            var reader = GetTestReader();
            var vm = new PeopleViewModel(reader);

            // Act
            await vm.RefreshPeople();

            // Assert
            Assert.IsNotNull(vm.People);
            Assert.AreEqual(2, vm.People.Count());
        }

        [TestMethod]
        public async Task ClearPeople_OnExecute_PoeopleIsEmpty()
        {
            // Arrange
            var reader = GetTestReader();
            var vm = new PeopleViewModel(reader);
            await vm.RefreshPeople();
            Assert.AreEqual(2, vm.People.Count(), "Invalid Arrangement");

            // Act
            vm.ClearPeople();

            // Assert
            Assert.AreEqual(0, vm.People.Count());
        }
    }
}
