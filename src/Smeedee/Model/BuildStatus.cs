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
        }

        private string _projectName;
        protected string ProjectName
        {
            get { return _projectName; }
            private set 
            { 
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Project name was null or empty");
                _projectName = value;
            }
        }
    }
}
