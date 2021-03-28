using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

enum ClientState
{
    CREATED = 0,
    GAME_STARTED = 1, 
    GAME_ENDED = 2,
    FINISHED = 3,
}

public class ClientServer
{
    private string Root = "http://127.0.0.1:8000/";
    private string Output = "output";
    private string Update = "update";
    
    private string OutputPath()
    {
        return Root + Output;
    }

    private string UpdatePath()
    {
        return Root + Update;
    }

    public UnityWebRequest UpdateServerCreateRequest()
    {
        // return a request instead of doing the whole thing so that
        // we can send error messages to the start screen console.
        var dict = new Dictionary<string, object>();
        dict.Add("state", (int)ClientState.CREATED);
        var query = QueryString(dict);
        return UnityWebRequest.Get(UpdatePath() + "?" + query);
    }

    private static string QueryString(IDictionary<string, object> dict)
    {
        // always send a token
        dict.Add("token", Battery.Instance.GetToken());
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

        var post = new UnityWebRequest(OutputPath() + "?" + query, "POST");
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
        var query = QueryString(dict);
        UnityWebRequest get = UnityWebRequest.Get(UpdatePath() + "?" + query);
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
        // better to send an int over urls then a large float
        dict.Add("maxgameseconds", (int)Math.Round(expected_game_length)); 
        return UpdateServerState(dict);
    }

}

