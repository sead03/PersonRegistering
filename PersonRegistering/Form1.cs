using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace PersonRegistering
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string apiUrl = "https://localhost:7050/WeatherForecast"; // Replace with your API endpoint
                    string response = webClient.DownloadString(apiUrl);

                }
                catch (WebException ex)
                {
                    // Handle API call errors
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            using (var httpClient = new HttpClient())
            {
                string role = "";
                string username = "";
                var loginRequest = new { Username = username_txt.Text, Password = password_txt.Text };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:7050/User", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent != "fail")
                    {
                        // Your JWT token
                        string jwtToken = responseContent;

                        // Define a token handler
                        var tokenHandler = new JwtSecurityTokenHandler();

                        try
                        {
                            // Read the JWT token
                            var token = tokenHandler.ReadJwtToken(jwtToken);

                            // Access the claims
                            var claims = token.Claims;

                            role = claims.Where(x => x.Type == "role").Select(x => x.Value).FirstOrDefault();
                            username = claims.Where(x => x.Type == "unique_name").Select(x => x.Value).FirstOrDefault();

                        }
                        catch (SecurityTokenException ex)
                        {
                            // Token validation failed
                            Console.WriteLine($"Token validation failed: {ex.Message}");
                        }

                        if (role == "admin")
                        {

                            DashboardForm dashboardForm = new DashboardForm(username, role) { };
                            dashboardForm.Visible = true;

                        }
                        else if (role == "user")
                        {
                            DashboardForm dashboardForm = new DashboardForm(username, role) { };
                            dashboardForm.Visible = true;
                        }

                    }
                    else
                    {
                        label1.Text = "wrong password or username";
                    }


                }

            }
        }
        public class AuthenticationResponse
        {
            public string Token { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

