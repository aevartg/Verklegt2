using System.IO;
using System.Linq;
using System.Text;

namespace Mooshak2
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

		public static bool BytesToFile(string filePath, byte[] blob)
		{
			using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				fs.Write(blob,0,blob.Length);
			}
			return File.Exists(filePath);
		}

		public static bool CompareByteArray(byte[] blob1, byte[] blob2)
		{
			return blob1.SequenceEqual(blob2);
		}
	}
}