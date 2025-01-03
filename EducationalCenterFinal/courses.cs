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
    
    public partial class courses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public courses()
        {
            this.attendance = new HashSet<attendance>();
            this.enrollments = new HashSet<enrollments>();
            this.exams = new HashSet<exams>();
            this.payments = new HashSet<payments>();
        }
    
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string Description { get; set; }
        public string WorkOn { get; set; }
        public Nullable<System.TimeSpan> beginning { get; set; }
        public Nullable<decimal> NoOfHours { get; set; }
        public Nullable<int> teacherId { get; set; }
        public Nullable<decimal> price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<attendance> attendance { get; set; }
        public virtual teachers teachers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enrollments> enrollments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<exams> exams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<payments> payments { get; set; }
    }
}
