using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class BuildStatusTests
    {
        private const string PROJECT = "test project";
        private const string USER = "test user";
        private readonly DateTime DATE = DateTime.MinValue;
         
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_not_allow_null_in_project_name()
        {
            var model = new BuildStatus(null, BuildSuccessState.Success, USER, DATE);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_not_allow_empty_string_in_project_name()
        {
            var model = new BuildStatus("", BuildSuccessState.Success, USER, DATE);
        }
    }
}
