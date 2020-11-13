Table of Contents
=================
* [Revision History](#revision-history)
* 1 [Anticipated Changes](#1-anticipated-changes)
* 2 [Unlikely Changes](#2-unlikely-changes)
* 3 [List of Concepts](#3-list-of-concepts)
* 4 [List of Modules](#4-list-of-modules)
  * 4.1 [Measurement Framework Modules](#41-measurement-framework-modules)
    * 4.1.1 [Abstract Metric Event Module](#411-abstract-metric-event-module)
    * 4.1.2 [Abstract Metric Module](#412-abstract-metric-module)
  * 4.2 [Minigame Modules](#42-minigame-modules)
    * 4.2.1 [Digger Level Manager Module](#421-digger-level-manager-module)
    * 4.2.2 [Player Controller Module](#422-player-controller-module)
    * 4.2.3 [Ground Breaker Module](#423-ground-breaker-module)
    * 4.2.4 [Chest Animator Module](#424-chest-animator-module)
* 5 [List of Changes to SRS](#5-list-of-changes-to-srs)
* 6 [Module Relationship Diagram](#6-module-relationship-diagram)
* 7 [Significant Algorithms/Non-Trivial Invariants](#7-significant-algorithms/non-trivial-invariants)

# Revision History

# 1. Anticipated Changes

# 2. Unlikely Changes

# 3. List of Concepts

# 4. List of Modules

## 4.1 Measurement Framework Modules

This section of the document contains all modules relating to the _Measurement Framework_. These modules are to be implemented alongside the _Minigame_ modules in the Unity environment, and will be accessed by modules in the _Minigame_ set of modules.

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

This section of the document contains all modules relating to the mini-games. These modules are to be implemented in the Unity environment alongside the _Measurement Framework_ modules.

## 4.2.1 Digger Level Manager Module
### Module inherits MonoBehaviour
DiggerLevelManager

### Uses
[`PlayerController`](#422-player-controller-module), [`GroundBreaker`](#423-ground-breaker-module), [`ChestAnimator`](#424-chest-animator-module), [`ButtonPressingMetric`](#button-pressing-metric-module), [`ButtonPressingEvent`](#button-pressing-event-module), {UnnamedJSONOutputter}, `UnityEngine.Event`, `UnityEngine.KeyCode`, `System.DateTime`

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
`player`: [`PlayerController`](#422-player-controller-module)\
`chest`: [`ChestAnimator`](#424-chest-animator-module)\
`bpMetric`: [`ButtonPressingMetric`](#button-pressing-metric-module)\
`digAmount`: ℕ\
`digKey`: `KeyCode`\
`lvlState`: ℕ\
`lvlCountDown`: ℝ

#### State Invariant
`digAmount`>0\
`lvlState`∈ {0..2}

#### Assumptions
`Start` is called at the beginning of the scene.\
`Update` is called once each game cycle.\
`OnGUI` is called when a GUI event occurs (keyboard/mouse); it is called after `Update` in the game cycle.

#### Design Decisions
This module manages the majority of functionality in the game. It is responsible for the pre and post-game features. The `digAmount` is the number of button presses required to finish the level. It rounds up to the nearest 10 (as there are 10 blocks to break in the level). `digAmount` and `digKey` have default values of 100 and the "B" key, but can be changed using the battery setup file. 

#### Access Routine Semantics
`Start()`
- transition: `bpMetric`, `digAmount`, `digKey`, `lvlState`, `lvlCountDown` := new [`ButtonPressingMetric`](#button-pressing-metric-module), 100, `KeyCode.B`, 0, 5.0 <br> window := An introductory text is displayed over a blurred game screen

`Update()`
- transition:
   |||
   |---|---|
   | `lvlState`==0 ∧ `countDown`<=4 | `StartLevel()` |
   | `lvlState`==1 ∧ `chest.opened` | `bpMetric.EndRec()`, `lvlState` := 2, `EndLevel()` |

`OnGUI()`
- transition: `e.isKey` ⇒
   |||
   |---|---|
   | `lvlState`==0 ∧ `countDown`>4 | `countdown` := 4.0, window := The intro text changes to a countdown starting at 3 |
   | `lvlState`==1 ∧ `e.keyCode`==`digKey` ∧ press| `bpMetric.AddEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, true))`, `player.DigDown()` |
   | `lvlState`==1 ∧ `e.keyCode`==`digKey` ∧ release | `bpMetric.AddEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, false))`, `player.DigUp()` |

#### Local Routine Semantics
`StartLevel()`
- transition: `countDown` := decrements 1.0/sec,<br>
   window := Countdown is removed after 0 and game screen is unblurred,<br>
   `SetDigKeyForGround()`,<br>
   `SetDigKAmountForGround()`

`EndLevel()`
- transition: `{UnnamedJSONOutputter}.AddMetric(bpMetric)`,<br> 
   window := The game screen is blurred and a "level complete" text appears. A button appears to go to the next scene (mini-game or main menu) 

`SetDigKeyForGround()`
- transition: ∀ b:[`GroundBreaker`](#423-ground-breaker-module)| b.`SetDigKey(digKey)`

`SetDigAmountForGround()`
- transition: ∀ b:[`GroundBreaker`](#423-ground-breaker-module)| b.`SetHitsToBreak( ⌈digAmount/10⌉ )`


## 4.2.2 Player Controller Module
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

`DigUp`
- transition: window := The location of the jackhammer is set to `hammerRest`. A dust sprite is generated in a random position near the jackhammer.


## 4.2.3 Ground Breaker Module
### Module inherits MonoBehaviour
GroundBreaker

### Uses
[`PlayerController`](#422-player-controller-module), `UnityEngine.KeyCode`, `UnityEngine.Input`, `UnityEngine.Collider2D`

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
`player`: [`PlayerController`](#422-player-controller-module)\
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


## 4.2.4 Chest Animator Module
### Module inherits MonoBehaviour
ChestAnimator

### Uses
[`PlayerController`](#422-player-controller-module), `UnityEngine.Collider2D`

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
`player`: [`PlayerController`](#422-player-controller-module)\
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

# 5. List of Changes to SRS

# 6. Module Relationship Diagram

# 7. Significant Algorithms/Non-Trivial Invariants
