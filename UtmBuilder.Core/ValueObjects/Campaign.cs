using System.Collections.Specialized;
using System.Web;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.ValueObjects
{
	public class Campaign : ValueObject
	{
		/// <summary>
		/// Generate a new campaign for an URL
		/// </summary>
		/// <param name="source">The referrer (e.g. bing, newsletter)</param>
		/// <param name="medium">Marketing medium (e.g. cpc, banner)</param>
		/// <param name="name">Product, promo code or slogan (e.g. spring_sale)</param>
		/// <param name="id">The ads campaign id</param>
		/// <param name="term">Identify the paid keywords</param>
		/// <param name="content">Use to differentiate ads</param>
		public Campaign(string source, string medium, string name,
			string? id = null,
			string? term = null,
			string? content = null)
		{
			Source = source;
			Medium = medium;
			Name = name;

			Id = id;
			Term = term;
			Content = content;

			InvalidCampaignException.ThrowIfNull(Source, "Source is invalid");
			InvalidCampaignException.ThrowIfNull(Medium, "Medium is invalid");
			InvalidCampaignException.ThrowIfNull(Name, "Name is invalid");
		}

		/// <summary>
		/// The referrer (e.g. bing, newsletter)
		/// </summary>
		public string Source { get; }

		/// <summary>
		/// Marketing medium (e.g. cpc, banner)
		/// </summary>
		public string Medium { get; }

		/// <summary>
		/// Product, promo code or slogan (e.g. spring_sale)
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The ads campaign id
		/// </summary>

		public string? Id { get; }

		/// <summary>
		/// Identify the paid keywords
		/// </summary>
		public string? Term { get; }

		/// <summary>
		/// Use to differentiate ads
		/// </summary>
		public string? Content { get; }

		/// <summary>
		/// Creates URL Query String based on current values
		/// </summary>
		/// <returns>Query String</returns>
		public string ToQueryString()
		{
			NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
			queryString.Add("utm_source", Source);
			queryString.Add("utm_medium", Medium);
			queryString.Add("utm_campaign", Name);

			if (Id is not null)
				queryString.Add("utm_id", Id);

			if (Term is not null)
				queryString.Add("utm_term", Term);

			if (Content is not null)
				queryString.Add("utm_content", Content);

			return $"?{queryString}";
		}

		public override string ToString() => ToQueryString();
		public override int GetHashCode() => HashCode.Combine(Source, Medium, Name, Id, Term, Content);
		public override bool Equals(object? obj) => obj != null
											  && obj.GetType() == typeof(Campaign)
											  && ((Campaign)obj).Source == Source
											  && ((Campaign)obj).Medium == Medium
											  && ((Campaign)obj).Name == Name
											  && ((Campaign)obj).Id == Id
											  && ((Campaign)obj).Term == Term
											  && ((Campaign)obj).Content == Content;

		private static Campaign FromQueryString(string queryString)
		{
			NameValueCollection parsedQueryString = HttpUtility.ParseQueryString(queryString);

			string? source = string.Empty;
			string? medium = string.Empty;
			string? name = string.Empty;
			string? id = string.Empty;
			string? term = string.Empty;
			string? content = string.Empty;

			foreach (string? key in parsedQueryString.AllKeys)
				switch (key)
				{
					case "utm_source":
						source = parsedQueryString[key];
						break;

					case "utm_medium":
						medium = parsedQueryString[key];
						break;

					case "utm_campaign":
						name = parsedQueryString[key];
						break;

					case "utm_id":
						id = parsedQueryString[key];
						break;

					case "utm_term":
						term = parsedQueryString[key];
						break;

					case "utm_content":
						content = parsedQueryString[key];
						break;

					default:
						break;
				}

			return new Campaign(source, medium, name, id, term, content);
		}

		public static implicit operator string(Campaign campaign) => campaign.ToString();
		public static implicit operator Campaign(string queryString) => FromQueryString(queryString);
	}
}