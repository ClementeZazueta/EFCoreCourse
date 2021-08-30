using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> builder)
        {
            builder.HasKey(b => b.BookDetail_Id);

            builder.Property(b => b.NumberOfChapters)
                .IsRequired();
        }
    }
}
