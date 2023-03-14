using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web.Http;
using getAPIUsers.Models;

namespace getAPIUsers.Controllers
{
    public class TokenController : ApiController
    {
        private static int secondLenght = 300;
        private static string client_id_String = "bOZdxAfCvWZkYdD82sfcp56cOL0a";
        private static string client_secret_String = "d5C3PmbGfiyS2dIShOafxxonFIAa";
        private static string grant_type_String = "client_credentials";
        private static string secretKeyString = "MbQeThWmZq4t7w!z";
        private static readonly ObjectCache cache = MemoryCache.Default;

        [HttpPost]
        public IHttpActionResult GenerateToken(Credentials credentials)
        {
            if (credentials.client_id == client_id_String
                && credentials.client_secret == client_secret_String
                && credentials.grant_type == grant_type_String)
            {
                TokenCreate tokenCreate = new TokenCreate();
                string serializedTokenCreate = (string)cache["tokenCreate"];

                if (serializedTokenCreate != null)
                {
                    tokenCreate = JsonSerializer.Deserialize<TokenCreate>(serializedTokenCreate);
                    if (countDownExpired(tokenCreate) < 0)
                    {
                        tokenCreate = GenerateAccessToken();
                        cache["tokenCreate"] = JsonSerializer.Serialize(tokenCreate);

                    }
                }
                else
                {
                    tokenCreate = GenerateAccessToken();
                    cache["tokenCreate"] = JsonSerializer.Serialize(tokenCreate);
                }

                TokenOutput tokenOutput = new TokenOutput();
                tokenOutput.bearerString = tokenCreate.bearerString;
                var sec = countDownExpired(tokenCreate);
                tokenOutput.secondLength = Convert.ToInt32(countDownExpired(tokenCreate));
                return Ok(new { tokenOutput });
            }
            else
            {
                return Unauthorized();
            }
        }

        private double countDownExpired(TokenCreate tokenCreate)
        {
            var deltaSecond = (DateTime.Now - tokenCreate.createDate).TotalSeconds;
            return tokenCreate.secondLength - deltaSecond;
        }

        private TokenCreate GenerateAccessToken()
        {
            var h = new JwtSecurityTokenHandler();
            var now = DateTime.Now;
            var token = h.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("client_id", client_id_String),
                    new Claim("client_secret", client_secret_String ),
                    new Claim("grant_type",grant_type_String )
                }),
                Expires = now.AddSeconds(secondLenght),
                //Subject = id,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString)), SecurityAlgorithms.HmacSha256)
            });
            TokenCreate tokenCreate = new TokenCreate();
            tokenCreate.createDate = DateTime.Now;
            tokenCreate.bearerString = h.WriteToken(token);
            tokenCreate.secondLength = secondLenght;

            return tokenCreate;



        }

    }

    public class TokenOutput
    {
        public string bearerString { get; set; }
        //public DateTime createDate { get; set; }
        public int secondLength { get; set; }
    }
    public class Credentials
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }
}
