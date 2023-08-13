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
    public class GameRequestServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        //private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

        public GameRequestServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            //var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            //new Mock<IUserStore<ApplicationUser>>().Object,
            //new Mock<IOptions<IdentityOptions>>().Object,
            //new Mock<IPasswordHasher<ApplicationUser>>().Object,
            //new IUserValidator<ApplicationUser>[0],
            //new IPasswordValidator<ApplicationUser>[0],
            //new Mock<ILookupNormalizer>().Object,
            //new Mock<IdentityErrorDescriber>().Object,
            //new Mock<IServiceProvider>().Object,
            //new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            //userManagerMock
            //    .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            //    .Returns(Task.FromResult(IdentityResult.Success));
            //userManagerMock
            //    .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }


        [Fact]
        public void CreateGameRequest_ShouldReturnGameRequestDto()
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

            GameRequestCreateDto gameRequestCreateDto = new()
            {
                UserId = user.Id,
                TotalGameRequests = 2
            };

            var gameRequest = new GameRequest
            {
                TotalGameRequests = gameRequestCreateDto.TotalGameRequests,
                UserId = gameRequestCreateDto.UserId,
            };

            var gameRequestSave = gameRequest;
            gameRequestSave.Id = 1;

            var gameRequestDto = new GameRequestDto
            {
                Id = gameRequestSave.Id,
                UserId = gameRequestSave.UserId,
                TotalGameRequests = gameRequestSave.TotalGameRequests
            };

            IGameRequestService gameRequestService = new GameRequestService(_unitOfWorkMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<GameRequest>(gameRequestCreateDto)).Returns(gameRequest);
            _mapperMock.Setup(m => m.Map<GameRequestDto>(gameRequestSave)).Returns(gameRequestDto);

            _unitOfWorkMock.Setup(s => s.GameRequest.AddAsync(gameRequest));
            _unitOfWorkMock.Setup(s => s.SaveAsync());

            // act
            var outPut = gameRequestService.CreateGameRequest(gameRequestCreateDto);

            // assert
            Assert.NotNull(outPut);
            Assert.True(outPut.Result.IsSuccess);
            Assert.Equal(gameRequestDto, outPut.Result.Data);
        }
    }
}