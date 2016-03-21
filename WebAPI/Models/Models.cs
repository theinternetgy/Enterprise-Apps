using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Feature: BaseEntity
    {

        public int Project { get; set; }
        public int Module { get; set; }
        public int Page { get; set; }
        public int Parent { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }


        public string Created { get; set; }
        public string Updated { get; set; }

        public List<File> Files { get; set; }
        //public List<Table> Tables { get; set; }
    }

    public class FilterProperties: Feature
    {

    }

    public class FeatureListItem:Feature
    {
        public string StatusName { get; set; }
        public string PageName { get; set; }
        public string ModuleName { get; set; }
    }

    public class BaseCrudClass:BaseEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }
    }

    public class File : BaseCrudClass
    {
        
    }

    public class Table : BaseCrudClass
    {

    }
     
}