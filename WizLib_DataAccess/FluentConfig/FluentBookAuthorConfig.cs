using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<Fluent_BookAuthor>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthor> builder)
        {
            builder.HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            builder.HasOne(ba => ba.Fluent_Book)
                .WithMany(b => b.Fluent_BookAuthor)
                .HasForeignKey(ba => ba.Book_Id);

            builder.HasOne(ba => ba.Fluent_Author)
                .WithMany(a => a.Fluent_BookAuthor)
                .HasForeignKey(ba => ba.Author_Id);
        }
    }
}
