namespace JiraFake.Domain.AppSettings
{
    public class RabbitMqSettings
    {
        public Dictionary<string, FilaConfiguracao> Filas { get; set; }
        public string ConnectionString { get; set; }
    }

    public class FilaConfiguracao
    {
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public string ConnectionString { get; set; }
    }

}
