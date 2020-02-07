namespace FeedbackApp_WebAPI.Models
{
    public class User
    {
        private int id;
        private string name;
        private string email;
        private string password;

        protected int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public User() : this(0, "", "", "") { }

        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}