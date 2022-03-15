using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolvoTestWebApp.Controllers;
using VolvoTestWebApp.Data.Repositories.Abstractions;
using Xunit;

namespace VolvoTestWebApp.Tests.Controllers
{
    public class VolvoTrucksControllerTests
    {
        Mock<IVolvoTruckRepository> mockRepo;
        VolvoTrucksController controller;

        public VolvoTrucksControllerTests()
        {
            mockRepo = new Mock<IVolvoTruckRepository>();
            controller = new VolvoTrucksController(mockRepo.Object);
        }

        private Models.VolvoTruckModel GetRegisteredTruck()
        {
            return new Models.VolvoTruckModel
            {
                Id = 1,
                Model = "FH",
                ModelYear = 2022,
                FabricationYear = 2022
            };
        }

        [Fact]
        public async Task Index_GET_ShouldReturnResultWithModel()
        {
            // Arrange
            var data = new List<Models.VolvoTruckModel>
            {
                new Models.VolvoTruckModel
                {
                    Id = 1,
                    Model = "FH",
                    ModelYear = 2022,
                    FabricationYear = 2022
                },
                new Models.VolvoTruckModel
                {
                    Id = 2,
                    Model = "FM",
                    ModelYear = 2023,
                    FabricationYear = 2022
                }
            };
            mockRepo.Setup(repo => repo.GetTrucks()).ReturnsAsync(data);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.VolvoTruckModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Create_POST_AddNewTruck()
        {
            var truck = new Models.VolvoTruckModel
            {
                Id = 1,
                Model = "FM",
                ModelYear = 2022,
                FabricationYear = 2023,
                IsEdit = true
            };

            mockRepo.Setup(repo => repo.UpsertTruck(It.IsAny<Models.VolvoTruckModel>()))
                   .Returns(Task.CompletedTask)
                   .Verifiable();

            // Act
            var result = await controller.UpdateTruck(null, truck);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        [Fact]
        public async Task Update_GET_EditTruck_ShouldReturnViewModel()
        {
            // Arrange
            int testId = 1;
            var model = "FH";
            var modelYear = 2022;

            mockRepo.Setup(repo => repo.GetTruckById(testId)).ReturnsAsync(GetRegisteredTruck());

            // Act
            var result = await controller.Edit(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<Models.VolvoTruckModel>(viewResult.ViewData.Model);
            Assert.Equal(testId, viewModel.Id);
            Assert.Equal(model, viewModel.Model);
            Assert.Equal(modelYear, viewModel.ModelYear);
        }

        [Fact]
        public async Task Update_POST_EditTruck()
        {
            // Arrange
            int testId = 1;
            var truck = new Models.VolvoTruckModel
            {
                Id = 1,
                Model = "FM",
                ModelYear = 2022,
                FabricationYear = 2023,
                IsEdit = true
            };

            mockRepo.Setup(repo => repo.GetTruckById(testId)).ReturnsAsync(GetRegisteredTruck());
            mockRepo.Setup(repo => repo.UpsertTruck(It.IsAny<Models.VolvoTruckModel>()))
                   .Returns(Task.CompletedTask)
                   .Verifiable();

            // Act
            var result = await controller.UpdateTruck(testId, truck);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        [Fact]
        public async Task Delete_GET_ShouldReturnViewModel()
        {
            // Arrange
            int testId = 1;
            mockRepo.Setup(repo => repo.GetTruckById(testId)).ReturnsAsync(GetRegisteredTruck());

            // Act
            var result = await controller.Delete(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<Models.VolvoTruckModel>(viewResult.ViewData.Model);
            Assert.Equal(testId, viewModel.Id);
        }

        [Fact]
        public async Task Delete_POST_ExistingIdPass_RemoveOneItem()
        {
            // Arrange
            var testId = 1;
            mockRepo.Setup(repo => repo.DeleteTruck(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await controller.DeleteConfirmed(testId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
