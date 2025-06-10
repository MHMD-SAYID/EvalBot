
namespace GraduationProject.Entities.Config;

public class Q_AConfiguration : IEntityTypeConfiguration<Q_A>
{
    public void Configure(EntityTypeBuilder<Q_A> builder)
    {

        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn(1, 1);

        //builder.Property(x=>x.audioLink).
       // builder.Property(i => i.level)
       //     .HasConversion(x=>x.ToString(), x => Enum.Parse<Level>(x));

       // //builder.Property(x => x.Question)
       // //.HasConversion(
       // //tags => string.Join(",", tags),         // Convert List<string> to a comma-separated string
       // //tags => tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList())
       // //.IsRequired();


       // builder.Property(i => i.Question)
       //      .HasConversion(
       //          new ValueConverter<ICollection<string>, string>(
       //              v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),           // Convert List<string> to JSON string
       //              v => JsonSerializer.Deserialize<ICollection<string>>(v, (JsonSerializerOptions)null) ?? new List<string>() // Convert JSON string to List<string>
       //          ))
       //      .IsRequired();
       // builder.Property(i => i.Answer)
       //.HasConversion(
       //    new ValueConverter<ICollection<string>, string>(
       //        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),  // Convert collection to string
       //        v => JsonSerializer.Deserialize<ICollection<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()  // Deserialize back to collection
       //    ))
       //.IsRequired();

       

        //builder.Property(x => x.Answer)
        //.HasConversion(
        //tags => string.Join(",", tags),         // Convert List<string> to a comma-separated string
        //tags => tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList())
        //    .IsRequired();  // Convert comma-separated string back to List<string>



        builder.ToTable("Q_A");
    
    }

}
