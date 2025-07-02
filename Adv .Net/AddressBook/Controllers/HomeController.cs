using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Models;

public class HomeController : Controller
{
    #region Confi

    public IConfiguration configuration;
    public HomeController(IConfiguration _configuration)
    {
        configuration = _configuration;
    }
    #endregion Confi

    public IActionResult Index()
    {
        int countryCount = 0, stateCount = 0, cityCount = 0;

        string connectionString = configuration.GetConnectionString("ConnectionString");
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Country Count
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AddressBook_Country", conn))
            {
                countryCount = (int)cmd.ExecuteScalar();
            }

            // State Count
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AddressBook_State", conn))
            {
                stateCount = (int)cmd.ExecuteScalar();
            }

            // City Count
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AddressBook_City", conn))
            {
                cityCount = (int)cmd.ExecuteScalar();
            }
        }

        ViewBag.CountryCount = countryCount;
        ViewBag.StateCount = stateCount;
        ViewBag.CityCount = cityCount;

        return View();
    }

    public ActionResult TopCities()

    {
        string connectionString = configuration.GetConnectionString("ConnectionString");

        List<HomeModel> cities = new List<HomeModel>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT TOP 5 
                                c.CityName,
                                c.PinCode,
                                s.StateName,
                                co.CountryName
                             FROM AddressBook_City c
                             JOIN AddressBook_State s ON c.StateID = s.StateID
                             JOIN AddressBook_Country co ON c.CountryID = co.CountryID
                             ORDER BY c.CityID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                HomeModel city = new HomeModel
                {
                    CityName = rdr["CityName"].ToString(),
                    PinCode = rdr["PinCode"].ToString(),
                    StateName = rdr["StateName"].ToString(),
                    CountryName = rdr["CountryName"].ToString()
                };
                cities.Add(city);
            }
        }

        return View(cities);
    }
}
