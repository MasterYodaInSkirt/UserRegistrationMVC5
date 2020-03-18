using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistrationMVC5;
using UserRegistrationMVC5.Controllers;

namespace UserRegistrationMVC5.Tests.Controllers
{
    [TestClass]
    class UsersControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
