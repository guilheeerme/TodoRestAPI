using TodoRestAPI.Application.InputModels;

namespace TodoRestAPI.Test.Generator
{
    public class TodoTaskDataGenerator
    {
        public static readonly Guid _todoTaskID = Guid.NewGuid();

        public static TodoTaskInputModel GetTodoTaskInputModel()
        {
            return new TodoTaskInputModel()
            {
                Title = "Tarefa teste 1",
                Description = "Descrição da tarefa teste 1"
            };
        }

        public static TodoTaskInputModel GetTodoTaskUpdateInputModel()
        {
            return new TodoTaskInputModel()
            {
                Title = "Tarefa teste 1 atualizada",
                Description = "Descrição da tarefa teste 1 atualizada"
            };
        }
    }
}
