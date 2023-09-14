using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonRegistering
{
    public partial class PersonList : Form
    {
        public PersonList()
        {
            InitializeComponent();
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
                        string apiUrl = $"https://localhost:7050/api/person/delete{id}";

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
    }
}
