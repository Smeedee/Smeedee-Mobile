using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var model = new BuildStatus(PROJECT, BuildSuccessState.Success, USER, DateTime.Now);
                Assert.AreEqual("#0F0", model.BuildSuccessStateString);
            }

            [Test]
            public void Should_properly_convert_failure_to_red()
            {
                var model = new BuildStatus(PROJECT, BuildSuccessState.Failure, USER, DateTime.Now);
                Assert.AreEqual("#F00", model.BuildSuccessStateString);
            }

            [Test]
            public void Should_properly_convert_unknown_to_orange()
            {
                var model = new BuildStatus(PROJECT, BuildSuccessState.Unknown, USER, DateTime.Now);
                Assert.AreEqual("#F25000", model.BuildSuccessStateString);
            }
        }
    }
}
