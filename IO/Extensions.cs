using System.IO;

namespace RhoMicro.Common.IO
{
	/// <summary>
	/// Extensions for the <c>RhoMicro.Common.IO</c> namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Deletes a directory, all its files and subdirectories recursively.
		/// </summary>
		/// <param name="directory">The directory to delete.</param>
		public static void DeleteRecursively(this DirectoryInfo directory)
		{
			foreach (var file in directory.GetFiles())
			{
				file.Delete();
			}
			foreach (var nestedDirectory in directory.GetDirectories())
			{
				nestedDirectory.DeleteRecursively();
			}
			directory.Delete();
		}
	}
}