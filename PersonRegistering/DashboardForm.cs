using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace PersonRegistering
{
    public partial class DashboardForm : Form
    {
        private string username;
        private string role;
        private const string ApiEndpointUrl = "https://localhost/7050/api/person"; // Replace with your API endpoint URL

        public DashboardForm(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;

            label10.Text = role;

            if (role == "user")
            {
                save_btn.Visible = false;
            }

            if (gender_m.Checked)
            {
                gender_f.Checked = false;
            }

            if (checkbox_no != checkbox_yes)
            {
                checkbox_no.Checked = false;
            }

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


            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(personData), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7050/person", content);

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

        private void search_btn_Click(object sender, EventArgs e)
        {

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
    }
}
