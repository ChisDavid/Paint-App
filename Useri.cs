using System;
using System.Collections.Generic;
using System.Text;

namespace Paint
{
    class User
    {
        int id;
        string Name, Surname, password, email;
        string UserName;

        public User(string name, string surname, string password, string UserName, string email)
        {
            id = 0;
            Name1 = name;
            Surname1 = surname;
            this.Password = password;
            this.Email = email;
            this.UserName1 = UserName;
        }

        public string Name1 { get => Name; set => Name = value; }
        public string Surname1 { get => Surname; set => Surname = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
    }
}
