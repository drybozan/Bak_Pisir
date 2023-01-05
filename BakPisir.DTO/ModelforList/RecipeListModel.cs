﻿using BakPisir.CORE.Paging;
using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.DTO.ModelforList
{
    public class RecipeListModel : BasePageableModel
    {
        public IList<RecipeDto> Items { get; set; }
    }
}
