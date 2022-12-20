namespace RhoMicro.Common.IO.Tests
{
	[TestClass]
	public class TemporaryDirectoryTests
	{
		private static Object[][] DisposeData => ExtensionsTests.CreateAndGetTestDirectories("Dispose");

		[TestMethod]
		[DynamicData(nameof(DisposeData))]
		public void Dispose(String subDirectoryName, Object[] files, Object[] subDirectories)
		{
			String tempDir = Path.GetTempPath();
			String dir = Path.Combine(tempDir, subDirectoryName);
			_ = Directory.CreateDirectory(dir);

			foreach (Object file in files)
			{
				String filePath = Path.Combine(dir, file.ToString()!);
				File.Create(filePath).Close();
			}

			foreach (Object subDirectory in subDirectories)
			{
				String directoryPath = Path.Combine(dir, subDirectory.ToString()!);
				_ = Directory.CreateDirectory(directoryPath);
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

				foreach (Object file in files)
				{
					String filePath = Path.Combine(dir, file.ToString()!);
					Assert.AreEqual(exists, File.Exists(filePath));
				}

				foreach (Object subDirectory in subDirectories)
				{
					String directoryPath = Path.Combine(dir, subDirectory.ToString()!);
					Assert.AreEqual(exists, Directory.Exists(directoryPath));
				}
			}
		}
		[TestMethod]
		[DynamicData(nameof(DisposeData))]
#pragma warning disable IDE0060 // Remove unused parameter
		public void CreateInTempPath(String subDirectoryName, Object[] files, Object[] subDirectories)
#pragma warning restore IDE0060 // Remove unused parameter
		{
			String tempDir = Path.GetTempPath();
			String dir = Path.Combine(tempDir, subDirectoryName);
			var expected = new DirectoryInfo(dir);

			DirectoryInfo actual = TemporaryDirectory.CreateInTempPath(subDirectoryName).Directory;
			Assert.AreEqual(expected.FullName, actual.FullName);
		}
	}
}