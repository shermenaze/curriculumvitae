public static class ExtensionMethods
{
    public static float Remap(this float value, float fromMin, float toMin, float fromMax, float toMax)
    {
        return (value - fromMin) / (toMin - fromMin) * (toMax - fromMax) + fromMax;
    }
}