//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlineSinavPortali.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cevap
    {
        public int cevapId { get; set; }
        public Nullable<int> cevapSecId { get; set; }
        public Nullable<int> cevapOgrId { get; set; }
        public string cevapYanit { get; set; }
    
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Secenek Secenek { get; set; }
    }
}
