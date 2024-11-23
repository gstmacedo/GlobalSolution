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
    public class ReportControllerTests
    {
        private readonly Mock<dbContext> _contextMock;
        private readonly Mock<DbSet<Report>> _reportSetMock;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _contextMock = new Mock<dbContext>();
            _reportSetMock = new Mock<DbSet<Report>>();
            _contextMock.Setup(c => c.Reports).Returns(_reportSetMock.Object);
            _controller = new ReportController(_contextMock.Object);
        }

        [Fact]
        public async Task GetReports_ShouldReturnOkWithReports()
        {
            var reports = new List<Report>
            {
                new Report { Id = 1, Descricao  = "Content 1" },
                new Report { Id = 2, Descricao = "Content 2" }
            };
            _reportSetMock.As<IQueryable<Report>>().Setup(m => m.Provider).Returns(reports.AsQueryable().Provider);
            _reportSetMock.As<IQueryable<Report>>().Setup(m => m.Expression).Returns(reports.AsQueryable().Expression);
            _reportSetMock.As<IQueryable<Report>>().Setup(m => m.ElementType).Returns(reports.AsQueryable().ElementType);
            _reportSetMock.As<IQueryable<Report>>().Setup(m => m.GetEnumerator()).Returns(reports.GetEnumerator());

            var result = await _controller.GetReports();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReports = Assert.IsType<List<Report>>(okResult.Value);
            Assert.Equal(2, returnedReports.Count);
        }

        [Fact]
        public async Task GetReport_ShouldReturnNotFound_WhenReportDoesNotExist()
        {
            _reportSetMock.Setup(c => c.FindAsync(It.IsAny<int>())).ReturnsAsync((Report)null);

            var result = await _controller.GetReport(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetReport_ShouldReturnOkWithReport_WhenReportExists()
        {
            var report = new Report { Id = 1, Descricao = "Content 1" };
            _reportSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(report);

            var result = await _controller.GetReport(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReport = Assert.IsType<Report>(okResult.Value);
            Assert.Equal(report.Id, returnedReport.Id);
        }

        [Fact]
        public async Task CreateReport_ShouldReturnCreatedAtAction_WhenReportIsCreated()
        {
            var newReport = new Report { Descricao = "Nova Descrição" };
            _reportSetMock.Setup(c => c.Add(It.IsAny<Report>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.CreateReport(newReport);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetReport", createdAtActionResult.ActionName);
            var returnedReport = Assert.IsType<Report>(createdAtActionResult.Value);
            Assert.Equal(newReport.Descricao, returnedReport.Descricao);
        }

        [Fact]
        public async Task UpdateReport_ShouldReturnNoContent_WhenReportIsUpdated()
        {
            var reportToUpdate = new Report { Id = 1, Descricao = "Updated Content" };
            _reportSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(reportToUpdate);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.UpdateReport(1, reportToUpdate);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateReport_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            var reportToUpdate = new Report { Id = 1, Descricao = "Updated Content" };

            var result = await _controller.UpdateReport(2, reportToUpdate);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteReport_ShouldReturnNoContent_WhenReportIsDeleted()
        {
            var reportToDelete = new Report { Id = 1, Descricao = "Content 1" };
            _reportSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(reportToDelete);
            _reportSetMock.Setup(c => c.Remove(It.IsAny<Report>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteReport(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteReport_ShouldReturnNotFound_WhenReportDoesNotExist()
        {
            _reportSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync((Report)null);

            var result = await _controller.DeleteReport(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
