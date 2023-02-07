namespace Application.Helpers
{
    public class RoundingHelper
    {
        public static decimal RoundEarnings(decimal hours)
        {
            decimal value = hours;
            int factor = 4;
            decimal nearestMultiple =
                    Math.Round(value * factor) / factor;

            return nearestMultiple;
        }
    }
}
