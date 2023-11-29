using TodoRestAPI.Abstractions.Entities;

namespace TodoRestAPI.Domain.Entities
{
    public class TodoTask : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Completed_at { get; set; }

        protected TodoTask(Guid id, string title, string description) 
        {
            Id = id;
            Title = title;
            Description = description;
            Created_at = DateTime.Now;
            Updated_at = DateTime.Now;
        }

        public static TodoTask Create(Guid id, string title, string description) 
        {
            return new TodoTask(id, title, description);
        }

        public void Update(string title, string description) 
        {
            Title = title;
            Description = description;
            Updated_at = DateTime.Now;
        }

        public void Complete()
        {
            Completed_at = DateTime.Now;
            Updated_at = DateTime.Now;
        }
    }
}
