using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.Entities.Config
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {


        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();


            builder.ToTable("Company");


        }
    }
}
