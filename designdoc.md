Table of Contents
=================
* [Revision History](#revision-history)
* 1 [Anticipated Changes](#1-anticipated-changes)
* 2 [Unlikely Changes](#2-unlikely-changes)
* 3 [List of Concepts](#3-list-of-concepts)
  * 3.1 [Design-time Entities](#3.1-design-time-entities)
  * 3.2 [Run-time Entities](#3.2-run-time-entities)
* 4 [List of Modules](#4-list-of-modules)
  * 4.1 [Measurement Modules](#41-measurement-modules)
    * 4.1.1 [Abstract Metric Event Module](#411-abstract-metric-event-module)
    * 4.1.2 [Abstract Metric Module](#412-abstract-metric-module)
  * 4.2 [Minigame Modules](#42-minigame-modules)
    * 4.2.0 [Abstract Level Manager Module](#420-abstract-level-manager-module)
    * 4.2.1 [Digger Modules](#421-digger-modules)
      * 4.2.1.1 [Digger Level Manager Module](#4211-digger-level-manager-module)
      * 4.2.1.2 [Player Controller Module](#4212-player-controller-module)
      * 4.2.1.3 [Ground Breaker Module](#4213-ground-breaker-module)
      * 4.2.1.4 [Chest Animator Module](#4214-chest-animator-module)
    * 4.2.2 [Conveyor Modules](#422-conveyor-modules)
    * 4.2.3 [ThirdGame Modules](#423-thirdgame-modules)
    * 4.3 [Battery Module](#43-battery-module)
* 5 [List of Changes to SRS](#5-list-of-changes-to-srs)
* 6 [Module Relationship Diagram](#6-module-relationship-diagram)
* 7 [Significant Algorithms/Non-Trivial Invariants](#7-significant-algorithms/non-trivial-invariants)

# Revision History

# 1. Anticipated Changes

* Mini-games will contain variables which will allow battery test administers/researchers to make small changes to the game's objectives and functionality. The number of variables from which the researchers can alter for each mini game will change over time as researchers think of new alterations for the games. In order to accommodate these changes each mini game will be passed a variable length dictionary of variables. This allows quicker additions or subtractions of variables and reduces confusion between researchers and mini game developer.

* The SRS document will continue to evolve throughout the life span of this project. It will be updated with changes discovered during the creation of a design document and for each subsequent major deliverable.

* There are 46 cognitive and motor abilities available to measure as defined in our [SRS](https://github.com/BryanChiu/Mactivision/wiki/Software-Requirements-Specification). In the design of the three games listed in this document we have specifically targeted Finger Pressing, Divided Attention, and Updating Working Memory. Which abilities each game measures is subject to change as we create the games and test them with players. The measurement modules will help determine whether a game is accurately and meaningfully capturing the targeted abilities. If the game is not capturing the targeted ability in a significant way, change must be made to the game or to the measurement module. Additionally, it may be discovered that some abilities are irrevocably linked and must be included in the targeted abilities list. Similarly, some games or measurement modules may be ineffective at measuring a specific ability within the scope of the project, in such as case, those abilities may be removed from the targeted list. These changes are expected to occur during the development of the mini games. In anticipation of this, the design the games are kept simple such that they have a minimal number of game elements. Unity allows for these game elements to be modular allowing for easy addition or subtraction from a particular game. Even eliminating an entire game design is a possibility. However, the module nature of the game elements will allow for reuse in any new design created as a replacement. This ensures that development of mini games is agile and can easily adapt to new discoveries in relationships between games and player abilities.

* Number of Unity packages will increase as development continues during the project. Thankfully, it easy to add and remove packages using the Unity package manager. Graphical and audio assets can also be managed in this way. This makes distributing the assets easier when sharing the unity project between team members and the final deliverable. It also makes it much easier to track which packages we have used over time, as each package and collection of assets have their on page on the unity store. This is particularly important for keeping track of the licenses of these packages to make sure they can be used in the project now and in the future.

* JSON is currently used to map data to C# objects and these objects are used to send and store data to and from the battery, mini games and measurement modules. The design and structure of these objects will change as more or less data of differing kinds are need to be shared and transported between modules. These objects are trivial to modify in C# which is why it so valuable that a third party Unity package can convert C# objects to JSON and back.

* During the internal and external phase of development the project will go under rigorous testing from the programmers and play testers. Results from testing will likely change the design of the modules to improve code quality, user interface design and game design. 

# 2. Unlikely Changes

* JSON data structure is used as input to initialize the battery and as output for measurement data. This may change to another format, for example YAML or XML in future. This change depends on the needs of the researchers administrating the battery. As the data manager manages convert JSON files into a presentable manner it unlikely for research will want a change in data formats.  

* A database may be required to store the data instead of using JSON or other data structures in the long term. However, that is currently outside the scope of this project.

* There is currently no plan to include functionality to handle the scenario where the player stops playing the games during the middle of the battery. The administrator of the battery will have to decide if the data collected so far is sufficient or if the battery should be re administered. Similarly, any software crashes or bugs which halt the execution of the battery or any of the mini games will be unrecoverable. The battery module contains no functionality to recover from these errors only to report them to the user and administrator. All attempts will be made to make the software robust enough for this to be unlikely and for error messages to be helpful to players and administrators so they can make actionable decisions. Thankfully, the duration of the battery should be short enough that a battery restart is possible.

* An unlikely change is the player input devices. Because of Covid 19 most of the players will be completing a battery using a browser and a keyboard. The supervisors of the project hope to be able to add more input types in future, however that will mostly be attempted after Mactivision has submitted their final work. Therefore, it is unlikely that keyboard will change before work is completed. However, since the future of the project will likely add new inputs all efforts have been made, now, to accommodate different input devices. Using Unity to abstract player inputs into actions like "left", "right" and "fire 1" instead of "pad-up", "right arrow", "trigger 2" make accommodating different yet similar input devices trivial. Different kinds of input devices like motion controls and mice can't be mapped as nicely and will require new modules.

# 3. List of Concepts
<!-- A list of concepts pertaining to your problem domain, and whether these concepts are design-time entities or run-time entities.
For example, if 'sorting' was an important item in your problem domain, then 'permutations' would be an important concept too; however, it is extremely unusual for permutations to exist in code, they are a design-time concept in most sorting algorithms. In the same domain, comparison functions are another concept, but needs to exist at run-time (when sorting proper names or addresses, the comparison functions involved are highly non-trivial if one is to respect usual conventions). There will be overlap between these and points 1-2 above.

Design time: a concept that appears in your design but not in your code. 
Run time: a concept that is clearly present in your code.

For example, most things to do with usability will have concepts that only appear at design time, as "the human" tends not to be reified in code (unless you're doing some funky actor-oriented architecture). Efficiency also tends to have artifacts that only appears outside the code itself.
-->

## 3.1 Design-time Entities

* The player. The user who is told to complete a battery. The player will be using a device to run the battery and a device to send inputs to the battery. For the purpose of this design document we will assume that the player is using a Web Browser and Keyboard respectively. The player is given the battery by the battery administrator. The player runs the battery and is subjected to a series of mini games. The player completes the mini games by using their Keyboard. After the battery is completed the player can stop. 
* The administrator. The user who creates the JSON configuration file which contains a sequence of mini-games to test the player with and variables which alter various parameters of the said mini-games. The player and administrator communicate throughout the battery to makes sure the player starts and completes it correctly. The administrator is also responsible for collected data output from the battery after it has concluded. Gathering completed completed, they will run the data through the data manager to organize and present the data to the researchers.

* The researcher. For simplicity, they are separate from administrators, however they will likely assume the same role in practice. Researchers will make decisions based on the information provided by the administrators. These decisions may require altering the JSON configuration file. 

* The browser. Used by players to interact with battery and mini-games. It will be recommended to users to run the battery on the newest version of Chrome. To provide a smooth experience to all browser players a target of 60fps should be maintained at all times. Furthermore, the FPS should be capped. Frame rates should remain constant to prevent hardware performance from affecting player performance measurements. 

* The keyboard. Players of the web based battery will be required to provide their own keyboard. 

* Unity Play. Allows developers to host their web based Unity game online for users to access. Using this provides quick turn around between making modifications to the project and letting to players and testers to try it out. Additionally, administrators can provide a link to the players and those players can complete the battery online with their web browser and keyboard.

## 3.2 Run-time Entities

* The player avatar. A collection sprites and animations that make up the player avatar. The player's movements and actions in game will be done through this avatar. The avatar will remain constant between mini games to provide familiarly to the player allowing the player to more easily adjust to the new games. Consistent game elements between games reduces the noise in player performance measurements. 

* Start and end screens. Each battery and each mini game will contain a start and end screen. The start screen will provide instructions to the player on who to proceed and the end screen will provide notification to the player that the game or battery has ended. 

* Player performance data presentation. Provide graphs and other visual aids to help researcher understand of the data collected after a battery completed. 

# 4. List of Modules

## 4.1 Measurement Modules

This section of the document contains all modules relating to the _Measurement Modules_. These modules are to be implemented alongside the _Minigame_ modules in the Unity environment, and will be accessed by modules in the _Minigame_ set of modules.

## 4.1.1 Abstract Metric Event Module
  `abstract AbstractMetricEvent` Module 
  ### Uses
  * `System.DateTime`
  ### Syntax
  #### **Exported Constants**
  None
  #### **Exported Types**
  None
  #### **Exported Access Programs**
  |Routine Name|In |Out |Exceptions |
  |---|---|---|---|
  |`AbstractMetricEvent`|`DateTime`|`AbstractMetricEvent`||
  |`getEventTime`||`DateTime`||

  ### Semantics
  #### **State Variables**
  * `eventTime: DateTime`
  #### **Assumptions**
  * The constructor `AbstractMetricEvent(DateTime)` is called before any other access routines are called for that object.
  #### **Design Decision**
  This module is to represent an abstract class for all _Metric Events_. Every _Metric Event_ at least has a time stamp (`eventTime`) in which the event occurred.
  #### **Access Routine Semantics**
  `AbstractMetricEvent(et)`
  * transition: eventTime = et
  * output: _out_ := _self_
  * exception: none

  `getEventTime()`
  * transition: none
  * output: _out_ := eventTime
  * exception: none
## Button Pressing Event Module
  `ButtonPressingEvent` Module inherits [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  ### Uses
  * [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  * `System.DateTime`
  * `UnityEngine.KeyCode`
  ### Syntax
  #### **Exported Constants**
  None
  #### **Exported Types**
  None
  #### **Exported Access Programs**
  |Routine Name|In |Out |Exceptions |
  |---|---|---|---|
  |`ButtonPressingEvent`|`DateTime`, `KeyCode`, `𝔹`|`ButtonPressingEvent`||
  |`getEventTime`||`DateTime`||
  |`getKeyCode`||`KeyCode`||
  |`isKeyDown`||`𝔹`||

  ### Semantics
  #### **State Variables**
  * `eventTime: DateTime` (inherited from `AbstractMetricEvent`)
  * `keyCode: KeyCode`
  * `keyDown: 𝔹` 
  #### **Assumptions**
  * The constructor `ButtonPressingEvent(DateTime,KeyCode, 𝔹)` is called before any other access routines are called for that object.
  #### **Design Decision**
  This module represents the `ButtonPressingEvent` class which can be instantiated by a _Minigame_ to store data relating to a single _Button Pressing Metric Event_. This object should be passed to a _Button Pressing Metric_ object ([`ButtonPressingMetric`](#button-pressing-metric-module)) for consumption. Every _Button Pressing Metric Event_ has a time stamp (`eventTime`) in which the event occurred, a key code (`keyCode`) representing the keyboard key which was pressed, and a key value (`keyDown`), representing if the key is pressed down or not (`true` indicated the key is pressed down).
  #### **Access Routine Semantics**
  `ButtonPressingEvent(et, kc, kd)`
  * transition: eventTime, keyCode, keyDown = et, kc, kd
  * output: _out_ := _self_
  * exception: none

  `getEventTime()` (inherited from `AbstractMetricEvent`)
  * transition: none
  * output: _out_ := eventTime
  * exception: none

  `getKeyCode()`
  * transition: none
  * output: _out_ := keyCode
  * exception: none

  `getKeyDown()`
  * transition: none
  * output: _output_ := keyCode
  * exception: none

## Position Event Module
  `PositionEvent` Module inherits [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  ### Uses
  * [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  * `System.DateTime`
  * `UnityEngine.Vector2`
  ### Syntax
  #### **Exported Constants**
  None
  #### **Exported Types**
  None
  #### **Exported Access Programs**
  |Routine Name|In |Out |Exceptions |
  |---|---|---|---|
  |`PositionEvent`|`DateTime`, seq of `Vector2`|`PositionEvent`||
  |`getEventTime`||`DateTime`||
  |`getPositions`||seq of `Vector2`||

  ### Semantics
  #### **State Variables**
  * `eventTime: DateTime` (inherited from `AbstractMetricEvent`)
  * `positions: `seq of `Vector2`
  #### **Assumptions**
  * The constructor `PositionEvent(DateTime,`seq of `Vector2)` is called before any other access routines are called for that object.
  #### **Design Decision**
  This module represents the `PositionEvent` class which can be instantiated by a _Minigame_ to store data relating to a single _Position Metric Event_. This object should be passed to a _Position Metric_ object ([`PositionMetric`](#position-metric-module)) for consumption. Every _Position Metric Event_ has a time stamp (`eventTime`) in which the event occurred, and an array of vector (`Vector2`) objects representing the positions of objects in the game. The names/ids of these objects are stored in the ([`PositionMetric`](#position-metric-module)) which will consume these events.
  #### **Access Routine Semantics**
  `PositionMetric(et, pos)`
  * transition: eventTime, positions = et, pos
  * output: _out_ := _self_
  * exception: none

  `getEventTime()` (inherited from `AbstractMetricEvent`)
  * transition: none
  * output: _out_ := eventTime
  * exception: none

  `getPositions()`
  * transition: none
  * output: _out_ := positions
  * exception: none

## Memory Choice Event Module
  `MemoryChoiceEvent` Module inherits [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  ### Uses
  * [`AbstractMetricEvent`](#411-abstract-metric-event-module)
  * `System.DateTime`
  ### Syntax
  #### **Exported Constants**
  None
  #### **Exported Types**
  None
  #### **Exported Access Programs**
  |Routine Name|In |Out |Exceptions |
  |---|---|---|---|
  |`MemoryChoiceEvent`|`DateTime`, seq of `string`, `string`, `𝔹`, `TimeSpan`|`MemoryChoiceEvent`||
  |`getEventTime`||`DateTime`||
  |`getObjectsSet`||seq of `string`||
  |`getObject`||`string`||
  |`getChoice`||`𝔹`||
  |`getChoiceTime`||`DateTime`||

  ### Semantics
  #### **State Variables**
  * `eventTime: DateTime` (inherited from `AbstractMetricEvent`)
  * `objectsSet:` seq of `string`
  * `object: string`
  * `choice: 𝔹`
  * `choiceTime: DateTime`
  #### **Assumptions**
  * The constructor `MemoryChoiceEvent(DateTime,`seq of `string, string, 𝔹, TimeSpan)` is called before any other access routines are called for that object.
  #### **Design Decision**
  This module represents the `MemoryChoiceEvent` class which can be instantiated by a _Minigame_ to store data relating to a single _Memory Choice Event_. This object should be passed to a _Memory Choice Metric_ object ([`MemoryChoiceMetric`](#memory-choice-metric-module)) for consumption. Every _Memory Choice Event_ has a time stamp (`eventTime`) in which the event occurred, and an array of strings (`objectsSet`) representing the set of objects which should be answered `true`: ie., if `object` is a member of `objectsSet`, `choice` should be `true` if the user guesses correctly; `false` if the user guesses incorrectly. `choiceTime` is the timestamp the user made the choice, and `eventTime` is the timestamp the choice was presented to the user. If you subtract the two, the result is the time it took for the user to make the choice.
  #### **Access Routine Semantics**
  `MemoryChoiceEvent(et, os, o, c, ct)`
  * transition: `eventTime`, `objectsSet`, `object`, `choice`, `choiceTime` = `et`, `os`, `o`, `c`, `ct` 
  * output: _out_ := _self_
  * exception: none

  `getEventTime()` (inherited from `AbstractMetricEvent`)
  * transition: none
  * output: _out_ := `eventTime`
  * exception: none

  `getObjectsSet()`
  * transition: none
  * output: _out_ := `objectsSet`
  * exception: none

  `getObject()`
  * transition: none
  * output: _out_ := `object`
  * exception: none

  `getChoice()`
  * transition: none
  * output: _out_ := `choice`
  * exception: none

  `getChoiceTime()`
  * transition: none
  * output: _out_ := `choiceTime`
  * exception: none

## 4.1.2 Abstract Metric Module
## Button Pressing Metric Module
## Position Metric Module
## Memory Choice Metric Module


## 4.2 Minigame Modules

This section of the document contains all modules relating to the mini-games. These modules are to be implemented in the Unity environment alongside the _Measurement_ modules.

## 4.2.0 Abstract Level Manager Module
abstract `LevelManager` module inherits MonoBehaviour

### Uses
None

### Syntax
#### Exported Constants
None

#### Exported Types
None

#### Exported Access Programs
| Routine Name | In | Out | Exceptions |
|---|---|---|---|
| `Setup` ||||
| `StartLevel` ||||
| `EndLevel` ||||

### Semantics
#### Environment Variables
window: The game window

#### State Variables
`lvlState`: ℕ\
`lvlCountDown`: ℝ

#### State Invariant
`lvlState`∈ {0..2}

#### Assumptions
None

#### Design Decisions
This module provides access routines to inherited modules for pre and post-game features. Specifically it starts the scene with a blurred game scene with an introductory text, then a countdown, and a end game text.

#### Access Routine Semantics
`Setup()`
- transition: `lvlState`, `lvlCountDown` := 0, 5.0,<br>
   window := An introductory text is displayed over a blurred game screen

`StartLevel()`
- transition: `lvlState`==0 ∧ `countDown`<=4 ⇒
   `countDown` := decrements 1.0/sec,<br>
   window := Countdown begins, starting at 3.<br>
   `countDown`<=0 ⇒ `lvlState` := 1, window := Countdown is removed after 0 and game screen is unblurred

`EndLevel()`
- transition: `lvlState` := 2, window := The game screen is blurred and a "level complete" text appears. A button appears to go to the next scene (mini-game or main menu).


## 4.2.1 Digger Modules

This section of modules are used in the Digger game.

## 4.2.1.1 Digger Level Manager Module
`DiggerLevelManager` module inherits [`LevelManager`](#420-abstract-level-manager-module)

### Uses
[`PlayerController`](#4212-player-controller-module), [`GroundBreaker`](#4213-ground-breaker-module), [`ChestAnimator`](#4214-chest-animator-module), [`ButtonPressingMetric`](#button-pressing-metric-module), [`ButtonPressingEvent`](#button-pressing-event-module), {UnnamedJSONOutputter}, `UnityEngine.Event`, `UnityEngine.KeyCode`, `System.DateTime`

### Syntax
#### Exported Constants
None

#### Exported Types
None

#### Exported Access Programs
| Routine Name | In | Out | Exceptions |
|---|---|---|---|
| `Start` ||||
| `Update` ||||
| `OnGUI` ||||

### Semantics
#### Environment Variables
`eventTime`: `DateTime`\
`e`: `Event`\
window: The game window

#### State Variables
`bpMetric`: [`ButtonPressingMetric`](#button-pressing-metric-module)\
`recording`: 𝔹\
`player`: [`PlayerController`](#4212-player-controller-module)\
`chest`: [`ChestAnimator`](#4214-chest-animator-module)\
`digAmount`: ℕ\
`digKey`: `KeyCode`\
`lvlState`: ℕ (inherited from [`LevelManager`](#420-abstract-level-manager-module))\
`lvlCountDown`: ℝ (inherited from [`LevelManager`](#420-abstract-level-manager-module))

#### State Invariant
`digAmount`>0\

#### Assumptions
`Start` is called at the beginning of the scene.\
`Update` is called once each game cycle.\
`OnGUI` is called when a GUI event occurs (keyboard/mouse); it is called after `Update` in the game cycle.

#### Design Decisions
This module manages the majority of functionality in the game. The `digAmount` is the number of button presses required to finish the level. It rounds up to the nearest 10 (as there are 10 blocks to break in the level). `digAmount` and `digKey` have default values of 100 and the "B" key, but can be changed using the battery setup file. 

#### Access Routine Semantics
`Start()`
- transition: `bpMetric`, `recording`, `digAmount`, `digKey`, `lvlState`, `lvlCountDown` := new [`ButtonPressingMetric`](#button-pressing-metric-module), `false`, 100, `KeyCode.B`, 0, 5.0 <br> window := An introductory text is displayed over a blurred game screen

`Update()`
- transition: `lvlState`==1 ⇒
   |||
   |---|---|
   | ¬`recording` | `recording` := `true`, `bpMetric.StartRec()`,  |
   | `chest.opened` | `bpMetric.EndRec()`, `EndLevel()` |

`OnGUI()`
- transition: `e.isKey` ⇒
   |||
   |---|---|
   | `lvlState`==0 ∧ `countDown`>4 | `countdown` := 4.0, `StartLevel()` |
   | `lvlState`==1 ∧ `e.keyCode`==`digKey` ∧ press| `bpMetric.AddEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, true))`, `player.DigDown()` |
   | `lvlState`==1 ∧ `e.keyCode`==`digKey` ∧ release | `bpMetric.AddEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, false))`, `player.DigUp()` |

#### Local Routine Semantics
`SetDigKeyForGround()`
- transition: ∀ b:[`GroundBreaker`](#4213-ground-breaker-module)| b.`SetDigKey(digKey)`

`SetDigAmountForGround()`
- transition: ∀ b:[`GroundBreaker`](#4213-ground-breaker-module)| b.`SetHitsToBreak( ⌈digAmount/10⌉ )`


## 4.2.1.2 Player Controller Module
### Module inherits MonoBehaviour
PlayerController

### Uses
None

### Syntax
#### Exported Constants
None

#### Exported Types
None

#### Exported Access Programs
| Routine Name | In | Out | Exceptions |
|---|---|---|---|
| `DigUp` ||||
| `DigDown` ||||

### Semantics
#### Environment Variables
window: The game window

#### State Variables
`hammerRest`: [ℝ, ℝ, ℝ]\
`hammerJump`: [ℝ, ℝ, ℝ]

#### State Invariant
None

#### Assumptions
`hammerRest` and `hammerJump` are vectors representing position relative to the player character.

#### Design Decisions
This module controls the digging action of the player.

#### Access Routine Semantics
`DigUp`
- transition: window := The location of the jackhammer is set to `hammerJump`.

`DigDown`
- transition: window := The location of the jackhammer is set to `hammerRest`. A dust sprite is generated in a random position near the jackhammer.


## 4.2.1.3 Ground Breaker Module
### Module inherits MonoBehaviour
GroundBreaker

### Uses
[`PlayerController`](#4212-player-controller-module), `UnityEngine.KeyCode`, `UnityEngine.Input`, `UnityEngine.Collider2D`

### Syntax
#### Exported Constants
None

#### Exported Types
None

#### Exported Access Programs
| Routine Name | In | Out | Exceptions |
|---|---|---|---|
| `Start` ||||
| `Update` ||||
| `OnTriggerStay2D` | `Collider2D` |||
| `SetDigKey` | `KeyCode` |||
| `SetHitsToBreak` | ℕ |||

### Semantics
#### Environment Variables
window: The game window

#### State Variables
`player`: [`PlayerController`](#4212-player-controller-module)\
`digKey`: `KeyCode`\
`hitsToBreak`: ℕ\
`hits`: ℕ\
`touching`: 𝔹

#### State Invariant
`hitsToBreak`>0

#### Assumptions
`Start` is called at the beginning of the scene\
`Update` is called once each game cycle\
`OnTriggerStay2D` is called when a trigger gameobject is touching another gameobject; it is called before `Update` in the game cycle.

#### Design Decisions
This module controls the breaking of an individual ground block. By default, each block has 10 visual states of breaking and `hitsToBreak` is 10. If `hitsToBreak` is less than 10, then not all visual states will be shown, and if greater than 10, then some/all visual states will take more than one button press to advance.

#### Access Routine Semantics
`Start()`
- transition: `hits`, `touching` := 0, `false`

`Update()`
- transition: `touching` ∧ `Input.GetKeyDown(digKey)` ⇒
   |||
   |---|---|
   | `hits`<`hitsToBreak`-1 | window := Progress the break animation of this block. |
   | `hits`==`hitsToBreak`-1 | window := Remove this block from the scene. The player falls down to the next block/platform. |

`OnTriggerStay2D(c)`
- transition: `c.gameObject.name`==`player.gameObject.name` ⇒ `touching` := `true`

`SetDigKey(key)`
- transition: `digKey` := `key`

`SetHitsToBreak(hits)`
- transition: `hitsToBreak` := `hits`


## 4.2.1.4 Chest Animator Module
### Module inherits MonoBehaviour
ChestAnimator

### Uses
[`PlayerController`](#4212-player-controller-module), `UnityEngine.Collider2D`

### Syntax
#### Exported Constants
None

#### Exported Types
None

#### Exported Access Programs
| Routine Name | In | Out | Exceptions |
|---|---|---|---|
| `Start` ||||
| `Update` ||||
| `OnTriggerStay2D` | `Collider2D` |||

### Semantics
#### Environment Variables
window: The game window

#### State Variables
`player`: [`PlayerController`](#4212-player-controller-module)\
`opened`: 𝔹\
`coinspeed`: ℝ\
`destination`: [ℝ, ℝ, ℝ]

#### State Invariant
`hitsToBreak`>0

#### Assumptions
`Start` is called at the beginning of the scene\
`Update` is called once each game cycle\
`OnTriggerStay2D` is called when a trigger gameobject is touching another gameobject; it is called before `Update` in the game cycle.
`destination` are vectors representing position relative to the chest gameobject.

#### Design Decisions
This module controls the chest and coin animation when the player reaches it.

#### Access Routine Semantics
`Start()`
- transition: `hits`, `touching` := 0, `false`

`Update()`
- transition: `opened` ⇒ window := The coin moves toward `destination`.

`OnTriggerStay2D(c)`
- transition: `c.gameObject.name`==`player.gameObject.name` ⇒ `opened` := `true`, window := The chest animates opening.

## 4.2.2 Conveyor Modules

This section of modules are used in the Conveyor game.

## 4.2.3 ThirdGame Modules

This section of modules are used in the ThirdGame game.

## 4.3 Battery Module

This section of the document contains all modules relating to the Battery. This module handles the administration of the battery. This includes loading the JSON configuration file, getting player credentials, starting the battery, loading and unloading mini games and ending the battery. 

### Module inherits MonoBehaviour
Battery 

### Uses
`UnityEngine.SceneManagement`,
`Newtonsoft.Json`,
`System.Collections`,
`System.IO`
`System.DateTime`

### Syntax

#### __Exported Constants__
JSON\_BATTERY\_FILENAME

#### __Exported Types__
None

#### __Exported Access Programs__

| Routine Name  | IN     | Out  | Exceptions                |
|---------------|--------|------|---------------------------|
|`LoadBattery`  | string |      | `FileNotFoundException`   | 
|`StartBattery` |        |      |                           | 
|`LoadGame`     | ℕ      | 𝔹    | `SceneCouldNotBeLoaded`   | 
|`UnloadGame`   | ℕ      | 𝔹    | `SceneCouldNotBeUnloaded` | 
|`EndBattery`   |        | JSON | `IOException`             |
|`OnGUI`        |        |      |                           |
|`Start`        |        |      |                           |

### Semantics

#### Environment Variables
window: The game window

#### __State Variables__
`battery`: JSONBattery 
`player_name`: string 
`completed`: 𝔹 
`current_game`: ℕ  
`start_time`: DateTime  
`end_time`: DateTime 

#### __Assumptions__
Battery is also a scene in unity. It is the primary scene and will load and unload other scenes (the games). Because Battery is a scene, Unity will automatically construct the object and then call Start(), therefore Start will act as the constructor. The `battery` variable will need to be available for each game as a global. 

#### __Design Decisions__
Battery was implemented as a scene because of Unity's SceneManager. It allows one scene to control the state (loaded or unloaded) of other scenes.

The `battery` variable is global because each mini game is a scene and scenes do not have constructors from which you can pass variables to.

The Battery module will output a copy of the JSON file used as input with additions of completed status and player name. This allows administrators to change the input file for new batterys will keeping a record of what battery settings were used by that particular player.

#### __Access Routine Semantics__

`LoadBattery(filename)`:
* transition: `battery` := `loadJSON(filename)`
* output: None
* exception: `FileNotFoundException`

`Start()`:
* transition: `completed` := `false`, `current_game` := `0`, `player_name` := `""`
* output: None
* exception: `FileNotFoundException`

`OnGUI()`:
* transition: `window` := Show battery start screen which allows player to input their name and start the battery by hitting a GUI button. `player_name := input`
* output: None
* exception: None

`StartBattery()`:
* transition: `current_game` := `1`, `start_time := DateTime.Now`
* output: None 
* exception: None

`LoadGame(game)`:
* transition: `current\_game` := `game`, `window` := Show loaded scene
* output: None 
* exception: `SceneCouldNotBeLoaded`

`UnloadGame(game)`:
* transition: `current_game` := `0`
* output: None 
* exception: `SceneCouldNotBeUnloaded` 

`EndBattery()`:
* transition: `completed` := `true`, `end_time` := `DateTime.Now`
* output: `JSON` 
* exception: `IOException`

__Local Functions__

`loadScenes(battery)`: ∀ game : battery.games | SceneManager.LoadScene(game, LoadSceneMode.Additive);

`loadJSON(filename)`: JsonConvert.DeserializeObject<JSONBattery>(File.ReadAllText(filename))

`writeJSON(filename)`: File.WriteAllText(filename, JsonConvert.SerializeObject(battery))

# 5. List of Changes to SRS

TODO(mike): Add links from here to SRS document

* Fixed general spelling and grammar mistakes that in the original SRS document.

* Added more specifics to the product / work scope. Added more information about the battery manager, the measurement modules and data manager. [LINK TO 1.2]

* Update the mini-game description to how long a player should be playing a game.

* Mini-games now have the ability for researchers to make small changes to the game's objectives by altering the game's variables.

* Include details about battery module and JSON data used to setup up the batter test.
* Added variable length dictionary of variables to mini games.

* Updated definitions, acronyms and abbreviations [LINK TO 1.3]. Added Battery definition. Removed database definition as it is outside the scope of this project. Updated vocabulary to be more consistent with the rest of the document.

* Updated references to include new research documents, websites and textbooks. [LINK TO 1.4]

* Updated Work Scope to provide more detail. Added work scope for three mini games, the battery module and measurement modules. Improved description of testing strategies. [LINK TO 1.6]

* Updated the product perspective to better reflect new separation of concerns between different parts of the project. Removed database information. [LINK TO 2.1] 

* Add battery module, data manager, and metric modules to product functions. Remove database information. [LINK TO 2.2] 

* Added "Free Platform Game Assets" and "JsonDotNet" as dependencies. The "Free Platform Game Assets" provides a while range of 2d sprites which can be used in a variety of different mini-games. A design goal is to keep the graphics style and avatar of the player similar between different mini-games. The "JsonDotNet" package allows for the transformation of C# objects to JSON objects making it terminal to modify data recorded by the metric modules.  [LINK TO 2.5] 

* Explained that admin users should be able to create a JSON file which describes a new battery. The file will contain the sequence of mini games that appear in the battery as well as variables that will make alterations of the mini games. Admin users should also be able to read and understand the data organized and presented by the Data Manager after a battery is completed. [LINK TO  2.4] 

* Updated the apportioning of requirements to break up "measurement framework" into battery module, metric modules and data manager. Removed database requirements. [LINK TO 2.6]

* Updated List of Stakeholders. Added McMaster University, TAs, game designers, and researchers/academics to the list of stakeholders. [LINK TO 2.7] 

* Update requirements to match modules in design document. [LINK TO 3]
    * Removed pause game and show score screen. After discussion with supervisor it was determined that these functions were unnecessary.
    * Renamed Database to Data Manager. A database will no longer be used to store data. Instead, a JSON file will be used to store the raw data from which the Data Manager will collect that data and organize and present it. 
    * Split Measurement Framework into Measurement Modules and Battery Module.
    * Added requirement that mini game start screen should contain instructions on how to play the game.

* User interface changes [LINK 3.1.1]
    * Removed database as a requirement to the user interface. Instead of recording and validating player identification with a database backend; the user identification will be put in a meta data header in the JSON data for the battery that player is participating in.
    * Users no longer launch individual games but launch the battery module which puts the users through sequence of mini games. 
    * Mini-game start screens must provide instructions to the player on how to play the game.
    * Removed concept of a main menu. That will now be broken up into the battery start screen and the mini game start screen.
    * Removed main menu example and added start and exit example.
    * Move error messages from the mini-games to the battery module. 

* Removed database from hardware interfaces [LINK 3.1.2]

* Changed software interface from database to JSON files. [LINK TO 3.1.3]

* Added present data and removed pause screen and score screen from functional requirements. Present data is required to make data collected readable to researchers. Pause and score screens are unnecessary as discussed with supervisor. [LINK TO 3.2]

* Removed database connection from Availability and added player time. A player must be available to complete a battery in one sitting without breaks. [LINK TO 3.3.4]

* Added player browsers as installation detail. [LINK OT 3.5.1]

# 6. Module Relationship Diagram
![](ModuleRelationShip.jpg)

# 7. Significant Algorithms/Non-Trivial Invariants


