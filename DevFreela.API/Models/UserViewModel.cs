﻿using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<string> Skills { get; private set; }

        public static UserViewModel FromEntity(User entity)
        {
            var skills = entity.Skills.Select(us => us.Skill.Description).ToList();

            return new UserViewModel(entity.FullName, entity.Email, entity.BirthDate, skills);
        }

    }
}