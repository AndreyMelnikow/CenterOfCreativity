//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CenterOfCreativity.BaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }
        public int IdAuditory { get; set; }
        public System.DateTime Date { get; set; }
        public string StartEndTime { get; set; }
    
        public virtual Auditory Auditory { get; set; }
        public virtual Event Event { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
