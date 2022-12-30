using RhoMicro.Common.System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace System.Tests.Collections.Concurrent
{
	[TestClass]
	public class EnumerableTests
	{
		[TestMethod]
		public async Task Synchronize()
		{
			var enumerableA = Enumerable.Range(0, 1_000_000);
			var enumerableB = Enumerable.Range(0, 1_000_000);

			var builder = new SynchronizedEnumerableBuilder<Int32>(2);

			var synchronizedAEnumerator = builder.SetSource(enumerableA).SetTurn(1).Build().GetEnumerator();
			var synchronizedBEnumerator = builder.SetSource(enumerableB).SetTurn(2).Build().GetEnumerator();

			var result = new List<Int32>();

			await Task.WhenAll(Task.Run(() =>
			{
				while (synchronizedAEnumerator.MoveNext())
				{
					result.Add(synchronizedAEnumerator.Current);
				}
			}), Task.Run(() =>
			{
				while (synchronizedBEnumerator.MoveNext())
				{
					result.Add(synchronizedBEnumerator.Current);
				}
			}));

			var mismatches = result
				.GroupBy(r=>r)
				.Where(g => g.Count() != 2)
				.Select(g => String.Join(',', g))
				.ToArray();

			Assert.Fail();
		}
	}
}
