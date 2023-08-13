using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Moq;
using Services;
using Shared.DTO;

namespace API.Tests.Services
{
    public class GameRequestServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;

        public GameRequestServiceTests()
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
                UserId = userId,
                TotalGameRequests = 2
            };
            GameRequestDto gameRequestDto = new()
            {
                Id = 1,
                UserId = userId,
                TotalGameRequests = 2
            };
            IGameRequestService gameRequestService = new GameRequestService(_unitOfWork.Object, _mapper.Object);
            
            // act
            var outPut = gameRequestService.CreateGameRequest(gameRequestCreateDto);

            // assert
            Assert.NotNull(outPut);
            Assert.Equal(gameRequestDto, outPut.Result.Data);
        }
    }
}