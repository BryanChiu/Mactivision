using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

enum ClientState
{
    CREATED = 0, // server does this by default
    GAME_STARTED = 1, 
    GAME_ENDED = 2,
    FINISHED = 3,
}

public class ClientServer
{
    private string Root = "http://127.0.0.1:8000/";
    private string Output = "output";
    private string Update = "updatestate";
    
    private string OutputPath()
    {
        return Root + Output;
    }

    private string UpdatePath()
    {
        return Root + Update;
    }

    private static string QueryString(IDictionary<string, object> dict)
    {
        dict.Add("token", Battery.Instance.GetToken()); // always send a token
        var list = new List<string>();
        foreach(var item in dict)
        {
            list.Add(item.Key + "=" + item.Value);
        }
        return string.Join("&", list);
    }

    private IEnumerator Post(string filename, string data, ClientState state)
    {
        var dict = new Dictionary<string, object>();
        dict.Add("state", (int)state);
        dict.Add("filename", filename);
        var query = QueryString(dict);

        var post = new UnityWebRequest (OutputPath() + "?" + query, "POST");
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        post.uploadHandler = new UploadHandlerRaw(bytes);
        post.downloadHandler = new DownloadHandlerBuffer();
        post.SetRequestHeader("Content-Type", "application/json");
        yield return post.SendWebRequest();

        if (post.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Network Error\n" + post.error);
        }
        else
        {
            Debug.Log("Post Success!");
        }
    }

    public IEnumerator PostGameEnd(string filename, string data)
    {
        return Post(filename,data, ClientState.GAME_ENDED);
    }

    public IEnumerator PostFinished(string filename, string data)
    {
        return Post(filename,data, ClientState.FINISHED);
    }

    public IEnumerator UpdateServerState(IDictionary<string, object> dict)
    {
        UnityWebRequest get = UnityWebRequest.Get("http://127.0.0.1:8000/updatestate?" + QueryString(dict));
        yield return get.SendWebRequest();

        if (get.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Network Error\n" + get.error);
        }
        else
        {
            Debug.Log("State Update to Server Success!");
        }
    }

    public IEnumerator UpdateServerGameStarted(float expected_game_length)
    {
        var dict = new Dictionary<string, object>();
        dict.Add("state", (int)ClientState.GAME_STARTED);
        dict.Add("maxgameseconds", (int)Math.Round(expected_game_length)); // better to send an int over urls then a large float
        return UpdateServerState(dict);
    }

}

