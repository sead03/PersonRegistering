using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PersonRegistering.Model;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace PersonRegistering
{
    public partial class DashboardForm : Form
    {
        private string username;
        private string role;
        private const string ApiEndpointUrl = "https://localhost/7050/person/add"; 

        public DashboardForm(PersonModel person)
        {
            InitializeComponent();

            if (person.gender == "M")
            {
                gender_m.Checked = true;
            }
            else if (person.gender == "F")
            {
                gender_f.Checked = true;
            }

            // Set employment checkbox based on the person's employment status
            checkbox_yes.Checked = person.employed;

            // Set TextBox values based on the person data
            name_txt.Text = person.name;
            surname_txt.Text = person.surname;
            birthday_txt.Value = person.birthday;
            phone_txt.Value = person.phoneNumber;
            gender_f.Equals(person.gender);
            gender_m.Equals(person.gender);
            marital_combo.Text = person.maritalStatus;
            birthplace_txt.Text = person.birthplace;
            label10.Text = person.role;
            label11.Text = person.username;
            this.username = person.username;
            this.role = person.role;

            label10.Text = role;
            label11.Text = username;
            string mySqlcon = "Datasource = 127.0.0.1;database=personregistering;user = root;password=;";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_no.Checked)
            {
                checkbox_yes.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            var personData = new
            {
                isAdmin = label10.Text == "admin",
                name = name_txt.Text,
                surname = surname_txt.Text,
                birthday = birthday_txt.Value,
                phoneNumber = Int64.Parse(phone_txt.Text),
                gender = gender_m.Checked ? "M" : "F", // Assuming you have "Male" and "Female" radio buttons
                employed = checkbox_yes.Checked ? 1 : 0,
                maritalStatus = marital_combo.SelectedItem.ToString(),
                birthplace = birthplace_txt.Text
            };


            if (string.IsNullOrWhiteSpace(personData.name) ||
                string.IsNullOrWhiteSpace(personData.surname) ||
                personData.birthday == null ||
                personData.phoneNumber == null ||
                string.IsNullOrWhiteSpace(personData.gender) ||
                personData.maritalStatus == null ||
                string.IsNullOrWhiteSpace(personData.birthplace))
            {
                MessageBox.Show("Please fill in all the required fields.");
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(personData), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7050/person", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Data successfully submitted to the API.");
                        ClearInputFields();
                    }
                    else if (response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
                    {
                        MessageBox.Show("The user is unauthorized");
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit data to the API.");
                    }
                }
            }

        }
        private void ClearInputFields()
        {
            name_txt.Clear();
            surname_txt.Clear();
            phone_txt.Value = 0;
            birthplace_txt.Clear();
            gender_m.Checked = false;
            gender_f.Checked = false;
            checkbox_yes.Checked = false;
            checkbox_no.Checked = false;
            marital_combo.SelectedIndex = -1;
            birthday_txt.Value = DateTime.Now;
        }

        private void checkbox_yes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_yes.Checked)
            {
                checkbox_no.Checked = false;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void marital_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gender_m_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void date_txt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void birthplace_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void surname_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void name_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void delete_btn_Click(object sender, EventArgs e)
        {

        }

        public async void search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an HttpClient to send the GET request to your API
                using (HttpClient client = new HttpClient())
                {
                    // Define the base URL of your API
                    string apiUrl = "https://localhost:7050/all";

                    // Send the GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    this.Visible = false;
                    // Check if the response is successful (HTTP status code 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the JSON response content
                        string json = await response.Content.ReadAsStringAsync();

                        // Parse the JSON data into a DataTable
                        DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                        // Create an instance of the PersonList form
                        PersonList personListForm = new PersonList(label11.Text, label10.Text);

                        // Pass the DataTable to the PersonList form
                        personListForm.SetDataSource(dataTable);

                        // Show the PersonList form
                        personListForm.Show();
                    }
                    else
                    {
                        // Handle non-successful HTTP status codes if needed
                        MessageBox.Show("Failed to retrieve data from the API: " + response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
