using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

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
			using (var fs = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
			{
				return StreamToBytes(fs);
			}
		}

		public static bool BytesToFile(string filePath, byte[] blob)
		{
			using (var fs = new FileStream(@filePath, FileMode.Create, FileAccess.Write))
			{
				fs.Write(blob,0,blob.Length);
			}
			return File.Exists(@filePath);
		}

		public static bool CompareByteArray(byte[] blob1, byte[] blob2)
		{
			return blob1.SequenceEqual(blob2);
		}

		public static string RunJavaScriptCode(string pathToNode, string pathToFile, string input, int timeOut)
		{
			var process = new Process
			{
				StartInfo =
								{
									FileName = @pathToNode,
									Arguments = @pathToFile,
									CreateNoWindow = true,
									UseShellExecute = false,
									RedirectStandardOutput = true,
									RedirectStandardInput = true
								}
			};
			process.Start();
			if (input != null)
			{
				process.StandardInput.WriteLine(input);
			}
			process.WaitForExit(timeOut);
			if (!process.HasExited)
			{
				if (process.Responding)
				{
					process.CloseMainWindow();
				}
				else
				{
					process.Kill();
				}
			}
			using (var reader = process.StandardOutput)
			{
				return reader.ReadToEnd();
			}
		}

		public static List<String []> PareInputOutput(Stream inputStream)
		{
			var temp = new List<String[]>();
			using (var tfp = new TextFieldParser(inputStream)
							{
								Delimiters = new []{";"},
								HasFieldsEnclosedInQuotes = false,
								TextFieldType = FieldType.Delimited
							})
			{
				while (!tfp.EndOfData)
				{
					temp.Add(tfp.ReadFields());
				}
				return temp;
			}
		}
	}
}