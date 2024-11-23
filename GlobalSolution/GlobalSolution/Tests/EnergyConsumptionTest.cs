using Microsoft.AspNetCore.Mvc;
using Moq;
using GlobalSolution.Controllers;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GlobalSolution.Tests
{
    public class EnergyConsumptionControllerTests
    {
        private readonly Mock<dbContext> _contextMock;
        private readonly Mock<DbSet<EnergyConsumption>> _consumptionSetMock;
        private readonly EnergyConsumptionController _controller;

        public EnergyConsumptionControllerTests()
        {
            _contextMock = new Mock<dbContext>();
            _consumptionSetMock = new Mock<DbSet<EnergyConsumption>>();
            _contextMock.Setup(c => c.EnergyConsumptions).Returns(_consumptionSetMock.Object);
            _controller = new EnergyConsumptionController(_contextMock.Object);
        }

        [Fact]
        public async Task GetEnergyConsumptions_ShouldReturnOkWithConsumptions()
        {
            var consumptions = new List<EnergyConsumption>
            {
                new EnergyConsumption { Id = 1, Custo = 100 },
                new EnergyConsumption { Id = 2, Custo= 150 }
            };
            _consumptionSetMock.As<IQueryable<EnergyConsumption>>().Setup(m => m.Provider).Returns(consumptions.AsQueryable().Provider);
            _consumptionSetMock.As<IQueryable<EnergyConsumption>>().Setup(m => m.Expression).Returns(consumptions.AsQueryable().Expression);
            _consumptionSetMock.As<IQueryable<EnergyConsumption>>().Setup(m => m.ElementType).Returns(consumptions.AsQueryable().ElementType);
            _consumptionSetMock.As<IQueryable<EnergyConsumption>>().Setup(m => m.GetEnumerator()).Returns(consumptions.GetEnumerator());

            var result = await _controller.GetEnergyConsumptions();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedConsumptions = Assert.IsType<List<EnergyConsumption>>(okResult.Value);
            Assert.Equal(2, returnedConsumptions.Count);
        }

        [Fact]
        public async Task GetEnergyConsumption_ShouldReturnNotFound_WhenConsumptionDoesNotExist()
        {
            _consumptionSetMock.Setup(c => c.FindAsync(It.IsAny<int>())).ReturnsAsync((EnergyConsumption)null);

            var result = await _controller.GetEnergyConsumption(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetEnergyConsumption_ShouldReturnOkWithConsumption_WhenConsumptionExists()
        {
            var consumption = new EnergyConsumption { Id = 1, Custo = 100 };
            _consumptionSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(consumption);

            var result = await _controller.GetEnergyConsumption(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedConsumption = Assert.IsType<EnergyConsumption>(okResult.Value);
            Assert.Equal(consumption.Id, returnedConsumption.Id);
        }

        [Fact]
        public async Task CreateEnergyConsumption_ShouldReturnCreatedAtAction_WhenConsumptionIsCreated()
        {
            var newConsumption = new EnergyConsumption { Custo = 200 };
            _consumptionSetMock.Setup(c => c.Add(It.IsAny<EnergyConsumption>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.CreateEnergyConsumption(newConsumption);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetEnergyConsumption", createdAtActionResult.ActionName);
            var returnedConsumption = Assert.IsType<EnergyConsumption>(createdAtActionResult.Value);
            Assert.Equal(newConsumption.Custo, returnedConsumption.Custo);
        }

        [Fact]
        public async Task UpdateEnergyConsumption_ShouldReturnNoContent_WhenConsumptionIsUpdated()
        {
            var consumptionToUpdate = new EnergyConsumption { Id = 1, Custo = 250 };
            _consumptionSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(consumptionToUpdate);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.UpdateEnergyConsumption(1, consumptionToUpdate);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEnergyConsumption_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            var consumptionToUpdate = new EnergyConsumption { Id = 1, Custo = 250 };

            var result = await _controller.UpdateEnergyConsumption(2, consumptionToUpdate);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteEnergyConsumption_ShouldReturnNoContent_WhenConsumptionIsDeleted()
        {
            var consumptionToDelete = new EnergyConsumption { Id = 1, Custo = 100 };
            _consumptionSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(consumptionToDelete);
            _consumptionSetMock.Setup(c => c.Remove(It.IsAny<EnergyConsumption>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteEnergyConsumption(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEnergyConsumption_ShouldReturnNotFound_WhenConsumptionDoesNotExist()
        {
            _consumptionSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync((EnergyConsumption)null);

            var result = await _controller.DeleteEnergyConsumption(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
