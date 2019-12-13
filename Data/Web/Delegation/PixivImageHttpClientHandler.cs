﻿using System.Net.Http;

namespace Pixeval.Data.Web.Delegation
{
    public class PixivImageHttpClientHandler : DnsResolvedHttpClientHandler
    {
        public static HttpMessageHandler Instance = new PixivImageHttpClientHandler();

        private PixivImageHttpClientHandler() : base(PixivAuthenticationHttpRequestHandler.Instance)
        {
        }

        protected override DnsResolver DnsResolver { get; set; } = PixivImageDnsResolver.Instance;
    }
}