namespace RhoMicro.Common.IO.Tests
{
	[TestClass]
	public class TemporaryDirectoryTests
	{
		private static Object[][] DisposeData
		{
			get
			{
				return ExtensionsTests.CreateAndGetTestDirectories("Dispose");
			}
		}

		[TestMethod]
		[DynamicData(nameof(DisposeData))]
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
		[DynamicData(nameof(DisposeData))]
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