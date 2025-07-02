namespace AddressBook.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
    }
    public class StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }

    public class CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
