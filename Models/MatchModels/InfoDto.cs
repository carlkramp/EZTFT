﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZTFT.Models
{
    public class InfoDto
    {
        public long game_datetime { get; set; }
        public float game_length { get; set; }
        public string game_variation { get; set; }
        public string game_version { get; set; }
        public List<ParticipantDto> participants { get; set; }
        public int queue_id { get; set; }
        public int tft_set_number { get; set; }
    }
}