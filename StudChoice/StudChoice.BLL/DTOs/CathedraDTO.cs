﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudChoice.BLL.DTOs
{
    public class CathedraDTO
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int FacultyId { get; set; }

        public string FacultyName { get; set; }
    }
}
