//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repo
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventRegistration
    {
        public int EventRegistrationID { get; set; }
        public Nullable<int> EventID { get; set; }
        public Nullable<int> ExamineeID { get; set; }
        public Nullable<int> EventRegistrationStatusID { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual EventRegistrationStatu EventRegistrationStatu { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}