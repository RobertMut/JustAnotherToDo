namespace JustAnotherToDo.Domain.Entities
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public ICollection<ToDo> ToDos { get; set; }
        //public ICollection<Category> Categories { get; set; }
    }
}
