﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BlogDomainProject.Utils;

namespace BlogDomainProject.Entities
{
    public class Entity
    {
        //[Required(ErrorMessage = "Please enter a the created date")]         
        //[Column("created_date")]
        //public DateTime CreatedDate { get; set; }
         
        //[Column("last_modified_date")]
        //public DateTime LastModifiedDate { get; set; }

        //[Required(ErrorMessage = "Please enter a the created by")]
        //[Column("created_by", TypeName = "nvarchar")]
        //[StringLength(50, ErrorMessage = "Please enter a created by that is not more than 50 characters")]
        //public string CreatedBy { get; set; }

        //[Column("last_modified_by", TypeName = "nvarchar")]
        //[StringLength(50, ErrorMessage = "Please enter a last created by that is not more than 50 characters")]
        //public string LastModifiedBy { get; set; }

        //[NotMapped]
        //public EntityStateEnum EntityState { get; set; }
       
    }
}