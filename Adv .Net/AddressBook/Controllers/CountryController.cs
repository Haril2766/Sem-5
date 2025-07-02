using System.Data;
using System.Data.SqlClient;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AddressBook.Controllers
{
    public class CountryController : Controller
    {
        #region Confi

        public IConfiguration configuration;
        public CountryController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #endregion Confi

        #region Country Add
        public IActionResult CountrySave(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.CountryId == 0)
                {
                    command.CommandText = "PR_Country_Insert";
                }
                else
                {
                    command.CommandText = "PR_Country_Update";
                    command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                }
                command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = model.CountryName;
                command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = model.CountryCode;
                command.ExecuteNonQuery();
                return RedirectToAction("CountryList");
            }
            return View("CountryAddEdit", model);
        }
        #endregion Country Add

        #region Country Edit
        public IActionResult CountryAddEdit(int CountryId)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectById";
            command.Parameters.AddWithValue("@CountryId", CountryId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            CountryModel model = new CountryModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.CountryName = @row["CountryName"].ToString();
                model.CountryCode = @row["CountryCode"].ToString();
            }
            return View("CountryAddEdit", model);
        }
        #endregion Country Edit

        #region CountryList
        public IActionResult CountryList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion CountryList

        #region  Country Delete
        public IActionResult CountryDelete(int CountryID)
        {
            string connectionstr = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_Country_Delete";
                    sqlCommand.Parameters.AddWithValue("@CountryID", CountryID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return RedirectToAction("CountryList");
        }
        #endregion Country Delete
    }
}
