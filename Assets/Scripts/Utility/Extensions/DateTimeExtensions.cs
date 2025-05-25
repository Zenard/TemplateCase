using System;

namespace Util.Extensions
{
    public static class DateTimeExtensions 
    {
        public static string ToFormattedString(this System.DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static DateTime ConvertRegionalToUTC(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }
        public static DateTime ConvertUTCToRegional(this DateTime dateTime)
        {
            return dateTime.ToLocalTime();
        }
        public static DateTime SpecifyKindAsLocal(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        }
        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int LocalTicksToUnixSeconds(long localTicks)
        {
            var local = new DateTime(localTicks, DateTimeKind.Local);
            var utc = local.ToUniversalTime();
            int parsedValue = (int)(utc - UnixEpoch).TotalSeconds;
            return parsedValue;
        }

        public static long UnixSecondsToLocalTicks(long unixSeconds)
        {
            var utc = UnixEpoch.AddSeconds(unixSeconds);     // UTC DateTime
            var local = utc.ToLocalTime();                  // back to player’s zone
            return local.Ticks;                             // 100-ns ticks
        }

        public static long LocalTicksToUnixMillis(long localTicks)
        {
            var local = new DateTime(localTicks, DateTimeKind.Local);
            var utc   = local.ToUniversalTime();
            return (long)(utc - UnixEpoch).TotalMilliseconds;
        }

        public static long UnixMillisToLocalTicks(long unixMillis)
        {
            var utc = UnixEpoch.AddMilliseconds(unixMillis);
            var local = utc.ToLocalTime();
            return local.Ticks;
        }

    }
}
