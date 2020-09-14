using System.Collections;
using System.Collections.Generic;

public static class Utils 
{
    private static bool isFirstPlay;

    static Utils()
    {
        // TODO: read from xml
        isFirstPlay = true;
    }
    

    public static bool FirstPlay
    {
        get
        {
            return isFirstPlay;
        }
        set{
            isFirstPlay = value;
        }
    }

    public static bool StopTimer { get; set; }
}
