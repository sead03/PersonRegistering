using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using PersonRegistrationAPI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonRegistrationAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "Login")]
        public string Login([FromBody] LoginViewModel model)
        {

            string role = "";
            string name = "";

            string connectionString = "datasource=127.0.0.1;Database=personregistering;User=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Users WHERE username = '" + model.username + "' AND password = '" + model.password + "'";
                using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            return "fail";
                        }

                        while (reader.Read())
                        {
                            role = reader["role"].ToString();
                            name = reader["username"].ToString();

                        }
                        return GenerateJwtToken(role, name);
                    }
                }

            }
            return "sucess";
        }
        private string GenerateJwtToken(string role, string username, int expirationMinutes = 60)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdasdasdasdajdajshhAKSLDASKLLKADSaddad"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, username),
            }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key.Key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
