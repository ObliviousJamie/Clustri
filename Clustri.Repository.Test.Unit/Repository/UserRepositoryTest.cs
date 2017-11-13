using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clustri.Repository.Entities;
using Clustri.Repository.Implementation.Repository;
using Moq;
using Neo4jClient;
using Neo4jClient.Cypher;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Clustri.Repository.Test.Unit.Repository
{
    [TestFixture]
    public class UserRepositoryTest
    {

        private Mock<ICypherFluentQuery<User>> CreateMockCypher()
        {
            var mockCypher = new Mock<ICypherFluentQuery<User>>();
            mockCypher.Setup(m => m.CreateUnique(It.IsAny<string>())).Returns(mockCypher.Object);
            
            mockCypher.Setup(m => m.Create(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Match(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Remove(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.DetachDelete(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Where(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Where<User>(It.IsAny<Expression<Func<User, bool>>>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Where<User>(user => true || false)).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.AndWhere<User>(user => true || false)).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Return<User>(It.IsAny<string>())).Returns(mockCypher.Object);

            return mockCypher;
        }

        [Test]
        public void DeleteByUser_Calls_DetachDelete()
        {
            var user = new User { UserId = "Test", Weight = 10 };
            var mockClient = new Mock<IGraphClient>();
            var mockCypher = CreateMockCypher();
            mockClient.Setup(m => m.Cypher).Returns(mockCypher.Object);
            var sut = new UserRepository(mockClient.Object);

            sut.DeleteByUser(user);
            mockCypher.Verify(m => m.DetachDelete(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAllFriends_Calls_Where()
        {
            var user = new User { UserId = "Test", Weight = 10 };
            var mockClient = new Mock<IGraphClient>();
            var mockCypher = CreateMockCypher();
            mockClient.Setup(m => m.Cypher).Returns(mockCypher.Object);
            var sut = new UserRepository(mockClient.Object);

            sut.GetAllFriends(user);
            mockCypher.Verify(m => m.Where(It.IsAny<string>()), Times.Once);
        }
    }
}
