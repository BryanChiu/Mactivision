## Uploading a Build of the Project
1. Build the project
    * In the Unity Editor window, go to `File > Build Settings...`
    * You should see the following dialog box:
        ![](Repo%20Assets/unity_build.png)
    * Make sure all 5 scenes are checked at the top, as shown above, and click `Build`, and follow the on-screen instructions to build as WebGL.
2. Move the build to the G-ScalE Lab server
    * Navigate to the output directory of the successful build.
    * Login to the G-ScalE Lab server to backup and remove the old build files from `~/build`
        * `index.html`
        * `Build/`
        * `TemplateData/`
    * Use `scp` or a similar program to move the contents of the build directory to `~/build` on the G-ScalE Lab server.
3. Follow the steps in [Starting the Server](#starting-the-server) to start the server with the new build.

## Starting the Server
1. Login to the G-ScalE Lab server
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

### `server.py` Architecture
`server.py` is a simple HTTP server written in Python 3 which manages all client sessions from incoming requests with a dictionary of `Session()` objects. 

Each session in the dictionary `SESSIONS` is addressed by a UUID token, which is generated and sent by the client in the initial `GET/connect` request. Every future request by each client **must** contain the same token so the server knows which client is making the request. 

The server cleans up sessions every 60 seconds by iterating through each session in `SESSIONS`, and checking if its `expire_time` is less than the current time (meaning the session has expired). If so, that session is removed from `SESSIONS`, and any incoming requests with the token of the expired session will no longer be accepted.

Each request sent from a client will move the session to a new state (outlined in [server.py State Diagram](#serverpy-state-diagram)) and set `expire_time` to 20 minutes from the current time. In the case of a session moving to the state `GAME_STARTED`, `expire_time` will be `2 * maxgameseconds + current_time`, where `maxgameseconds` is passed in the request.

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