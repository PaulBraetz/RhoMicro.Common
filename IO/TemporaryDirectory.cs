using Fort;
using RhoMicro.Common.System;
using System;
using System.Collections.Generic;
using System.IO;

namespace RhoMicro.Common.IO
{
	/// <summary>
	/// Represents a temporary, disposable directory that will delete itself upon disposal.
	/// </summary>
	public sealed class TemporaryDirectory : DisposableBase, IEquatable<TemporaryDirectory>
	{
		/// <summary>
		/// Initializes a new temporary directory.
		/// </summary>
		/// <param name="directory">The underlying directory.</param>
		public TemporaryDirectory(DirectoryInfo directory)
		{
			directory.ThrowIfDefault(nameof(directory));

			directory.Create();
			Directory = directory;
		}

		/// <summary>
		/// The temporary directory, to be deleted upon instance disposal.
		/// </summary>
		public DirectoryInfo Directory { get; private set; }

		/// <summary>
		/// Invoked after <see cref="Directory"/> is deleted as a result of this instance's disposal.
		/// </summary>
		public event EventHandler DirectoryDeleted;

		/// <summary>
		/// Creates a new instance pointing to a subdirectory of the current user's temporary folder.
		/// </summary>
		/// <param name="subDirectory">The name of the subdirectory.</param>
		/// <returns>A new instance, initialized to the specified subdirectory of the current user's temporary folder.</returns>
		public static TemporaryDirectory CreateInTempPath(String subDirectory)
		{
			subDirectory.ThrowIfDefault(nameof(subDirectory));

			var directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), subDirectory));
			var result = new TemporaryDirectory(directory);

			return result;
		}

		/// <summary>
		/// Finalizer.
		/// </summary>
		~TemporaryDirectory()
		{
			FinalizeDispose();
		}
		/// <inheritdoc/>
		protected override void DisposeUnmanaged(Boolean disposing)
		{
			Directory.DeleteRecursively();
		}
		/// <inheritdoc/>
		protected override void OnDiposed()
		{
			Directory = new DirectoryInfo(Directory.FullName);
			DirectoryDeleted?.Invoke(this, EventArgs.Empty);
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			return Directory?.ToString();
		}
		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return Equals(obj as TemporaryDirectory);
		}
		/// <inheritdoc/>
		public Boolean Equals(TemporaryDirectory other)
		{
			return !(other is null) &&
				   EqualityComparer<DirectoryInfo>.Default.Equals(Directory, other.Directory);
		}
		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return 772425832 + EqualityComparer<DirectoryInfo>.Default.GetHashCode(Directory);
		}
	}
}