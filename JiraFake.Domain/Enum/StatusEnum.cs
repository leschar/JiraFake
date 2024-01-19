using System.ComponentModel;

namespace JiraFake.Domain.Enum
{
    /// <summary>
    /// Representa o status da tarefa.
    /// </summary>
    public enum StatusEnum
    {
        Fechado = 0,
        Aberto = 1,
        ParaFazer = 2,
        EmProgresso = 3,
        EmTestes = 4,
        TestesFinalizados = 5,
        Concluido = 6
    }
}
