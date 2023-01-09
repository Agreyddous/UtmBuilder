using UtmBuilder.Core.Tests.ValueObjects;

namespace UtmBuilder.Core.Tests
{
	[TestClass]
	public class UtmTests
	{
		[TestMethod("Resulting Utm string should be a well formated URL")]
		[TestCategory("Utm Tests")]
		public void UtmShouldReturnURL()
		{
			Utm utm = ValidUtm();
			string result = WellFormattedUtmUrl(utm);

			Assert.AreEqual(result, utm.ToString());
			Assert.AreEqual<string>(result, utm);
		}

		[TestMethod("Utm should be correctly instantiated given a valid url")]
		[TestCategory("Utm Tests")]
		public void UtmCreatedFromValidURL()
		{
			Utm testUtm = ValidUtm();
			string validUrl = testUtm;

			Utm utm = validUrl;

			Assert.IsInstanceOfType(utm, typeof(Utm));

			Assert.AreEqual(testUtm.Url, utm.Url);
			Assert.AreEqual(testUtm.Campaign, utm.Campaign);

			Assert.AreEqual<string>(validUrl, utm);
		}

		internal static Utm ValidUtm() => new Utm(UrlTests.ValidUrl(), CampaignTests.ValidCompleteCampaign());
		internal static string WellFormattedUtmUrl(Utm utm) => $"{utm.Url.Address}?" +
															$"utm_source={utm.Campaign.Source}" +
															$"&utm_medium={utm.Campaign.Medium}" +
															$"&utm_campaign={utm.Campaign.Name}" +
															$"&utm_id={utm.Campaign.Id}" +
															$"&utm_term={utm.Campaign.Term}" +
															$"&utm_content={utm.Campaign.Content}";
	}
}