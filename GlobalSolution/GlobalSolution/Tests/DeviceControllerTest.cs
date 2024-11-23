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
    public class DeviceControllerTests
    {
        private readonly Mock<dbContext> _contextMock;
        private readonly Mock<DbSet<Device>> _deviceSetMock;
        private readonly DeviceController _controller;

        public DeviceControllerTests()
        {
            _contextMock = new Mock<dbContext>();
            _deviceSetMock = new Mock<DbSet<Device>>();
            _contextMock.Setup(c => c.Devices).Returns(_deviceSetMock.Object);
            _controller = new DeviceController(_contextMock.Object);
        }

        [Fact]
        public async Task GetDevices_ShouldReturnOkWithDevices()
        {
            var devices = new List<Device>
            {
                new Device { Id = 1, Nome = "Device 1" },
                new Device { Id = 2, Nome = "Device 2" }
            };
            _deviceSetMock.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(devices.AsQueryable().Provider);
            _deviceSetMock.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(devices.AsQueryable().Expression);
            _deviceSetMock.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(devices.AsQueryable().ElementType);
            _deviceSetMock.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(devices.GetEnumerator());

            var result = await _controller.GetDevices();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDevices = Assert.IsType<List<Device>>(okResult.Value);
            Assert.Equal(2, returnedDevices.Count);
        }

        [Fact]
        public async Task GetDevice_ShouldReturnNotFound_WhenDeviceDoesNotExist()
        {
            _deviceSetMock.Setup(d => d.FindAsync(It.IsAny<int>())).ReturnsAsync((Device)null);

            var result = await _controller.GetDevice(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetDevice_ShouldReturnOkWithDevice_WhenDeviceExists()
        {
            var device = new Device { Id = 1, Nome = "Device 1" };
            _deviceSetMock.Setup(d => d.FindAsync(1)).ReturnsAsync(device);

            var result = await _controller.GetDevice(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDevice = Assert.IsType<Device>(okResult.Value);
            Assert.Equal(device.Id, returnedDevice.Id);
        }

        [Fact]
        public async Task CreateDevice_ShouldReturnCreatedAtAction_WhenDeviceIsCreated()
        {
            var newDevice = new Device { Nome = "New Device" };
            _deviceSetMock.Setup(d => d.Add(It.IsAny<Device>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.CreateDevice(newDevice);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetDevice", createdAtActionResult.ActionName);
            var returnedDevice = Assert.IsType<Device>(createdAtActionResult.Value);
            Assert.Equal(newDevice.Nome, returnedDevice.Nome);
        }

        [Fact]
        public async Task UpdateDevice_ShouldReturnNoContent_WhenDeviceIsUpdated()
        {
            var deviceToUpdate = new Device { Id = 1, Nome = "Updated Device" };
            _deviceSetMock.Setup(d => d.FindAsync(1)).ReturnsAsync(deviceToUpdate);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.UpdateDevice(1, deviceToUpdate);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateDevice_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            var deviceToUpdate = new Device { Id = 1, Nome = "Updated Device" };

            var result = await _controller.UpdateDevice(2, deviceToUpdate);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteDevice_ShouldReturnNoContent_WhenDeviceIsDeleted()
        {
            var deviceToDelete = new Device { Id = 1, Nome = "Device to Delete" };
            _deviceSetMock.Setup(d => d.FindAsync(1)).ReturnsAsync(deviceToDelete);
            _deviceSetMock.Setup(d => d.Remove(It.IsAny<Device>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteDevice(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteDevice_ShouldReturnNotFound_WhenDeviceDoesNotExist()
        {
            _deviceSetMock.Setup(d => d.FindAsync(1)).ReturnsAsync((Device)null);

            var result = await _controller.DeleteDevice(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
