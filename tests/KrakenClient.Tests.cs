﻿using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using Xunit;

namespace KrakenCore.Tests
{
    public partial class KrakenClientTests : IDisposable
    {
        private readonly KrakenClient _client;

        public KrakenClientTests()
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot config = configBuilder.Build();

            string apiKey = config["ApiKey"];

            string privateKey = config["PrivateKey"];

            if (!Enum.TryParse(config["AccountTier"], out AccountTier accountTier))
                accountTier = AccountTier.Unknown;

            _client = new KrakenClient(apiKey, privateKey, accountTier);
        }

        public void Dispose() => _client.Dispose();

        [DebuggerHidden]
        private void AssertNotDefault<T>(T value) => Assert.NotEqual(default(T), value);
    }
}