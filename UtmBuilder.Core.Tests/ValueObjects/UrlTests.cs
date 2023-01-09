using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Tests.ValueObjects
{
	[TestClass]
	public class UrlTests
	{
		internal const string InvalidAddress = "InvalidUrl";
		internal const string ValidAddress = "https://nandovbmgomes.io";

		[TestMethod("Should return an exception when the provided URL is invalid")]
		[TestCategory("URL Tests")]
		public void InvalidURLShouldReturnException() => Assert.ThrowsException<InvalidUrlException>(InvalidUrl);

		[TestMethod("Should not return an exception when the provided URL is valid")]
		[TestCategory("URL Tests")]
		public void ValidURLShouldNotReturnException() => Assert.IsInstanceOfType(ValidUrl(), typeof(Url));

		internal static Url ValidUrl() => new Url(ValidAddress);
		internal static Url InvalidUrl() => new Url(InvalidAddress);
	}
}