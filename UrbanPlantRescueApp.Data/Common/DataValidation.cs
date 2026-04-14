using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanPlantRescueApp.Data.Common
{
    public static class DataValidation
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
    }
}
