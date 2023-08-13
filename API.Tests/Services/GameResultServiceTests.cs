using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Moq;
using Services;
using Shared;
using Shared.DTO;

namespace API.Tests.Services
{
    public class GameResultServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GameResultServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }


        [Fact]
        public void CreateGameResult_ShouldReturnGameResultDto()
        {
            // arrange
            var user = new ApplicationUser
            {
                Id = new Guid(),
                Email = "admin" + SD.Domain,
                UserName = "admin" + SD.Domain,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true,
            };

            GameRequest gameRequest = new()
            {
                Id = 1,
                UserId = user.Id,
                TotalGameRequests = 2
            };

            GameSetup gameSetup = new()
            {
                Id = 1,
                GameRequestId = gameRequest.Id,
                GameRequestNo = 1,
                FirstDoor = "G",
                SecondDoor = "C",
                ThirdDoor = "G"
            };

            GameResultCreateDto gameResultCreateDto = new()
            {
                GameSetupId = gameSetup.Id,
                FirstChoice = 3,
            };

            GameResult gameResult = new()
            {
                GameSetupId = gameResultCreateDto.GameSetupId,
                FirstChoice = gameResultCreateDto.FirstChoice,
                SecondChoice = 0,
                OpenedDoorNo = 2
            };

            GameResult gameResultSave = gameResult;
            gameResultSave.Id = 1;

            GameResultDto gameResultDto = new GameResultDto
            {
                Id = gameResultSave.Id,
                GameSetupId = gameResultSave.GameSetupId,
                FirstChoice = gameResultSave.FirstChoice,
                SecondChoice = gameResultSave.SecondChoice,
                OpenedDoorNo = gameResultSave.OpenedDoorNo,
                IsWin = true
            };

            IGameResultService gameResultService = new GameResultService(_unitOfWorkMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<GameResult>(gameResultCreateDto)).Returns(gameResult);
            _mapperMock.Setup(m => m.Map<GameResultDto>(gameResultSave)).Returns(gameResultDto);

            _unitOfWorkMock.Setup(s => s.GameResult.AddAsync(gameResult));
            _unitOfWorkMock.Setup(s => s.SaveAsync());

            // act
            var outPut = gameResultService.CreateGameResult(gameResultCreateDto);

            // assert
            Assert.NotNull(outPut);
            Assert.True(outPut.Result.IsSuccess);
            Assert.Equal(gameResultDto, outPut.Result.Data);
        }
    }
}