using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDomainProject.Logic;
using BlogDomainProject.Interface;

namespace BlogDomainProject
{ 
    public static class DomainModule
    {
        public static IBlogDomain blogDomainFactory()
        {
            return new DomainService();
        }
    }
}
