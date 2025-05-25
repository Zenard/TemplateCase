using UnityEngine;

namespace Util.Extensions
{
    public static class RangeExtensions
    {
        /// <summary>
        /// Check if the value is in the specific range
        /// </summary>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(this RangeInt range, int value)
        {
            return value >= range.start && value < range.end;
        }
        
    }
}