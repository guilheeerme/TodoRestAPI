using System.ComponentModel.DataAnnotations;

namespace TodoRestAPI.Application.InputModels
{
    public class TodoTaskInputModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
