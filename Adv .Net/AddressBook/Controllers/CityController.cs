using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class CityController : Controller
    {
        public IConfiguration configuration;
        public CityController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        #region CityList
        public IActionResult CityList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion CityList

        #region City Delete
        public IActionResult CityDelete(int CityID)
        {
            string connectionstr = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_City_Delete";
                    sqlCommand.Parameters.AddWithValue("@CityID", CityID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return RedirectToAction("CityList");
        }
        #endregion City Delete

        #region Add
        // This action displays the City Add/Edit form
        public IActionResult CityAddEdit(int? CityID)
        {
            // Load the dropdown list of countries
            LoadCountryList();

            // Check if an edit operation is requested
            if (CityID.HasValue)
            {
                string connectionstr = configuration.GetConnectionString("ConnectionString");
                DataTable dt = new DataTable();

                // Fetch city details by ID
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    conn.Open();
                    using (SqlCommand objCmd = conn.CreateCommand())
                    {
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_City_SelectById";
                        objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;

                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            dt.Load(objSDR); // Load data into DataTable
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    // Map data to CityModel
                    CityModel model = new CityModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.CityId = Convert.ToInt32(dr["CityID"]);
                        model.CityName = dr["CityName"].ToString();
                        model.StateId = Convert.ToInt32(dr["StateID"]);
                        model.CountryId = Convert.ToInt32(dr["CountryID"]);
                        ViewBag.StateList = GetStateByCountryID(model.CountryId); // Load states for selected country
                    }
                    GetStatesByCountry(model.CountryId);
                    return View("CityAddEdit", model); // Return populated model to view
                }
            }

            return View("CityAddEdit"); // For adding a new city
        }
        #endregion

        #region Save
        // Save action handles both insert and update operations
        [HttpPost]
        public IActionResult Save(CityModel modelCity)
        {
            if (ModelState.IsValid)
            {
                string connectionstr = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    conn.Open();
                    using (SqlCommand objCmd = conn.CreateCommand())
                    {
                        objCmd.CommandType = CommandType.StoredProcedure;

                        // Choose procedure based on operation (insert or update)
                        if (modelCity.CityId == null)
                        {
                            objCmd.CommandText = "PR_City_Insert";
                        }
                        else
                        {
                            objCmd.CommandText = "PR_City_Update";
                            objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCity.CityId;
                        }

                        // Pass parameters
                        objCmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = modelCity.CityName;
                        objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCity.StateId;
                        objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCity.CountryId;

                        objCmd.ExecuteNonQuery(); // Execute the query
                    }
                }

                TempData["CityInsertMsg"] = "Record Saved Successfully"; // Success message
                return RedirectToAction("Index"); // Redirect to city listing
            }

            LoadCountryList(); // Reload dropdowns if validation fails
            return View("CityAddEdit", modelCity);
        }
        #endregion

        #region LoadCountryList
        // Load the dropdown list of countries
        private void LoadCountryList()
        {
            string connectionstr = configuration.GetConnectionString("ConnectionString");
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand objCmd = conn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_LOC_Country_SelectComboBox";

                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        dt.Load(objSDR); // Load data into DataTable
                    }
                }
            }

            // Map data to list
            List<CountryDropDownModel> countryList = new List<CountryDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                countryList.Add(new CountryDropDownModel
                {
                    CountryID = Convert.ToInt32(dr["CountryID"]),
                    CountryName = dr["CountryName"].ToString()
                });
            }
            ViewBag.CountryList = countryList; // Pass list to view
        }
        #endregion

        #region GetStatesByCountry
        // AJAX handler for loading states dynamically
        [HttpPost]
        public JsonResult GetStatesByCountry(int CountryID)
        {
            List<StateDropDownModel> loc_State = GetStateByCountryID(CountryID); // Fetch states
            return Json(loc_State); // Return JSON response
        }
        #endregion

        #region GetStateByCountryID
        // Helper method to fetch states by country ID
        public List<StateDropDownModel> GetStateByCountryID(int CountryID)
        {
            string connectionstr = configuration.GetConnectionString("ConnectionString");
            List<StateDropDownModel> loc_State = new List<StateDropDownModel>();

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand objCmd = conn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_LOC_State_SelectComboBoxByCountryID";
                    objCmd.Parameters.AddWithValue("@CountryID", CountryID);

                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                loc_State.Add(new StateDropDownModel
                                {
                                    StateID = Convert.ToInt32(objSDR["StateID"]),
                                    StateName = objSDR["StateName"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return loc_State;
        }
        #endregion
    }
}
