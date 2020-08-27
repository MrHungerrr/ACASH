
public static class PlayerSettings
{
    public static float SensetivityCoef => _sensetivityCoef;

    private static float _sensetivityCoef;


    public static void Sensetivity(float sensetivity)
    {
        _sensetivityCoef = sensetivity;
    }
}
