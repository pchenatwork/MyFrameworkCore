﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Models
{
    public class EmployeeVM : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
