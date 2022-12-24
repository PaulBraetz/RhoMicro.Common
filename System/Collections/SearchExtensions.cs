using Fort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RhoMicro.Common.System.Collections
{
	public static class SearchExtensions
	{
		public static T BinaryFuzzySearch<T>(this ICollection<T> collection, Func<T, Int32> accuracyPredicate, Boolean sorted = false)
		{
			collection.ThrowIfDefaultOrEmpty(nameof(collection));
			accuracyPredicate.ThrowIfDefault(nameof(accuracyPredicate));

			var result = collection.BinaryFuzzySearch(0, collection.Count - 1, accuracyPredicate, sorted);

			return result;
		}

		private static T BinaryFuzzySearch<T>(this ICollection<T> collection, Int32 start, Int32 end, Func<T, Int32> accuracyPredicate, Boolean sorted)
		{
			if (set(start, out var startAccuracy, out var startElement) || end - start == 0)
			{
				//match at start or only on element
				return startElement;
			}
			if (set(end, out var endAccuracy, out var endElement))
			{
				//match at end
				return endElement;
			}
			if (end - start == 1)
			{
				//no more elements
				return getMostAccurate(
						startElement,
						startAccuracy,
						endElement,
						endAccuracy,
						out var a);
			}

			var center = start + (end - start) / 2;
			if (set(center, out var centerAccuracy, out var centerElement) || end - start == 2)
			{
				//match at center or center is last element to check
				return getMostAccurate(
					getMostAccurate(
						startElement,
						startAccuracy,
						centerElement,
						centerAccuracy,
						out var a),
					a,
					endElement,
					endAccuracy,
					out var _);
			}

			if (sorted)
			{
				//fuzzy result
				var subElement = centerAccuracy > 0 ?
					getLeftElement() :
					getRightElement();
				var subAccuracy = accuracyPredicate.Invoke(subElement);
				return getMostAccurate(centerElement, centerAccuracy, subElement, subAccuracy, out var _);
			}
			else
			{

				var leftElement = getLeftElement();
				if (isAccurate(leftElement, out var leftAccuracy))
				{
					//match at left
					return leftElement;
				}

				var rightElement = getRightElement();
				if (isAccurate(rightElement, out var rightAccuracy))
				{
					//match at right
					return rightElement;
				}

				//fuzzy result
				return getMostAccurate(leftElement, leftAccuracy, rightElement, rightAccuracy, out var _);
			}

			Boolean set(Int32 index, out Int32 accuracy, out T element)
			{
				element = collection.ElementAt(index);
				return isAccurate(element, out accuracy);
			}
			Boolean isAccurate(T element, out Int32 accuracy)
			{
				accuracy = accuracyPredicate.Invoke(element);
				return accuracy == 0;
			}
			T getLeftElement()
			{
				return center - start == 1 ?
					centerElement :
					collection.BinaryFuzzySearch(start, center, accuracyPredicate, sorted);
			}
			T getRightElement()
			{
				return end - center == 1 ?
					endElement :
					collection.BinaryFuzzySearch(center + 1, end, accuracyPredicate, sorted);
			}
			T getMostAccurate(T element1, Int32 accuracy1, T element2, Int32 accuracy2, out Int32 resultAccuracy)
			{
				if (Math.Abs(accuracy1) < Math.Abs(accuracy2))
				{
					resultAccuracy = accuracy1;
					return element1;
				}
				else
				{
					resultAccuracy = accuracy2;
					return element2;
				}
			}
		}
	}
}
