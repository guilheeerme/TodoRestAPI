using Moq;
using Xunit;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TodoRestAPI.Application.Services;
using TodoRestAPI.Domain.Abstractions.Repositories;
using TodoRestAPI.Domain.Entities;
using TodoRestAPI.Application.Mapper;
using TodoRestAPI.Application.InputModels;
using TodoRestAPI.Application.ViewModels;
using TodoRestAPI.Test.Generator;

namespace TodoRestAPI.Test.Application
{
    public class TodoTaskAppServiceTest
    {
        private readonly TodoTaskAppService _todoTaskAppService;
        private readonly List<TodoTask> _todoTasksData;

        public TodoTaskAppServiceTest()
        {
            _todoTasksData = new List<TodoTask>();

            var todoTaskRepository = new Mock<ITodoTaskRepository>();

            #region Setup

            todoTaskRepository.Setup(t => t.AddAsync(It.IsAny<TodoTask>()))
                .Callback((TodoTask todoTask) =>
                {
                    _todoTasksData.Add(todoTask);
                }).Returns(Task.FromResult(0));

            todoTaskRepository.Setup(t => t.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    return _todoTasksData.FirstOrDefault(t => t.Id == id);
                });

            todoTaskRepository.Setup(t => t.GetAllAsync())
                .ReturnsAsync(() =>
                {
                    return _todoTasksData.ToList();
                });

            todoTaskRepository.Setup(t => t.UpdateAsync(It.IsAny<TodoTask>()))
                .Callback((TodoTask todoTask) =>
                {
                    _todoTasksData.RemoveAll(t => t.Id == todoTask.Id);
                    _todoTasksData.Add(todoTask);
                }).Returns(Task.FromResult(0));

            todoTaskRepository.Setup(t => t.RemoveAsync(It.IsAny<TodoTask>()))
                .Callback((TodoTask todoTask) =>
                {
                    _todoTasksData.RemoveAll(t => t.Id == todoTask.Id);
                }).Returns(Task.FromResult(0));

            #endregion

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(typeof(TodoTaskMapperProfile));
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();

            _todoTaskAppService = new TodoTaskAppService(mapper, todoTaskRepository.Object);
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task AddAsyncTest(Guid id, TodoTaskInputModel input)
        {
            //Arrange
           
            //Act
            await _todoTaskAppService.AddAsync(id, input);
            var todoTask = _todoTasksData.FirstOrDefault(t => t.Id == id);

            //Assert
            AssertTodoTask(id, input, todoTask);
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task GetByIdAsyncTest(Guid id, TodoTaskInputModel input)
        {
            //Arrange
            await _todoTaskAppService.AddAsync(id, input);

            //Act
            var todoTask = await _todoTaskAppService.GetByIdAsync(id);

            //Assert
            AssertTodoTask(id, input, todoTask);
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task GetAllAsync(Guid id, TodoTaskInputModel input)
        {
            //Arrange
            await _todoTaskAppService.AddAsync(id, input);

            //Act
            var todoTask = await _todoTaskAppService.GetAllAsync();

            //Assert
            AssertTodoTask(id, input, todoTask.FirstOrDefault());
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskUpdateData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task UpdateAsync(Guid id, TodoTaskInputModel input, TodoTaskInputModel inputUpdate) 
        {
            //Arrange
            await _todoTaskAppService.AddAsync(id, input);

            //Act
            await _todoTaskAppService.UpdateAsync(id, inputUpdate);
            var todoTask = _todoTasksData.FirstOrDefault(t => t.Id == id);

            // Assert
            AssertTodoTask(id, inputUpdate, todoTask);
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task CompleteAsync(Guid id, TodoTaskInputModel input)
        {
            //Arrange
            await _todoTaskAppService.AddAsync(id, input);

            //Act
            await _todoTaskAppService.CompleteAsync(id);
            var todoTask = _todoTasksData.FirstOrDefault(t => t.Id == id);

            //Assert
            AssertTodoTaskCompleted(id, input, todoTask);
        }

        [Theory]
        [MemberData(nameof(TodoTaskAppServiceGenerator.GetTodoTaskData), MemberType = typeof(TodoTaskAppServiceGenerator))]
        public async Task RemoveAsync(Guid id, TodoTaskInputModel input) 
        {
            //Arrange
            await _todoTaskAppService.AddAsync(id, input);

            //Act
            await _todoTaskAppService.RemoveAsync(id);
            var todoTask = _todoTasksData.FirstOrDefault(t => t.Id == id);

            //Assert
            Assert.Null(todoTask);
        }

        private void AssertTodoTask(Guid id, TodoTaskInputModel input, TodoTask todoTask)
        {
            Assert.Equal(id, todoTask.Id);
            Assert.Equal(input.Title, todoTask.Title);
            Assert.Equal(input.Description, todoTask.Description);
            Assert.Null(todoTask.Completed_at);
        }

        private void AssertTodoTask(Guid id, TodoTaskInputModel input, TodoTaskViewModel todoTask)
        {
            Assert.Equal(id, todoTask.Id);
            Assert.Equal(input.Title, todoTask.Title);
            Assert.Equal(input.Description, todoTask.Description);
            Assert.Null(todoTask.Completed_at);
        }

        private void AssertTodoTaskCompleted(Guid id, TodoTaskInputModel input, TodoTask todoTask)
        {
            Assert.Equal(id, todoTask.Id);
            Assert.Equal(input.Title, todoTask.Title);
            Assert.Equal(input.Description, todoTask.Description);
            Assert.NotNull(todoTask.Completed_at);
        }
    }
}
