using AutoMapper;
using TodoRestAPI.Application.ViewModels;
using TodoRestAPI.Domain.Entities;

namespace TodoRestAPI.Application.Mapper
{
    public class TodoTaskMapperProfile : Profile
    {
        public TodoTaskMapperProfile() 
        {
            TodoTaskEntityToViewModel();
        }

        private void TodoTaskEntityToViewModel() 
        {
            CreateMap<TodoTask, TodoTaskViewModel>();
        }
    }
}
