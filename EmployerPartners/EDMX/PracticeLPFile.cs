//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployerPartners.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class PracticeLPFile
    {
        public int Id { get; set; }
        public int PracticeLPId { get; set; }
        public int DocTypeId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileData { get; set; }
        public System.DateTime DateLoad { get; set; }
        public double FileSizeKBytes { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual DocType DocType { get; set; }
        public virtual PracticeLP PracticeLP { get; set; }
    }
}
