﻿using System.Collections.Generic;
using NUnit.Framework;
using Smeedee.Lib;

namespace Smeedee.UnitTests
{
    [TestFixture]
    public class CsvTests
    {
        [Test]
        public void Should_handle_single_item_deserialization()
        {
            var result = Csv.FromCsv("1");
            var expected = new List<string[]> { new [] { "1" } };
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Should_handle_single_line_deserialization()
        {
            var result = Csv.FromCsv("1\f2\f3");
            var expected = new List<string[]> {new [] {"1", "2", "3"}};
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Should_handle_multiline_deserialization()
        {
            var result = Csv.FromCsv("1\f2\f3\a4\f5\f6");
            var expected = new List<string[]>
                               {
                                   new [] { "1", "2", "3" },
                                   new [] { "4", "5", "6" }
                               };
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Should_handle_singleline_serialization()
        {
            var result = Csv.ToCsv(new List<string[]>
                               {
                                   new [] { "1", "2", "3" }
                               });
            Assert.AreEqual("1\f2\f3", result);
        }

        [Test]
        public void Should_handle_multiline_serialization()
        {
            var result = Csv.ToCsv(new List<string[]>
                               {
                                   new [] { "1", "2", "3" },
                                   new [] { "4", "5", "6" }
                               });
            Assert.AreEqual("1\f2\f3\a4\f5\f6", result);
        }

        [Test]
        public void Should_strip_seperators_when_serializing()
        {

            var result = Csv.ToCsv(new List<string[]>
                               {
                                   new [] { "1\f", "\a2", "\f3" },
                                   new [] { "4", "5", "\f6" }
                               });
            Assert.AreEqual("1\f2\f3\a4\f5\f6", result);
        }
    }
}
