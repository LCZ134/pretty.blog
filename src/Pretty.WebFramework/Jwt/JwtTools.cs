using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Jwt
{
    public class JwtTools
    {
        public static string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encode(Dictionary<string, object> pairs,int day=1, string key = null)
        {
            pairs.Add("createon", DateTime.Now.AddDays(day));

            key = string.IsNullOrEmpty(key) ? secret : key;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();

            IJsonSerializer serializer = new JsonNetSerializer();

            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(pairs, key);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Decodes(string token, string key = null)
        {
            key = string.IsNullOrEmpty(key) ? secret : key;

            try
            {   
                IJsonSerializer serializer = new JsonNetSerializer();

                IDateTimeProvider provider = new UtcDateTimeProvider();

                IJwtValidator validator = new JwtValidator(serializer, provider);

                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, key, verify: true);

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if ((DateTime)result["createon"] < DateTime.Now)
                    throw new Exception("过期");
                result.Remove("timeout");
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }

}
