using Bernhoeft.GRT.Teste.Api.Controllers.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers.v1
{
    public class AvisosControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly AvisosController _controller;

        public AvisosControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(IMediator))).Returns(_mediatorMock.Object);
            _controller = new AvisosController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAviso_ShouldReturnOk_WhenAvisoExists()
        {
            // Arrange
            var avisoId = 1;
            var avisoResponse = new GetAvisosResponse { Id = avisoId, Titulo = "Test", Mensagem = "Test message", Ativo = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAvisoRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(OperationResult<GetAvisosResponse>.ReturnOk(avisoResponse));

            // Act
            var result = await _controller.GetAviso(avisoId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(avisoResponse);
        }

        [Fact]
        public async Task CreateAviso_ShouldReturnCreated_WhenAvisoIsCreated()
        {
            // Arrange
            var createRequest = new CreateAvisoRequest { Titulo = "Test", Mensagem = "Test message" };
            var avisoResponse = new GetAvisosResponse { Id = 1, Titulo = "Test", Mensagem = "Test message", Ativo = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAvisoRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(OperationResult<GetAvisosResponse>.ReturnOk(avisoResponse));

            // Act
            var result = await _controller.CreateAviso(createRequest, CancellationToken.None);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>().Which.Value.Should().BeEquivalentTo(avisoResponse);
        }

        [Fact]
        public async Task CreateAviso_ShouldReturnErrorValidation_WhenAvisoIsCreated_TituloIsEmpty()
        {
            // Arrange
            var createRequest = new CreateAvisoRequest { Titulo = "", Mensagem = "Test message" };


            // Act
            var result = await _controller.CreateAviso(createRequest, CancellationToken.None);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(new[] { "Título não pode ser vazio." });
        }

        [Fact]
        public async Task UpdateAviso_ShouldReturnOk_WhenAvisoIsUpdated()
        {
            // Arrange
            var avisoId = 1;
            var updateRequest = new UpdateAvisoRequest
            {
                Id = avisoId,
                Mensagem = "Test message",

            };
            var avisoResponse = new GetAvisosResponse { Id = avisoId, Titulo = "Test", Mensagem = "Test message", Ativo = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAvisoRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(OperationResult<GetAvisosResponse>.ReturnOk(avisoResponse));

            // Act
            var result = await _controller.UpdateAviso(avisoId, updateRequest, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(avisoResponse);
        }

        [Fact]
        public async Task DeleteAviso_ShouldReturnOk_WhenAvisoIsDeleted()
        {
            // Arrange
            var avisoId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAvisoRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(OperationResult<object>.ReturnNoContent());

            // Act
            var result = await _controller.DeleteAviso(avisoId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }


    }
}
