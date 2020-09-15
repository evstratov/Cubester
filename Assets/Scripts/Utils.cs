public static class Utils 
{
    private static bool isFirstPlay;

    static Utils()
    {
        // TODO: read from xml
        FirstPlay = true;
    }
    
    ///<summary>
    ///При первом запуске игры возвращает true 
    ///</summary>
    public static bool FirstPlay { get; set; }
    ///<summary>
    /// Если игра на паузе, true
    ///</summary>
    public static bool PauseGame { get; set; }

    ///<summary>
    /// когда обучение свайпу в сторону, возвращает true 
    ///</summary>
    public static bool FirstPhase { get; set; }
    ///<summary>
    /// когда обучение свайпу по вертикали, возвращает true 
    ///</summary>
    public static bool SecondPhase { get; set; }
    ///<summary>
    /// во время отображения панели подверждения, возвращает true 
    ///</summary>
    public static bool TapToContinueButtonShowing { get; set; }

    ///<summary>
    /// когда игра окончена, возвращает true 
    ///</summary>
    public static bool GameOver { get; set; }

    ///<summary>
    /// когда надо остановить время, возвращает true 
    ///</summary>
    public static bool IsGamePause() 
    {
        if (FirstPhase || SecondPhase || TapToContinueButtonShowing || GameOver || PauseGame)
            return true;
        else
            return false;
    }

}
