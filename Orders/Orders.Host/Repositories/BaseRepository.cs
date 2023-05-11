using Orders.Host.Data;

namespace Orders.Host.Repositories;

public abstract class BaseRepository<T>
{
    protected BaseRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<T> logger)
    {
        DbContext = dbContext;
        Mapper = mapper;
        Logger = logger;
    }

    protected ApplicationDbContext DbContext { get; set; }

    protected IMapper Mapper { get; set; }
    protected ILogger<T> Logger { get; set; }
}