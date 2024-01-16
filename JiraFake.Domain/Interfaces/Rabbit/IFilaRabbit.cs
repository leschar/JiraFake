namespace JiraFake.Domain.Interfaces.Rabbit
{
    public interface IFilaRabbit
    {
        public abstract void Processar(string mensagem);
    }
    
}
