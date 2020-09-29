namespace BlazorServerAppointmentApp.Data.Interfaces
{
    public interface IPasswordGenerator
    {
        public string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
            int passwordSize);
    }
}