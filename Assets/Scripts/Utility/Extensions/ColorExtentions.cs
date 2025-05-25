using UnityEngine;

namespace Util.Extensions
{
    public static class ColorExtentions
    {
        public static Color Merge(this Color c, Color other)
        {
            return new Color((c.r + other.r) / 2, (c.g + other.g) / 2, (c.b + other.b) / 2, (c.a + other.a) / 2);
        }
        public static Color Merge(this Color c, Color other,float weightThis,float weightOther)
        {
            float totalWeight = weightThis + weightOther;
            return new Color(((c.r * weightThis) + (other.r * weightOther))/totalWeight, ((c.g * weightThis) + (other.g * weightOther))/totalWeight, ((c.b * weightThis) + (other.b * weightOther))/totalWeight, ((c.a * weightThis) + (other.a * weightOther))/totalWeight);
        }
    }
}
