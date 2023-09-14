using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PersonRegistrationAPI.Model;


namespace PersonRegistrationAPI.Controllers
{

    public class PersonController : ControllerBase
    {
        [HttpPost("person")]
        public IActionResult CreatePerson([FromBody] PersonModel newPerson)
        {
            try
            {

                string connectionString = "datasource=127.0.0.1;Database=personregistering;User=root;";
                string insertSql = "INSERT INTO person (name, surname, birthday, phone_number, gender, employed, marital_status, birthplace) VALUES (@name, @surname, @birthday, @phone_number, @gender, @employed, @marital_status, @birthplace)";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                using (MySqlCommand command = new MySqlCommand(insertSql, connection))
                {
                    connection.Open();

                    try
                    {
                        string sqlQuery = "SELECT * FROM person WHERE name = '" + newPerson.name + "' AND surname = '" + newPerson.surname + "'";


                        using (MySqlConnection connection2 = new MySqlConnection(connectionString))
                        using (MySqlCommand command2 = new MySqlCommand(sqlQuery, connection2))
                        {
                            connection2.Open();

                            using (MySqlDataReader reader = command2.ExecuteReader())
                            {

                                //Check if a row exists, if it does, run the UPDATE script, else run the INSERT script
                                if (reader.HasRows == false)
                                {
                                    try
                                    {

                                        // Set parameters for the INSERT statement
                                        command.Parameters.AddWithValue("@name", newPerson.name);
                                        command.Parameters.AddWithValue("@surname", newPerson.surname);
                                        command.Parameters.AddWithValue("@birthday", newPerson.birthday);
                                        command.Parameters.AddWithValue("@phone_number", newPerson.phoneNumber);
                                        command.Parameters.AddWithValue("@gender", newPerson.gender);
                                        command.Parameters.AddWithValue("@employed", newPerson.employed);
                                        command.Parameters.AddWithValue("@marital_status", newPerson.maritalStatus);
                                        command.Parameters.AddWithValue("@birthplace", newPerson.birthplace);


                                        // Execute the INSERT statement
                                        int rowsAffected = command.ExecuteNonQuery();
                                        return Ok();
                                    }
                                    catch (Exception ex)
                                    {
                                        // Handle exceptions (e.g., database errors)
                                        return BadRequest("Failed to save data to the database.");
                                    }
                                }


                                else
                                {
                                    if (!newPerson.isAdmin)
                                    {
                                        return Unauthorized("You dont have permission to update!");
                                    }
                                    else
                                    {
                                        insertSql = "update person SET (name, surname, birthday, phone_number, gender, employed, marital_status, birthplace) VALUES (@name, @surname, @birthday, @phone_number, @gender, @employed, @marital_status, @birthplace) WHERE name = '" + newPerson.name + "' AND surname = '" + newPerson.surname + "'";
                                        string update = "UPDATE person SET name = @name, surname = @surname, birthday=@birthday, phone_number=@phone_number, gender=@gender, employed=@employed, marital_status=@marital_status, birthplace=@birthplace Where name = @name and surname = @surname";

                                        command.Parameters.AddWithValue("@name", newPerson.name);
                                        command.Parameters.AddWithValue("@surname", newPerson.surname);
                                        command.Parameters.AddWithValue("@birthday", newPerson.birthday);
                                        command.Parameters.AddWithValue("@phone_number", newPerson.phoneNumber);
                                        command.Parameters.AddWithValue("@gender", newPerson.gender);
                                        command.Parameters.AddWithValue("@employed", newPerson.employed);
                                        command.Parameters.AddWithValue("@marital_status", newPerson.maritalStatus);
                                        command.Parameters.AddWithValue("@birthplace", newPerson.birthplace);
                                        command.CommandText = update;
                                        int rowsAffected = command.ExecuteNonQuery();


                                        return Ok();
                                    }
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }


                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // DELETE: api/resource/5
        [HttpDelete("delete{id}")]
        public IActionResult DeleteResource(int id)
        {
            try
            {
                // Connect to your MySQL database and execute a DELETE SQL statement
                string connectionString = "datasource=127.0.0.1;Database=personregistering;User=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM person WHERE id = @id";
                    using (MySqlCommand command = new MySqlCommand(deleteSql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return NoContent(); // Return a 204 No Content response if the record was successfully deleted
                        }
                        else
                        {
                            return NotFound(); // Return a 404 Not Found response if the record does not exist
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the delete operation
                return BadRequest($"Failed to delete resource. Error: {ex.Message}");
            }
        }
        [HttpGet("getperson")]
        public IActionResult GetPersonByName([FromQuery] string name)
        {
            try
            {
                string connectionString = "datasource=127.0.0.1;Database=personregistering;User=root;";
                string selectSql = "SELECT * FROM person WHERE name = @name";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                using (MySqlCommand command = new MySqlCommand(selectSql, connection))
                {
                    connection.Open();

                    try
                    {
                        // Set parameters for the SELECT statement
                        command.Parameters.AddWithValue("@name", name);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Create a PersonModel or Person object to hold the data
                                var person = new PersonModel
                                {
                                    name = reader["name"].ToString(),
                                    surname = reader["surname"].ToString(),
                                    birthday = Convert.ToDateTime(reader["birthday"]),
                                    phoneNumber = (int)reader["phone_number"],
                                    gender = (string)reader["gender"],
                                    employed = ((bool)reader["employed"]) ? 1 : 0,
                                    maritalStatus = reader["marital_status"].ToString(),
                                    birthplace = reader["birthplace"].ToString()
                                };

                                return Ok(person);
                            }
                            else
                            {
                                // Person not found
                                return NotFound();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., database errors)
                        return BadRequest("Failed to retrieve data from the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., database connection errors)
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
