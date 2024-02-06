using Microsoft.AspNetCore.Identity;
using Schedule.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Entities
{
    public class DbUser : IdentityUser<int>
    {
		public int id_group { get; set; }
		public string PIB { get; set; }
    }
}
