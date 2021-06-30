﻿using AppBase.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Models
{
    public class BaseVM
    {
        public int Id { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsOpt1Enabled { get; set; }
        public bool IsOpt2Enabled { get; set; }
        public bool IsOpt3Enabled { get; set; }
    }
}