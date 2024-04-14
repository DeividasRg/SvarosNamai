namespace SvarosNamai.Web.Utility
{
	public class SD
	{
		public static string OrderAPIBase { get; set; }
		public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public const string TokenCookie = "JWTToken";
		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
		public enum OrderStatus
		{
			Pending = 0,
			Approved = 1,
			Completed = 2,
			Cancelled = -1
		}
		public static string GetStatusDescription(int statusCode)
		{
			switch (statusCode)
			{
				case (int)OrderStatus.Pending:
					return "Laukiantis patvirtinimo";
				case (int)OrderStatus.Approved:
					return "Patvirtintas";
				case (int)OrderStatus.Completed:
					return "Užbaigtas";
				case (int)OrderStatus.Cancelled:
					return "Atšauktas";
				default:
					return "Nežinomas";
			}
		}
	}
}
