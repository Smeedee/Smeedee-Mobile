using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Model
{
    public class BuildStatus
    {
        public BuildStatus(string projectName, BuildSuccessState buildSuccessState, string username, DateTime dateTime)
        {
            ProjectName = projectName;
            BuildSuccessState = buildSuccessState;
            Username = username;

        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            private set 
            { 
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Project name was null or empty");
                _projectName = value;
            }
        }

        public BuildSuccessState BuildSuccessState { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Username was null or empty");
                _username = value;
            }
        }
    }
}
