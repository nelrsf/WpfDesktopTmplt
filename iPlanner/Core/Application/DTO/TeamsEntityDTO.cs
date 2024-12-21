﻿using System.Collections.ObjectModel;

namespace iPlanner.Core.Application.DTO
{
    public class TeamBaseDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public TeamBaseDTO(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class TeamMemberDTO : TeamBaseDTO
    {
        public TeamMemberDTO(string id, string name) : base(id, name)
        {
            Position = "";
        }

        public string? Position { get; set; }

    }

    public class TeamDTO : TeamBaseDTO
    {

        public string? Description { get; set; }
        public TeamMemberDTO? Leader { get; set; }
        public ObservableCollection<TeamMemberDTO>? Members { get; set; }

        public TeamDTO(string id, string name) : base(id, name)
        {
            Members = new ObservableCollection<TeamMemberDTO>();
        }

    }

}
