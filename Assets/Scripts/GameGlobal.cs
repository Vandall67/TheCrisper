using UnityEngine;

public class GameGlobal : MonoBehaviour
{
    public static int levelcompleted = 0;



    public static void updatelevelcompleted(int x)
    {
        levelcompleted = x;
    }

    public static int getlevelcompleted()
    {
        return levelcompleted;
    }

}
