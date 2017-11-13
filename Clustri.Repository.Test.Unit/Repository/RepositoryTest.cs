using System;
using Clustri.Repository.Implementation.Repository;
using Moq;
using Neo4jClient;
using Neo4jClient.Cypher;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Clustri.Repository.Test.Unit.Repository
{
    [TestFixture]
    public class RepositoryTest
    {

        private Mock<ICypherFluentQuery<String>> CreateMockCypher()
        {
            var mockCypher = new Mock<ICypherFluentQuery<string>>();
            mockCypher.Setup(m => m.Create(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Match(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Remove(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.DetachDelete(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Where(It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.WithParam(It.IsAny<string>(), It.IsAny<string>())).Returns(mockCypher.Object);
            mockCypher.Setup(m => m.Return<string>(It.IsAny<string>())).Returns(mockCypher.Object);

            return mockCypher;
        }

        [Test]
        public void Calls_Create_On_Add()
        {
            var mockClient = new Mock<IGraphClient>();

            var mockCypher = CreateMockCypher();
            mockClient.Setup(c => c.Cypher).Returns(mockCypher.Object);

            var repo = new Repository<string>(mockClient.Object);
            repo.Add("Test");

            mockCypher.Verify(m => m.Create(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        public void Calls_Delete_On_Remove()
        {
            var mockClient = new Mock<IGraphClient>();

            var mockCypher = CreateMockCypher();
            mockClient.Setup(c => c.Cypher).Returns(mockCypher.Object);

            var repo = new Repository<string>(mockClient.Object);
            repo.Remove(0);

            mockCypher.Verify(m => m.DetachDelete(It.IsAny<string>()), Times.AtLeastOnce);
        }


        [Test]
        public void Calls_Where_On_Contains()
        {
            var mockClient = new Mock<IGraphClient>();

            var mockCypher = CreateMockCypher();
            mockClient.Setup(c => c.Cypher).Returns(mockCypher.Object);

            var repo = new Repository<string>(mockClient.Object);
            repo.Contains(0);

            mockCypher.Verify(m => m.Where(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
