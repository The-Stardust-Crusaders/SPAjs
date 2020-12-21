using SPAjs;
using SPAjs.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public class UserProfileControllerTesting : IClassFixture<TestingFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserProfileControllerTesting(TestingFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Registration_ShouldReturnJWT_IfUserIsRegistered()
        {
            var newUser = new RegisterViewModel 
            {
                UserName = "NewUser",
                UserEmail = "new@email.com",
                UserPass = "123456mM-",
                ConfirmPass = "123456mM-"
            };

            var jsonString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/UserProfile/Register/", jsonString);

            var registrationResponse = response.Content.ReadAsStringAsync().Result;
            Assert.NotNull(registrationResponse);

            var email = ParseClaimsFromJwt(registrationResponse).Single(x => x.Type == "sub").Value;
            Assert.Equal(email, newUser.UserEmail);
        }

        [Fact]
        public async Task Registration_ShoulReturnError_IfUserAlreadyExists()
        {
            var newUser = new RegisterViewModel 
            {
                UserName = "NewUser1",
                UserEmail = "new1@email.com",
                UserPass = "123456mM-",
                ConfirmPass = "123456mM-"
            };

            var jsonString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/UserProfile/Register/", jsonString);

            var registrationResponse = response.Content.ReadAsStringAsync().Result;
            Assert.NotNull(registrationResponse);

            jsonString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            response = await _client.PostAsync("api/UserProfile/Register/", jsonString);

            registrationResponse = response.Content.ReadAsStringAsync().Result;
            Assert.NotNull(registrationResponse);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Registration_ShoulReturnError_IfConfirmPasswordDifferent()
        {
            var newUser = new RegisterViewModel 
            {
                UserName = "NewUser2",
                UserEmail = "new2@email.com",
                UserPass = "123456mM-",
                ConfirmPass = "123456mM-diff"
            };

            var jsonString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/UserProfile/Register/", jsonString);

            var registrationResponse = response.Content.ReadAsStringAsync().Result;
            Assert.NotNull(registrationResponse);
            Assert.False(response.IsSuccessStatusCode);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            //keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
            keyValuePairs.TryGetValue("role", out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = System.Text.Json.JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}
