using System.Collections.Generic;
using System.Collections;

public class SceneController
{
    private int Index;
    private string Start;
    private string End;
    private List<string> Games;

    public SceneController(string start, string end, List<string> games)
    {
        Index = -1;
        Start = start;
        End = end;
        Games = games;
    }

    public void Next()
    {
        Index++;
    }

    public string Name()
    {
        if (Index < 0)
        {
            return Start;
        }
        
        if (Index < Games.Count)
        {
            return Games[Index];
        }

        return End;
    }

    public int Number()
    {
        return Index;        
    }
}
