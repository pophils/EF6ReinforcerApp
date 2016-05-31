using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomainProject.Utils;

namespace BlogDomainProject.Interface
{
    public interface IEntity
    {        
        DateTime CreatedDate { get; set; } 

        DateTime LastModifiedDate { get; set; }         

        string CreatedBy { get; set; } 

        string LastModifiedBy { get; set; } 

        EntityStateEnum EntityState { get; set; }
        byte[] RowVersion { get; set; }
    }
}
