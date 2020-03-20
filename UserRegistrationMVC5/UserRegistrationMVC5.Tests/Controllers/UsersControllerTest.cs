using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Lw.Data.Entity;
using Moq;
using NUnit.Framework;
using Unity;
using UserRegistrationMVC5.Controllers;
using UserRegistrationMVC5.Models;
using UserRegistrationMVC5.Repository;

namespace UserRegistrationMVC5.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTest
    {
        private IUnityContainer myContainer;
        private UsersController classUnderTest;
        private Mock<IUsersRepository> usersRepository;
        private List<User> newUsers;

        [TestFixtureSetUp]
        public void UnitTestBaseSetUp()
        {
            myContainer = new UnityContainer();
            myContainer.RegisterType<UserRegistrationEntities>();
            myContainer.RegisterType<IUsersRepository, UsersRepository>();

           

            newUsers = new List<User>() { new User { Id = 10, FirstName = "Nikola", LastName = "Tesla" },
            new User { Id = 3, FirstName= "Mileva", LastName="Maric"}, new User { Id = 5, FirstName = "Novak", LastName= "Djokovic" } };


            usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(x => x.GetAllUsers()).Returns(newUsers);

            classUnderTest = new UsersController(usersRepository.Object);
        }

        [Test]
        public void NewUserViewTest()
        {
      
            // Act
            ViewResult result = classUnderTest.NewUserView() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
        }

        [Test]
        public void IndexTest()
        {

            // Act
            var result = classUnderTest.Index();
            var model = ((ViewResult)result).Model as List<User>;

            // Assert

            Assert.AreEqual(3, model.Count);
        }

        [Test]
        public void GetUserTest()
        {

            this.usersRepository.Setup(x => x.GetUserById(3)).Returns(newUsers.Find(x => x.Id == 3));

            // Act
            var result = classUnderTest.GetUser(3);
            var model = ((ViewResult)result).Model as User ;

            // Assert
            Assert.AreEqual(model.Id, 3);
            
         }


        [Test]
        public void DeleteUserTest()
        {

            // Act
            var result = classUnderTest.Delete(newUsers[1].Id);

            // Assert
            usersRepository.Verify(m => m.DeleteUser(It.IsAny<int>()), Times.Once());

        }

        [Test]
        public void UpdateUserTest()
        {
            newUsers[1].FirstName = "Zaklina";

            // Act
            var result = classUnderTest.Update(newUsers[1]);
         
            // Assert
            usersRepository.Verify(m => m.UpdateUser(It.IsAny<User>()), Times.Once());

        }

        [Test]
        public void AddNewUserTest()
        {

            // Act
            var result = classUnderTest.AddNewUser(new User() { Id = 10, FirstName = "Darth", LastName = "Vader" });

            // Assert
            usersRepository.Verify(m => m.AddUser(It.IsAny<User>()), Times.Once());

        }

        [TestFixtureTearDown]
        public void VerifyAndTearDown()
        {
            myContainer = null;
            classUnderTest = null;
            usersRepository = null;
            newUsers = null;
        }
    }
}
