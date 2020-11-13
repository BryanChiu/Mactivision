
Table of Contents
=================
* [Revision History](#revision-history)
* 1 [Anticipated Changes](#1-anticipated-changes)
* 2 [Unlikely Changes](#2-unlikely-changes)
* 3 [List of Concepts](#3-list-of-concepts)
* 4 [List of Modules](#4-list-of-modules)
  * 4.1 [Measurement Framework Modules](#41-measurement-framework-modules)
* 5 [List of Changes to SRS](#5-list-of-changes-to-srs)
* 6 [Module Relationship Diagram](#6-module-relationship-diagram)
* 7 [Significant Algorithms/Non-Trivial Invariants](#7-significant-algorithms/non-trivial-invariants)

# Revision History

# 1. Anticipated Changes

* Mini-games will contain variables which will allow battery test administers/researchers to make small changes to the game's objectives and functionality. The number of variables from which the researchers can alter for each mini game will change over time as researchers think of new alterations for the games. In order to accommodate these changes each mini game will be passed a variable length dictionary of variables. This allows quicker additions or subtractions of variables and reduces confusion between researchers and mini game developer.

* The SRS document will continue to evolve throughout the life span of this project. It will be updated with changes discovered during the creation of a design document and for each subsequent major deliverable.

TODO: Fill in A, B, C

* There are 46 cognitive and motor abilities available to measure as defined in our SRS.[LINK]. In the design of the three games listed in this document we have specifically targeted A, B, and C. Which abilities each game measures is subject to change as we create the games and test them with players. The measurement modules will help determine whether a game is accurately and meaningfully capturing the targeted abilities. If the game is not capturing the targeted ability in a significant way change must be made to the game or to the measurement module. Additionally, it may be discovered that some abilities are irrevocably linked and must be included in the targeted abilities list. Similarly, some games or measurement modules may be ineffective at measuring a specific ability within the scope of the project, in such as case, those abilities may be removed for the targeted list. These changes are expected to occur during the development of the mini games. In anticipation of this the design the games are kept simple such that they have a minimal number of game elements. Unity allows for these game elements to be module allowing for easy addition or subtraction from a particular game. Even eliminating an entire game design is a possibility. However, the module nature of the game elements will allow for reuse in any new design created as a replacement. This ensures that development of mini games is agility and can easily adapt to new discoveries in relationship between games and player abilities.

* Number of Unity packages will increase as development continues during the project. Thankfully, it easy to add and remove packages using the Unity package manager. Graphical and audio assets can also be managed in this way. This makes distributing the assets easier when sharing the unity project between team members and the final deliverable. It also makes it much easier to track which packages we have used over time, as each package and collection of assets have their on page on the unity store. This is particularly important for keeping track of the licenses of these packages to make sure they can be used in the project now and in the future.

# 2. Unlikely Changes

* JSON data structure is used as input to initialize the battery and as output for measurement data. This may change to another format, for example YAML or XML in future. This change depends on the needs of the researchers administrating the battery. As the data manager manages convert JSON files into a presentable manner it unlikely for research will want a change in data formats.  

* A database may be required to store the data instead of using JSON or other data structures in the long term. However, that is currently outside the scope of this project.

* There is currently no plan to include functionality to handle the scenario where the player stops playing the games during the middle of the battery. The administrator of the battery will have to decide if the data collected so far is sufficient or if the battery should be re administered. Similarly, any software crashes or bugs which halt the execution of the battery or any of the mini games will be unrecoverable. The battery application contains no functionality to recover from these errors only to report them to the user and administrator. All attempts will be made to make the software robust enough for this to be unlikely and for error messages to be helpful to players and administrators so they can make actionable decisions. Thankfully, the duration of the battery should be short enough that a battery restart is possible.

* A somewhat likely and unlikely change is the player input devices. Because of Covid 19 most of the players will be completing a battery using a browser and a keyboard. The supervisors of the project hope to be able to add more input types in future, however that will mostly be attempted after Mactivision has submitted their final work. Therefore, it is unlikely that keyboard will change before work is completed. However, since the future of the project will likely add new inputs all efforts have been made, now, to accommodate different input devices. Using Unity to abstract player inputs into actions like "left", "right" and "fire 1" instead of "pad-up", "right arrow", "trigger 2" make accommodating different yet similar input devices trivial. Different kinds of input devices like motion controls are mice can't be mapped to the ab

# 3. List of Concepts

# 4. List of Modules

## 4.1 Measurement Framework Modules

# 5. List of Changes to SRS

TODO(mike): Add links from here to SRS document

* Fixed general spelling and grammar mistakes that in the original SRS document.

* Added more specifics to the product / work scope. Added more information about the battery manager, the measurement modules and data manager. [LINK TO 1.2]

* Update the mini-game description to how long a player should be playing a game.

* Mini-games now have the ability for researchers to make small changes to the game's objectives by altering the game's variables.

* Include details about battery application and JSON data used to setup up the batter test.
* Added variable length dictionary of variables to mini games.

* Updated definitions, acronyms and abbreviations [LINK TO 1.3]. Added Battery definition. Removed database definition as it is outside the scope of this project. Updated vocabulary to be more consistent with the rest of the document.

* Updated references to include new research documents, websites and textbooks. [LINK TO 1.4]

* Updated Work Scope to provide more detail. Added work scope for three mini games, the battery module and measurement modules. Improved description of testing strategies. [LINK TO 1.6]

* Updated the product perspective to better reflect new separation of concerns between different parts of the project. Removed database information. [LINK TO 2.1] 

* Add battery application, data manager, and metric modules to product functions. Remove database information. [LINK TO 2.2] 

* Added "Free Platform Game Assets" and "JsonDotNet" as dependencies. The "Free Platform Game Assets" provides a while range of 2d sprites which can be used in a variety of different mini-games. A design goal is to keep the graphics style and avatar of the player similar between different mini-games. The "JsonDotNet" package allows for the transformation of C# objects to JSON objects making it terminal to modify data recorded by the metric modules.  [LINK TO 2.5] 

* Explained that admin users should be able to create a JSON file which describes a new battery. The file will contain the sequence of mini games that appear in the battery as well as variables that will make alterations of the mini games. Admin users should also be able to read and understand the data organized and presented by the Data Manager after a battery is completed. [LINK TO  2.4] 

* Updated the apportioning of requirements to break up "measurement framework" into battery application, metric modules and data manager. Removed database requirements. [LINK TO 2.6]

* Updated List of Stakeholders. Added McMaster University, TAs, game designers, and researchers/academics to the list of stakeholders. [LINK TO 2.7] 

* Update requirements to match modules in design document. [LINK TO 3]
    * Removed pause game and show score screen. After discussion with supervisor it was determined that these functions were unnecessary.
    * Renamed Database to Data Manager. A database will no longer be used to store data. Instead, a JSON file will be used to store the raw data from which the Data Manager will collect that data and organize and present it. 
    * Split Measurement Framework into Measurement Modules and Battery Application.
    * Added requirement that mini game start screen should contain instructions on how to play the game.

* User interface changes [LINK 3.1.1]
    * Removed database as a requirement to the user interface. Instead of recording and validating player identification with a database backend; the user identification will be put in a meta data header in the JSON data for the battery that player is participating in.
    * Users no longer launch individual games but launch the battery application which puts the users through sequence of mini games. 
    * Mini-game start screens must provide instructions to the player on how to play the game.
    * Removed concept of a main menu. That will now be broken up into the battery start screen and the mini game start screen.
    * Removed main menu example and added start and exit example.
    * Move error messages from the mini-games to the battery application. 

* Removed database from hardware interfaces [LINK 3.1.2]

* Changed software interface from database to JSON files. [LINK TO 3.1.3]

* Added present data and removed pause screen and score screen from functional requirements. Present data is required to make data collected readable to researchers. Pauser and score screens are unnesscary as discussed with supervisor. [LINK TO 3.2]

* Removed database connection from Avaiability and added player time. A player must be available to complete a battery in one sitting without breaks. [LINK TO 3.3.4]

* Added player browsers as installation detail. [LINK OT 3.5.1]

# 6. Module Relationship Diagram

# 7. Significant Algorithms/Non-Trivial Invariants
