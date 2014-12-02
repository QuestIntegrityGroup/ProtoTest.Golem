﻿using System;
using System.Collections.Generic;
using System.Net;
using MbUnit.Framework;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.Proxy;

namespace ProtoTest.Golem.Rest
{
    /// <summary>
    /// Test Base class to be inherited by all test fixtures.  Will automatically instantiate an object named Given
    /// </summary>
    public class RestTestBase : TestBase
    {
        protected IDictionary<string, string> Tokens;

        protected Given Given
        {
            get
            {
                WebProxy proxy = null;
                if (Config.Settings.httpProxy.startProxy)
                {
                    proxy = new WebProxy("localhost:" + TestBase.proxy.proxyPort);
                }
                return new Given(proxy);
            }
        }

        private void LogHttpTrafficMetrics()
        {
            //if (Config.Settings.httpProxy.startProxy)
            //{
            //    TestBase.proxy.GetSessionMetrics();
            //    TestLog.BeginSection("HTTP Metrics");
            //    TestLog.WriteLine("Number of Requests : " + TestBase.proxy.numSessions);
            //    TestLog.WriteLine("Min Response Time : " + TestBase.proxy.minResponseTime);
            //    TestLog.WriteLine("Max Response Time : " + TestBase.proxy.maxResponseTime);
            //    TestLog.WriteLine("Avg Response Time : " + TestBase.proxy.avgResponseTime);
            //    TestLog.End();
            //}
        }

        private void GetHTTPTrafficInfo()
        {
            //if (Config.Settings.httpProxy.startProxy)
            //{
            //    string name = Common.GetShortTestName(80);
            //    TestBase.proxy.SaveSessionsToFile();
            //    TestLog.Attach(new BinaryAttachment("HTTP_Traffic_" + name + ".saz",
            //        "application/x-fiddler-session-archive", File.ReadAllBytes(TestBase.proxy.GetSazFilePath())));

            //    LogHttpTrafficMetrics();

            //    TestBase.proxy.ClearSessionList();
            //}
        }

        private void StartProxy()
        {
            try
            {
                if (Config.Settings.httpProxy.startProxy)
                {
                    proxy = new BrowserMobProxy();
                    proxy.StartServer();
                    proxy.CreateProxy();
                    proxy.CreateHar();
                }
            }
            catch (Exception)
            {
            }
        }

        private void QuitProxy()
        {
            if (Config.Settings.httpProxy.startProxy)
            {
                proxy.QuitServer();
            }
        }
        [NUnit.Framework.SetUp]
        [SetUp]
        public void SetUp()
        {
            Tokens = new Dictionary<string, string>();
            StartProxy();
        }

        [NUnit.Framework.TearDown]
        [TearDown]
        public void TearDown()
        {
            QuitProxy();
            GetHTTPTrafficInfo();
        }
    }
}