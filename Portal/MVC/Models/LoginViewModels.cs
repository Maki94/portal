﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    // u imenima klasa sam stavio Member jer je bez toga zauzeto vec
    // sredice se tamo kad izbacimo onaj njihov AccountViewModels.cs
    public class MemberLoginViewModel
    {
        [Required]
        [Display(Name = "Gmail")]
        [EmailAddress]
        public string Gmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}