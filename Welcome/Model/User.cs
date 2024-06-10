using System.Text;
using Welcome.Others;
using System.Security.Cryptography;


namespace Welcome.Model
{
    public class User
    {
        private string password;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password
        {
            get { return Decrypt(password); }
            set { password = Encrypt(value); }
        }

        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; }
        //}
        public UserRoleEnum Role { get; set; }
        public string StudentNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime Expires { get; set; }

        public User(){}

        public User(string name, string password, UserRoleEnum role, string studentNumber, string email, DateTime birthDay) 
        {
            Name = name;
            Password = password;
            Role = role;
            StudentNumber = studentNumber;
            Email = email;
            BirthDay = birthDay;
        }

        public string Encrypt(string plainText)
        {
            byte[] Key = Encoding.UTF8.GetBytes("asdfghyuiopytret");
            byte[] vector  = new byte[16];

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = vector;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] Key = Encoding.UTF8.GetBytes("asdfghyuiopytret");
            byte[] vector = new byte[16];


            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = vector;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                string plaintext = null;
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                return plaintext;
            }
        }

        public override string ToString() 
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"All information about {Name}: ");
            stringBuilder.AppendLine($"Role: {Role}");
            stringBuilder.AppendLine($"Student Number: {StudentNumber}");
            stringBuilder.AppendLine($"Email: {Email}");
            stringBuilder.AppendLine($"Birthday: {BirthDay}");
            stringBuilder.AppendLine($"Expires: {Expires}");

            return stringBuilder.ToString();
        }
    }
}
