﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Journal.ViewModels.Controller.WorkDays
{
    public class CreateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [Required]
        public int JournalId { get; set; }
    }
}