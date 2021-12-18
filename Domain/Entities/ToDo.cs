namespace JustAnotherToDo.Domain.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProfileId { get; set; }
        public Category Category { get; set; }
        public UserProfile Profile { get; set; }
    }
}
