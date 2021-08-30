using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> builder)
        {
            builder.HasKey(b => b.Book_Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(b => b.Price)
                .IsRequired();

            #region One to one relationship between book and book detail
            builder.HasOne(b => b.Fluent_BookDetail)
                .WithOne(bd => bd.Fluent_Book)
                .HasForeignKey<Fluent_Book>("BookDetail_Id");
            #endregion

            #region One to many relationship between book and publisher
            builder.HasOne(b => b.Fluent_Publisher)
                .WithMany(fp => fp.Fluent_Books)
                .HasForeignKey(b => b.Publisher_Id);
            #endregion
        }
    }
}
