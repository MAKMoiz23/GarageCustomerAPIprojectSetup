using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace DAL.GlobalAndCommon
{
    public static class Global
    {
        public static string? GarageConnection { get; set; }
        public static string? GarageCustomerConnection { get; set; }

        public static readonly string ImagePreURL = "";

        public static void StrDateTimeSqlFormat<T>(T model, string prop)
        {
            PropertyInfo? dobProperty = typeof(T).GetProperty(prop);
            if (dobProperty != null && dobProperty.GetValue(model) is string dobValue)
            {
                DateTime modifieddob = DateTime.ParseExact(dobValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string formattedDate = modifieddob.ToString("yyyy-MM-dd");
                dobProperty.SetValue(model, formattedDate);
            }
        }

        public static string DateParse(string Date)
        {
            if (DateTime.TryParseExact(Date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd/MM/yyyy hh:mm tt");
            }
            else
            {
                // Parsing failed, return the original input
                return Date;
            }
        }

        public static decimal TimespanToDecimal(TimeSpan span)
        {
            decimal spanSecs = (span.Hours * 3600) + (span.Minutes * 60) + span.Seconds;
            decimal spanPart = spanSecs / 86400M;
            decimal result = span.Days + spanPart;
            return result;
        }

        public static void InsertImagePreURL<T>(T item)
		{
			PropertyInfo? imageProperty = typeof(T).GetProperty("Image");
			ChangeImageURL(imageProperty, item);
		}

		public static void InsertImagePreURL<T>(List<T> itemsGroup)
		{
			PropertyInfo? imageProperty = typeof(T).GetProperty("Image");
			foreach (T item in itemsGroup)
			{
				ChangeImageURL(imageProperty, item);
			}
		}

		private static void ChangeImageURL<T>(PropertyInfo? imageProperty, T item)
		{
			if (imageProperty != null && imageProperty.GetValue(item) is string imageValue)
			{
				string modifiedImageUrl = string.Format("{0}{1}", ImagePreURL, imageValue);
				imageProperty.SetValue(item, modifiedImageUrl);
			}
		}

		public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decrypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        
    }
}
