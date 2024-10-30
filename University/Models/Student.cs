namespace University.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Enrollment = new HashSet<Enrollment>();
        }

        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        public string Phone { get; set; }

        // Nueva propiedad para la eliminación lógica
        public bool IsDeleted { get; set; } = false; // Inicializar en falso

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
