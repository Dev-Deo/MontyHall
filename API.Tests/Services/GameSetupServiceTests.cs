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
    public class GameSetupServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GameSetupServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }


        [Fact]
        public void CreateGameSetup_ShouldReturnGameSetupDto()
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


            GameSetupCreateDto gameSetupCreateDto = new()
            {
                GameRequestId = 1,
                GameRequestNo = 1,
            };

            var gameSetup = new GameSetup
            {
                GameRequestId = gameSetupCreateDto.GameRequestId,
                GameRequestNo = gameSetupCreateDto.GameRequestNo,
                FirstDoor = "G",
                SecondDoor = "C",
                ThirdDoor = "G"
            };

            var gameSetupSave = gameSetup;
            gameSetupSave.Id = 1;

            var gameSetupDto = new GameSetupDto
            {
                Id = gameSetupSave.Id,
                GameRequestId = gameSetupSave.GameRequestId,
                GameRequestNo = gameSetupSave.GameRequestNo,
                FirstDoor = gameSetupSave.FirstDoor,
                SecondDoor = gameSetupSave.SecondDoor,
                ThirdDoor = gameSetupSave.ThirdDoor,
            };

            IGameSetupService gameSetupService = new GameSetupService(_unitOfWorkMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<GameSetup>(gameSetupCreateDto)).Returns(gameSetup);
            _mapperMock.Setup(m => m.Map<GameSetupDto>(gameSetupSave)).Returns(gameSetupDto);

            _unitOfWorkMock.Setup(s => s.GameSetup.AddAsync(gameSetup));
            _unitOfWorkMock.Setup(s => s.SaveAsync());

            // act
            var outPut = gameSetupService.CreateGameSetup(gameSetupCreateDto);

            // assert
            Assert.NotNull(outPut);
            Assert.True(outPut.Result.IsSuccess);
            //Assert.Equal(gameSetupDto, outPut.Result.Data);
        }
    }
}