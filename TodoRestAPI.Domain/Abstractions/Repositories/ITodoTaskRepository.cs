using TodoRestAPI.Abstractions.Interfaces.Repository;
using TodoRestAPI.Domain.Entities;

namespace TodoRestAPI.Domain.Abstractions.Repositories
{
    public interface ITodoTaskRepository : IRepositoryBase<TodoTask>
    {

    }
}
