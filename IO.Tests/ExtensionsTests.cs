namespace RhoMicro.Common.IO.Tests
{
	[TestClass]
	public class ExtensionsTests
	{
		private static Object[] Files => new Object[]
						{
							"File 1.xyz",
							"File 2.xyz",
							"File 3.xyz",
							"21367.xyz",
							"09v48732mc93287n4c8.xyz",
							"file.xyz"
						};
		private static Object[] SubDirectories => new Object[]
						{
							"SubDirectory1",
							"SubDirectory2",
							"SubDirectory3",
							"2847cn238v243",
							"4v238vn7234nc",
							"subDir"
						};
		private static Object[][] TestCopyDirectories => CreateAndGetTestDirectories("Copy");
		private static Object[][] TestDeleteDirectories => CreateAndGetTestDirectories("Delete");

		public static Object[][] CreateAndGetTestDirectories(String discriminator)
		{
			Object[][] data = new Object[][]
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

			foreach (Object[] datum in data)
			{
				String fullDir = Path.Combine(Path.GetTempPath(), (String)datum[0] + discriminator);
				datum[0] = fullDir;

				_ = Directory.CreateDirectory(fullDir);

				foreach (Object file in (Object[])datum[1])
				{
					String filePath = Path.Combine(fullDir, (String)file);
					File.Create(filePath).Close();
				}

				foreach (Object subDirectory in (Object[])datum[2])
				{
					String directoryPath = Path.Combine(fullDir, (String)subDirectory);
					_ = Directory.CreateDirectory(directoryPath);
				}

				try
				{
					Directory.Delete(fullDir);
					Assert.Fail($"Expected: <<System.IO.IOException: The directory is not empty. : '{fullDir}'>>");
				}
				catch (IOException ex)
				when (ex.Message == $"The directory is not empty. : '{fullDir}'")
				{

				}
			}

			return data;
		}

		[TestMethod]
		[DynamicData(nameof(TestDeleteDirectories))]
		public void DeleteRecursively(String dir, Object[] files, Object[] subDirectories)
		{
			assert(exists: true, dir, files, subDirectories);

			new DirectoryInfo(dir).DeleteRecursively();

			assert(exists: false, dir, files, subDirectories);
		}

		[TestMethod]
		[DynamicData(nameof(TestCopyDirectories))]
		public void TestCopyRecursively(String dir, Object[] files, Object[] subDirectories)
		{
			assert(exists: true, dir, files, subDirectories);

			String target = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			new DirectoryInfo(dir).CopyRecursively(target);

			assert(exists: true, dir, files, subDirectories);
			assert(exists: true, target, files, subDirectories);
		}

		[TestMethod]
		public void TestIsDescendantOf()
		{
			for (Int32 i = 0; i < 25; i++)
			{
				String parent = Path.GetTempPath();
				String child = parent;

				for (Int32 j = 0; j < i / 5; j++)
				{
					child = Path.Combine(child, Guid.NewGuid().ToString());
				}

				Boolean expected = true;
				Boolean actual = new DirectoryInfo(child).IsDescendantOf(new DirectoryInfo(parent));
				Assert.AreEqual(expected, actual);
			}
		}
		[TestMethod]
		public void TestIsNotDescendantOf()
		{
			for (Int32 i = 0; i < 25; i++)
			{
				String child = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
				String parent = Path.Combine(child, Guid.NewGuid().ToString());

				for (Int32 j = 0; j < i / 5; j++)
				{
					child = Path.Combine(child, Guid.NewGuid().ToString());
				}

				Boolean expected = false;
				Boolean actual = new DirectoryInfo(child).IsDescendantOf(new DirectoryInfo(parent));
				Assert.AreEqual(expected, actual);
			}
		}

		private static void assert(Boolean exists, String dir, Object[] files, Object[] subDirectories)
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
}