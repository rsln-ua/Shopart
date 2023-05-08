using AutoMapper;

namespace Infrastructure.UnitTests.Mocks;

public class MockService : BaseDataService<MockDbContext>
{
    public MockService(
        IDbContextWrapper<MockDbContext> dbContextWrapper,
        ILogger<MockService> logger,
        IMapper mapper)
        : base(dbContextWrapper, logger, mapper)
    {
    }

    public async Task RunWithException()
    {
        await ExecuteSafeAsync<bool>(() => throw new Exception());
    }

    public async Task RunWithoutException()
    {
        await ExecuteSafeAsync<bool>(() => Task.FromResult(true));
    }
}