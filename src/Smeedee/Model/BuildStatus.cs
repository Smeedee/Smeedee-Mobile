using System;

namespace Smeedee.Model
{
    public class BuildStatus : IModel
    {
        public BuildStatus(string projectName, BuildSuccessState buildSuccessState, string username, DateTime buildTime)
        {
            BuildTime = buildTime;
            BuildSuccessState = buildSuccessState;
            ProjectName = projectName;
            Username = username;
        }

        public DateTime BuildTime { get; private set; }
        public BuildSuccessState BuildSuccessState { get; private set; }

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

        // TODO: Make this individual based on platform?
        public static int SuccessStateColor(int state)
        {
            switch (state)
                {
                    case (int)Model.BuildSuccessState.Success:
                        return 0xf00ff00;
                    case (int)Model.BuildSuccessState.Failure:
                        return 0xfff0000;
                    default:
                        return 0xff25000;
                }
        }
    }
}
