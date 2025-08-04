namespace ViltrapportenApi.Modal
{
    public class ApiRoutes
    {
        public const string Version = "v1";
        public static class Auth
        {
            public const string Login = $"{Version}/Auth/login";
            public const string Refresh = $"{Version}/Auth/refresh";
            public const string Logout = $"{Version}/Auth/logout";
        }
        
        public static class Main
        {
            public const string UserUnit = $"{Version}/Main/userunit";
            public const string ChildNode = $"{Version}/Main/childNode";
        }
        
        public static class LandRoute
        {
            public const string Municipality = $"{Version}/land/municipality";
            public const string Land = $"{Version}/land/land";
            public const string LandDetail = $"{Version}/land/landdetail";
            public const string LandOwners = $"{Version}/land/landowners";
            public const string OwnersLand = $"{Version}/land/ownersland";
            public const string SharedOwnersLand = $"{Version}/land/sharedownersland";
            public const string ArchiveLand = $"{Version}/land/archiveLand";
            public const string Owners = $"{Version}/land/owners";
            public const string OwnerDetail = $"{Version}/land/ownerdetail";
           // public const string OwnershipType = $"{Version}/land/ownershiptype";
            public const string FilteredUser = $"{Version}/land/filtereduser";
        }
        
        public static class Map
        {
            public const string BaseMap = $"{Version}/map/basemap";
            public const string Maps = $"{Version}/map/map";
            public const string SelectedMap = $"{Version}/map/SelectedMap";
            public const string SaveDrawnFeatures = $"{Version}/map/savedrawnfeatures";
            public const string SaveModifiedLandFeatures = $"{Version}/map/savemodifiedlandfeatures";
            public const string DeleteLandLayer = $"{Version}/map/deleteLandLayer";
            public const string GetDrawnFeatures = $"{Version}/map/features";
            public const string UnitLandLayers = $"{Version}/map/unitlandlayers";
            public const string UnitOwnersLandLayers = $"{Version}/map/unitownerslandlayers";
        }
    }
}
