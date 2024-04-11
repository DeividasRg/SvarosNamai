﻿namespace SvarosNamai.Web.Utility
{
    public class SD
    {
        public static string OrderAPIBase {  get; set; }
        public static string AuthAPIBase { get; set; }
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
