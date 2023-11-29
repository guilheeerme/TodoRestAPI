using Microsoft.EntityFrameworkCore;
using TodoRestAPI.Domain.Abstractions.Repositories;
using TodoRestAPI.Domain.Entities;
using TodoRestAPI.Infrastructure.Context;

namespace TodoRestAPI.Repository.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private AppDbContext _appDbContext;

        public TodoTaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(TodoTask entity)
        { 
            await _appDbContext.TodoTasks.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<TodoTask> GetByIdAsync(Guid id)
        {
            var todoTask = await _appDbContext
                .TodoTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(todoTask => todoTask.Id == id);

            return todoTask;
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            var todoTasks = await _appDbContext
                .TodoTasks
                .AsNoTracking()
                .ToListAsync();

            return todoTasks;
        }

        public async Task UpdateAsync(TodoTask entity)
        { 
            _appDbContext.TodoTasks.Update(entity);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(TodoTask entity) 
        {
            _appDbContext.TodoTasks.Remove(entity);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
