using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace PersonRegistering
{
    public partial class DashboardForm : Form
    {
        private string username;
        private string role;
        private const string ApiEndpointUrl = "https://localhost/7050/api/person"; // Replace with your API endpoint URL
        myDbConnection con = new myDbConnection();
        MySqlCommand command;
        MySqlDataAdapter mySqlDataAdapter;
        DataTable dataTable;


        public DashboardForm(string username, string role)
        {
            InitializeComponent();

            con.Connect();
            string mySqlcon = "Datasource = 127.0.0.1;database=personregistering;user = root;password=;";

            this.username = username;
            this.role = role;

            label10.Text = role;
            label11.Text = username;

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
                    var response = await httpClient.PostAsync("https://localhost:7050/person/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Data successfully submitted to the API.");
                        ClearInputFields();
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

        public void search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                con.cn.Open();
                command = new MySqlCommand("Select * from person", con.cn);
                command.ExecuteNonQuery();
                dataTable = new DataTable();
                mySqlDataAdapter = new MySqlDataAdapter(command);
                mySqlDataAdapter.Fill(dataTable);

                // Create an instance of the PersonList form
                PersonList personListForm = new PersonList();

                // Pass the DataTable to the PersonList form
                personListForm.SetDataSource(dataTable);

                // Show the PersonList form
                personListForm.Show();

                con.cn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
