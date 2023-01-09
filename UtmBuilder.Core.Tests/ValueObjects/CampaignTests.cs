using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Tests.ValueObjects
{
	[TestClass]
	public class CampaignTests
	{
		internal const string ValidSource = "Source";
		internal const string ValidMedium = "Medium";
		internal const string ValidName = "Name";
		internal const string ValidId = "Id";
		internal const string ValidTerm = "Term";
		internal const string ValidContent = "Content";

		[TestMethod("Testing combinations of valid and invalid required parameters for Campaign")]
		[TestCategory("Campaign Tests")]

		[DataRow("", "", "", false)]
		[DataRow(ValidSource, "", "", false)]
		[DataRow("", ValidMedium, "", false)]
		[DataRow("", "", ValidName, false)]
		[DataRow(ValidSource, ValidMedium, "", false)]
		[DataRow(ValidSource, "", ValidName, false)]
		[DataRow("", ValidMedium, ValidName, false)]

		[DataRow(ValidName, ValidMedium, ValidName, true)]

		public void TestCampaignCreation(string source, string medium, string name, bool shouldBeValid)
		{
			if (shouldBeValid)
				Assert.IsInstanceOfType(new Campaign(source, medium, name), typeof(Campaign));

			else
				Assert.ThrowsException<InvalidCampaignException>(() => new Campaign(source, medium, name));
		}

		internal static Campaign ValidCampaign() => new Campaign(ValidSource, ValidMedium, ValidName);
		internal static Campaign ValidCompleteCampaign() => new Campaign(ValidSource, ValidMedium, ValidName, ValidId, ValidTerm, ValidContent);
		internal static Campaign InvalidCampaign() => new Campaign(string.Empty, string.Empty, string.Empty);
	}
}