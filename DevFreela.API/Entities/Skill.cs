﻿namespace DevFreela.API.Entities
{
    public class Skill : BaseEntity
    {
        protected Skill()
        {
        }
        public Skill(string description)
            : base()
        {
            Description = description;
        }
        public string Description { get; private set; }
    }
}
