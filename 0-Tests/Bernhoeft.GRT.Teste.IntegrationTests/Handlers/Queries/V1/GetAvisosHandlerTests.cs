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
using Bernhoeft.GRT.Core.Enums;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Handlers.Queries.V1
{
    public class GetAvisosHandlerTests
    {
        private readonly Mock<IAvisoRepository> _avisoRepositoryMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly GetAvisosHandler _handler;

        public GetAvisosHandlerTests()
        {
            _avisoRepositoryMock = new Mock<IAvisoRepository>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(IAvisoRepository))).Returns(_avisoRepositoryMock.Object);
            _handler = new GetAvisosHandler(_serviceProviderMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_List()
        {
            List<AvisoEntity> aviso = new List<AvisoEntity>()
            {
                new AvisoEntity
                {
                    Titulo = "titulo teste",
                    Mensagem = "mensagem teste",
                    Ativo = true
                },
                new AvisoEntity
                {
                    Titulo = "titulo teste 2",
                    Mensagem = "mensagem teste 2",
                    Ativo = true
                },
                new AvisoEntity
                {
                    Titulo = "titulo teste 3",
                    Mensagem = "mensagem teste 3",
                    Ativo = false
                }
            };

            var idProperty = typeof(AvisoEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            for (int i = 0; i < aviso.Count; i++)
            {
                idProperty.SetValue(aviso[i], i + 1);
            }

            var trackingBehavior = TrackingBehavior.NoTracking;

            // Arrange
            _avisoRepositoryMock.Setup(
                x => x.ObterTodosAvisosAsync(trackingBehavior, default)).
                ReturnsAsync(aviso.Where(x => x.Ativo == true).ToList());

            // Act
            var result = await _handler.Handle(new GetAvisosRequest(), default);

            // Assert
            result.Data.Should().NotBeNull();
            result.Data.Count().Should().Be(2);
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_List()
        {
            List<AvisoEntity> aviso = new List<AvisoEntity>()
            {
                new AvisoEntity
                {
                    Titulo = "titulo teste",
                    Mensagem = "mensagem teste",
                    Ativo = false
                },
                new AvisoEntity
                {
                    Titulo = "titulo teste 2",
                    Mensagem = "mensagem teste 2",
                    Ativo = false
                },
                new AvisoEntity
                {
                    Titulo = "titulo teste 3",
                    Mensagem = "mensagem teste 3",
                    Ativo = false
                }
            };

            var idProperty = typeof(AvisoEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            for (int i = 0; i < aviso.Count; i++)
            {
                idProperty.SetValue(aviso[i], i + 1);
            }

            var trackingBehavior = TrackingBehavior.NoTracking;

            // Arrange
            _avisoRepositoryMock.Setup(
                x => x.ObterTodosAvisosAsync(trackingBehavior, default)).
                ReturnsAsync(aviso.Where(x => x.Ativo == true).ToList());

            // Act
            var result = await _handler.Handle(new GetAvisosRequest(), default);

            // Assert
            result.Data.Should().BeNull();
        }
    }
}
