using System.Threading;
using Microsoft.Extensions.Options;
using Orders.Host.Configurations;
using Orders.Host.Data.Entities;
using Orders.Host.Models.Dtos;

namespace Orders.UnitTests.Services;

public class ItemInfoServiceTest
{
    private readonly IItemInfoService _itemInfoService;

    private readonly Mock<IItemInfoRepository> _itemInfoRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<ItemInfoService>> _logger;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IOptionsSnapshot<OrdersConfig>> _config;
    private readonly Mock<IInternalHttpClientService> _httpClientService;
    private readonly ItemInfoEntity _testItemInfoEntity;
    private readonly ItemInfoDto _testItemInfoDto;

    public ItemInfoServiceTest()
    {
        _itemInfoRepository = new Mock<IItemInfoRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<ItemInfoService>>();
        _mapper = new Mock<IMapper>();
        _config = new Mock<IOptionsSnapshot<OrdersConfig>>();
        _httpClientService = new Mock<IInternalHttpClientService>();
        _testItemInfoEntity = new Mock<ItemInfoEntity>().Object;
        _testItemInfoEntity.Id = 1;
        _testItemInfoDto = new Mock<ItemInfoDto>().Object;
        _testItemInfoDto.Id = 1;

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _itemInfoService = new ItemInfoService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _itemInfoRepository.Object, _httpClientService.Object, _config.Object);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _itemInfoRepository.Setup(s => s.Remove(
            It.Is<int>(el => el == testId))).ReturnsAsync(testId);

        // act
        var result = await _itemInfoService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrange
        var testId = 1;
        int? badResponse = null;
        int? testResult = null;

        _itemInfoRepository.Setup(s => s.Remove(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _itemInfoService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _itemInfoRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(_testItemInfoEntity);

        _mapper.Setup(s => s.Map<ItemInfoDto>(
            It.Is<ItemInfoEntity>(i => i.Equals(_testItemInfoEntity)))).Returns(_testItemInfoDto);

        // act
        var result = await _itemInfoService.Get(testId);

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Failed()
    {
        // arrange
        var testId = 1;
        ItemInfoEntity? badResponse = null;

        _itemInfoRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _itemInfoService.Get(testId);

        // assert
        result.Should().BeNull();
    }
}