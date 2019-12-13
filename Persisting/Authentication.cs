﻿using System;
using System.Threading.Tasks;
using Pixeval.Data.Web;
using Pixeval.Data.Web.Delegation;
using Pixeval.Data.Web.Protocol;
using Pixeval.Data.Web.Request;
using Pixeval.Objects;
using Refit;

namespace Pixeval.Persisting
{
    public class Authentication
    {
        private const string ClientHash = "28c1fdd170a5204386cb1313c7077b34f83e4aaf4aa829ce78c231e05b0bae2c";

        private static string UtcTimeNow => DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss+00:00");

        public static async Task Authenticate(string name, string pwd)
        {
            var time = UtcTimeNow;
            var hash = Cipher.Md5Hex(time + ClientHash);

            var token = await RestService.For<ITokenProtocol>(HttpClientFactory.PixivApi(ProtocolBase.OAuthBaseUrl))
                .GetToken(new TokenRequest {Name = name, Password = pwd}, time, hash);

            Identity.Global = Identity.Parse(pwd, token);
        }
    }
}