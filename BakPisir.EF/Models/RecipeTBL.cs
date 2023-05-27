//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BakPisir.EF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecipeTBL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RecipeTBL()
        {
            this.StepTBL = new HashSet<StepTBL>();
            this.SubTransitionTBL = new HashSet<SubTransitionTBL>();
        }
    
        public int recipeId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> categoryId { get; set; }
        public string recipeVideoUrl { get; set; }
        public string ingredients { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public string cookingTime { get; set; }
        public Nullable<System.DateTime> recipeDate { get; set; }
        public string recipeName { get; set; }
        public string recipeImageUrl { get; set; }
    
        public virtual CategoryTBL CategoryTBL { get; set; }
        public virtual UserTBL UserTBL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StepTBL> StepTBL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubTransitionTBL> SubTransitionTBL { get; set; }
    }
}
