using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.IO;

namespace RhoMicro.Common.IO.Tests
{
	[TestClass]
	public class ExtensionsTests
	{
		private static Object[] Files
		{
			get
			{
				return new Object[]
						{
							"File 1.xyz",
							"File 2.xyz",
							"File 3.xyz",
							"21367.xyz",
							"09v48732mc93287n4c8.xyz",
							"file.xyz"
						};
			}
		}
		private static Object[] SubDirectories
		{
			get
			{
				return new Object[]
						{
							"SubDirectory1",
							"SubDirectory2",
							"SubDirectory3",
							"2847cn238v243",
							"4v238vn7234nc",
							"subDir"
						};
			}
		}
		public static Object[][] DeleteRecursivelyValidData
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						"TestDirectory1",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"TestDirectory2",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"TestDirectory3",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"TestDirectory4",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"TestDirectory5",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"With Space",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"12423456246",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"rewtgiowen 9u 23m",
						Files,
						SubDirectories
					},
					new Object[]
					{
						"dir",
						Files,
						SubDirectories
					}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(DeleteRecursivelyValidData))]
		public void DeleteRecursively(String subDirectoryName, Object[] files, Object[] subDirectories)
		{
			var tempDir = Path.GetTempPath();
			var dir = Path.Combine(tempDir, subDirectoryName);
			Directory.CreateDirectory(dir);

			foreach (var file in files)
			{
				var filePath = Path.Combine(dir, file.ToString()!);
				File.Create(filePath).Close();
			}

			foreach (var subDirectory in subDirectories)
			{
				var directoryPath = Path.Combine(dir, subDirectory.ToString()!);
				Directory.CreateDirectory(directoryPath);
			}

			assert(exists: true);

			try
			{
				Directory.Delete(dir);
			}
			catch (IOException ex)
			when (ex.Message == $"The directory is not empty. : '{dir}'")
			{

			}
			catch
			{
				Assert.Fail($"Expected: <<System.IO.IOException: The directory is not empty. : '{dir}'>>");
			}

			new DirectoryInfo(dir).DeleteRecursively();
			assert(exists: false);

			void assert(Boolean exists)
			{
				Assert.AreEqual(exists, Directory.Exists(dir));

				foreach (var file in files)
				{
					var filePath = Path.Combine(dir, file.ToString()!);
					Assert.AreEqual(exists, File.Exists(filePath));
				}

				foreach (var subDirectory in subDirectories)
				{
					var directoryPath = Path.Combine(dir, subDirectory.ToString()!);
					Assert.AreEqual(exists, Directory.Exists(directoryPath));
				}
			}
		}
	}
	[TestClass]
	public class TemporaryDirectoryTests
	{
		private static Object[][] DeleteRecursivelyValidData
		{
			get
			{
				return ExtensionsTests.DeleteRecursivelyValidData;
			}
		}

		[TestMethod]
		[DynamicData(nameof(DeleteRecursivelyValidData))]
		public void Dispose(String subDirectoryName, Object[] files, Object[] subDirectories)
		{
			var tempDir = Path.GetTempPath();
			var dir = Path.Combine(tempDir, subDirectoryName);
			Directory.CreateDirectory(dir);

			foreach (var file in files)
			{
				var filePath = Path.Combine(dir, file.ToString()!);
				File.Create(filePath).Close();
			}

			foreach (var subDirectory in subDirectories)
			{
				var directoryPath = Path.Combine(dir, subDirectory.ToString()!);
				Directory.CreateDirectory(directoryPath);
			}

			try
			{
				Directory.Delete(dir);
			}
			catch (IOException ex)
			when (ex.Message == $"The directory is not empty. : '{dir}'")
			{

			}
			catch
			{
				Assert.Fail($"Expected: <<System.IO.IOException: The directory is not empty. : '{dir}'>>");
			}

			var dirInfo = new DirectoryInfo(dir);
			using (var tmpDir = new TemporaryDirectory(dirInfo))
			{
				Assert.AreEqual(dirInfo.FullName, tmpDir.Directory.FullName);

				assert(exists: true);
			}

			assert(exists: false);

			void assert(Boolean exists)
			{
				Assert.AreEqual(exists, Directory.Exists(dir));

				foreach (var file in files)
				{
					var filePath = Path.Combine(dir, file.ToString()!);
					Assert.AreEqual(exists, File.Exists(filePath));
				}

				foreach (var subDirectory in subDirectories)
				{
					var directoryPath = Path.Combine(dir, subDirectory.ToString()!);
					Assert.AreEqual(exists, Directory.Exists(directoryPath));
				}
			}
		}
		[TestMethod]
		[DynamicData(nameof(DeleteRecursivelyValidData))]
#pragma warning disable IDE0060 // Remove unused parameter
		public void CreateInTempPath(String subDirectoryName, Object[] files, Object[] subDirectories)
#pragma warning restore IDE0060 // Remove unused parameter
		{
			var tempDir = Path.GetTempPath();
			var dir = Path.Combine(tempDir, subDirectoryName);
			var expected = new DirectoryInfo(dir);

			var actual = TemporaryDirectory.CreateInTempPath(subDirectoryName).Directory;
			Assert.AreEqual(expected.FullName, actual.FullName);
		}
	}
}