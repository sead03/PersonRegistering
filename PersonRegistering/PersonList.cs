using PersonRegistering.Model;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonRegistering
{
    public partial class PersonList : Form
    {
        string username;
        string role;
        public PersonList(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;
            if (role == "user") { delete_btn.Visible = false; }
        }
        public void SetDataSource(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void PersonList_Load(object sender, EventArgs e)
        {

        }

        private async void delete_btn_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the unique identifier (e.g., ID) of the selected record
                int id = Convert.ToInt32(selectedRow.Cells["id"].Value);

                // Send an HTTP DELETE request to your API to delete the record
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        string apiUrl = $"https://localhost:7050/delete{id}";

                        HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            // Delete was successful
                            // Remove the selected row from the DataGridView
                            dataGridView1.Rows.Remove(selectedRow);

                            MessageBox.Show("Record deleted successfully.");
                        }
                        else
                        {
                            // Handle the case where the DELETE request was not successful
                            MessageBox.Show("Failed to delete the record. Check your API or backend.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the HTTP request
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the values from the selected row in DataGridView
                string name = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();

                // Make an HTTP GET request to fetch the person data
                var person = await FetchPersonFromApiAsync(name);

                if (person != null)
                {
                    // Create an instance of the DashboardForm and pass the person data
                    person.username = username;
                    person.role = role;
                    DashboardForm dashboardForm = new DashboardForm(person);
                    dashboardForm.Visible = true;
                }
                else
                {
                    MessageBox.Show("Person not found.");
                }
            }
        }
        private async Task<PersonModel> FetchPersonFromApiAsync(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://localhost:7050/getperson?name={name}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var person = await response.Content.ReadAsAsync<PersonModel>();
                    return person;
                }
                else
                {
                    return null;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            PersonModel person = new PersonModel()
            {
                username = username,
                role = role,
                birthday = DateTime.Now
            };

            if (person != null)
            {
                this.Visible = false;
                DashboardForm dashboardForm = new DashboardForm(person);
                dashboardForm.Visible = true;
            }
        }
    }
}
