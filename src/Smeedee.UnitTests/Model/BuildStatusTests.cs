using System;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    public class BuildStatusTests
    {
        protected const string PROJECT = "test project";
        protected const string USER = "test user";
        protected readonly DateTime DATE = DateTime.MinValue;

        [TestFixture]
        public class In_general : BuildStatusTests
        {
            [Test]
            public void Should_implement_IModel()
            {
                Assert.True(typeof (IModel).IsAssignableFrom(typeof (BuildStatus)));
            }
        }


        [TestFixture]
        public class When_instantiating : BuildStatusTests
        {
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

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_null_in_username()
            {
                var model = new BuildStatus(PROJECT, BuildSuccessState.Success, null, DATE);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_empty_string_in_username()
            {
                var model = new BuildStatus(PROJECT, BuildSuccessState.Success, "", DATE);
            }

        }

        [TestFixture]
        public class When_converting_color_to_string
        {
            [Test]
            public void Should_properly_convert_successful_to_green()
            {
                Assert.AreEqual(0xf00ff00, BuildStatus.SuccessStateColor((int)BuildSuccessState.Success));
            }

            [Test]
            public void Should_properly_convert_failure_to_red()
            {
                Assert.AreEqual(0xfff0000, BuildStatus.SuccessStateColor((int)BuildSuccessState.Failure));
            }

            [Test]
            public void Should_properly_convert_unknown_to_orange()
            {
                Assert.AreEqual(0xff25000, BuildStatus.SuccessStateColor((int)BuildSuccessState.Unknown));
            }
        }
    }
}
