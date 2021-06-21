using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsOpt1Enabled { get; set; }
        public bool IsOpt2Enabled { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDT { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDT { get; set; }
    }
}
