using System;
namespace Core.Helpers
{
	public static class PasswordHasher
	{
		public static string Encyrpt(string password)
		{
			try
			{
				byte[] encDate_byte = new byte[password.Length];
				encDate_byte = System.Text.Encoding.UTF8.GetBytes(password);
				string encodedData = Convert.ToBase64String(encDate_byte);
				return encodedData;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in base64Encode" + ex.Message);
			};
		}

        public static string Decyrpt(string hashedPassword)
        {
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.Decoder utf8Decode = encoder.GetDecoder();
			byte[] tedecode_byte = Convert.FromBase64String(hashedPassword);
			int charCount = utf8Decode.GetCharCount(tedecode_byte, 0, tedecode_byte.Length);
			char[] decoded_char = new char[charCount];
			utf8Decode.GetChars(tedecode_byte, 0, tedecode_byte.Length, decoded_char, 0);
			string result = new String(decoded_char);
			return result;
        }

    }
}

