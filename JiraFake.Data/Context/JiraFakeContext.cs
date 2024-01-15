using FluentValidation.Results;
using JiraFake.Data.Extensions;
using JiraFake.Domain.Interfaces.Common;
using JiraFake.Domain.Mediator;
using JiraFake.Domain.Messages.Events;
using JiraFake.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;


namespace JiraFake.Data.Context
{
    public class JiraFakeContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ILogger<JiraFakeContext> _logger;
        public JiraFakeContext(DbContextOptions<JiraFakeContext> options,
                            IMediatorHandler mediator,
                            ILogger<JiraFakeContext> logger) : base(options)
        {
            _mediatorHandler = mediator;
            _logger = logger;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _logger.LogInformation("Entrou no DbContext");
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<SubTarefa> SubTarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ToSnakeCaseNames();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JiraFakeContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }
}
