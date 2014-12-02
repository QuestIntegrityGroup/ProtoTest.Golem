﻿using System.Collections.Generic;
using System.Net;
using MbUnit.Framework;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Rest
{
    /// <summary>
    /// Example tests on how to use the REST Framework
    /// </summary>
    internal class Tests : RestTestBase
    {
        [Test]
        public void TestGetStringFromBody()
        {
            string id = Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER")
                .Then.GetStringFromBody("//CUSTOMER[10]/text()");

            Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER/" + id)
                .Then.Verify().BodyContainsText("<ID>" + id + "</ID>");
        }

        [Test]
        public void TestDynamicBody()
        {
            dynamic body = Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER")
                .Then.GetBodyAsDynamic();
            string id = body.CUSTOMERList.CUSTOMER[20];

            Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER/" + id)
                .Then.Verify().BodyContainsText("<ID>" + id + "</ID>");
        }

        [Test]
        public void TestResponseCode()
        {
            Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER/10")
                .Then.Verify().ResponseCode(HttpStatusCode.OK);
        }


        [Test]
        public void TestFailedVerification()
        {
            Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER/asdfasdfsdfasdfasdfsadf")
                .Then.Verify().ResponseCode(HttpStatusCode.OK);

            Assert.AreEqual(testData.VerificationErrors.Count, 1, "Expected 1 error");
            testData.VerificationErrors = new List<VerificationError>();
        }

        [Test]
        public void TestCRUD()
        {
            Given.Domain("http://www.thomas-bayer.com")
                .Body(
                    @"<CUSTOMER><ID>201312</ID><FIRSTNAME>Nikolai</FIRSTNAME><LASTNAME>Tesla</LASTNAME><STREET>111 Madison Avenue.</STREET><CITY>New York</CITY></CUSTOMER>")
                .When.Post("/sqlrest/CUSTOMER")
                .Then.Verify().ResponseCode(HttpStatusCode.Created);


            Given.Domain("http://www.thomas-bayer.com")
                .Body(
                    @"<CUSTOMER><ID>201312</ID><FIRSTNAME>Nikolai</FIRSTNAME><LASTNAME>Tesla</LASTNAME><STREET>111 Madison Avenue.</STREET><CITY>New York</CITY></CUSTOMER>")
                .When.Get("/sqlrest/CUSTOMER/201312")
                .Then.Verify().BodyContainsText("<ID>201312</ID>").BodyContainsText("<FIRSTNAME>Nikolai</FIRSTNAME")
                .ResponseCode(HttpStatusCode.OK);

            Given.Domain("http://www.thomas-bayer.com")
                .Body(
                    @"<CUSTOMER><ID>201312</ID><FIRSTNAME>Einstein</FIRSTNAME><LASTNAME>Albert</LASTNAME><STREET>111 Madison Avenue.</STREET><CITY>New York</CITY></CUSTOMER>")
                .When.Put("/sqlrest/CUSTOMER")
                .Then.Verify().BodyContainsText("<FIRSTNAME>Einstein</FIRSTNAME>")
                .BodyContainsText("<LASTNAME>Albert</LASTNAME>");

            Given.Domain("http://www.thomas-bayer.com")
                .When.Delete("/sqlrest/CUSTOMER/201312")
                .Then.Verify().ResponseCode(HttpStatusCode.OK);

            Given.Domain("http://www.thomas-bayer.com")
                .When.Get("/sqlrest/CUSTOMER/201312")
                .Then.Verify().ResponseCode(HttpStatusCode.NotFound);
        }
    }
}