using AutoMapper;
using Common.Core.Interfaces;
using EasyNetQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Users.API.Interfaces;
using Users.API.Services;
using Users.Core.Domain;

namespace Users.Tests.Services
{
    [TestClass]
    public class UsersServiceTests
    {
        [TestMethod]
        public void CheckDeleteUser_DeleteUserWithValidId_OnSuccess()
        {
            var userId = Guid.NewGuid();
            var userServiceMock = new Mock<IUsersService>();

            userServiceMock.Setup(x => x.DeleteUserAsync(userId)).Verifiable();
        }

        //[ExpectedException(typeof(Exception))]
        [TestMethod]
        public void CheckGetUserById_GetUserWithNotExistId_OnFailed()
        {
            var userIdToCheck = Guid.NewGuid();
            var userEntryId = Guid.NewGuid();

            var userEntries = new List<User>() { new User(userEntryId, null) }.AsQueryable();

            var userRepositoryMock = new Mock<IRepository<User>>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.FindBy(u => u.Id == userEntryId)).Returns(userEntries);

            var userService = new UsersService(null, null, mapperMock.Object, userRepositoryMock.Object);
            userService.GetUserById(userIdToCheck);
        }
    }
}
