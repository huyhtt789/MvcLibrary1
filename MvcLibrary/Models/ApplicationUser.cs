﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLibrary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String City { get; set; }

    }
}
