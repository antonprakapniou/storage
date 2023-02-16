namespace Storage.Core.EF.ModelConfigurations;

public sealed class BookModelConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .ToTable("Books")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .HasOne(_ => _.Author)
            .WithMany(_ => _.Books)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(_ => _.Topic)
            .WithMany(_ => _.Books)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();
    }
}
