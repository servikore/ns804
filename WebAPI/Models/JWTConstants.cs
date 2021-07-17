using System;

namespace WebAPI.Models
{
    public static class JWTConstants
    {
        public static readonly string Secret = "ns804_security_key";
        public static readonly string Audience = "ns804";
        public static readonly string Issuer = "ns804";
        public static readonly TimeSpan Expiration = TimeSpan.FromDays(1);
    }
}