__Mactivision Team Members:__
* Kunyuan Cao
* Bryan Chiu
* David Hospital
* Michael Tee
* Sijie Zhou

Table of Contents
=================
* [Revision History](#revision-history)
* 1 [Introduction](#1-introduction)
  * 1.1 [Document Purpose](#11-document-purpose)
  * 1.2 [Product Scope](#12-product-scope)
  * 1.3 [Definitions, Acronyms and Abbreviations](#13-definitions-acronyms-and-abbreviations)
  * 1.4 [References](#14-references)
  * 1.5 [Document Overview](#15-document-overview)
  * 1.6 [Work Scope](#16-work-scope)
* 2 [Product Overview](#2-product-overview)
  * 2.1 [Product Perspective](#21-product-perspective)
  * 2.2 [Product Functions](#22-product-functions)
  * 2.3 [Product Constraints](#23-product-constraints)
  * 2.4 [User Characteristics](#24-user-characteristics)
  * 2.5 [Assumptions and Dependencies](#25-assumptions-and-dependencies)
  * 2.6 [Apportioning of Requirements](#26-apportioning-of-requirements)
  * 2.7 [Stakeholders](#27-stakeholders)
* 3 [Requirements](#3-requirements)
  * 3.1 [External Interfaces](#31-external-interfaces)
    * 3.1.1 [User Interfaces](#311-user-interfaces)
    * 3.1.2 [Hardware Interfaces](#312-hardware-interfaces)
    * 3.1.3 [Software Interfaces](#313-software-interfaces)
  * 3.2 [Functional](#32-functional)
  * 3.3 [Quality of Service](#33-quality-of-service)
    * 3.3.1 [Performance](#331-performance)
    * 3.3.2 [Security](#332-security)
    * 3.3.3 [Reliability](#333-reliability)
    * 3.3.4 [Availability](#334-availability) 
  * 3.4 [Compliance](#34-compliance)
  * 3.5 [Design and Implementation](#35-design-and-implementation)
    * 3.5.1 [Installation](#351-installation)
    * 3.5.2 [Distribution](#352-distribution)
    * 3.5.3 [Maintainability](#353-maintainability)
    * 3.5.4 [Reusability](#354-reusability)
    * 3.5.5 [Portability](#355-portability)
    * 3.5.6 [Cost](#356-cost)
    * 3.5.7 [Deadline](#357-deadline)
    * 3.5.8 [Proof of Concept](#358-proof-of-concept)
* 4 [Verification](#4-verification)
* 5 [Appendixes](#5-appendixes)
  * 5.1 [Appendix A: Figures and Excerpts](#51-appendix-a-figures-and-excerpts)
    * 5.1.1 [Cognitive and Motor Abilities](#511-cognitive-and-motor-abilities)
    * 5.1.2 [Project Background](#512-project-background)

## Revision History
| Name | Date              | Reason For Changes                | Version   |
| ---- | ----------------- | --------------------------------- | --------- |
| Team | October 19, 2020  | First Version                     | 1.0.0     |
| Team | November 12, 2020 | Updates from design document      | 1.1.0     |
| Team | November 18, 2020 | More updates from design document | 1.2.0     |
| Team | January 1, 2021   | Prototype 2 updates               | 2.0.0     |
| Team | March 28, 2021    | Final Version updates             | 3.0.0     |

## 1. Introduction
This section will provide an overview of the entire document.

### 1.1 Document Purpose

The purpose of the SRS is to specify the functional and non-functional requirements of the project. It describes how the software will be developed and lays out the road map that will be followed by Mactivision. The SRS will be used internally to help facilitate development during the project's life cycle. It will also be used by Sasha Soraine and Dr. Jacques Carette as supervisors of the project. 

### 1.2 Product Scope

Three Mini-games will be created to measure player Cognitive and Motor Abilities, a Battery that administers multiple Mini-games of varying configurations, a set of Modules to measure player abilities, a Data Manager to load and store Battery configurations and to aggregate module data, and a Server to host the Battery, as well as, store data from each completed Battery.

Each Mini-game will attempt to measure a subset of abilities from the Cognitive and Motor Abilities table (see Appendix 5.1.1). These profiles will be used to provide a base line of player abilities for use in further games related testing. 

The first Mini-game will profile finger pressing.

The second Mini-game will profile updating working memory.

The third Mini-game will profile divided attention.

A Battery provides an interface from which players will be given Mini-games to play. The Battery must have a start screen, the ability to switch between multiple Mini-games of differing configurations and an end screen. Players will be sent a URL to the Battery from the Researchers. The players will interact with the Battery and the Mini-games using their keyboard and web browser. Researchers have the ability to administer copies of each Mini-game each with their own unique configuration. These configurations, alter the behavior of the Mini-game, allowing Researchers to fine tune the games to provide better profiling. Researchers can choose which Mini-games get played, in which order and how many times. 

A set of Metric Modules will be created to measure cognitive and physical abilities. A Mini-game that targets a set of abilities will require the relevant set of modules. These modules handle the collection data for abilities for the duration of the Mini-game.

A Client-Server Module will to take the data from the Battery and Metric Modules send the data to the Server to be stored.  

Mactivision's objective is to develop the above applications to help Sasha’s project in G-ScalE and also provide an opportunity for the team to use and continue to software develop the skills they have obtained throughout their studies.

### 1.3 Definitions, Acronyms and Abbreviations

__Cognitive and Motor Abilities__

There are 46 _Cognitive and Motor Abilities_ which are axiomatic abilities used by a human while playing through challenges in video games. We wish to create Mini-games which will measure these abilities for a user playing them. Tables displaying a full list of these abilities can be found in [Section 5.1.1](#511-cognitive-and-motor-abilities)

__Mini-game__

A game designed to measure a subset of abilities, from the Cognitive and Motor Abilities, of the player. 

__Battery__

Provides an interface from which players will be given Mini-games to play.

__Metric Modules__

A set of Metric Modules that measure individual cognitive and physical abilities.

__Player__

Users who will be playing the Mini-games and having their Cognitive and Motor Abilities measured.

__Administrator / Researcher__

Users that will be administering the Battery to Players, configuration in the Battery, and analyzing the measurement results.

__Player Profile__

A collection of measurement data for a given player created after the player's completion of a Battery.

__Unity__

Unity is a cross platform game engine with powerful development tools to enable developers to more easily create games.

__WebGL__

A graphics API based on OpenGL ES which allows browsers to render graphics was if they were desktop applications.

__OpenGL__

An open source graphics API which provides cross platform access to the GPU in order to provide applications with hardware accelerated rendering.

__GPU__

A specialized processing unit designed to render graphics.

__UX__

User interface and design.

### 1.4 References

[1]: __Unity Engine Input System__: https://docs.unity3d.com/ScriptReference/Input.html

[2]: __Mozilla WebSockets API__: https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API

[3]: __C# Coding Conventions__: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions

[4]: __Create Commons License__: https://en.wikipedia.org/wiki/Creative_Commons_licensehttps://en.wikipedia.org/wiki/Creative_Commons_license

[5]: __McMaster Academic Integrity Policy__: https://secretariat.mcmaster.ca/app/uploads/Academic-Integrity-Policy-1-1.pdf

[6]: __Soraine, S and Carette, J. (2020). Mechanical Experience, Competency Profiles, and Jutsu. Journal of Games, Self, and Society. Vol .2. p. 150 – 207__: https://press.etc.cmu.edu/index.php/product/journal-of-games-self-society-vol-2-issue-1/

[7]: __Soraine, S. (2018). Ooh What's This Button Do__: https://macsphere.mcmaster.ca/handle/11375/24028 

[8]: __Cognifit__: https://www.cognifit.com/cognitive-assessment/Battery-of-tests/wom-rest-test

[9]: __Eysenck, Michael W., and Mark T. Keane. Cognitive psychology__ : a student's handbook. Hove, Eng. New York: Psychology Press, 2010. 

### 1.5 Document Overview

This document contains the functional and non functional requirements for the Mactivision Mini-game Battery Project, specifically, the tools, methods, and systems required to get the project from the design stage to final version. 

### 1.6 Work Scope

Completed | Work                          | Description
----------|-------------------------------|----------------------
✅ | Requirements Specification     | Complete SRS document.
✅ | Prototype                      | Half of the group works on the Metric Modules and the other half creates one Mini-game that showcases the interaction between the player and the game and the game and the Data Manager. 
✅ | Design Battery Module          | Battery administrates the Battery of Mini-games.
✅ | Design Mini-game 1             | Digger game. Measure finger button key presses. Create a game objective that requires mashing. Choose which assets to use.
✅ | Design Mini-game 2             | Feeder game. Measure updating working memory. Create a game objective that requires an NPC to choose which food - they want to eat, player has to remember and feed them. Choose which assets to use
✅ | Design Mini-game 3             | Rockstar game. Measures divided attention. Create a game objective that requires the player put on a good show while interacting with technical problems that could ruin it. Choose which assets to use.
✅ | Design Metric Modules          | Create Metric Modules that games can use the measure the specific abilities they wish to record.
✅ | Design Data Presentation       | Take data created from Battery and present it in such a way that it is easy for researchers to use. 
✅ | Design Document                | After reflecting on the good and bad decisions from the prototype, redesign prototype using data collected to more accurately measure targeted cognitive and physical abilities of that particular game. Formalize a list of design decisions for games 2, 3 the Battery module, Metric Modules and Data Manager. 
✅ | Create Mini-game 1             | Mostly complete from prototype 1. Make any refinements needed.
✅ | Create Mini-game 2             | Make a prototype game 2 from design document.
✅ | Create Mini-game 3             | Make a prototype game 3 from design document.
✅ | Create Metric Modules          | Create sufficient Metric Modules in order to capture the basic requirements from game 1, 2, and 3.
✅ | Create Battery Module          | Battery module should be able to load a new Battery and run game 1, 2, 3 in sequence, end Battery and output recorded data.
✅ | Prototype 2.0                  | Prototype 2.0 should be able to run a Battery module, run the three games, have each game output meaningful player ability performance data, end the Battery and then the Data Manager should be able to present that data in a meaningful way. 
✅| Internal and External Testing   | Internal Testing involves creating Unit , Acceptance and Integration tests. External Testing hands Prototype 2 to other development groups to find outstanding bugs from the Internal Testing phase and fix them.
✅| Polish Code          		   | Add graphics and audio to the games to make them feel more like games and less like tests. Clean up old code and tests that are no longer used. Refactor code and add more comments.
✅ | Final Version                  | Code, Testing, SRS, Design Doc and User Manuals are complete.
⬜️ | Capstone Day Showcase          | Showcase the Mini-games on Capstone day.

__Mini-game Inspirations:__

NdCube. Looking for Love. Super Mario Party. [Nintendo Switch]. (2018)

__Context of work:__

The work will be graded for the 4ZP6 capstone course.

The Mini-game will be used in G-ScalE project after the development.

## 2. Product Overview

This section will give an overview of the entire architecture of the system and its requirements, as well as the general factors that will affect the system. Further information regarding the requirements will be defined in detail in [Section 3](#3-requirements).

### 2.1 Product Perspective

This system will consist of five parts: three Mini-games, a Battery, a set of Metric Modules, a Client-Server Module and a Server. A Battery will be hosted on a Server.The Battery will initialize a series of Mini-games for the player to play from a configuration on the Server. Each Mini-game will communicate with the Metric Modules to measure abilities. A Client-Server Module send Battery and Metric Module data from the Mini-games to the Server for storage.

Administrator or Researchers will develop a Battery configuration they wish to distribute. To distribute to players they will start the Server and send the URL to the players. Players will visit the Battery hosted on the Server with their Web Browser. 

The Battery consists of a Battery Manager to manage scene transitions in Unity as well as a start and end screen. Each Battery will have a configuration detailing which Mini-Games to run and with what configurations for each game. The Battery also contains a Client-Server Module to send data back and forth between the client or player side Battery and the Server. At the end of the Battery the configuration used for the current Battery is stored on the Server to allow researchers to know which Battery configuration produces which results. 

Each Mini-game will be scene inside the Unity engine. Each game will feature graphics and audio to make the game feel more like a game and less like a test. The user input in these Mini-games will be restricted to keyboard input, as keyboards are widely available to most users, however input abstraction in the Unity engine will be made use of in order to switch between input types.  

The Metric Modules are responsible for measuring the data generated by players of the Mini-games. Each Mini-game will record a set of metrics from the player. This data will include key presses, completion of objectives, the time spent playing the game, etc. When the Mini-game is complete the Battery uses the ClientServer Module to send the Metric Module data to the Server.

### 2.2 Product Functions

* Administrator / Researcher creates Battery configuration. 
* Administrator / Researcher starts Server.
* Administrator / Researcher send Battery URL to Players. 
* Players navigate their Web Browser to URL.
* Battery gets configuration from Server.
* Player starts Battery.
* The Player is directed to the start screen.
* Player plays Mini-games defined by Battery Configuration.
* Each Mini-game uses Metric Modules to measure player abilities.
* After each Mini-game is complete the Battery uses the Client-Server Module to send the data to the Server.
* Once the Player has completed all Mini-games the Battery uses the Client-Server Module to send the configuration back to the Server signaling the Battery has ended.
* The Battery sends the Player to the end screen.
* The Player is finished and can closet he page.
* Administrator / Researcher analyses the results stored on Server.
* Close Server when the Battery of that specific configuration is no longer required.

### 2.3 Product Constraints

* All Mini-games must be built using the Unity engine, and must be designed for play on a WebGL compatible browser. 
* The games must be able to be run on machines as possible because the device type of each player will be unknown. 
* The required game controller is the keyboard because players may not have access to a mouse or other types of controllers. 
* A server must be running to serve the Battery and to store data sent to the server by the Battery.
* WebGL compatible browsers.
* Each Mini-game should take no longer than one minute to finish.
* The Mini-Games should attempt to measure the smallest subset of abilities possible.
* The Mini-Games should feel like games not like tests.
* Need a Server to host the Battery and store data collected from Battery.
* Player requires an internet connection.

### 2.4 User Characteristics

__There are two user classes which will use this product:__

* Players who will play the Mini-games. This group of users is very broad and will contain users from many different demographics. However, these users should have basic computer literacy in order to run the Battery from their Web Browser.

* Administrators / Researchers will be able to administrate the Server, create configurations for the Battery and modify variables inside the Battery configuration to alter the behavior of the Mini-games to fine-tune ability measurements. This group should also be able to understand the abilities being tested and the results from a completed Battery. 

### 2.5 Assumptions and Dependencies

The Mini-games portion of this project will rely on assets from the Unity asset store to develop the Mini-games. It is assumed that developers will be able to find relevant assets in the Unity asset store to build Mini-games that fit the requirements described in this document in a more time-efficient manner, as opposed to creating the assets from scratch. These assets include:

* 2D Sprites
* Sound clips
* Music
* Unity Plugins

[JsonDotNet](https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347) 2.0.1 Unity package will be used to convert C# object into JSON strings.

[Free Platform Game Assets](https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838) Unity package will be used as the primary source of game graphics for the Mini-games. The red character will be used as the avatar for the player across multiple Mini-games.

[Free Sound Effects](https://freesound.org/) Audio clips are used in the Mini-games to create sound effects.

[Youtube Music Library](https://www.youtube.com/audiolibrary) Audio Library is a channel dedicated to search, catalog, sort and publish No Copyright Music

### 2.6 Apportioning of Requirements

__Server Module__
* Manage multiple users running the Battery.
* Clean up sessions tokens from users no longer playing the game.
* Handle network errors.
* Load a configuration that will be sent to the Battery upon request.
* Change Server states to sync up with Battery state.
* Start Server.
* Stop Server.

__Client-Server Module__
* Manage session token between Battery (client) and Server.
* Tell the server a Battery has started.
* Tell the server a Mini-game has started.
* Tell the server a Mini-game has ended.
* Tell the server a Battery has finished.
* Handle POST and GET web requests to and from the server.

__Battery Module__
* Connects to the server and gets the configuration file.
* Creates a session token so the server can handle multiple users.
* Handles network errors.
* Loads configuration file.
* Determines if a configuration file is valid.
* Switch between Mini-Games.
* Switch to Start Screen and End Screen.
* Use Client-Server to send which configuration file was used to the server.
* Allow developers to generate a blank configuration file.

__Metric Modules__
* Each module records one metric attempting to capture a cognitive or motor ability.
* Multiple modules may be used in Mini-games to capture abilities that cannot be easily separated.

__Mini-games__
* Provide instructions to the player on how to play the game.
* Provide a countdown to start.
* Start the game.
* End the game.
* Display graphics and play audio.
* Use Metric Modules to measure abilities targeted by the game.
* At the end of the game use Client-Server Module to send Metric Module data to server.

### 2.7 Stakeholders

The stakeholders for this project are:
* __Sasha Soraine__: Supervisor of this project
* __Dr. Jacques Carette__: Supervisor of this project
* __Researchers__: Academics investigating the correlations games and Cognitive and Motor Abilities.
* __Games Designers__: Designers looking for player ability performance data to inform their design decisions when creating new player challenges.
* __Players__: The people who will play the Mini-games, and will have their Cognitive and Motor Abilities measured and recorded into player profiles.
* __McMaster University__: The project is created by the students of McMaster University and should uphold the policies and standards of that institution.
* __Mactivision__: Team of COMP SCI 4ZP6 students, consisting of __Kunyuan Cao__, __Bryan Chiu__, __David Hospital__, __Michael Tee__, and __Sijie Zhou__, who are responsible for designing, implementing, and testing all software outlined in this document.
* __Ethan Chan and Brendan Fallon__: COMP SCI 4ZP6 TAs who will be evaluating a portion of the deliverables. 

## 3. Requirements

__Server Module Settings Table__

These are the options which can be altered in the Server Module to change it's behavior.

Option        | Description
----------------|--------------------------------
IP              | IP of the server. Default "".
PORT            | Port of the server. Default 8000.
OUTPUT\_PATH    | Output path for Mini-game measurement data and Battery configuration. Default is ROOT + "/output/" 
CLEANUP\_TIME   | Every X seconds clean up session data. Default is 60
EXPIRE\_TIME    | If a session has gone over X minutes mark session for clean up. Default is 20 minutes. 

__Client-Server State Table__

The follow is a list of states that is shared between the Battery and the Server.

State Name    | Description
--------------|--------------------------------
CREATED       | Battery has started
GAME\_STARTED | A Mini-game has started
GAME\_ENDED   | A Mini-game has ended
FINISHED      | Battery has finished.

__Mini-game State Table__

The follow is a list of states a generic Mini-game can be in.

State Name    | Description
--------------|--------------------------------
SETUP         | Mini-game Setup()
START\_LEVEL  | Mini-game has started
END\_LEVEL    | Mini-game has ended
INTRO         | Mini-game is display start of game instructions.
OUTRO         | Mini-game is display end of game instructions.

__Product Use Case (PUC) Table (Metric Module)__
PUC | PUC Name           | Actor(s)         | Input / Output
----|--------------------|------------------|---------------
M-1 | Start Recording    | System           | Success(OUT)
M-2 | Stop Recording     | System           | Success(OUT)
M-3 | Record Event       | System           | Event(IN) Success(OUT)
M-4 | Button Event       | Player, System   | Time DateTime(IN) KeyCode(IN) KeyState Bool(IN)
M-5 | Position Event     | Player, System   | Vector3(IN) 
M-6 | Memory Event       | Player, System   | Time DateTime(IN) Answers List (IN) Current String(IN) Choice Bool(IN)
M-7 | Get Metrics        | System           | JSON String(OUT)

__Product Use Case (PUC) Table (Mini-Game Module)__
PUC  | PUC Name           | Actor(s)         | Input / Output
-----|--------------------|------------------|---------------
G-1  | Move Player        | Player           | Key Input(IN), Pos Vector3(OUT)
G-2  | Move Camera        | Player, System   | Key Input(IN), Pos Vector3(OUT)
G-3  | Move Entity        | System           | Key Input(IN), Pos Vector3(OUT)
G-4  | Animate Sprite     | Player, System   | Sprites EntityList(IN), Animation Entity(OUT)
G-5  | Switch Animation   | Player, System   | Event(IN), Animation Entity(OUT)
G-6  | Get Event          | System           | Unity Event(OUT)
G-7  | Create Entity      | System           | Game Entity(IN)
G-8  | Destroy Entity     | System           | Game Entity(IN)
G-9  | Set Physics        | System           | Game Entity(IN)
G-10 | Set Level State    | System           | State Object(IN), State Object(OUT)
G-11 | Setup              | System           | LevelState Int(OUT)
G-12 | Start Level        | System           | LevelState Int(OUT)
G-13 | End Level          | System           | Delay Float(IN) Success Bool(OUT)
G-14 | Countdown          | System           | LevelState Int(OUT)
G-15 | Show GUI           | System           | Success Bool(OUT)
G-16 | Hide GUI           | System           | Success Bool(OUT)
G-17 | On GUI             | System           | Event Event(IN) LevelState(OUT)
G-18 | Update             | System           | GameConfig(OUT)

__Product Use Case (PUC) Table (Battery Module)__
PUC  | PUC Name             | Actor(s)         | Input / Output
-----|----------------------|------------------|---------------
B-1  | Start Battery        | System           | Success Bool(OUT)
B-2  | Serialize Config     | System           | Config String(OUT)
B-3  | Current Game Config  | System           | Config GameConfig(OUT)
B-4  | Load Battery         | System           | Config String(IN) BatteryConfig(OUT)
B-5  | Load Scene           | System           | Scene String(IN) Success Bool(OUT)
B-6  | End Battery          | System           | Success Bool(OUT)
B-7  | Get Current Time     | System           | Time DateTime(OUT)
B-8  | Load Next Scene      | System           | Success Bool(OUT)
B-9  | Get Current Scene    | System           | Scene String(OUT)
B-10 | Game Name List       | System           | Names List<String>(OUT)
B-11 | Write Blank Config   | System           | JSON File(OUT) 
B-12 | Create Session Token | System           | Token String(OUT)
B-13 | Send Config to Server| System           | Filename String(IN) Token String(IN) State String(IN) JSON String(OUT)

__Product Use Case (PUC) Table (Server Module)__
PUC | PUC Name           | Actor(s)         | Input / Output
----|--------------------|------------------|---------------
S-1 | Load Config        | Admin            | JSON String(IN)
S-2 | Check Bad Config   | Server           | JSON String(IN) Exception(OUT)
S-3 | Start              | Admin            | Arguments String(IN)
S-4 | Stop               | Admin            | Interrupt Keyboard(IN)
S-5 | Send Config to Battery | Server           | JSON HTTP(OUT)
S-6 | Create Session     | Server           | Token String(IN)
S-7 | Delete Session     | Server           | Token String(IN)
S-8 | Update Session     | Server           | Token String(IN) State Int(INT) Sessions(OUT)
S-9 | Receive Post File Request | System           | Post HTTP(IN) URL String(IN) Data File(INT) Sessions(OUT)
S-10 | Receive Get Request | System           | Get HTTP(IN) URL String(IN) Data File(INT) Sessions(OUT)

__Product Use Case (PUC) Table (Client-Server Module)__
PUC | PUC Name           | Actor(s)         | Input / Output
----|--------------------|------------------|---------------
C-1 | Handle Get Request   | System           | URLQuery String(IN) Success Bool(OUT)
C-2 | Handle Post Request  | System           | Data JSON(IN) URLQuery String(IN) Success Bool(OUT)

__Individual Product Use Cases__

PUC M-1       | Event: Start Recording
--------------|--------------------------------
Trigger       | Start of measurement recording.
Preconditions | A Mini-game has started.
Procedure     | Records date and time for the current Mini-game.
Outcome       | Store start date and time. 

PUC M-2       | Event: Stop Recording
--------------|--------------------------------
Trigger       | Stop of measurement recording.
Preconditions | A Mini-game has ended.
Procedure     | Records date and time for the current Mini-game.
Outcome       | Store end date and time. 

PUC M-3       | Event: Record Event
--------------|--------------------------------
Trigger       | Mini-games asks to record event.
Preconditions | A Mini-game is running.
Procedure     | Record event by event type.
Outcome       | Stores events in a list.

PUC M-4       | Event: Button Event
--------------|--------------------------------
Trigger       | Mini-game calls button event.
Preconditions | A Mini-game is running.
Procedure     | Records the up and down states of a button.
Outcome       | Button events are added to a list of buttons events.

PUC M-5       | Event: Position Event
--------------|--------------------------------
Trigger       | Mini-game calls position event.
Preconditions | A Mini-game is running.
Procedure     | Records the current position of entity.
Outcome       | Position event is added to a list of positions for that entity.

PUC M-6       | Event: Memory Event
--------------|--------------------------------
Trigger       | Mini-game calls memory event.
Preconditions | A Mini-game is running.
Procedure     | Records expected outcome and actual outcome from a Mini-game objective.
Outcome       | Expected outcome and actual outcomes is added to a list of memory events.

PUC M-7       | Event: Get Metrics
--------------|--------------------------------
Trigger       | Mini-game sends metrics to Server.
Preconditions | A Mini-game has ended and Server is running.
Procedure     | Send recorded events to Server in JSON format.
Outcome       | JSON file containing events is created sent to Server using Client-Server Module.

PUC G-1       | Event: Move Player
--------------|--------------------------------
Trigger       | Player input or Mini-game moves player.
Preconditions | A Mini-game is running.
Procedure     | Update player position in game world.
Outcome       | Player moves to new coordinates.

PUC G-2       | Event: Move Camera
--------------|--------------------------------
Trigger       | Player moves camera or camera follows player.
Preconditions | A Mini-game is running.
Procedure     | Get player input. Set new camera coordinates.
Outcome       | Camera is moved.

PUC G-3       | Event: Move Entity
--------------|--------------------------------
Trigger       | A Mini-game requires entity to move or player interacts with entity.
Preconditions | A Mini-game is running.
Procedure     | Set new entity coordinates.
Outcome       | Entity is moved.

PUC G-4       | Event: Animate Sprite
--------------|--------------------------------
Trigger       | A Mini-game calls animate sprite.
Preconditions | A Mini-game is running.
Procedure     | Display sprites in a sequence in order to give the appearance that the entity is animated.
Outcome       | Sprite animation appears on screen.

PUC G-5       | Event: Switch Animation
--------------|--------------------------------
Trigger       | A Mini-game calls switch animation.
Preconditions | A Mini-game is running.
Procedure     | A sequence of sprites is switched to another sequence of sprites.
Outcome       | Sprite animation changes from one to another.

PUC G-6       | Event: Get Event
--------------|--------------------------------
Trigger       | A Mini-game listens for events.
Preconditions | A Mini-game is running.
Procedure     | A Mini-game asks Unity for if any new events have been made. Most common event will be player input. 
Outcome       | A new event occurs.

PUC G-7       | Event: Create Entity
--------------|--------------------------------
Trigger       | A Mini-game calls create entity.
Preconditions | A Mini-game is running.
Procedure     | A Mini-game creates a new entity and adds it to the game world.
Outcome       | A new entity is created. Update game world.

PUC G-8       | Event: Destroy Entity
--------------|--------------------------------
Trigger       | A Mini-game calls destroy entity.
Preconditions | A Mini-game is running.
Procedure     | A Mini-game destroys an entity and removes it from the game world.
Outcome       | An entity is destroyed. Update game world.

PUC G-9       | Event: Set Physics
--------------|--------------------------------
Trigger       | A Mini-game calls set physics.
Preconditions | A Mini-game is running.
Procedure     | A Mini-game applies physics to a game entity. Most common will be Unity's Collider2D.
Outcome       | An entity now has physics.

PUC G-10      | Event: Set Level State
--------------|--------------------------------
Trigger       | A Mini-game updates game state variables.
Preconditions | A Mini-game is running.
Procedure     | As the player interacts with the game world the game state must be updated to make sure the rendering of the game world reflects that interaction.
Outcome       | A game state is changed. Update game world.

PUC G-11      | Event: Setup
--------------|--------------------------------
Trigger       | A Mini-game is started.
Preconditions | Battery is running.
Procedure     | Set all game variables from game configuration.
Outcome       | Game variables are set and game is now ready to run.

PUC G-12      | Event: Start Level
--------------|--------------------------------
Trigger       | A Mini-game is started.
Preconditions | Battery is running.
Procedure     | Player reads instructions and when ready starts the Mini-game.
Outcome       | Mini-game is started and measurements begin recording.

PUC G-13      | Event: End Level
--------------|--------------------------------
Trigger       | A Mini-game is finished.
Preconditions | Battery is running.
Procedure     | Player is told the Mini-game has ended. Tell Battery to go to next Mini-game.
Outcome       | Current Mini-game is stopped and measurements stop recording. Next Mini-game.

PUC G-14      | Event: Countdown
--------------|--------------------------------
Trigger       | A Mini-game started or finished.
Preconditions | Mini-game is running.
Procedure     | Countdown is played to prepare the player to start the game or go on to the next game.
Outcome       | Countdown timer is displayed on screen. When countdown finishes the game starts or the game exits.

PUC G-15      | Event: Show GUI
--------------|--------------------------------
Trigger       | A Mini-game started or finished.
Preconditions | Mini-game is running.
Procedure     | Show the start level and end level GUI.
Outcome       | Player interacts with GUI to start or end the game.

PUC G-16      | Event: Hide GUI
--------------|--------------------------------
Trigger       | A Mini-game has started.
Preconditions | Mini-game is running.
Procedure     | Hide the start level GUI.
Outcome       | Player has started the game, hide the start game GUI.

PUC G-17      | Event: On GUI
--------------|--------------------------------
Trigger       | A Mini-game has started.
Preconditions | Mini-game is running.
Procedure     | Handle the player events made on the GUI.
Outcome       | Start and end level GUI is responds to player events.

PUC G-18      | Event: Update
--------------|--------------------------------
Trigger       | A Mini-game has started.
Preconditions | Mini-game is running.
Procedure     | Update game state.
Outcome       | Each frame update is called to manage game state.

PUC B-1       | Event: Start Battery
--------------|--------------------------------
Trigger       | Start Unity Application
Preconditions | Battery can load configuration data.
Procedure     | Load configuration file. Apply configuration to Battery. Get player identification. Wait for player to start.
Outcome       | Battery Starts. Player is asked for identification and to start Battery.

PUC B-2       | Event: Output Path
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Engine is running.
Procedure     | Used to get the output path where metric data and configuration data will be stored.
Outcome       | Metric and configuration data files will be stored in this path.

PUC B-3       | Event: Current Config
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Engine is running.
Procedure     | Get the current configuration data for the current Mini-game.
Outcome       | When Mini-game is create it will request it's configuration data. Current config is called to get the data.

PUC B-4       | Event: Load Battery
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running. No Mini-games are running.
Procedure     | Load a Battery configuration from JSON file. 
Outcome       | A Battery configuration will be loaded which contains Battery configuration data (which Mini-games to run) and Mini-game configuration data.

PUC B-5       | Event: Load Scene
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running. No Mini-games are running.
Procedure     | A list of Mini-games is created from the configuration file. Given a game on the list, the Battery will load that game.  
Outcome       | A Mini-game is loaded and started.

PUC B-6       | Event: End Battery
--------------|--------------------------------
Trigger       | Battery runs out of Mini-games.
Preconditions | Battery is running.
Procedure     | Output JSON file with player name, configuration file and results of the Battery. Player is prompted to close application.
Outcome       | JSON file is created. Application is closed.

PUC B-7       | Event: Get Current Time
--------------|--------------------------------
Trigger       | Battery as started or ended.
Preconditions | Battery is running.
Procedure     | Called function to return current time.
Outcome       | Used to log the start and end time of the Battery.

PUC B-8       | Event: Load Next Scene
--------------|--------------------------------
Trigger       | Battery is start or Mini-game requests next scene.
Preconditions | Battery is running.
Procedure     | A Battery contains a sequence of Mini-games. A scene index is maintained to know which scene is current. Load next scene increments the index and loads the next scene.
Outcome       | Current scene is unloaded and next scene is loaded. 

PUC B-9       | Event: Get Current Scene
--------------|--------------------------------
Trigger       | Battery is started or Mini-game requests current scene.
Preconditions | Battery is running.
Procedure     | Get current scene based on the index. Scenes are named.
Outcome       | Return current scene.

PUC B-10      | Event: Game Name List
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running.
Procedure     | Get the list of games that will be run during the Battery.
Outcome       | Players can see what games they will have to play.

PUC B-11      | Event: Write Config
--------------|--------------------------------
Trigger       | Battery is ended.
Preconditions | Battery is running.
Procedure     | Write the current Battery's configuration and player data to file.
Outcome       | A JSON file will be created in Output Path which contains Battery configuration data and player data.

PUC B-12      | Event: Create Session Token
--------------|--------------------------------
Trigger       | Battery is starting.
Preconditions | Battery is running.
Procedure     | Create a unique session token to manage communication between Battery and Server.
Outcome       | Battery creates a UUID, stores it for future use and sends it to the serve.

PUC B-13      | Event: Send Config to Server
--------------|--------------------------------
Trigger       | Battery has ended.
Preconditions | Battery, Client-Server, and Server modules are running.
Procedure     | Battery sends HTTP Post request to Server Module with token to identify session, a state to update state, a filename for the configuration and configuration data as JSON in string format.
Outcome       | Server Module writes Battery configuration to file as filename and sets the state of the session identified by the token to FINISHED.

PUC S-1       | Event: Load Config
--------------|--------------------------------
Trigger       | Server is started. 
Preconditions | Configuration must exist as a JSON file.
Procedure     | When the Server is started is takes the configuration path as an input.
Outcome       | Load the configuration file into memory to send to Battery in future.

PUC S-2       | Event: Check Bad Config
--------------|--------------------------------
Trigger       | Server is started. 
Preconditions | Configuration is loaded.
Procedure     | Check that configuration is valid JSON.
Outcome       | Throw Exception is not valid and log error.

PUC S-3       | Event: Start
--------------|--------------------------------
Trigger       | Admin starts Server. 
Preconditions | Server Module is not running. Have a server to host the Server Module.
Procedure     | Run the Server Module with a path to a Battery configuration. 
Outcome       | Server is started and attempts to load the configuration. 

PUC S-4       | Event: Stop
--------------|--------------------------------
Trigger       | Admin stops Server. 
Preconditions | Server Module is running.
Procedure     | Halt the execution of the Server Module.
Outcome       | Server Module has stopped. Battery and Server can no longer communicate.

PUC S-5       | Event: Send Config to Battery
--------------|--------------------------------
Trigger       | Battery sends CREATED state and asks for configuration. 
Preconditions | Server Module, Battery and Client-Server Module are running.
Procedure     | Battery uses Client-Server Module to send a HTTP get request with Token and State to Server Module.
Outcome       | Battery receives a 404 or the configuration as text. 

PUC S-6       | Event: Create Session
--------------|--------------------------------
Trigger       | Battery requests a configuration. 
Preconditions | Server Module, Battery and Client-Server Module are running.
Procedure     | Create a unique session identified by the token sent by the Battery.
Outcome       | Create a new session identified by the token and set the state of the session to CREATED and expire time. 

PUC S-7       | Event: Delete Session 
--------------|--------------------------------
Trigger       | Session clean up.
Preconditions | Session identified by token exists.
Procedure     | Check if session has expired.
Outcome       | Remove session from list of sessions. 

PUC S-8       | Event: Update Session
--------------|--------------------------------
Trigger       | Battery requests session state change.
Preconditions | Session identified by token exists.
Procedure     | Set the session state to a new valid session state.
Outcome       | Session state has been updated.

PUC S-9       | Event: Receive Post File Request
--------------|--------------------------------
Trigger       | Battery or Mini-game sends bulk data as HTTP Post request.
Preconditions | Battery or Mini-game has ended.
Procedure     | Battery or Mini-game send a HTTP Post request with bulk data, a filename, a token and a state.
Outcome       | Server Module writes bulk data to file where the name of the file is filename. The session is updated using token and state.

PUC S-10      | Event: Receive Post File Request
--------------|--------------------------------
Trigger       | Battery requests configuration or Mini-game has started.
Preconditions | Battery has started or Mini-game has started.
Procedure     | Battery or Mini-game sends HTTP request a token and a state.
Outcome       | Update session using token and state. If state is CREATED send back the configuration the Server Module was started with. 

PUC C-1       | Event: Handle Get Request
--------------|--------------------------------
Trigger       | Client-Server Modules needs a GET response. 
Preconditions | Client-Server Module sends GET request.
Procedure     | Do whatever the GET request wants and send and a response.
Outcome       | Respond with 200 OK or an appropriate ERROR code.

PUC C-2       | Event: Handle Post Request
--------------|--------------------------------
Trigger       | Client-Server Modules needs a POST response. 
Preconditions | Client-Server Module sends POST request.
Procedure     | Do whatever the POST request wants and send and a response.
Outcome       | Respond with 200 OK or an appropriate ERROR code.

### 3.1 External Interfaces

#### 3.1.1 User interfaces

* Start screen with instructions on how to complete the Battery.
* A Console on the start screen to display errors that might occur to the player to they can send that information back to the Administrators or Researchers.
* A start button that is only enabled if the Battery successfully loaded a configuration.
* When the start button the scene switches to a Mini-game defined by the configuration.
* Each Mini-game will provide instructions to the player on how to play the game.
* Players should be able to go back and forth between pages if the instructions have multiple pages.
* After Player has understood the instructions they can start the Mini-game.
* A countdown will begin before the Mini-game is started to let to player prepare.
* Each Mini-game will have a unique gameplay user interface and experience. However, all Mini-games should share common pre and post game instuction interfaces in terms of design and style. These interfaces will have similar layouts for any buttons and text; fonts, text sizes, button and title positions, audio, etc., and should be consistent across Mini-games. 
* When the Mini-game games is completed a post game instruction menu will be shown telling the Player that they have completed the Mini-game and how to move on to the next game in the Battery.
* When all Mini-games are completed, the Battery will switch to the End screen where the Player will be thanked for participating in the Battery test.
* The WebGL version of Battery will have a maximize button so Players can play the Battery at full screen.

#### 3.1.2 Hardware interfaces

The Mini-games will will be controlled by the user's controller (keyboard, keyboard + mouse, game controller, etc.). The Mini-games will use the _Unity Engine Input System_<sup>[1]</sup> to respond to keyboard and controller key or button presses, or mouse movements and clicks. This system will abstract away the type of a controller the user is using to play the game so that the Mini-game code only needs to reference the _action_ the user performs. For example, the _W_ key on the user's keyboard might be used to move the player on screen forward. The _Unity Engine Input System_ will have the _W_ key mapped to the _forward_ action, so the code only needs to listen for the _forward_ action to occur to move the player forward. This allows for other controllers' buttons to be mapped to the same actions, which lets users use different controllers without having to write extra code to allow for those controllers.

Some Mini-games will require that certain keys are pressed _g_, _j_, etc that are outside of the normal _forward_, _back_ etc., scheme. These specific keys will be defined in that Mini-game's configuration is can be changed according to the wishes of the Adminstrators / Researchers.

#### 3.1.3 Software interfaces

The Mini-games will use Metric Modules to output player data collected during the Mini-game into a JSON file for that specific Mini-games. There will be one JSON file per Mini-game for a Battery. A single JSON file will also be created for the Battery which contains configuration data about Battery and the Mini-games of that Battery. The Metric Modules are responsible for converting lists, dictionaries and variables from C# into JSON objects. These object are then turned into a string which which contains all the data collected the Mini-games. This JSON data is sent to a server where the string of JSON data is written to files on the serer.

### 3.2 Functional

ID F-1         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-4,5,6   | Originator: Team
Description    | Record input from player.
Rationale      | In order to measure physical abilities of player, the Metric Modules need to be able to record input and game states
Constraints    | The project must support multiple controllers so a controller abstraction will be needed. Keyboard being the highest priority.
Priority       | Very High

ID F-2         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-1,2,3   | Originator: Team
Description    | Record start and end times for each game.
Rationale      | Timers are needed to measure the cognitive and physical abilities of the player. Measure how fast or slow the player is.
Constraints    | Metric Modules must coordinate timers with each individual Mini-game.
Priority       | Very High

ID F-3         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-4       | Originator: Team
Description    | Show game instructions
Rationale      | Display input instructions on which inputs are required to play the a game.
Constraints    | Instructions should be understandable. If the instructions require mulitple pages, players should be able to be got back for forth.
Priority       | Very High

ID F-4         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-6       | Originator: Team
Description    | Show post game instructions
Rationale      | Notify to the player that the game has ended and they can hit a key when ready to go to the next game.
Constraints    | Don't want the user to accidentally skip the introduction of the next scene.
Priority       | Very High

ID F-5         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-3,4,11 S-1,2,5  | Originator: Team
Description    | Use different configurations for each game.
Rationale      | Allows researcher to change the functionality of the game to improve tests.
Constraints    | Make sure configurations are valid.
Priority       | High

ID F-6         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-13 M-7 S-9 C-2 | Originator: Team
Description    | Output the measurements collected for each game session.
Rationale      | Required to tune measurement collecting techniques and for overall analyze of player's cognitive and physical abilities.
Constraints    | Output must be organized enough to be human readable.
Priority       | Very High

ID F-7         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-13 C-2  | Originator: Team
Description    | Save different configurations for each game after Battery.
Rationale      | Researchers need to know which configurations produced which results. 
Constraints    | Where to save configurations so they don't overwrite each other.
Priority       | Very High

ID F-8         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: S-6,7,8   | Originator: Team
Description    | Multiple users can run the Battery at once.
Rationale      | Players do not complete the Battery one at a time. Players may play the battery at the same time.  
Constraints    | How does the Battery and Server Module distinguish different players?
Priority       | Very High

ID F-9         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-10      | Originator: Team
Description    | A Mini-game needs to switch between states.
Rationale      | A Mini-game needs to run the game, show pre and post game instructions and a countdowns for the when the game begins.  
Constraints    | Making sure the Player doesn't accidentally switch states too quickly.
Priority       | Very High

ID F-10        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-1,G-12,13,14,15,16,17   | Originator: Team
Description    | Battery and Mini-game Instructions 
Rationale      | Players need instructions on how to complete the run the Battery, complete the Battery and to play the Mine-games. 
Constraints    | Make sure instructions are easy to understand.
Priority       | Very High

ID F-11        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-3,7,8,9 | Originator: Team
Description    | Use game entities to interact with the Player.
Rationale      | Almost all games require the Player to interact with non player entities in the game world.
Constraints    | Make sure entities are properly created and destroyed to not waste memory.
Priority       | Very High

ID F-12        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-5,8 G-10| Originator: Team
Description    | Battery can switch between Mini-games.
Rationale      | A Battery contains multiple Mini-games in order for the player to move on to the next Mini-game the Battery must switch to that Mini-game.
Constraints    | Keep Mini-games light-weight to improve switching performance.
Priority       | Very High

ID F-13        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-11      | Originator: Team
Description    | Provide a mechanism to generate a blank configurations.
Rationale      | Which Mini-game variables are available to change are stored using GameConfig objects. If these are changed, it will be helpful for developers to automatically generate a new complete configuration file with all the new and old variables which can be changed.
Constraints    | The JSON configuration files have to contain type data in order to convert from GameConfig objects to JSON and back.
Priority       | Low

ID F-14        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-11      | Originator: Team
Description    | Ability to measure Cognitive and Physical abilities during a Mini-game. 
Rationale      | The purpose of the project is, through a series of Mini-games, to measure the abilities of players. 
Constraints    | Requires a research in Psychology understand the abilities and how to measure them effectively. 
Priority       | Very High

ID F-15        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: C-1,2     | Originator: Team
Description    | Send GET and POST requests between Battery and Server. 
Rationale      | These requests allow communication between the Battery and the Server and allow for data transmission, state changes and session management.
Constraints    | Requires knowledge of HTTP requests and a running server to host the Battery and Server Module.
Priority       | Very High

ID F-16        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-4,G-5   | Originator: Team
Description    | Player and Entity Animations
Rationale      | Allow for animations to exist within the game.
Constraints    | Must work with in the constraints of Unity API. Animations requires many more assets to act as key frames.
Priority       | Very High

ID F-17        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-9       | Originator: Team
Description    | 2D Physics
Rationale      | In order to have gravity and collision detects there must exist a way to give game entities physics.
Constraints    | Must work with in the constraints of Unity API. 3D physics is complicated and should be avoided.
Priority       | Very High

### 3.3 Quality of Service

ID Q-1         | Type: Non-Functional (Quality)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Games are made with a playful and aesthetically pleasing interface.
Rationale      | Makes the player treat the game more as a game than a test which increases the accuracy of measurements.
Constraints    | Difficulty to find high quality free assets
Priority       | High

#### 3.3.1 Performance

ID P-1         | Type: Non-Functional (Performance)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Minimum FPS of 60 at 1080p
Rationale      | Unstable frame rates or frame rates below 60 negatively effect player performance. Measurements will be inaccurate.
Constraints    | Amount of graphical effects on screen.
Priority       | High

ID P-2         | Type: Non-Functional (Performance)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Player input is low latency.
Rationale      | Latency negatively effect player performance. Measurements will be inaccurate.
Constraints    | Cannot improve latency if player's hardware / software is at fault.
Priority       | Low

#### 3.3.2 Security

ID SEC-1         | Type: Non-Functional (Security)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Measurement data will tied to specific game or series of games and not to a specific person.
Rationale      | Lowers complexity project. An invigilator of the game testing can record personal information that would help measure player ability.
Constraints    | If games are played online through Unity, Unity will have it's own privacy policy. 
Priority       | Low

#### 3.3.3 Reliability

ID R-1         | Type: Non-Functional (Reliability)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Player measurements must be reliable
Rationale      | If measurements aren't reliable the player analyze will be inaccurate. 
Constraints    | Maintaining reliability over multiple games.
Priority       | High

#### 3.3.4 Availability

ID A-1         | Type: Non-Functional (Availability)
---------------|----------------------------------------------------------------
PUC: N\A       | Originator: Team
Description    | Make sure player has set aside enough time to complete the Battery.
Rationale      | A player must complete the Battery in one sitting without taking breaks. There is will be no pause functionality as it will interfere with the data measurements.
Constraints    | Mini-games must be of short enough length so a player can complete multiple Mini-games in one sitting of one Battery.
Priority       | High

ID A-2        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Run a server to host the Battery remotely. 
Rationale      | Players will play Battery remotely on their own time instead of in person due to COVID-19.
Constraints    | Player connection issues, browser support for WebGL, and server administration.  
Priority       | Very High

### 3.4 Compliance

ID: COMP-1        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will adhere to Unity terms and conditions.
Rationale      | Required for legal use of software.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

ID: COMP-2        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will adhere to McMaster's Academic Integrity policy.
Rationale      | Required to be a student of McMaster University.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

ID: COMP-3        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will licensed under Creative Commons Attribution 3.0 License.
Rationale      | Required for legal use of software and assets.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

### 3.5 Design and Implementation

#### 3.5.1 Installation

Administrators of the Battery will have to run a server on a platform of their choosing. The Server Module will have to be placed on the server. 

Players who are completing the Battery with a browser may need to update or install a modern browser which supports WebGL.

#### 3.5.2 Distribution
The source code and builds of the project will be distributed through GitHub.

WebGL builds can be played online or locally through a modern browser as long as a Server Module is running on a server or locally. 

This project does not provide any hardware or controller devices, they must be supplied by the user or administrators.

#### 3.5.3 Maintainability

Unity games are written in C# which is strongly typed to prevent common programming errors. C# is object oriented, which provides modularity.  The .NET standard library reduces complexity and increases maintainability. Debugging C# code can be done with Visual Studio. 

#### 3.5.4 Reusability

Game logic and player cognitive and physical analysis could be reusable in other projects. For example, functionality determining the success or failure rate of the player based on measurements of timers and objective states.

Unity interface code would only be reusable with other Unity projects.

#### 3.5.5 Portability

Unity provides multi-platform support for personal computers, phones, websites and consoles. Unity also supports multiple input devices such as, keyboard, mouse, touch, joystick and game pads. C# code is portable to Windows, Mac and Linux.

#### 3.5.6 Cost

Unity is free as long as “revenue or funding is less than $100K in the last 12 months”. Editors for coding with Unity such as Visual Studio and Visual Studio Code have free community versions. Github provides free public repos. Some assets are available in the Unity Store for free. Other free assets may be found online.

#### 3.5.7 Deadline

Week   | Deliverables
-------|----------------------
Oct 19 | Requirements Specification
Oct 28 | Prototype
Nov 13 | Design
Jan 11 | Prototype 2.0
Feb 4  | Internal and External Testing
Mar 11 | Final Version
Mar 18 | Internal Demo
Mar 30 | Code Complete
Apr 1  | Class Video
Apr 5  | Capstone Day Video
Apr 8  | Capstone Day Showcase

#### 3.5.8 Proof of Concept
The following is a design mockup of how the Mini-games will look like during development.
When the Mini-game is running there will be an overlay over the screen or off to the side of the game world, which will display all the measurement data. This will be helpful in tuning the measurement collection methods and will be used in refining the Mini-game to meet specific cognitive and physical measurement goals of that particular Mini-games.

![Image of Design MockUp](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/proof_of_concept_001.png)

Below is a proof of concept of the above mock up design. A simple game was created in which the user controls the green square and collects the pink squares. If the player hits a wall they are transported back to the middle of the game world. If the player collects a pink square (food), their food count is increased. The food count represents the player's score. The higher the better. On the left side of the game world an overlay exists displaying some of the measurement data collected during the game. For now, it keeps track of time and number of foods collected and number of times the player has hit the wall. Even from these rudimentary measurements, player analyze can begin. Future iterations of the project will be more feature rich in both game play, presentation and measurement fidelity.  

![Image of Proof of Concept](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/mike_game_poc_001.png)

## 4. Verification

ID: V-1        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Design Specialist
Rationale      | Team member commits to researching Physiology and Psychology works to learn which game designs can be employed improve a game's ability to measure abilities accurately.
Constraints    | May require more than 1 person.
Priority       | Very High

ID: V-2        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Game Tester
Rationale      | Make sure games are functional and without bugs that will interfere with ability measurements.
Constraints    | Make sure the developers of the each Mini-game don't just test their own game. Seek an outside perspectives.
Priority       | Medium

ID: V-3        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | UX Design and Aesthetics Critic 
Rationale      | Make sure the game feels like a game and not like a test. Should be aesthetically pleasing.
Constraints    | Will require testing from non developers to get higher quality feedback.
Priority       | Medium

ID: V-4        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Unit Testing
Rationale      | Facilitates more robust code. Metric Modules are best candidates for unit testing.
Constraints    | Games are difficult to break down into units.
Priority       | Low

ID: V-5        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Code Quality
Rationale      | Make sure each member of the team is adhering to good coding practices and conventions laid out in the specification.
Constraints    | Some members might have coding styles they are unwilling to change.
Priority       | Low

ID: V-6        | Type: Non-functional Requirements (Verification)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Implementation Inspector
Rationale      | To determine the viability of third party products team members may wish to integrate into the project.
Constraints    | May not be as knowledgeable as other members.
Priority       | Low

## 5. Appendixes

This section will contain useful information regarding the project as a whole, and is presented in a arbitrary order.

### 5.1 Appendix A: Figures and Excerpts

#### 5.1.1 Cognitive and Motor Abilities

__Figure A.1: List of Lower Order Cognitive Abilities for Player Model.__

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/cognitive_abilities.png)

__Figure A.2: Fine Motor Actions Divided by Body Parts.__

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/fine_motor_actions.png)

__Figure A.3: Gross Motor Actions by Body Part__

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/gross_motor_actions.png)

__Figure A.4: Motor Abilities for Video Game Controllers Categorized as Fine Motor, Gross Motor, or Both__

From [6]: __Soraine, S and Carette, J. (2020). Mechanical Experience, Competency Profiles, and Jutsu. Journal of Games, Self, and Society. Vol .2. p. 150 – 207.__

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/motor_abilities_for_video_game_controllers.png)

__Figure A.5: Fine and Gross Motor Abilities Used to Interact with Video Games__

From [6]: __Soraine, S and Carette, J. (2020). Mechanical Experience, Competency Profiles, and Jutsu. Journal of Games, Self, and Society. Vol .2. p. 150 – 207.__

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/fine_and_gross_motor_abilities.png)

#### 5.1.2 Project Background

__Excerpt A.6: Project Background__

This excerpt was taken from a document given to us (Mactivision) from the client (Sasha Soraine) regarding the basic description of the project.

![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/project_background.png)
