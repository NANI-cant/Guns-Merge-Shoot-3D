namespace Gameplay.Economic {
    public static class EconomicExtensions {
        public static string ToEconomicString(this long value) {
            if (value >= 1000000000000) return (value / 1000000000000f).ToString("F1") + "T";
            if (value >= 1000000000) return (value / 1000000000f).ToString("F1") + "B";
            if (value >= 1000000) return (value / 1000000f).ToString("F1") + "M";
            if (value >= 1000) return (value / 1000f).ToString("F1") + "K";
            return value.ToString();
        }
    }
}