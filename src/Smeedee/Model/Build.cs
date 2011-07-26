using System;
using System.Collections.Generic;

namespace Smeedee.Model
{
    public class Build
    {
        public Build(string projectName, BuildState buildSuccessState, string username, DateTime buildTime)
        {
            Guard.NotNullOrEmpty(projectName, username);
            Guard.NotNull(buildSuccessState, buildTime);
            BuildTime = buildTime;
            BuildSuccessState = buildSuccessState;
            ProjectName = projectName;
            Username = username;
        }

        public DateTime BuildTime { get; private set; }
        public BuildState BuildSuccessState { get; private set; }
        public string ProjectName { get; private set; }
        public string Username { get; set; }
    }


    public class BuildTimeComparer : IComparer<Build>
    {
        public int Compare(Build x, Build y)
        {
            if (x.BuildTime == y.BuildTime) return 0;

            return x.BuildTime < y.BuildTime ? 1 : -1;
        }
    }

    public class BuildNameComparer : IComparer<Build>
    {
        public int Compare(Build x, Build y)
        {
            return string.Compare(x.ProjectName, y.ProjectName);
        }
    }
}
