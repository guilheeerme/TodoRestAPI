namespace TodoRestAPI.Abstractions.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
