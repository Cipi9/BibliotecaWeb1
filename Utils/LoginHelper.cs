using PA_project.Models;
using System;
using System.Collections.Generic;


namespace PA_project.Utils
{
    public class LoginHelper
    {
        private static LoginHelper loginHelper;
        private static readonly object lockObject = new object();

        private int userId;
        private string username;
        private List<Collection> existingCollections;
        private int currentCollectionId;

        private LoginHelper()
        {
            // Private constructor to prevent instantiation outside of the class
        }

        public static LoginHelper Instance
        {
            get
            {
                // Double-check locking to ensure thread safety
                if (loginHelper == null)
                {
                    lock (lockObject)
                    {
                        if (loginHelper == null)
                        {
                            loginHelper = new LoginHelper();
                        }
                    }
                }
                return loginHelper;
            }
        }

        public int UserId { get; set; } = -1;

        public string Username { get; set; }

        public List<Collection> ExistingCollections { get; set; }

        public int CurrentCollectionId { get; set; }
    }
}
