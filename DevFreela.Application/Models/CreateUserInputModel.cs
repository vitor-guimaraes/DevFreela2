using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class CreateUserInputModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //public InsertUserCommand(int id, string fullName, string email, string password, string role)
        //{
        //    Id = id;
        //    FullName = fullName;
        //    Email = email;
        //    Password = password;
        //    Role = role;
        //}

        public User ToEntity()
            => new(FullName, Email);
    }

}
