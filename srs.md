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

## 1. Introduction
This section will provide an overview of the entire document.

### 1.1 Document Purpose

The purpose of the SRS is to specify the functional and non-functional requirements of the project. It describes how the software will be developed and lays out the road map that will be followed by Mactivision. The SRS will be used internally to help facilitate development during the project's life cycle. It will also be used by Sasha Soraine and Dr. Jacques Carette as supervisors of the project. 

### 1.2 Product Scope

The product is a set of mini-games designed to measure player cognitive and motor abilities, an interface which administers a battery of mini-games, a set of modules to measure player performance and a data manager to collect metric data.

Mini-games will be used to build profiles of the cognitive and motor abilities of players. These profiles could be used by game designers to make informed decisions surrounding the design of challenges before having to implement them. 

The mini-game will focus on a small amount of the abilities that is determined by the Cognitive and Motor Abilities table (see Appendix 5.1.1). In order to satisfy its purposes, the game will be able to collect and analyze the player data to build profiles of the cognitive and motor abilities of players. These profiles could be used by game designers to make informed decisions surrounding the design of challenges before having to implement them.

A battery of mini-games will be administrated to the player. The application which administers the battery will have a start screen, the ability to switch between mini games and a end screen. The player will be administered these tests by a researcher and each mini games will be 30-60 seconds. The number of mini-games administered will be up to the researcher.

A set of metric modules will be created the measure the player data as they complete the mini-game battery. The mini-games will decide which modules it will use to measure player performance, as each game might measure different abilities. Once the mini-game is completed the modules will handle the collection and output of data for the entire battery.

A data manager will be used to take the data output from the battery and organize the data by mini-game and by ability measured. The manager should present the data in such a way that researchers are able to understand the data.  

Mactivision's objective is to develop the above applications to help Sasha’s project in G-ScalE and also provide an opportunity for the team to use and continue to software develop the skills they have obtained in throughout their studies.

### 1.3 Definitions, Acronyms and Abbreviations

__Cognitive and Motor Abilities__

