using NUnit.Framework.Internal;
using NUnit.Framework;
using Clustri.Repository.Core.Interfaces;
using Clustri.Repository.Implementation;
using Moq;
using Neo4jClient;
using Neo4jClient.Transactions;

namespace Clustri.Repository.Test.Unit
{
    [TestFixture]
    public class UnitOfWorkTest
    {

        [Test]
        public void User_Repository_Accessable()
        {
            var mockAdapter = new Mock<IGraphClientAdapter>();
            mockAdapter.SetupGet(g => g.GraphClient).Returns(new Mock<IGraphClient>().Object);
            var sut = new UnitOfWork(mockAdapter.Object);
            Assert.NotNull(sut.Users);
        }


        [Test]
        public void Calls_Begin_When_Connected()
        {
            var mockAdapter = new Mock<IGraphClientAdapter>();
            mockAdapter.SetupGet(g => g.GraphClient).Returns(new Mock<IGraphClient>().Object);
            mockAdapter.Setup(a => a.IsConnected()).Returns(true);
            var sut = new UnitOfWork(mockAdapter.Object);
            sut.Start();
            mockAdapter.Verify(m => m.BeginTransaction(), Times.Once);
        }

        [Test]
        public void Calls_Dispose()
        {
            var mockAdapter = new Mock<IGraphClientAdapter>();
            mockAdapter.SetupGet(g => g.GraphClient).Returns(new Mock<IGraphClient>().Object);
            var sut = new UnitOfWork(mockAdapter.Object);
            sut.Dispose();
            mockAdapter.Verify(m => m.Dispose(), Times.Once);
        }

        [Test]
        public void Calls_Commit_When_Connected()
        {
            var mockAdapter = new Mock<IGraphClientAdapter>();
            mockAdapter.SetupGet(g => g.GraphClient).Returns(new Mock<IGraphClient>().Object);
            mockAdapter.Setup(a => a.IsConnected()).Returns(true);

            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(t => t.IsOpen).Returns(true);
            mockAdapter.Setup(a => a.Transaction()).Returns(mockTransaction.Object);

            var sut = new UnitOfWork(mockAdapter.Object);
            sut.Complete();
            mockTransaction.Verify(m => m.Commit(), Times.Once);
        }

        [Test]
        public void Disposes_In_Transaction()
        {
            var mockAdapter = new Mock<IGraphClientAdapter>();
            mockAdapter.SetupGet(g => g.GraphClient).Returns(new Mock<IGraphClient>().Object);
            mockAdapter.Setup(a => a.InTransaction()).Returns(true);

            var mockTransaction = new Mock<ITransaction>();
            mockAdapter.Setup(a => a.Transaction()).Returns(mockTransaction.Object);

            var sut = new UnitOfWork(mockAdapter.Object);
            sut.Dispose();

            mockTransaction.Verify(t => t.Dispose(), Times.Once);
        }
    }
}
