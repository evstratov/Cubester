public static class Utils 
{
    private static bool isFirstPlay;

    static Utils()
    {
        // TODO: read from xml
        FirstPlay = false;
    }
    

    public static bool FirstPlay { get; set; }

    public static bool FirstPhase { get; set; }
    public static bool SecondPhase { get; set; }

    public static bool ConfirmationPanelShowing { get; set; }

    public static bool GameOver { get; set; }

}
