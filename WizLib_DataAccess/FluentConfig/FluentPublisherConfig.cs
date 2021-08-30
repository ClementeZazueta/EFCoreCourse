using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> builder)
        {
            builder.HasKey(p => p.Publisher_Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Location)
                .IsRequired();
        }
    }
}
