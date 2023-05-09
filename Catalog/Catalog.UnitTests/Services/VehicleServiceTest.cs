using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly IVehicleService _catalogService;

    private readonly Mock<IVehicleRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<VehicleService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly VehicleEntity _testVehicleEntity;
    private readonly VehicleDto _testVehicleDto;
    private readonly BasketItemDto _basketItemDto;

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<IVehicleRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<VehicleService>>();
        _mapper = new Mock<IMapper>();
        _testVehicleEntity = new Mock<VehicleEntity>().Object;
        _testVehicleEntity.Id = 1;
        _testVehicleDto = new Mock<VehicleDto>().Object;
        _testVehicleDto.Id = 1;
        _basketItemDto = new Mock<BasketItemDto>().Object;
        _basketItemDto.Id = 1;

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new VehicleService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<float>(),
            It.IsAny<float>())).ReturnsAsync(testId);

        // act
        var result = await _catalogService.Add(_testVehicleEntity.MakeId, _testVehicleEntity.ModelId, _testVehicleEntity.Vin, _testVehicleEntity.Year, _testVehicleEntity.Cylinders, _testVehicleEntity.EngineSizeL, _testVehicleEntity.Mileage);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        // arrange
        int? badResponse = null;
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<float>(),
            It.IsAny<float>())).ReturnsAsync(badResponse);

        // act
        var result = await _catalogService.Add(_testVehicleEntity.MakeId, _testVehicleEntity.ModelId, _testVehicleEntity.Vin, _testVehicleEntity.Year, _testVehicleEntity.Cylinders, _testVehicleEntity.EngineSizeL, _testVehicleEntity.Mileage);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _catalogItemRepository.Setup(s => s.Update(
            It.Is<int>(el => el == testId),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<float>(),
            It.IsAny<float>())).ReturnsAsync(testId);

        // act
        var result = await _catalogService.Update(testId, _testVehicleEntity.MakeId, _testVehicleEntity.ModelId, _testVehicleEntity.Vin, _testVehicleEntity.Year, _testVehicleEntity.Cylinders, _testVehicleEntity.EngineSizeL, _testVehicleEntity.Mileage);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        // arrange
        var testId = 1;
        int? badResponse = null;
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Update(
            It.Is<int>(el => el == testId),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<float>(),
            It.IsAny<float>())).ReturnsAsync(badResponse);

        // act
        var result = await _catalogService.Update(testId, _testVehicleEntity.MakeId, _testVehicleEntity.ModelId, _testVehicleEntity.Vin, _testVehicleEntity.Year, _testVehicleEntity.Cylinders, _testVehicleEntity.EngineSizeL, _testVehicleEntity.Mileage);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _catalogItemRepository.Setup(s => s.Remove(
            It.Is<int>(el => el == testId))).ReturnsAsync(testId);

        // act
        var result = await _catalogService.Remove(testId);

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

        _catalogItemRepository.Setup(s => s.Remove(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _catalogService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _catalogItemRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(_testVehicleEntity);

        _mapper.Setup(s => s.Map<VehicleDto>(
            It.Is<VehicleEntity>(i => i.Equals(_testVehicleEntity)))).Returns(_testVehicleDto);

        // act
        var result = await _catalogService.Get(testId);

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Failed()
    {
        // arrange
        var testId = 1;
        VehicleEntity? badResponse = null;

        _catalogItemRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _catalogService.Get(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemInfo_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _catalogItemRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(_testVehicleEntity);

        _mapper.Setup(s => s.Map<BasketItemDto>(
            It.Is<VehicleEntity>(i => i.Equals(_testVehicleEntity)))).Returns(_basketItemDto);

        // act
        var result = await _catalogService.GetItemInfo(testId);

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testResult);
    }

    [Fact]
    public async Task GetItemInfo_Failed()
    {
        // arrange
        var testId = 1;
        VehicleEntity? badResponse = null;

        _catalogItemRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _catalogService.GetItemInfo(testId);

        // assert
        result.Should().BeNull();
    }
}