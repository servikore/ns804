using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Domain.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService userService;
        private Mock<IUserRepository> useRepository;
        private Mock<INotificationFactory> notificationFactory;
        private Mock<INotificationService> notificationService;

        [TestInitialize]
        public void Init()
        {
            useRepository = new Mock<IUserRepository>();
            
            notificationFactory = new Mock<INotificationFactory>();
            
            notificationService = new Mock<INotificationService>();
            notificationFactory.Setup(s => s.GetNotificationService(It.IsAny<NotificationTypes>())).Returns(notificationService.Object);

            userService = new UserService(useRepository.Object, notificationFactory.Object);
        }

        [DataTestMethod]
        [DataRow(1,"test@gmail.com","test","user","p@ssword")]
        [DataRow(0,"","test","user","p@ssword")]
        [DataRow(0, "test@gmail.com", "", "user", "p@ssword")]
        [DataRow(0, "test@gmail.com", "test", "", "p@ssword")]
        [DataRow(0, "test@gmail.com", "test", "user", "")]
        public void WhenTryRegister_WithInvalidUser_DomainExceptionIsThrown(int Id, string email, string firstName, string lastName, string password)
        {
            var user = new User { 
                Id = Id, 
                Email = email, 
                Password = password, 
                FirstName = firstName, 
                LastName = lastName 
            };

            Assert.ThrowsExceptionAsync<DomainException>(() => userService.Register(user));
        }

        [DataTestMethod]
        [DataRow(0, "test@gmail.com", "test", "user", "p@ssword")]
        public async Task WhenTryRegister_WithValidUser_RepositoryInsertShouldBeCalled(int Id, string email, string firstName, string lastName, string password)
        {
            var user = new User
            {
                Id = Id,
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            await userService.Register(user);

            useRepository.Verify(m => m.Insert(user), Times.Once);            
        }

        [DataTestMethod]
        [DataRow(0, "test@gmail.com", "test", "user", "p@ssword")]
        public async Task WhenTryRegister_WithValidUser_GetNotificationServiceShouldBeCalled(int Id, string email, string firstName, string lastName, string password)
        {
            var user = new User
            {
                Id = Id,
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            await userService.Register(user);

            notificationFactory.Verify(nf => nf.GetNotificationService(NotificationTypes.Email), Times.Once);
        }
    }
}
