using static TodoRestAPI.Test.Generator.TodoTaskDataGenerator;

namespace TodoRestAPI.Test.Generator
{
    public class TodoTaskAppServiceGenerator
    {
        public static IEnumerable<object[]> GetTodoTaskData()
        {
            yield return new object[]
            {
                _todoTaskID,
                GetTodoTaskInputModel()
            };
        }

        public static IEnumerable<object[]> GetTodoTaskUpdateData()
        {
            yield return new object[]
            {
                _todoTaskID,
                GetTodoTaskInputModel(),
                GetTodoTaskUpdateInputModel()
            };
        }
    }
}
