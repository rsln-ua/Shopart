using System.Threading;
using Orders.Host.Data.Entities;
using Orders.Host.Models.Dtos;

namespace Orders.UnitTests.Services;

public class OrderServiceTest
{
    private readonly IOrdersService _orderService;

    private readonly Mock<IItemInfoService> _itemInfoService;
    private readonly Mock<IOrderRepository> _orderRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrdersService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly OrderEntity _orderEntity;
    private readonly OrderDto _orderDto;
    private readonly ItemInfoEntity _testItemInfoEntity;
    private readonly ItemInfoDto _testItemInfoDto;

    public OrderServiceTest()
    {
        _orderRepository = new Mock<IOrderRepository>();
        _itemInfoService = new Mock<IItemInfoService>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrdersService>>();
        _mapper = new Mock<IMapper>();
        _testItemInfoEntity = new Mock<ItemInfoEntity>().Object;
        _testItemInfoEntity.Id = 1;
        _testItemInfoDto = new Mock<ItemInfoDto>().Object;
        _testItemInfoDto.Id = 1;
        _orderEntity = new Mock<OrderEntity>().Object;
        _orderEntity.Id = 1;
        _orderEntity.Items = new Mock<List<ItemInfoEntity>>().Object;
        _orderDto = new Mock<OrderDto>().Object;
        _orderDto.Id = 1;
        _orderDto.Items = new Mock<List<ItemInfoDto>>().Object;

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _orderService = new OrdersService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _orderRepository.Object, _itemInfoService.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _orderRepository.Setup(
            s => s.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>())).ReturnsAsync(testId);

        _orderRepository.Setup(
                s => s.Update(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>()))
            .ReturnsAsync(testId);

        // act
        var result = await _orderService.Add("1", new Mock<List<BaseItemInfoDto>>().Object);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        // arrange
        int? badResponse = null;
        int? testResult = null;

        _orderRepository.Setup(
            s => s.Add(
                It.IsAny<string>(),
                It.IsAny<decimal>())).ReturnsAsync(badResponse);

        // act
        var result = await _orderService.Add("1", new Mock<List<BaseItemInfoDto>>().Object);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _orderRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(_orderEntity);

        _orderRepository.Setup(s => s.Remove(
            It.Is<int>(el => el == testId))).ReturnsAsync(testId);

        // act
        var result = await _orderService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrange
        var testId = 1;

        int? testResult = null;

        _orderRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync((OrderEntity?)null);

        // act
        var result = await _orderService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Success()
    {
        // arrange
        var testId = 1;
        var testResult = testId;

        _orderRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(_orderEntity);

        _mapper.Setup(s => s.Map<OrderDto>(
            It.Is<OrderEntity>(i => i.Equals(_orderEntity)))).Returns(_orderDto);

        // act
        var result = await _orderService.Get(testId);

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(testResult);
    }

    [Fact]
    public async Task Get_Failed()
    {
        // arrange
        var testId = 1;
        OrderEntity? badResponse = null;

        _orderRepository.Setup(s => s.Get(
            It.Is<int>(el => el == testId))).ReturnsAsync(badResponse);

        // act
        var result = await _orderService.Get(testId);

        // assert
        result.Should().BeNull();
    }
}