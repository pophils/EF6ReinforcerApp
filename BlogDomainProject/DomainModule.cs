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
