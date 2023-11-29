namespace TodoRestAPI.Domain.Exceptions
{
    public class TodoTaskNotFoundException : Exception
    {
        public TodoTaskNotFoundException() : base("Não foi possível concluir a ação: Tarefa não encontrada.")
        {

        }
    }
}
