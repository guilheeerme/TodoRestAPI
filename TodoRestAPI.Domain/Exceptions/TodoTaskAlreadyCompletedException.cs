namespace TodoRestAPI.Domain.Exceptions
{
    public class TodoTaskAlreadyCompletedException : Exception
    {
        public TodoTaskAlreadyCompletedException() : base("Não foi possível concluir a ação: Tarefa já está completa.") 
        { 
        
        }
    }
}
