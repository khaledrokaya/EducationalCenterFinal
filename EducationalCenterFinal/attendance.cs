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
    
    public partial class attendance
    {
        public int attendanceId { get; set; }
        public int studentId { get; set; }
        public int courseId { get; set; }
        public System.DateTime attendanceDate { get; set; }
        public Nullable<bool> isPresent { get; set; }
    
        public virtual courses courses { get; set; }
        public virtual students students { get; set; }
    }
}
