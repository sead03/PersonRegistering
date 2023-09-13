using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PersonRegistrationAPI.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace PersonRegistrationAPI.Controllers
{
    public class PersonController : Controller
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
                                    command.CommandText= update;
                                    int rowsAffected = command.ExecuteNonQuery();


                                    return Ok();
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
    }
}
