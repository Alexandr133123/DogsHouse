using DogsHouse.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Presentation.Tests.Controllers
{
    public class PingControllerTests
    {
        [Fact]
        public void PingController_Should_Return_Message()
        {
            var controller = new PingController();

            var result = (controller.Ping() as OkObjectResult)?.Value as string;

            Assert.NotNull(result);
            Assert.Equal("DogsHouseService.Version1.0.1", result);
        } 
    }
}
