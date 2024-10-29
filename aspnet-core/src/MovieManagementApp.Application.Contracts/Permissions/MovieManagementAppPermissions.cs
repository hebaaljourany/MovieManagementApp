namespace MovieManagementApp.Permissions;

public static class MovieManagementAppPermissions
{
    public const string GroupName = "MovieManagementApp";

    public static class Movies
    {
        public const string Default = GroupName + ".Movies";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Actors
    {
        public const string Default = GroupName + ".Actors";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Categories
    {
        public const string Default = GroupName + ".Categories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    
    public static class MyLists
    {
        public const string Default = GroupName + ".MyLists";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Ratings
    {
        public const string Default = GroupName + ".Ratings";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}

