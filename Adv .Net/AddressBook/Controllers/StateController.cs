using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class StateController : Controller
    {
        #region Confi

        public IConfiguration configuration;
        public StateController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #endregion Confi

        #region StateList
        public IActionResult StateList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion StateList

        #region Add
        public IActionResult StateSave(StateModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (model.StateId == 0)
                {
                    command.CommandText = "PR_State_Insert";
                }
                else
                {
                    command.CommandText = "PR_State_Update";
                    command.Parameters.Add("@StateId", SqlDbType.Int).Value = model.StateId;
                }
                command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = model.StateName;
                command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = model.StateCode;
                command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                command.ExecuteNonQuery();
                return RedirectToAction("StateList");
            }
            return View("StateAddEdit", model);
        }
        #endregion Add

        #region Edit
        public IActionResult StateAddEdit(int StateId)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectById";
            command.Parameters.AddWithValue("@StateId", StateId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            StateModel model = new StateModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.StateName = @row["StateName"].ToString();
                model.StateCode = @row["StateCode"].ToString();
            }
            return View("StateAddEdit", model);
        }
        #endregion Edit 

        #region State Delete
        public IActionResult StateDelete(int StateID)
        {
            string connectionstr = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_State_Delete";
                    sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return RedirectToAction("StateList");
        }
        #endregion State Delete
    }
}
