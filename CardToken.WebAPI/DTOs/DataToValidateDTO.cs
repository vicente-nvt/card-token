namespace CardToken.WebAPI.Controllers
{
    public class DataToValidateDTO
    {
        public string RegistrationDate { get; set; }
        public string Token { get; set; }
        public string Cvv { get; set; }
    }
}
