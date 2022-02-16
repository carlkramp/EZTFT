using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class ParticipantDto
    {   
        public int gold_left { get; set; }
        public int last_round { get; set; }
        public int level { get; set; }
        public int placement { get; set; }
        public int players_eliminated { get; set; }
        public string puuid { get; set; }
        public float time_eliminated { get; set; }
        public int total_damage_to_players { get; set; }
        public List<TraitDto> traits { get; set; }
        public List<UnitDto> units { get; set; }
    }
}