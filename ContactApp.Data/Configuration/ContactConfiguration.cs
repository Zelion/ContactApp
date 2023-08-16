using ContactApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactApp.Data.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasData(
                new Contact { Id = 1, FirstName = "Martin", LastName = "Paladino", Age = 30, Address = "FakeStreet 123", Phone = "12345678", Created = DateTime.Now, CreatedBy = "Seed", LastUpdate = DateTime.Now },
                new Contact { Id = 2, FirstName = "John", LastName = "Smith", Age = 45, Address = "FakeStreet 456", Phone = "345563234", Created = DateTime.Now, CreatedBy = "Seed", LastUpdate = DateTime.Now }
            );
        }
    }
}
