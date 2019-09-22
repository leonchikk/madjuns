using AutoFixture;
using Common.Core.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Users.API.Interfaces;
using Users.API.Services;
using Users.Core.Domain;
using Users.Data;
using Users.Data.Repositories;
using Users.Tests.Extensions;

namespace Users.Tests.Services
{
    [TestClass]
    public class UsersServiceTests
    {
        private Guid _accountId = Guid.Empty;

        private Mock<Repository<User>> _mockUsersRepository;

        private readonly Fixture _fixture;
        private IUsersService _usersService;

        public UsersServiceTests()
        {
            _fixture = new Fixture();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _accountId = _fixture.Create<Guid>();

            var testData = new List<User>
            {
                new User(_accountId, null)
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<UsersContext>();
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            _mockUsersRepository = new Mock<Repository<User>>(mockContext.Object);

            _usersService = new UsersService(null, null, null, _mockUsersRepository.Object);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void GetUserById_IncorrectId_ShouldReturn_Exception()
        {
            var invalidAccountId = _fixture.Create<Guid>();
             _usersService.GetUserById(invalidAccountId);
        }
    }
}
