using AutoMapper;
using TodoRestAPI.Application.InputModels;
using TodoRestAPI.Application.ViewModels;
using TodoRestAPI.Domain.Abstractions.Repositories;
using TodoRestAPI.Domain.Entities;
using TodoRestAPI.Domain.Exceptions;

namespace TodoRestAPI.Application.Services
{
    public class TodoTaskAppService
    {
        private readonly IMapper _mapper;
        private readonly ITodoTaskRepository _todoTaskRepository;

        public TodoTaskAppService(
            IMapper mapper,
            ITodoTaskRepository todoTaskRepository)
        {
            _mapper = mapper;
            _todoTaskRepository = todoTaskRepository;
        }

        public async Task AddAsync(Guid id, TodoTaskInputModel todoTaskInput)
        {
            await _todoTaskRepository.AddAsync(TodoTask.Create(
                id,
                todoTaskInput.Title,
                todoTaskInput.Description));
        }

        public async Task<TodoTaskViewModel> GetByIdAsync(Guid id)
        { 
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
                throw new TodoTaskNotFoundException();

            return _mapper.Map<TodoTaskViewModel>(todoTask);
        }

        public async Task<IEnumerable<TodoTaskViewModel>> GetAllAsync()
        { 
            var todoTasks = await _todoTaskRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TodoTaskViewModel>>(todoTasks);
        }

        public async Task UpdateAsync(Guid id, TodoTaskInputModel todoTaskInput)
        {
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
                throw new TodoTaskNotFoundException();

            todoTask.Update(todoTaskInput.Title, todoTaskInput.Description);

            await _todoTaskRepository.UpdateAsync(todoTask);
        }

        public async Task CompleteAsync(Guid id)
        {
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
                throw new TodoTaskNotFoundException();

            if (todoTask.Completed_at != null)
                throw new TodoTaskAlreadyCompletedException();

            todoTask.Complete();

            await _todoTaskRepository.UpdateAsync(todoTask);
        }

        public async Task RemoveAsync(Guid id)
        {
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
                throw new TodoTaskNotFoundException();

            await _todoTaskRepository.RemoveAsync(todoTask);
        }
    }
}
