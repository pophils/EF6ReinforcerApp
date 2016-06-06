
namespace BlogDomainProject.Utils
{
    public enum EntityStateEnum
    {
        Added = 1,
        Modified = 2,
        Unchanged = 3,
        Deleted = 4
    }

    public struct UserRoleStruct
    {
        public static string Admin
        {
            get { return "admin"; }
        }

        public static string Author
        {
            get { return "author"; }
        }

        public static string Reader
        {
            get { return "reader"; }
        }
    }
}
