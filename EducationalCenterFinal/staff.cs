//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationalCenterFinal
{
    using System;
    using System.Collections.Generic;
    
    public partial class staff
    {
        public int staffId { get; set; }
        public string staffName { get; set; }
        public string staffEmail { get; set; }
        public string staffPhone { get; set; }
        public string staffAddress { get; set; }
        public string role { get; set; }
        public Nullable<decimal> staffSalary { get; set; }
        public int userId { get; set; }
    
        public virtual user user { get; set; }
    }
}