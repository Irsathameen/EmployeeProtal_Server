using Employee.Test.Mock;
using EmployeePortal.Controllers;
using EmployeePortal.Data.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Employee.Test
{
    public class UserTest
    {
        [Fact]
        public void Test1()
        {

            //Aarrange

            var userService = new Mock<ILoginRepository>();
            userService.Setup(obj => obj.GetUserRoles()).Returns(UserMockData.GetMockUser());
            var sut = new LoginController(userService.Object);

            //Act
            var result = sut.GetUserRoles1();

            //Assert

            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}