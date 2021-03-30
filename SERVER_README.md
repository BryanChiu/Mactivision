## Starting the Server
1. Login to the G-ScalE Lab server:
    * `ssh <user>@130.113.68.238`
    * Enter password for `<user>`
2. Check if `server.py` is already running:
    * `ps -aux | grep server.py`
    * If it is running, you will see something like this:
        ![](Repo%20Assets/server_check_if_running.png)
    * In the case it is running and you would like to restart it, terminate it:
        * `kill -2 <pid>`
            * Where `<pid>` is the process id from `ps -aux` (in this example, `<pid> = 873806`)

3. Start `server.py`:    
    * `cd ~/build`
    * `nohup python3 server.py input/Battery.json &`
    * You should see something like this:
        ![](Repo%20Assets/server_start_nohup.png)
    * You can verify that the server has started with:
        * `ps -aux | grep server.py`

## Server Documentation

### `server.py` Endpoints
The server listens for incomming HTTP requests on the following endpoints:

| Endpoint                       | HTTP Method | Parameters       | Description |
| ------------------------------ | ----------- | ---------------- | ----------- |
| `/`, `/Build`, `/TemplateData` | `GET`       | -                | Retrieves the build files used to run the minigames in the user's browser. (Called once when user navigates to website) |
| `/connect`                     | `GET`       | `token`, `state`, `maxgameseconds` | Used to set the state of the session with token `[token]` to one of `[state]={CREATED, GAME_STARTED}`. If `[state]=GAME_STARTED`, `[maxgameseconds]` is represents the maximum number of seconds it will take for the client to complete this minigame. The server uses this to set a timer (`[maxgameseconds] * 2`) which will terminate this session if no new requests are made before the timer is up. |
| `/connect`                     | `POST`      | `token`, `state` | Used to set the state of the session with token `[token]` to one of `[state]={GAME_ENDED, FINISHED}`. Additionally, client sends JSON data in the body of this request which is either the output of a minigame (`[state] = GAME_ENDED`), or the output of the battery (`[state]=FINISHED`). |

### `server.py` State Diagram
The following diagram represents the state changes that **each** battery session follows. The server keeps track of the state of each session seperately.

![](Repo%20Assets/server_state_diagram.svg)