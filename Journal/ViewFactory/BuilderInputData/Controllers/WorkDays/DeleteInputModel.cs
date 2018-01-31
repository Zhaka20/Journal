﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journal.ViewModels.Controller.WorkDays
{
    public class DeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int JournalId { get; set; }
    }
}