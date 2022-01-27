namespace JustAnotherToDo.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Guid ProfileId { get; set; }
        public IList<ToDo> ToDos { get; set; }
        public UserProfile Profile { get; set; }
    }
}
