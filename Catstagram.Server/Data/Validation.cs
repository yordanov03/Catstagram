﻿namespace Catstagram.Server.Data
{
    public class Validation
    {
        public class Cat
        {
            public const int MaxDescriptionLength = 2000;
        }

        public class User
        {
            public const int MaxNameLength = 40;
            public const int MaxBiographyDescription = 150;
        }
    }
}
