using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core
{
	public class Utm
	{
		public Utm(Url url, Campaign campaign)
		{
			Url = url;
			Campaign = campaign;
		}

		/// <summary>
		/// URL (Website Link)
		/// </summary>
		/// <value></value>
		public Url Url { get; }

		/// <summary>
		/// Campaign details
		/// </summary>
		public Campaign Campaign { get; }

		private static Utm FromUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
				throw new InvalidUrlException();

			string[] urlSegments = url.Split('?');

			if (urlSegments.Length != 2)
				throw new InvalidUrlException("No segments were provided");

			return new Utm(new Url(urlSegments[0]), urlSegments[1]);
		}

		public override string ToString() => $"{Url}{Campaign.ToQueryString()}";
		public override int GetHashCode() => HashCode.Combine(Url, Campaign);
		public override bool Equals(object? obj) => obj != null
											  && obj.GetType() == typeof(Utm)
											  && ((Utm)obj).Url == Url
											  && ((Utm)obj).Campaign == Campaign;

		public static implicit operator string(Utm utm) => utm.ToString();
		public static implicit operator Utm(string url) => FromUrl(url);
	}
}