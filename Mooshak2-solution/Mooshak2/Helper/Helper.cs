using System.IO;
using System.Text;

namespace Mooshak2.Helper
{
	public static class Helper
	{
		public static byte[] StreamToBytes(Stream inputStream)
		{
			using (var ms = new MemoryStream())
			{
				inputStream.CopyTo(ms);
				return ms.ToArray();
			}
		}

		public static string StreamToUtf8String(Stream inputStream)
		{
			using (var sr = new StreamReader(inputStream, Encoding.UTF8))
			{
				string output = sr.ReadToEnd();
				return output;
			}
		}

		public static byte[] FileToBytes(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				return StreamToBytes(fs);
			}
		}

		public static void BytesToFile(string filePath, byte[] blob)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
			{
				fs.Write(blob,0,blob.Length);
			}
		}
	}
}