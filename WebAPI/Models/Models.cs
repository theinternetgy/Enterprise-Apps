using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class Feature: BaseEntity
    {
        public Guid GUID { get; set; }
        public int Project { get; set; }
        public int Module { get; set; }
        public int Page { get; set; }
        public int Parent { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }


        public string Created { get; set; }
        public string Updated { get; set; }
        public List<Stackholder> Stackholders { get; set; }
        public List<File> Files { get; set; }
        public List<Table> Tables { get; set; }
        public List<StoredProcedure> StoredProcedures { get; set; }
        public List<Function> Functions { get; set; }
        public List<UnitTestCase> UnitTestCases { get; set; }
        public List<RepositoryItem> RepositoyItems { get; set; }
        public List<OtherInfoItem> OtherInfoItems { get; set; }
        public List<Log> Logs {get; set;}
        public List<StoryPoint> StoryPoints { get; set; }
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
        public bool Active { get; set; }
        public int FeatureId { get; set; }
    }

    public interface IBaseCrud
    {
        int Id { get; }
        string Name { get; }
        string Info { get; }
        bool Active { get; }
        int FeatureId { get; }
    }

    public class File : BaseCrudClass
    {
        
    }

    public class Table : BaseCrudClass
    {

    }

    public class StoredProcedure : BaseCrudClass
    {

    }

    public class Function : BaseCrudClass
    {

    }

    public class Stackholder : BaseCrudClass, IBaseCrud
    {

    }

    public class UnitTestCase : BaseCrudClass
    {

    }

    public class RepositoryItem : BaseCrudClass
    {

    }

    public class OtherInfoItem : BaseCrudClass
    {

    }

    public class Log : BaseCrudClass
    {
        public string Date { get; set; }
    }

    public class StoryPoint : BaseCrudClass
    {
        public string Date { get; set; }
    }
}