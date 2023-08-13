using AutoMapper;
using Domain.Interfaces.Repositories;
using System.ComponentModel.Design;
using Moq;
using Domain.Interfaces;
using Services;

namespace API.Test.Services
{
    public class GameRequestServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;

        public GameRequestServiceTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async void CreateGameRequest_ShouldCreateGameRequest()
        {
            // arrange
            Guid userId = new Guid();
            GameRequestCreateDto gameRequestCreateDto = new()
            {
                userId = userId,
                
            };
            IGameRequestService gameRequestService = new GameRequestService(_unitOfWork.Object, _mapper.Object);

            // act
            var result = allowanceService.GetAllowancesFromFileImport(viewModel);

            // assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }
    }
}