﻿using System.Collections.Generic;
using Gallio.Framework;
using MbUnit.Framework;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Tests
{
    class VerificationTests : TestBase
    {
        [Test]
        public void TestVerificationCount()
        {
            Assert.AreEqual(testData.VerificationErrors.Count,0);
            AddVerificationError("Test error");
            Assert.AreEqual(testData.VerificationErrors.Count, 1);
            testData.VerificationErrors = new List<VerificationError>();
        }

        [Test]
        public void TestAssertionCount()
        {
            Assert.AreEqual(TestContext.CurrentContext.AssertCount,0);
            AddVerificationError("Test Error");
            Assert.AreEqual(TestContext.CurrentContext.AssertCount, 2);
            testData.VerificationErrors = new List<VerificationError>();
        }

    }
}
