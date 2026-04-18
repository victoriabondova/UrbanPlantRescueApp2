namespace UrbanPlantRescueApp.Data.Common
{
    public class DataValidation
    {
        public static class Category
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 100;
        }
        public static class Plant
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1500;
        }
        public static class Comment
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 500;
        }
        public static class PlantCondition
        {
            public const int NotesMinLength = 2;
            public const int NotesMaxLength = 1000;
        }
        public static class UserProfile
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 50;
            public const int LastNameMinLength = 4;
            public const int LastNameMaxLength = 50;
            public const int BioMinLength = 10;
            public const int BioMaxLength = 500;
        }
    }
}