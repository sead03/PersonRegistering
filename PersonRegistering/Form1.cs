using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

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

                            DashboardForm dashboardForm = new DashboardForm(username, role) {};
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

        //string pass = password_txt.Text;
        //string name = username_txt.Text;

        //string apiUrl = "https://localhost:7050/User"; // Replace with the actual URL

        //try
        //{
        //    // Create an instance of HttpClient
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        // Define the POST data as an object
        //        var postData = new
        //        {
        //            username = name,
        //            password = pass
        //        };

        //        // Serialize the POST data object to JSON
        //        string postDataJson = JsonConvert.SerializeObject(postData);

        //        // Set the Content-Type header
        //        httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

        //        // Create a StringContent with the JSON data
        //        var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");

        //        // Send the POST request and get the response
        //        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

        //        // Ensure a successful response status code (e.g., 200 OK)
        //        response.EnsureSuccessStatusCode();

        //        // Read the response content
        //        string responseContent = await response.Content.ReadAsStringAsync();

        //        if (responseContent != "fail")
        //        {
        //            // Close the current login form
        //            this.Visible = false;

        //            // Create and show the authenticated form
        //            DashboardForm authenticatedForm = new DashboardForm();
        //            authenticatedForm.ShowDialog();
        //        }
        //        else
        //        {
        //            // Handle authentication failure
        //            // For example, display an error message
        //            MessageBox.Show("Login failed. Please check your username and password.");
        //        }

        //        // Process the response here
        //        Console.WriteLine("Response: " + responseContent);
        //    }
        //}
        //catch (HttpRequestException ex)
        //{
        //    // Handle any exceptions or errors that may occur during the request
        //    Console.WriteLine("Error: " + ex.Message);
        //}
    }
}

