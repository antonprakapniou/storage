namespace Storage.Core.EF.ModelConfigurations;

public sealed class AuthorModelConfig : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .ToTable("Authors")
            .HasKey(_ => _.Id)
            .HasName("Id");

        builder
            .Property(_ => _.Id)
            .HasColumnName("Id");

        builder
            .Property(_ => _.Name)
            .HasColumnName("Name")
            .IsRequired();
    }
}
