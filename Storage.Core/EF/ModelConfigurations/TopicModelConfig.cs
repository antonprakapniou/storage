namespace Storage.Core.EF.ModelConfigurations;

public sealed class TopicModelConfig : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder
            .ToTable("Topics")
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
