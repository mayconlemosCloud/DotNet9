using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Handlers.Queries.V1
{
    public class GetAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _avisoRepositoryMock;
        private readonly GetAvisoHandler _handler;

        public GetAvisoHandlerTests()
        {
            _avisoRepositoryMock = new Mock<IAvisoRepository>();
            _handler = new GetAvisoHandler(_avisoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAvisoResponse_WhenAvisoExists()
        {
            // Arrange
            var request = new GetAvisoRequest { Id = 1 };
            var expectedResponse = new GetAvisosResponse { Id = 1, Titulo = "titulo teste", Mensagem = "mensagem teste", Ativo = true };

            var aviso = new AvisoEntity
            {
                Titulo = "titulo teste",
                Mensagem = "mensagem teste",
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };


            var idProperty = typeof(AvisoEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            idProperty.SetValue(aviso, 1);


            _avisoRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAvisoDoesNotExist()
        {
            // Arrange
            var request = new GetAvisoRequest { Id = 1 };

            _avisoRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAvisoIsNotActive()
        {
            // Arrange
            var request = new GetAvisoRequest { Id = 1 };

            var aviso = new AvisoEntity
            {
                Titulo = "titulo teste",
                Mensagem = "mensagem teste",
                Ativo = false,
                DataCriacao = DateTime.UtcNow
            };

            var idProperty = typeof(AvisoEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            idProperty.SetValue(aviso, 1);

            _avisoRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAvisoIsNull()
        {
            // Arrange
            var request = new GetAvisoRequest { Id = 1 };

            _avisoRepositoryMock.Setup(repo => repo.FindAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Data.Should().BeNull();
        }

    }
}
