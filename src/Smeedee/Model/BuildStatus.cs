using System;

namespace Smeedee.Model
{
    public class BuildStatus : IModel
    {
        public BuildStatus(string projectName, BuildSuccessState buildSuccessState, string username, DateTime buildTime)
        {
            Guard.NotNullOrEmpty(projectName, username);
            Guard.NotNull(buildSuccessState, buildTime);
            BuildTime = buildTime;
            BuildSuccessState = buildSuccessState;
            ProjectName = projectName;
            Username = username;
        }

        public DateTime BuildTime { get; private set; }
        public BuildSuccessState BuildSuccessState { get; private set; }
        public string ProjectName { get; private set; }
        public string Username { get; private set; }
    }
}
