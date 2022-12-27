using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.Math.Statistics;
using System.Globalization;

namespace Math.Statistics.Tests
{
	[TestClass]
	public class EmpiricalStatisticTests
	{
		private const Int32 ASSERTION_PRECISION = 10;

		private static Object[][] Samples
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						new Double[]
						{
							4.9,4.8,5.0,5.2,5.2,5.1,4.7,5.0,5.0,4.9,4.8,4.9,5.1,5.0,5.0,5.1,5.0,4.9,4.8,4.9,4.9,5.0,5.0,5.1,5.0
						},
						4.9719999999999995, //arithmetic mean
						6, //distinct sample size
						4.9705061929303866, //geometric mean
						4.9734897205081259, //quadratic mean
						4.96900843010443, //harmonic Mean
						5.2, //maximum
						4.7, //minimum
						4.95, //median
						5, //modal
						0.5, //range
						0.015433333333333306, //variance
						0.12423096769056138, //standardDeviation
						0.024986115786516772, //variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							4.9,-4.8,5,5.2,5.2,5.1,-4.7,5,5,4.9,4.8,-4.9,5.1,5,-5,5.1,5,4.9,4.8,4.9,-4.9,5,5,5.1
						},
						2.945833333333333,	//arithmetic mean
						9,	//distinct sample size
						3.574903658909939,	//geometric mean
						4.972382058262753,	//quadratic mean
						8.65594849449616,	//harmonic Mean
						5.2,	//maximum
						-5,	//minimum
						4.8,	//median
						5,	//modal
						10.2,	//range
						16.744329710144928,	//variance
						4.09198359113828,	//standardDeviation
						1.3890750521544375	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							63.93137576265061,94.68036815143176,35.867353480275824,27.468523790596876,31.78325140902186,16.034051947304917,42.56128897290885,7.526895057363725,53.69816214692729,59.690259131939904,20.593816566915578,44.34906010429267,89.3142139913962,42.81921823151937,6.217326549968282,31.954662485869477,43.910504546964944,99.4321147731343,65.65395361282945,74.26786663388275,68.88824697729005,58.70947385335734,72.58586730294533,84.30049674110876,43.68642943158686,26.238328679163747,81.36085632652053,81.72035579270064,36.1973011250633,71.28624434297303,69.90445497674942,42.75039577787649,47.91855383130618,40.85407241697233,9.601996401716772,51.932142101963684,0.2132073809379409,30.203889456325218,40.87992348069845,2.9312178308858683,49.882930783352606,33.96196105135131,13.115115037247993,95.67764938464681,12.497086182232087,24.66157081442315,29.396138671687886,21.980236489263017,23.039279083205177,25.346160088431592
						},
						44.86951698322357,	//arithmetic mean
						50,	//distinct sample size
						33.26048786189613,	//geometric mean
						51.91233942073716,	//quadratic mean
						7.536580201553281,	//harmonic Mean
						99.4321147731343,	//maximum
						0.2132073809379409,	//minimum
						42.655842375392666,	//median
						63.93137576265061,	//modal
						99.21890739219636,	//range
						695.5279896184018,	//variance
						26.37286464566187,	//standardDeviation
						0.5877679640617152	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							61.36950772864919,64.38702981085977,9.268311086122438,14.04208499755718,85.62827407629813,27.493316038462346,80.47434387095353,69.80505579272884,5.591780919705014,18.63164100162652,15.176179373233033,50.468583070691366,48.22586982165925,25.229982974643793,82.07871106333955,94.57378593010436,75.24172038437653,91.32784789286247,81.65801387281623,38.68015378101251,59.02835267268406,86.05679192706494,23.242453553510998,32.9695969701641,3.4614861031450395,98.63490322307291,16.8092843175762,95.16499975622516,67.78621808225435,86.329925955453,11.272851299975883,52.09520068331803,92.74208521399872,31.208354284573293,52.20332313853143,29.77412465119479,84.08610560346698,32.31120618704914,33.320939801548946,22.57746052564832,44.93123881637664,31.62350802626055,59.884854916930564,61.53907586294122,1.4800136310422207,24.673474143832262,78.75794086567116,33.967356152668415,65.1596153512944,5.879976997469949
						},
						49.16649824405292,	//arithmetic mean
						50,	//distinct sample size
						36.586067607533444,	//geometric mean
						57.209194685630045,	//quadratic mean
						19.24634036351596,	//harmonic Mean
						98.63490322307291,	//maximum
						1.4800136310422207,	//minimum
						49.34722644617531,	//median
						61.36950772864919,	//modal
						97.15488959203068,	//range
						873.0075581590435,	//variance
						29.546701307574818,	//standardDeviation
						0.6009519156908583	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							1.9953560103866619,74.89251241378892,24.420972458594836,90.9713577873342,9.022388538209514,41.2055436193368,37.24369250633871,93.02051728905852,54.08475825421112,94.86591364997949,17.666648801172702,72.64608730921766,38.74682438768983,2.7912058133029993,3.6765110364385123,9.979773289854155,88.84097439660165,14.719449861822763,67.6196853449581,3.901671011362229,80.4341996375668,11.368186437967786,32.22358732606387,81.00021692686207,35.9126627396561,61.694705038451744,76.18341222171256,79.951974903732,12.818652150604182,14.278764856656846,0.22168796349549824,55.740426092479936,81.79341846981987,93.33604728765235,20.474546333584108,67.49590036225884,8.221911736736242,13.21618387730804,46.612470607188186,60.01502646520843,22.774455559735852,96.35777814682129,19.535437215325445,56.20483196002341,10.912226251118573,81.25849825817757,89.39905471994233,35.521101562457346,27.702671889505293,4.833766372384951
						},
						44.39611294300315,	//arithmetic mean
						50,	//distinct sample size
						27.431986784122536,	//geometric mean
						54.70061085222498,	//quadratic mean
						6.521500834306018,	//harmonic Mean
						96.35777814682129,	//maximum
						0.22168796349549824,	//minimum
						37.995258447014265,	//median
						1.9953560103866619,	//modal
						96.13609018332579,	//range
						1041.981615468024,	//variance
						32.27974001549616,	//standardDeviation
						0.7270848251273192	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							-4.757184855917174,49.61140243489467,-13.276161701072908,-12.746547423614174,11.791588015132126,25.40992940949478,-31.184776175450814,-8.224438274765511,47.69696091523766,-36.22086281525233,-29.65630949832295,-39.5500215762702,39.431053696821095,5.968596968340478,26.34235242032451,-14.995249171167323,15.206468570731912,37.47384314567943,-35.14789813501365,-35.05983253900169,-24.610230756949047,-11.796663097270631,-0.7703346173145387,-21.546491618823637,29.334453571845376,-31.90111665433032,46.874982236848595,-7.113426342204643,-30.34003320081544,25.249194421759892,2.682905466190477,-35.979525323087444,-26.378716120224034,15.696670593050499,33.78778147865983,0.6840621039998163,31.021854321718912,34.16306703069571,-5.239370994952031,-47.304426171616754,45.27749642416866,27.550203250984385,26.208997692350387,-40.864039346036485,28.35650859555019,43.2236850490588,-35.179351631014235,-17.990025748317827,-19.80872002495002,-20.53346672239882
						},
						0.21737674554767097,	//arithmetic mean
						50,	//distinct sample size
						4.084258543879364,	//geometric mean
						29.043184913224856,	//quadratic mean
						-312.18008451073274,	//harmonic Mean
						49.61140243489467,	//maximum
						-47.304426171616754,	//minimum
						-4.998277925434603,	//median
						-4.757184855917174,	//modal
						96.91582860651143,	//range
						860.672793116599,	//variance
						29.33722538203978,	//standardDeviation
						134.96027511188447	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							33.40952088743138,18.452941597292615,22.590469904692632,-11.370030647554763,-19.90666205151954,-14.388158175834477,25.41384888317114,-2.372267841244946,47.000075396536175,5.985424553240626,-12.387637914065508,-25.73374903479645,-24.64240316785521,1.3582029977439292,-29.05795258869145,4.859384602626315,-45.783750198362696,-42.576644741656835,-19.679249429228406,44.74557435077471,-8.790393338617298,38.8618176017654,-15.502414289149025,-38.9417835706928,-7.769718133242398,-21.923634450925668,13.777553487773309,-8.785898516013946,12.76653748721197,-0.6152980945043018,-10.65253281928742,-39.60159838390721,-30.536983248906424,7.3451622482594185,45.67932961321242,-48.02099217326975,-40.93849970088777,3.365131368340335,20.218736037451567,-12.49165182299481,18.636475005386664,18.766167477426123,-48.310425400939216,-32.97006884924546,-15.736897950655537,-16.558582707519555,-9.412004181011458,-17.208220111950688,-29.928501074056957,-18.836664032851292
						},
						-6.76397830282205,	//arithmetic mean
						50,	//distinct sample size
						2.655266518293365,	//geometric mean
						26.11252046140516,	//quadratic mean
						-30.646220462709813,	//harmonic Mean
						47.000075396536175,	//maximum
						-48.310425400939216,	//minimum
						-11.011281733421091,	//median
						33.40952088743138,	//modal
						95.31050079747538,	//range
						649.094206496179,	//variance
						25.4773273028428,	//standardDeviation
						-3.7666187208514867	//variationCoefficient
					},
					new Object[]{
						new Double[]
						{
							-18.276901183002682,18.511182515312264,-24.6579104920695,24.459597750916984,-1.083434562915342,11.88262244237811,-13.629420277418603,-2.4589544963768972,-12.638279937850971,-15.693343853423736,42.17877957526247,-2.0122377009087478,47.31725350597713,-31.471623674182947,-27.997289788729084,-39.89342332907968,-47.27799445938761,-46.35542948708755,44.64256520313644,-39.52059536597577,-37.90128578625615,-12.752837640583326,17.54621088108631,-32.491800477682744,14.653929991441794,37.19976389007071,41.8289754421887,25.84005907359814,34.48765127975159,-21.42907959860065,21.720526859724043,35.686669234112834,44.456657720791036,13.319012481710379,8.946089501117694,6.50588076527735,-13.284944043198731,-4.713544263358083,-31.972823751086775,1.937016192704455,-48.284202657102725,30.825381716519995,-34.74142717688421,-49.73583787582888,19.61546606899276,-0.035336136009045394,-31.835743439080144,-29.969584398858085,-22.33375507138915,8.599933236097312
						},
						-2.845756311923188,	//arithmetic mean
						50,	//distinct sample size
						3.7432825368313765,	//geometric mean
						28.863376034636474,	//quadratic mean
						-1.6871750537908727,	//harmonic Mean
						47.31725350597713,	//maximum
						-49.73583787582888,	//minimum
						-2.2355960986428225,	//median
						-18.276901183002682,	//modal
						97.05309138180601,	//range
						841.8328031938537,	//variance
						29.014355122832796,	//standardDeviation
						-10.195656951112808	//variationCoefficient
					}
				};
			}
		}

		public void EmpiricalTestDataGenerator()
		{
			var samples = Enumerable.Range(0, 50).Select(i => (Random.Shared.NextDouble() - 0.5) * 100).ToArray();
			var arithmeticMean = samples.Average();
			var distinctSamplesSize = samples.Distinct().Count();
			var geometricMean = Double.RootN(samples.Where(s => s > 0).Aggregate((d1, d2) => d1 * d2), samples.Length);
			var quadraticMean = Double.RootN(samples.Select(s => s * s).Sum() / samples.Length, 2);
			var harmonicMean = samples.Length / samples.Select(s => 1 / s).Sum();
			var maximum = samples.Max();
			var minimum = samples.Min();
			var distinct = samples.Distinct().Order().ToArray();
			var median = distinct.Length % 2 == 0 ?
				distinct.Skip((distinct.Length / 2) - 1).Take(2).Sum() / 2 :
				distinct.Skip(distinct.Length / 2).Take(1).Sum();
			var modal = samples.GroupBy(s => s).OrderByDescending(g => g.Count()).First().Key;
			var range = maximum - minimum;
			var variance = samples.Select(s => s - arithmeticMean).Select(vi => vi * vi).Sum() / (samples.Length - 1);
			var standardDeviance = Double.RootN(variance, 2);
			var variationCoefficient = standardDeviance / arithmeticMean;

			var en = CultureInfo.GetCultureInfo("en-En");

			var result =
$@"					new Object[]{{
						new Double[]
						{{
							{String.Join(',', samples.Select(s => s.ToString(en)))}
						}},
						{arithmeticMean.ToString(en)},	//arithmetic mean
						{distinctSamplesSize},	//distinct sample size
						{geometricMean.ToString(en)},	//geometric mean
						{quadraticMean.ToString(en)},	//quadratic mean
						{harmonicMean.ToString(en)},	//harmonic Mean
						{maximum.ToString(en)},	//maximum
						{minimum.ToString(en)},	//minimum
						{median.ToString(en)},	//median
						{modal.ToString(en)},	//modal
						{range.ToString(en)},	//range
						{variance.ToString(en)},	//variance
						{standardDeviance.ToString(en)},	//standardDeviation
						{variationCoefficient.ToString(en)}	//variationCoefficient
					}}";
		}

		[TestMethod]
		[DynamicData(nameof(Samples))]
		public void Create(Double[] samples,
						   Double arithmeticMean,
						   Int32 distinctSampleSize,
						   Double geometricMean,
						   Double quadraticMean,
						   Double harmonicMean,
						   Double maximum,
						   Double minimum,
						   Double median,
						   Double modal,
						   Double range,
						   Double variance,
						   Double standardDeviation,
						   Double variationCoefficient)
		{
			var statistic = Statistic.Create<Double>(samples, division1, division2);

			AssertApproximateEquality(statistic.ArithmeticMean, arithmeticMean, nameof(statistic.ArithmeticMean));
			Assert.AreEqual(statistic.DistinctSampleSize, distinctSampleSize, nameof(statistic.DistinctSampleSize));
			AssertApproximateEquality(statistic.GeometricMean, geometricMean, nameof(statistic.GeometricMean));
			AssertApproximateEquality(statistic.QuadraticMean, quadraticMean, nameof(statistic.QuadraticMean));
			AssertApproximateEquality(statistic.HarmonicMean, harmonicMean, nameof(statistic.HarmonicMean));
			AssertApproximateEquality(statistic.Maximum, maximum, nameof(statistic.Maximum));
			AssertApproximateEquality(statistic.Minimum, minimum, nameof(statistic.Minimum));
			AssertApproximateEquality(statistic.Median, median, nameof(statistic.Median));
			AssertApproximateEquality(statistic.Modal, modal, nameof(statistic.Modal));
			AssertApproximateEquality(statistic.Range, range, nameof(statistic.Range));
			AssertApproximateEquality(statistic.Variance, variance, nameof(statistic.Variance));
			AssertApproximateEquality(statistic.StandardDeviation, standardDeviation, nameof(statistic.StandardDeviation));
			AssertApproximateEquality(statistic.VariationCoefficient, variationCoefficient, nameof(statistic.VariationCoefficient));

			Double division1(Double d, Int32 i)
			{
				return d / i;
			}
			Double division2(Int32 i, Double d)
			{
				return i / d;
			}
		}

		private static void AssertApproximateEquality(Double a, Double b, String message)
		{
			var delta = global::System.Math.Pow(10, -1 * ASSERTION_PRECISION);
			Assert.AreEqual(a, b, delta, message);
		}
	}
}