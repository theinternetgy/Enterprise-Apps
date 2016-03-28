using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models.Common
{
   
    public enum CrudTypes
    {
        Stackholders=0,
        Files = 1,
        Tables =2,
        StoredProcedures=3,
        Functions=4,
        UnitTestcases=5,
        RepositoryInfo=6,
        Queries=7
    }
}