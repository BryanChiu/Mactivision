using System.Collections;
using UnityEngine;

public class ClientServerMock : ClientServer
{

    public string filename;
    public string data;

    public ClientServerMock() : base("")
    {
    }
    public override IEnumerator PostGameEnd(string filename, string data)
    {
        this.filename = filename;
        this.data = data;
        yield return null;
    }

}