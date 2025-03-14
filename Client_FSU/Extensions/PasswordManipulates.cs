using System.Text;

namespace Client_FSU.Extensions
{
    public static class PasswordManipulates
    {
        public static string EncryptPassword(string password)
        {
            byte[] bytesArray = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytesArray);            
        }

        public static string DecryptionPassword(string base64Str)
        {
            byte[] bytesDec = Convert.FromBase64String(base64Str);
            return Encoding.UTF8.GetString(bytesDec);
        }
    }
}
