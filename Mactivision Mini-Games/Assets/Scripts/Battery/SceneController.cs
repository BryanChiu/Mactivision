using System.Collections.Generic;
using System.Collections;

public class SceneController
{
    private int Index = -1; // < 0 are Start Screens
    private string Start;
    private string End;
    private List<string> Games;

    // Unlikely to ever hit max, just there to prevent bugs.
    private int Max = 100;

    public SceneController(string start, string end, List<string> games)
    {
        Start = start;
        End = end;
        Games = games;
    }

    public void setIndex(int n)
    {
        Index = n; 
    }

    public void setMax(int n)
    {
        Max = n;
    }

    public void Next()
    {
        Index++;
    }

    public int Current()
    {
        return Index;
    }

    public string Name()
    {
        if (Index < 0)
        {
            return Start;
        }
      
        // Prevents issues with index or list sizes
        if (Index >= Max)
        {
            return End;
        }

        if (Index < Games.Count)
        {
            return Games[Index];
        }

        return End;
    }

}
