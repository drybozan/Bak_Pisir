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
    
    public partial class SubTransitionTBL
    {
        public int categoryTransitionId { get; set; }
        public Nullable<int> subCategoryId { get; set; }
        public Nullable<int> recipeId { get; set; }
        public Nullable<bool> isDelete { get; set; }
    
        public virtual RecipeTBL RecipeTBL { get; set; }
        public virtual SubCategoryTBL SubCategoryTBL { get; set; }
    }
}
