﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Tests
{
	internal static class TestData
	{
		public static Object[][] EqualNames
		{
			get
			{
				return new Object[][]
				{
					new Object[]{"m", "m"},
					new Object[]{"MyUnit", "MyUnit"},
					new Object[]{"s", "s"},
					new Object[]{"°", "°"},
					new Object[]{"m/s^2", "m/s^2"}
				};
			}
		}
		public static Object[][] EqualUnits
		{
			get
			{
				return EqualNames.Select(args => args.Select(arg => (Object)new Unit((String)arg)).ToArray()).ToArray();
			}
		}

		public static Object[][] NotEqualNames
		{
			get
			{
				return new Object[][]
				{
					new Object[]{"m", "s"},
					new Object[]{"MyUnit", "MyUnit1"},
					new Object[]{"s", "mPa"},
					new Object[]{"°", "rad"},
					new Object[]{"m/s^2", "km/s^2"}
				};
			}
		}
		public static Object[][] NotEqualUnits
		{
			get
			{
				return NotEqualNames.Select(args => args.Select(arg => (Object)new Unit((String)arg)).ToArray())
					.Concat(new Object[][]
					{
						new Object[]{null, new Unit()},
						new Object[]{null, new Unit("m")},
						new Object[]{default(Unit), new Unit("m")}
					})
					.ToArray();
			}
		}

		public static Object[][] Names
		{
			get
			{
				return new Object[][]
				{
					new Object[]{"m"},
					new Object[]{"MyUnit"},
					new Object[]{"s"},
					new Object[]{"°"},
					new Object[]{"m/s^2"}
				};
			}
		}
		public static Object[][] Units
		{
			get
			{
				return Names.Select(args => args.Select(arg => (Object)new Unit((String)arg)).ToArray()).ToArray();
			}
		}
	}
}
