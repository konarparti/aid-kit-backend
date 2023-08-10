using System.Collections;
using Moq;
using System.Net.Http.Headers;
using AidKit.BLL.DTO.Medicine;
using AidKit.BLL.Interfaces;
using AidKit.Service.Implemetions;
using AidKit.Service.Interfaces;
using AidKit.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using AidKit.WebApi.ViewModels.Response.Medicine;

namespace AidKit.Tests
{
    public class MedicineControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsAllMedicines()
        {
            //Arrange
            var mockManager = new Mock<IMedicineManager>();
            var mockService = new Mock<IFileStorageService>();
            mockManager.Setup(x => x.GetAllAsync()).ReturnsAsync(GetAllMedicineDtos());
            var controller = new MedicineController(mockManager.Object, mockService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<MedicineViewModel>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualValues = Assert.IsAssignableFrom<IEnumerable<MedicineViewModel>>(okObjectResult.Value);
            var expectedCount = GetAllMedicineDtos().Count();
            Assert.Equal(expectedCount, actualValues.Count());
        }

        private IEnumerable<MedicineDTO> GetAllMedicineDtos()
        {
            var medicines = new List<MedicineDTO>
            {
                new()
                {
                    Id = 1,
                    Name = "Name 1"
                },
                new()
                {
                    Id = 2,
                    Name = "Name 2"
                },
            };

            return medicines;
        }
    }
}