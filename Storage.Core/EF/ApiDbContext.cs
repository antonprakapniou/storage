namespace Storage.Core.EF;

public sealed class ApiDbContext:DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Book> Books { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new AuthorModelConfig())
            .ApplyConfiguration(new TopicModelConfig())
            .ApplyConfiguration(new BookModelConfig());
    }
}
