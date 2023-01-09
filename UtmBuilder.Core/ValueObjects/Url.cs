using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.ValueObjects
{
	public class Url : ValueObject
	{
		/// <summary>
		///  Creatre a new URL
		/// </summary>
		/// <param name="address">Address of URL (Website Link)</param>
		public Url(string address)
		{
			Address = address;

			InvalidUrlException.ThrowIfInvalid(Address);
		}

		/// <summary>
		/// Address of URL (Website Link)
		/// </summary>
		public string Address { get; }

		public override string ToString() => Address;
		public override int GetHashCode() => HashCode.Combine(Address);
		public override bool Equals(object? obj) => obj != null
											  && obj.GetType() == typeof(Url)
											  && ((Url)obj).Address == Address;
	}
}