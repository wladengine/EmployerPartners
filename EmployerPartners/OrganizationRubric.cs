//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployerPartners
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrganizationRubric
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int RubricId { get; set; }
        public string Author { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual Rubric Rubric { get; set; }
    }
}
