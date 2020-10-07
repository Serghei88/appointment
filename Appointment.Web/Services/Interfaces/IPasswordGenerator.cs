namespace Appointment.Web.Data.Interfaces
{
    public interface IPasswordGenerator
    {
        public string GeneratePassword(bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, 
            bool useSpecial = true,
            int passwordSize = 12);
    }
}