There are 46 _Cognitive and Motor Abilities_ which are axiomatic abilities used by a human while playing through challenges in video games. We wish to create mini-games which will measure these abilities for a user playing them. Tables displaying a full list of these abilities can be found in [Section 5.1.1](#511-cognitive-and-motor-abilities)

__mini-game__

In this document, _mini-game(s)_ refer to the Unity games that will be designed for users to play, in order to test users and transmit player ability performance data.

__Battery__

A series of mini-games setup up by an researcher that a player is asked to complete. The player will be complete the series of mini-games. The battery will provide the researcher with player ability performance data for each mini-game.

__Metric Modules__

Metric modules will be created for this project, that the mini-games will use to record player ability data.

__Player__

These are the users which will be playing the mini-games and having their cognitive and motor abilities measured.

__Administrator__

These are the users which will be accessing the data from the metric modules, which will consist of the clients.

__Player: Profile__

A player profile is a collection of performance data for a given player created after the player's completion of a battery.

__PC (personal computer)__

A PC is a computer that is used by one person at a time in a business, a school, or at home. It is not usually a portable computer. PC is an abbreviation for 'personal computer'

__Steam__

Steam is a video game digital distribution service by Valve

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

[8]: __Cognifit__: https://www.cognifit.com/cognitive-assessment/battery-of-tests/wom-rest-test

[9]: __Eysenck, Michael W., and Mark T. Keane. Cognitive psychology__ : a student's handbook. Hove, Eng. New York: Psychology Press, 2010. 

### 1.5 Document Overview

This document contains the functional and non functional requirements for the Mactivision Mini-game Battery Project, specifically, the tools, methods, and systems required to get the project from the design stage to final version. 

### 1.6 Work Scope

This project needs the following steps to be done:

Completed | Work                          | Description
----------|-------------------------------|----------------------
✅ | Requirements Specification     | Complete SRS document.
✅ | Prototype                      | Half of the group works on the metric modules and the other half creates one mini game that showcases the interaction between the player and the game and the game and the data manager. 
✅ | Design Battery Module          | Battery administrates the battery of mini games.
✅ | Design Mini Game 1             | Digger game. Measure finger button key presses. Create a game objective that requires mashing. Choose which assets to use.
✅ | Design Mini Game 2             | Feeder game. Measure updating working memory. Create a game objective that requires an NPC to choose which food - they want to eat, player has to remember and feed them. Choose which assets to use
✅ | Design Mini Game 3             | Rockstar game. Measures divided attention. Create a game objective that requires the player put on a good show while interacting with technical problems that could ruin it. Choose which assets to use.
✅ | Design Metric Modules          | Create metric modules that games can use the measure the specific abilities they wish to record.
✅ | Design Data Presentation       | Take data created from battery and present it in such a way that it is easy for researchers to use. 
✅ | Design Document                | After reflecting on the good and bad decisions from the prototype, redesign prototype using data collected to more accurately measure targeted cognitive and physical abilities of that particular game. Formalize a list of design decisions for games 2, 3 the battery module, metric modules and data manager. 
✅ | Create Mini Game 1             | Mostly complete from prototype 1. Make any refinements needed.
✅ | Create Mini Game 2             | Make a prototype game 2 from design document.
⬜️ | Create Mini Game 3             | Make a prototype game 3 from design document.
✅ | Create Metric Modules          | Create sufficient metric modules in order to capture the basic requirements from game 1, 2, and 3.
✅ | Create Battery Module          | Battery module should be able to load a new battery and run game 1, 2, 3 in sequence, end battery and output recorded data.
⬜️ | Create Data Presentation       | Implement data presentation layer from design.
✅ | Prototype 2.0                  | Prototype 2.0 should be able to run a battery module, run the three games, have each game output meaningful player ability performance data, end the battery and then the data manager should be able to present that data in a meaningful way. 
⬜️ | Internal and External Testing  | UX testing will be done on the feature complete mini game and there will be a review of its code quality. Test whether players can complete a battery and understand how to play each mini game and can successfully do so. Attempt to see if the data collected matches with the tester's perceived performance of their abilities.
⬜️ | Polish Games Elements          | Add bells and whistles to the games to make them feel more like games and less like tests.
⬜️ |Extra Time                     | If time allows create more mini games and or create better data visualization.
⬜️ | Final Version                  | Battery, games, metric modules and data manager are complete and decoupled from mini games. A battery can be run and customized by researchers without the help for the developers.
⬜️ | Internal Demo                  | Externally test the games with friends and family to make sure there are no bugs or glaring omissions.
⬜️ | Capstone Day Showcase          | Showcase the mini games on Capstone day.

__Mini Game Inspirations:__

NdCube. Looking for Love. Super Mario Party. [Nintendo Switch]. (2018)

__Context of work:__

The work will be graded for the 4ZP6 capstone course.

The mini game will be used in G-ScalE project after the development.

## 2. Product Overview

This section will give an overview of the entire architecture of the system and its requirements, as well as the general factors that will affect the system. Further information regarding the requirements will be defined in detail in [Section 3](#3-requirements).

### 2.1 Product Perspective

This system will consist of four parts: a set of mini-games, a battery module, a set of metric modules and a data manager. The battery module will initialize a series of mini-games for the player to play. Each mini-game will communicate with the metric modules while an end user plays the mini-game, in order to record information from the game. Recorded data will be given to the data manager.

Each mini-game will be built using the Unity engine and each will be designed to measure a relatively small, specific subset of cognitive and motor abilities. The user input in these mini-games will mostly be restricted to keyboard input, as keyboards are widely available to most users, however input abstraction in the Unity engine will be made use of in order to switch between input types for most mini-games.

The metric modules is responsible for recording the data generated by end users playing the mini-games. Each mini-game will record a set of metrics from the player. This data will include input data (what controller is being used), the result of the mini-game, information about the user playing the game, the time spent playing the game, etc.

The data manager will organize data about specific mini-games, such as which cognitive and motor abilities that mini-game measured, the type of mini-game, and other mini-game specific data.

### 2.2 Product Functions

__Battery Module__
* During initialization, allow battery administrators to define the sequence of mini games to be used in the battery.
* During initialization, allow the administrators to pass variables to the mini games to customize functionality of those games.
* Start battery
* Load and unload mini games in the sequence defined by administrators.
* Stop battery.
* Takes the collected JSON data from a completed battery and organizes data by mini game and by metrics measured during that particular game.
* Each game is a sub set of a battery.
* Each metric is a sub set of a game.

__Metric Modules__
* Each module records one metric attempting to capture a cognitive or motor ability.
* A module is responsibly for outputting recorded into an JSON file the particular game it is being used in.
* Multiple modules may be used to capture one cognitive or motor ability depending on the abilities difficulty in being measured.
* Multiple modules can be used by a single game.

__Mini-Games__
* The user interfacing with the mini-game will be able to:
  * Start the mini-game
  * Quit/exit the mini-game
  * Perform inputs on their controller to play the mini-game
* The mini-game will display the game to the user, as well as play all sounds to the user
* The mini-game will record data about each play through of the game using metric modules
  * That data will be sent to the back-end to be processed
  * This data will NOT be presented to the user
* With some mini-games, the user will have more than one controller option; these mini-games will recognize and use alternative controllers

__Data Presentation__
* Metric data will be organized and presented in a readable form.

### 2.3 Product Constraints

This mini-game battery project has some constraints regarding the design of the mini-games. All mini-games must be build using the Unity engine, and must be designed for play on PC. The mini-games must be able to be run on lower end machines, as they will be given to various users to play on their personal machines, many of which may not be built to handle higher-end graphics or games. Furthermore, Some of these users may not have access a mouse, and only have the trackpad on their personal laptop to control their cursor, so most mini-games must be designed to only require a keyboard, as the subset of motor abilities to use a trackpad versus a mouse are different, and we can assume that every user playing these mini-games will have access to a keyboard.

### 2.4 User Characteristics

__There are two user classes which will use this product:__

* General users, which will play the mini-games. This group of users is very broad and can contain users in almost any demographic. These users should have basic computer literacy as they will mostly play mini-games on a PC.

* Admin users will be able to create JSON files which describes the number and order of the mini games that will be tested during a battery which they create. Furthermore they will be able to send define variables inside the JSON file that can alter the behavior of the games for each time it is run. Once data manager organizes and presents the data collected from the battery any admin user should be able to understand what is being presented. 

### 2.5 Assumptions and Dependencies

The mini-games portion of this project will rely on assets from the Unity asset store to develop the mini-games. It is assumed that developers will be able to find relevant assets in the Unity asset store that will help us build mini-games that fit the requirements described in this document in a more time-efficient manner, as opposed to spending more time creating the assets we require. These assets might include:
* 3D models or 2D sprites
* Audio clips for game sounds effects
* Various pre-built scripts to control various mechanics in the mini-games

We estimate that the time saved when using pre-made assets like these will be substantial, and will help us develop mini-games much more efficiently, so more time can be allocated to the design of the games rather than the programming of them.

[JsonDotNet](https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347) 2.0.1 Unity package will be used to convert C# object into JSON strings.

[Free Platform Game Assets](https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838) Unity package will be used as the primary source of game graphics for the mini games. The red character will be used as the avatar for the player across multiple mini games.

[Free Sound Effects](https://freesound.org/) Audio clips are used in the mini games to create sound effects.

### 2.6 Apportioning of Requirements

There are four software elements that will make up this project: the mini-games, battery module, the metric modules and data manager. The following table shows how functions are split up between the two software elements:

| Function                                            | Software Element      |
| --------------------------------------------------- | --------------------- |
| Create user profile                                 | Battery Module        |
| Create new battery                                  | Battery Module        |
| Start / end battery                                 | Battery Module        |
| Load / unload mini-games                            | Battery Module        |
| Present game instructions to the user               | Mini-game             |
| Initialize metric modules                           | Mini-game             |
| Record inputs from the controller                   | Metric Module         |
| Send gameplay data to the metric modules            | Mini-game             |
| Process the gameplay and rendering logic            | Mini-game             |
| Output data in JSON format                          | Metric Modules        |
| * Provide data in a graphical format                | Data Presentation     |

There are some requirements that will be designed and implemented only if there is time at the end of the project life cycle. These include
* __Output data in a graphical format__
  * This feature is nice to have, but is not fully necessary for this success of the project. It will be implemented at the end if there is time.
* __The number of mini-games designed/implemented__
  * The estimated number of mini-games to be designed is relatively unknown at this point in time. Depending on how long it takes to design/implement a single mini-game, and how much of the code can be reused to build future mini-games, this number may vary widely.

### 2.7 Stakeholders

The stakeholders for this project are:
* __Sasha Soraine__: Supervisor of this project
* __Dr. Jacques Carette__: Supervisor of this project
* __Researchers__: Academics investigating the correlations games and cognitive and motor abilities.
* __Games Designers__: Designers looking for player ability performance data to inform their design decisions when creating new player challenges.
* __Players__: The people who will play the mini-games, and will have their cognitive and motor abilities measured and recorded into player profiles.
* __McMaster University__: The project is created by the students of McMaster University and should uphold the policies and standards of that institution.
* __Mactivision__: Team of COMP SCI 4ZP6 students, consisting of __Kunyuan Cao__, __Bryan Chiu__, __David Hospital__, __Michael Tee__, and __Sijie Zhou__, who are responsible for designing, implementing, and testing all software outlined in this document.
* __Ethan Chan and Brendan Fallon__: COMP SCI 4ZP6 TAs who will be evaluating a portion of the deliverables. 

## 3. Requirements

__Product Use Case (PUC) Table (Measurement Module)__
PUC | PUC Name           | Actor(s)         | Input / Output
----|--------------------|------------------|---------------
M-1 | Start Recording    | System           | Success(OUT)
M-2 | Stop Recording     | System           | Success(OUT)
M-3 | Record Event       | System           | Event(IN) Success(OUT)
M-4 | Button Event       | Player, System   | Time DateTime(IN) KeyCode(IN) KeyState Bool(IN)
M-5 | Position Event     | Player, System   | Vector3(IN) 
M-6 | Memory Event       | Player, System   | Time DateTime(IN) Answers List (IN) Current String(IN) Choice Bool(IN)
M-7 | Log Metrics        | System           | JSON File(OUT)

__Product Use Case (PUC) Table (Game Scene)__
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
PUC  | PUC Name          | Actor(s)         | Input / Output
-----|-------------------|------------------|---------------
B-1  | Start Battery     | System           | Success Bool(OUT)
B-2  | Output Path       | System           | Path String(OUT)
B-3  | Current Config    | System           | Config GameConfig(OUT)
B-4  | Load Battery      | System           | Config String(IN) BatteryConfig(OUT)
B-5  | Load Scene        | System           | Scene String(IN) Success Bool(OUT)
B-6  | End Battery       | System           | Success Bool(OUT)
B-7  | Set Player Name   | System           | Name String(IN)
B-8  | Load Next Scene   | System           | Success Bool(OUT)
B-9  | Get Current Scene | System           | Scene String(OUT)
B-10 | Game Name List    | System           | Names List<String>(OUT)
B-11 | Write Config      | System           | JSON File(OUT) 

__Product Use Case (PUC) Table (Data Presentation)__
PUC | PUC Name           | Actor(s)         | Input / Output
----|--------------------|------------------|---------------
D-1 | Read Data          | System           | In String(IN), Success Bool(OUT)
D-2 | Present Data       | System           | Image(OUT) 

__Individual Product Use Cases__

PUC M-1       | Event: Start Recording
--------------|--------------------------------
Trigger       | Start of measurement recording.
Preconditions | A mini-game has started.
Procedure     | Records date and time for the current mini-game.
Outcome       | Store start date and time. 

PUC M-2       | Event: Stop Recording
--------------|--------------------------------
Trigger       | Stop of measurement recording.
Preconditions | A mini-game has started.
Procedure     | Records date and time for the current mini-game.
Outcome       | Store end date and time. 

PUC M-3       | Event: Record Event
--------------|--------------------------------
Trigger       | Mini games asks to record event.
Preconditions | A mini-game has started.
Procedure     | Record event by event type.
Outcome       | Stores events in a list.

PUC M-4       | Event: Button Event
--------------|--------------------------------
Trigger       | Mini-game calls button event.
Preconditions | A mini-game is running and in start state.
Procedure     | Records the state of a button. Up or down.
Outcome       | Button state is added to a list of buttons states.

PUC M-5       | Event: Position Event
--------------|--------------------------------
Trigger       | Mini-game calls position event.
Preconditions | A mini-game is running.
Procedure     | Records the current position of entity.
Outcome       | Position added to a list of positions for that entity.

PUC M-6       | Event: Memory Event
--------------|--------------------------------
Trigger       | Mini-game calls memory event.
Preconditions | A mini-game is running.
Procedure     | Records expected outcome and actual outcome from a mini-game objective.
Outcome       | Expected outcome and actual outcomes is added to a list of outcomes.

PUC M-7       | Event: Log Metrics
--------------|--------------------------------
Trigger       | Mini-game calls memory event.
Preconditions | A mini-game is running but is in end state.
Procedure     | Output recorded events into a JSON file.
Outcome       | New JSON file containing recorded events is created in current Battery output directory for specific game/configuration being run.

PUC G-1       | Event: Move Player
--------------|--------------------------------
Trigger       | Player input or mini-game call.
Preconditions | A mini-game is running.
Procedure     | Update player position in game world.
Outcome       | Player moves to new coordinates.

PUC G-2       | Event: Move Camera
--------------|--------------------------------
Trigger       | Player moves camera or camera follows player.
Preconditions | A mini-game is running.
Procedure     | Get player input. Set new camera coordinates.
Outcome       | Camera is moved.

PUC G-3       | Event: Move Entity
--------------|--------------------------------
Trigger       | A mini-game requires entity to move or player interacts with entity.
Preconditions | A mini-game is running.
Procedure     | Set new entity coordinates.
Outcome       | Entity is moved.

PUC G-4       | Event: Animate Sprite
--------------|--------------------------------
Trigger       | A mini-game calls animate sprite.
Preconditions | A mini-game is running.
Procedure     | Display sprites in a sequence in order to evoke the appearance that the entity is animated.
Outcome       | Sprite animation appears on screen.

PUC G-5       | Event: Switch Animation
--------------|--------------------------------
Trigger       | A mini-game calls switch animation.
Preconditions | A mini-game is running.
Procedure     | A sequence of sprites is switched to another sequence of sprites.
Outcome       | Sprite animation changes from one to another.

PUC G-6       | Event: Get Event
--------------|--------------------------------
Trigger       | A mini-game listens for events.
Preconditions | A mini-game is running.
Procedure     | A mini-game asks unity for if any new events have been made. Most common event will be player input. 
Outcome       | A new event occurs.

PUC G-7       | Event: Create Entity
--------------|--------------------------------
Trigger       | A mini-game calls create entity.
Preconditions | A mini-game is running.
Procedure     | A mini-game creates a new entity and adds it to the game world.
Outcome       | A new entity is created. Update game world.

PUC G-8       | Event: Destroy Entity
--------------|--------------------------------
Trigger       | A mini-game calls destroy entity.
Preconditions | A mini-game is running.
Procedure     | A mini-game destroys an entity and removes it from the game world.
Outcome       | An entity is destroyed. Update game world.

PUC G-9       | Event: Set Physics
--------------|--------------------------------
Trigger       | A mini-game calls set physics.
Preconditions | A mini-game is running.
Procedure     | A mini-game applies physics to a game entity. Most common will be Unity's Collider2D.
Outcome       | An entity now has physics.

PUC G-10      | Event: Set Level State
--------------|--------------------------------
Trigger       | A mini-game updates game state variables.
Preconditions | A mini-game is running.
Procedure     | As the player interacts with the game world the game state must be updated to make sure the rendering of the game world reflects that interaction.
Outcome       | A game state is changed. Update game world.

PUC G-11      | Event: Setup
--------------|--------------------------------
Trigger       | A mini-game is started.
Preconditions | Battery is running.
Procedure     | Set all game variables from game configuration.
Outcome       | Game variables are set and game is now ready to run.

PUC G-12      | Event: Start Level
--------------|--------------------------------
Trigger       | A mini-game is started.
Preconditions | Battery is running.
Procedure     | Player reads instructions and when ready starts the mini-game.
Outcome       | Mini-game is started and measurements begin recording.

PUC G-13      | Event: End Level
--------------|--------------------------------
Trigger       | A mini-game is finished.
Preconditions | Battery is running.
Procedure     | Player is told the mini game has ended. Tell battery to go to next mini-game.
Outcome       | Current mini-game is stopped and measurements stop recording. Next mini-game.

PUC G-14      | Event: Countdown
--------------|--------------------------------
Trigger       | A mini-game started or finished.
Preconditions | Mini game is running.
Procedure     | Countdown is played to prepare the player to start the game or go on to the next game.
Outcome       | Countdown timer is displayed on screen. When countdown finishes the game starts or the game exits.

PUC G-15      | Event: Show GUI
--------------|--------------------------------
Trigger       | A mini-game started or finished.
Preconditions | Mini game is running.
Procedure     | Show the start level and end level GUI.
Outcome       | Player interacts with GUI to start or end the game.

PUC G-16      | Event: Hide GUI
--------------|--------------------------------
Trigger       | A mini-game has started.
Preconditions | Mini game is running.
Procedure     | Hide the start level GUI.
Outcome       | Player has started the game, hide the start game GUI.

PUC G-17      | Event: On GUI
--------------|--------------------------------
Trigger       | A mini-game has started.
Preconditions | Mini game is running.
Procedure     | Handle the player events made on the GUI.
Outcome       | Start and end level GUI is responds to player events.

PUC G-18      | Event: Update
--------------|--------------------------------
Trigger       | A mini-game has started.
Preconditions | Mini game is running.
Procedure     | Update game state.
Outcome       | Each frame update is called to manage game state.

PUC B-1       | Event: Start Battery
--------------|--------------------------------
Trigger       | Start Unity Application
Preconditions | Battery can load configuration data.
Procedure     | Load configuration file. Apply configuration to battery. Get player identification. Wait for player to start.
Outcome       | Battery Starts. Player is asked for identification and to start battery.

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
Procedure     | Get the current configuration data for the current mini game.
Outcome       | When mini game is create it will request it's configuration data. Current config is called to get the data.

PUC B-4       | Event: Load Battery
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running. No mini-games are running.
Procedure     | Load a battery configuration from JSON file. 
Outcome       | A battery configuration will be loaded which contains battery configuration data (which mini games to run) and mini game configuration data.

PUC B-5       | Event: Load Scene
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running. No mini-games are running.
Procedure     | A list of mini-games is created from the configuration file. Given a game on the list, the battery will load that game.  
Outcome       | A mini-game is loaded and started.

PUC B-6       | Event: End Battery
--------------|--------------------------------
Trigger       | Battery runs out of mini-games.
Preconditions | Battery is running.
Procedure     | Output JSON file with player name, configuration file and results of the battery. Player is prompted to close application.
Outcome       | JSON file is created. Application is closed.

PUC B-7       | Event: Set Player Name
--------------|--------------------------------
Trigger       | Battery runs out of mini-games.
Preconditions | Battery is running.
Procedure     | Request players name. Battery can't start mini games until valid player name is input.
Outcome       | Battery starts and asks player for their name. Once the player has input their name the first game in the sequence of mini games will start, when the player is ready.

PUC B-8       | Event: Load Next Scene
--------------|--------------------------------
Trigger       | Battery is start or mini-game requests next scene.
Preconditions | Battery is running.
Procedure     | A battery contains a sequence of mini games. A scene index is maintained to know which scene is current. Load next scene increments the index and loads the next scene.
Outcome       | Current scene is unloaded and next scene is loaded. 

PUC B-9       | Event: Get Current Scene
--------------|--------------------------------
Trigger       | Battery is started or mini-game requests current scene.
Preconditions | Battery is running.
Procedure     | Get current scene based on the index. Scenes are named.
Outcome       | Return current scene.

PUC B-10      | Event: Game Name List
--------------|--------------------------------
Trigger       | Battery is started.
Preconditions | Battery is running.
Procedure     | Get the list of games that will be run during the battery.
Outcome       | Players can see what games they will have to play.

PUC B-11      | Event: Write Config
--------------|--------------------------------
Trigger       | Battery is ended.
Preconditions | Battery is running.
Procedure     | Write the current Battery's configuration and player data to file.
Outcome       | A JSON file will be created in Output Path which contains Battery configuration data and player data.

PUC D-1       | Event: Read Data
--------------|--------------------------------
Trigger       | Select JSON file to read.
Preconditions | Data Manager is running
Procedure     | Launch Data Manager, select JSON file to read.
Outcome       | Parses JSON file and either succeeds or fails with a message.

PUC D-2       | Event: Present Data
--------------|--------------------------------
Trigger       | Select present data
Preconditions | JSON is file is read.
Procedure     | read JSON file data, present data.
Outcome       | Present the data from the parsed JSON file in an organized way.


### 3.1 External Interfaces

#### 3.1.1 User interfaces

A user launching any of the mini-games in this project should be greeted with a _user ID menu_, followed by a _main menu_ screen. The _user ID menu_ screen will simply contain a text field below a phrase asking the user to enter their _user ID_, with a _Continue_ button below that. This ID is unique to the user and should be given to them prior to them playing the game. An example of this screen is displayed below:
![](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/example_user_id_001.png)
When a user presses the _Continue_ button, the battery module will record
the user id, and provide the user with the first mini game.

Each mini game will provide instructions on how to play the games. Users should be able to start the game, and exit the game. The design of this screen should be very simple, with little to no game options, as the mini-games are specifically designed to test specific cognitive and motor abilities, so the user should not a complex menu. If there are options for the user, they should be minimal, as to not affect the cognitive and motor abilities being measured. The following on-screen buttons should be included with each mini-game, which the user can press using their cursor to navigate through the mini-game interface:

Menu Button | Function
---|---
Start Game | Starts the mini-game for the user. The user should now be able to play the mini-game using their input device. Instructions on how to play the game are shown during this period.
Exit Game | mini-game should exit and the battery module will start the next mini game if one exists.

Each mini-game will have a unique gameplay user interface and experience, which will be specified in the future when individual mini-games are being designed. However, all mini-games should share a common start and exit interface in terms of design and style. These interfaces will have similar layouts for any buttons and text; fonts, text sizes, button and title positions, audio, etc., should be consistent across mini-games. 

![Start Screen](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/interface_example_of_start_screen.png)

![End Screen](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/interface_example_of_end_screen.png)

Mini games should not show error messages to the user if the game encounters an error during play. If a mini games fails it should fail silently, record the failure in the JSON file for that particular battery and the battery should attempt to start the next mini game in the sequence. If the battery module fails let the let the user know so they can relay that information to the administrator. The administrator can make the decision as to whether the battery must be restarted or the data collected so far is sufficient.

![Example Error Message](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/example_error_001.png)

The data manager will be used to organize and present recorded player ability data from a completed battery. This data should be presented in such a way that it is readable and understandable. An example below:

![Data Presentation](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/interface_example_data_presentation.png)

#### 3.1.2 Hardware interfaces

The mini-games will will be controlled by the user's controller (keyboard, keyboard + mouse, game controller, etc.). The mini-games will use the _Unity Engine Input System_<sup>[1]</sup> respond to keyboard and controller key or button presses, or mouse movements and clicks. This system will abstract away the type of a controller the user is using to play the game so that the mini-game code only needs to reference the _action_ the user performs. For example, the _W_ key on the user's keyboard might be used to move the player on screen forward. The _Unity Engine Input System_ will have the _W_ key mapped to the _forward_ action, so the code only needs to listen for the _forward_ action to occur to move the player forward. This allows for other controllers' buttons to be mapped to the same actions, which lets users use different controllers without having to write extra code to allow for those controllers.

#### 3.1.3 Software interfaces

The mini-games will use metric modules to output player data collected during the mini-game into a JSON file for that specific mini games. There will be one JSON file per mini game for a battery. A single JSON file will also be created for the battery which contains meta data about battery which will be ran include which mini-games. The metric modules are responsible for converting lists, dictionaries and variables from C# into JSON objects. These object are then turned into a string which which contains all the data collected the mini games. A new file is created on the operating system and the string of JSON data is written to that file.

Administrators will interface with the Data Manager which will be used to parse the JSON files created during the battery and to organize and convert the mini game data into presentable metrics which can be viewed by the administrators of the battery. 

### 3.2 Functional

ID F-1         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-4,5,6   | Originator: Team
Description    | Record input from player.
Rationale      | In order to measure physical abilities of player, the metric modules need to be able to record input and game states
Constraints    | The project must support multiple controllers so a controller abstraction will be needed.
Priority       | Very High

ID F-2         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-1,2,3   | Originator: Team
Description    | Record start and end times for each game.
Rationale      | Timers are needed to measure the cognitive and physical abilities of the player. Measure how fast or slow the player is.
Constraints    | Metric modules must coordinate timers with each individual game.
Priority       | Very High

ID F-3         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-4       | Originator: Team
Description    | Show instruction screen
Rationale      | Display input instructions on which inputs are required to play the a game.
Constraints    | Instructions should be understandable.
Priority       | Very High

ID F-4         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-6       | Originator: Team
Description    | Show end screen that game has ended.
Rationale      | Notify to the player that the game has ended and they can hit a key when ready to go to the next game.
Constraints    | Don't want the user to accidentally skip the introduction of the next scene.
Priority       | Very High

ID F-5         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-3,4,11  | Originator: Team
Description    | Use different configurations for each game.
Rationale      | Allows researcher to change the functionality of the game to improve tests.
Constraints    | Make sure configurations are valid.
Priority       | Medium

ID F-6         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-7, B-11 | Originator: Team
Description    | Output the measurements collected for each game session.
Rationale      | Required to tune measurement collecting techniques and for overall analyze of player's cognitive and physical abilities.
Constraints    | Output must be organized enough to be human readable.
Priority       | Very High

ID F-7         | Type: Functional 
---------------|----------------------------------------------------------------
PUC: M-1,2     | Originator: Team
Description    | Save different configurations for each game after battery.
Rationale      | Since objectives will vary in type and difficulty this is need to contextualize the measurements collected from the player. 
Constraints    | Where to save configurations so they don't overwrite each other.
Priority       | Very High

ID F-11        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-3,7,8,9 | Originator: Team
Description    | Player input changes game state.
Rationale      | Most game types require non player objects to move around the game world.
Constraints    | Make sure objects doesn't go out of bounds and are properly created and destroyed to not waste memory.
Priority       | Very High

ID F-12        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: B-5,8 G-10| Originator: Team
Description    | Battery can switch between games.
Rationale      | A battery contains multiple games, each game is a scene. Use battery to switch between games.
Constraints    | Be able to load games.
Priority       | Very High

ID F-15        | Type: Functional 
---------------|----------------------------------------------------------------
PUC: G-7,8,9   | Originator: Team
Description    | Entity Manager
Rationale      | Game entities must be created, destroyed, and updated.   
Constraints    | Must work with in the constraints of Unity API.
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

ID S-1         | Type: Non-Functional (Security)
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
Description    | Make sure player has set aside enough time to complete the battery.
Rationale      | A player must complete the battery in one sitting without taking breaks. There is will be no pause functionality as it will interfere with the data measurements.
Constraints    | Mini games must be of short enough length so a player can complete multiple mini games in one sitting of one battery.
Priority       | High

### 3.4 Compliance

ID: C-1        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will adhere to Unity terms and conditions.
Rationale      | Required for legal use of software.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

ID: C-2        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will adhere to McMaster's Academic Integrity policy.
Rationale      | Required to be a student of McMaster University.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

ID: C-3        | Type: Non-functional Requirements (Compliance)
---------------|----------------------------------------------------------------
PUC: N/A       | Originator: Team
Description    | Project will licensed under Creative Commons Attribution 3.0 License.
Rationale      | Required for legal use of software and assets.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

### 3.5 Design and Implementation

#### 3.5.1 Installation

Administrators of the battery will have to install the project on a platform of their choosing. An package manager can be used to create an installer for the project and manage updates and removal. 

Players who are completing the battery with a browser may need to update or install a modern browser which supports WebGL.

#### 3.5.2 Distribution

Unity Distribution Portal allows game developers to distribute games to multiple app stores through a single hub. Projects can also be distributed manually by supplying users with copies of production builds.  WebGL builds can be played online through a modern browser. Other builds require a download onto the user’s hardware. The size of download will depend on the size of the code base and assets. Distribution to those without internet connections would be more difficult. However, physical media, such as spinning discs, could be used to reach those without connections.
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
Mar 25 | Extra Time
Apr 5  | Capstone Day Materials
Apr 8  | Capstone Day Showcase

#### 3.5.8 Proof of Concept
The following is a design mockup of how the mini games will look like during development.
When the mini game is running there will be an overlay over the screen or off to the side of the game world, which will display all the measurement data. This will be helpful in tuning the measurement collection methods and will be used in refining the mini game to meet specific cognitive and physical measurement goals of that particular mini games.

![Image of Design MockUp](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/proof_of_concept_001.png)

Below is a proof of concept of the above mock up design. A simple game was created in which the user controls the green square and collects the pink squares. If the player hits a wall they are transported back to the middle of the game world. If the player collects a pink square (food), their food count is increased. The food count represents the player's score. The higher the better. On the left side of the game world an overlay exists displaying some of the measurement data collected during the game. For now, it keeps track of time and number of foods collected and number of times the player has hit the wall. Even from these rudimentary measurements, player analyze can begin. Future iterations of the project will be more feature rich in both game play, presentation and measurement fidelity.  

![Image of Proof of Concept](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/mike_game_poc_001.png)

The proof of concept is also "playable" online by visiting the link below:

[https://connect.unity.com/mg/other/untitled-34576](https://connect.unity.com/mg/other/untitled-34576)

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
Constraints    | Make sure the developers of the each mini game don't just test their own game. Seek an outside perspectives.
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
Rationale      | Facilitates more robust code. Metric modules are best candidates for unit testing.
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
