using LinenAndBird.Controllers;
using LinenAndBird.DataAccess;
using System;
using Xunit;

namespace LinenAndBird.Tests
{
    public class HatsControllerTests
    {
        [Fact]
        public void Requesting_all_hats_returns_all_hats()
        {
            // Arrange 

            var controller = new HatsController(new FakeHatRepository);
            // Act
            public class FakeHatRepository : IHatRepository
        {

        }
            // Assert
        }
    }
}
