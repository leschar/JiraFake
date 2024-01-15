using JiraFake.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraFake.Data.Mappings
{
    public class SubTarefaMapping : IEntityTypeConfiguration<SubTarefa>
    {
        public void Configure(EntityTypeBuilder<SubTarefa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");


            builder.ToTable("SubTarefas");

        }
    }
}
