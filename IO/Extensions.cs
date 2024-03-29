﻿using Fort;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

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
		/// <param name="fileDeletedProgress">Progress to report a file after it has been deleted.</param>
		/// <param name="directoryDeletedProgress">Progress to report a directory after it has been deleted.</param>
		/// <param name="cancellationToken">Token for requesting cancellation before a deletion attempt.</param>
		public static void DeleteRecursively(this DirectoryInfo directory,
									   IProgress<FileInfo> fileDeletedProgress = null,
									   IProgress<DirectoryInfo> directoryDeletedProgress = null,
									   CancellationToken cancellationToken = default)
		{
			directory.ThrowIfDefaultOrNot(d => d.Exists, $"{nameof(directory)} does not exist.", nameof(directory));

			foreach (FileInfo file in directory.GetFiles())
			{
				cancellationToken.ThrowIfCancellationRequested();
				file.Delete();
				fileDeletedProgress?.Report(file);
			}

			foreach (DirectoryInfo nestedDirectory in directory.GetDirectories())
			{
				nestedDirectory.DeleteRecursively(cancellationToken: cancellationToken);
				directoryDeletedProgress?.Report(nestedDirectory);
			}

			directory.Delete();
		}
		/// <summary>
		/// Copies the contents of a directory to another.
		/// </summary>
		/// <param name="directory">The source directory whose files and subdirectories to copy to another.</param>
		/// <param name="targetDirectory">The target directory which to copy files and subdirectories into.</param>
		/// <param name="overwrite">When set to <see langword="true"/>, files matching the path of files to copy will be overwritten; otherwise, not.</param>
		/// <param name="fileCopiedProgress">Progress to report a file after it has been copied.</param>
		/// <param name="directoryCopiedProgress">Progress to report a directory after it has been copied.</param>
		/// <param name="cancellationToken">Token for requesting cancellation before a copy attempt.</param>
		public static void CopyRecursively(this DirectoryInfo directory,
									   String targetDirectory,
									   Boolean overwrite = true,
									   IProgress<FileInfo> fileCopiedProgress = null,
									   IProgress<DirectoryInfo> directoryCopiedProgress = null,
									   CancellationToken cancellationToken = default)
		{
			directory.ThrowIfDefaultOrNot(d => d.Exists, $"{nameof(directory)} does not exist.", nameof(directory));
			targetDirectory.ThrowIfDefault(nameof(targetDirectory));

			String fullTargetPath = Path.GetFullPath(targetDirectory);
			String rootPattern = $"^{Regex.Escape(directory.FullName)}";
			if (Regex.IsMatch(fullTargetPath, rootPattern))
			{
				throw new ArgumentException($"{nameof(targetDirectory)} {fullTargetPath} is equal to or a subdirectory of {nameof(directory)} {directory}.", nameof(targetDirectory));
			}

			_ = Directory.CreateDirectory(fullTargetPath);

			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files)
			{
				cancellationToken.ThrowIfCancellationRequested();
				String newFilePath = Regex.Replace(file.FullName, rootPattern, targetDirectory, RegexOptions.None);
				File.Copy(file.FullName, newFilePath, overwrite);
				fileCopiedProgress?.Report(file);
			}

			DirectoryInfo[] subDirectories = directory.GetDirectories();
			foreach (DirectoryInfo subDirectory in subDirectories)
			{
				cancellationToken.ThrowIfCancellationRequested();
				String newSubDirectory = Path.Combine(targetDirectory, subDirectory.Name);
				subDirectory.CopyRecursively(newSubDirectory, overwrite, fileCopiedProgress, directoryCopiedProgress, cancellationToken);
				directoryCopiedProgress?.Report(subDirectory);
			}
		}

		/// <summary>
		/// Evaluates whether or not a directory is contained in another or equal to it.
		/// </summary>
		/// <param name="directory">The directory to check for being a descendant of <paramref name="parent"/>.</param>
		/// <param name="parent">The directory that is checked to contain <paramref name="directory"/>.</param>
		/// <returns><see langword="true"/> if <paramref name="directory"/> is a descendant of <paramref name="parent"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsDescendantOf(this DirectoryInfo directory, DirectoryInfo parent)
		{
			directory.ThrowIfDefault(nameof(directory));
			parent.ThrowIfDefault(nameof(parent));

			Char[] directorySeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

			String directoryPath = directory.FullName.TrimEnd(directorySeparators);
			String parentPath = parent.FullName.TrimEnd(directorySeparators);

			Boolean result;
			if (directoryPath != parentPath)
			{
				String[] directoryParts = directoryPath.Split(directorySeparators);
				String[] parentParts = parentPath.Split(directorySeparators);

				if (directoryParts.Length < parentParts.Length)
				{
					result = false;
				}
				else
				{
					result = true;

					for (Int32 i = parentParts.Length - 1; i >= 0; i--)
					{
						if (directoryParts[i] != parentParts[i])
						{
							result = false;
							break;
						}
					}
				}
			}
			else
			{
				result = true;
			}

			return result;
		}
	}
}