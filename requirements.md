# Software Requirements Specification
## For Mini-game Battery Project

Version 0.1  
Prepared by <author> 
<organization>  
<date created>  

Table of Contents
=================
* [Revision History](#revision-history)
* 1 [Introduction](#1-introduction)
  * 1.1 [Document Purpose](#11-document-purpose)
  * 1.2 [Product Scope](#12-product-scope)
  * 1.3 [Definitions, Acronyms and Abbreviations](#13-definitions-acronyms-and-abbreviations)
  * 1.4 [References](#14-references)
  * 1.5 [Document Overview](#15-document-overview)
* 2 [Product Overview](#2-product-overview)
  * 2.1 [Product Perspective](#21-product-perspective)
  * 2.2 [Product Functions](#22-product-functions)
  * 2.3 [Product Constraints](#23-product-constraints)
  * 2.4 [User Characteristics](#24-user-characteristics)
  * 2.5 [Assumptions and Dependencies](#25-assumptions-and-dependencies)
  * 2.6 [Apportioning of Requirements](#26-apportioning-of-requirements)
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

## Revision History
| Name | Date    | Reason For Changes  | Version   |
| ---- | ------- | ------------------- | --------- |
|      |         |                     |           |
|      |         |                     |           |
|      |         |                     |           |

## 1. Introduction
> This section should provide an overview of the entire document

### 1.1 Document Purpose
Describe the purpose of the SRS and its intended audience.

### 1.2 Product Scope
Identify the product whose software requirements are specified in this document, including the revision or release number. Explain what the product that is covered by this SRS will do, particularly if this SRS describes only part of the system or a single subsystem. Provide a short description of the software being specified and its purpose, including relevant benefits, objectives, and goals. Relate the software to corporate goals or business strategies. If a separate vision and scope document is available, refer to it rather than duplicating its contents here.

### 1.3 Definitions, Acronyms and Abbreviations

### 1.4 References
List any other documents or Web addresses to which this SRS refers. These may include user interface style guides, contracts, standards, system requirements specifications, use case documents, or a vision and scope document. Provide enough information so that the reader could access a copy of each reference, including title, author, version number, date, and source or location.

### 1.5 Document Overview
Describe what the rest of the document contains and how it is organized.

## 2. Product Overview
<!-- > This section should describe the general factors that affect the product and its requirements. This section does not state specific requirements. Instead, it provides a background for those requirements, which are defined in detail in Section 3, and makes them easier to understand. -->

This section will give an overview of the entire architecture of the system and its requirements, as well as the general factors that will affect the system. Further information regarding the requirements will be defined in detail in Section 3.

### 2.1 Product Perspective
<!-- Describe the context and origin of the product being specified in this SRS. For example, state whether this product is a follow-on member of a product family, a replacement for certain existing systems, or a new, self-contained product. If the SRS defines a component of a larger system, relate the requirements of the larger system to the functionality of this software and identify interfaces between the two. A simple diagram that shows the major components of the overall system, subsystem interconnections, and external interfaces can be helpful. -->

This system will consist of two parts: a set of minigames, and a backend. Each minigame will communicate with the backend while a user plays the minigame, in order to record information from the game.

Each minigame will be built using the Unity engine and each will be designed to measure a relatively small, specific subset of cognitive and motor abilities. The user input in these minigames will mostly be restricted to keyboard input, as keyboards are widely available to most users, however input abstraction in the Unity engine will be made use of in order to switch between input types for most minigames.

The backend is responsible for collecting and storing the data generated by users playing the minigames. It will run on a server machine either in the cloud or on a local machine. Each minigame will pass the backend data about the game as it is played by the user. This data will include input data (what controller is being used), the result of the minigame, information about the user playing the game, the time spent playing the game, etc.

In order for the backend to store the data, it will be coupled with a database. The database will store information about specific minigames, such as which cognitive and motor abilities that minigame measures, the type of minigame, and other minigame specific minigame information. The database will also include data gathered from instances of the minigames being played, such as the date and time of the gameplay, the input type used, the user that played the minigame, etc.

### 2.2 Product Functions
<!-- Summarize the major functions the product must perform or must let the user perform. Details will be provided in Section 3, so only a high level summary (such as a bullet list) is needed here. Organize the functions to make them understandable to any reader of the SRS. A picture of the major groups of related requirements and how they relate, such as a top level data flow diagram or object class diagram, is often effective. -->

The minigames must perform specific tasks in order to accurately measure the cognitive and motor abilities of the user. These tasks include:

* The user interfacing with the minigame will be able to:
  * Start the minigame
  * Quit/exit the minigame
  * Perform inputs on their controller to play the minigame
* The minigame will display the game to the user, as well as play all sounds to the user
* The minigame will record data about each play through of the game
  * That data will be sent to the backend to be processed
  * This data will NOT be presented to the user
* With some minigames, the user will have more than one controller option; these minigames will recognize and use alternative controllers

Since each minigame is essentially be a medium for the backend to measure and record the cognitive and motor abilities of the user who is playing the minigame, the backend will preform the following tasks:
* Establish connections to current minigames being played
* Receive live data from the minigames before, after, and as they are being played
* Read and write to the database to verify and record incoming minigame data

### 2.3 Product Constraints
<!-- This subsection should provide a general description of any other items that will limit the developer’s options. These may include:  

* Interfaces to users, other applications or hardware.  
* Quality of service constraints.  
* Standards compliance.  
* Constraints around design or implementation. -->

This minigame battery project has some constraints regarding the design of the minigames. All minigames must be build using the Unity engine, and must be designed for play on PC. The minigames must be able to be run on lower end machines, as they will be given to various users to play on their personal machines, many of which may not be built to handle higher-end graphics or games. Furthermore, Some of these users may not have access a mouse, and only have the trackpad on their personal laptop to control their cursor, so most minigames must be designed to only require a keyboard, as the subset of motor abilities to use a trackpad versus a mouse are different, and we can assume that every user playing these minigames will have access to a keyboard.

### 2.4 User Characteristics
<!-- Identify the various user classes that you anticipate will use this product. User classes may be differentiated based on frequency of use, subset of product functions used, technical expertise, security or privilege levels, educational level, or experience. Describe the pertinent characteristics of each user class. Certain requirements may pertain only to certain user classes. Distinguish the most important user classes for this product from those who are less important to satisfy. -->

There are two user classes which will use this product:
* General users, which will play the minigames. This group of users is very broad and can contain users in almost any demographic. These users should have basic computer literacy as they will mostly play minigames on a PC.
* Admin users, which will interface with the backend to gather general user profiles and other minigame data. This group of users will have a higher computer literacy, as they may be interfacing with the backend or database directly, possibly through a terminal. Admin users will be the clients of the project.

### 2.5 Assumptions and Dependencies
<!-- List any assumed factors (as opposed to known facts) that could affect the requirements stated in the SRS. These could include third-party or commercial components that you plan to use, issues around the development or operating environment, or constraints. The project could be affected if these assumptions are incorrect, are not shared, or change. Also identify any dependencies the project has on external factors, such as software components that you intend to reuse from another project, unless they are already documented elsewhere (for example, in the vision and scope document or the project plan). -->

The minigames portion of this project will rely on assets from the Unity asset store to develop the minigames. It is assumed that we the developers will find relevant assets in the Unity asset store that will help us build minigames that fit the requirements described in this document in a more time-efficient manner, as opposed to spending more time creating the assets we require. These assets might include:
* 3D models or 2D sprites
* Audio clips for game sounds effects
* Various prebuilt scripts to control various mechanics in the minigames

We estimate that the time saved when using premade assets like these will be substantial, and will help us develop minigames much more efficiently, so more time can be allocated to the design of the games rather than the programming of them.

<!-- TODO(dhospital): Add more assumptions and dependencies -->

### 2.6 Apportioning of Requirements
<!-- Apportion the software requirements to software elements. For requirements that will require implementation over multiple software elements, or when allocation to a software element is initially undefined, this should be so stated. A cross reference table by function and software element should be used to summarize the apportioning.

Identify requirements that may be delayed until future versions of the system (e.g., blocks and/or increments). -->

## 3. Requirements
> This section specifies the software product's requirements. Specify all of the software requirements to a level of detail sufficient to enable designers to design a software system to satisfy those requirements, and to enable testers to test that the software system satisfies those requirements.

> The specific requirements should:
* Be uniquely identifiable.
* State the subject of the requirement (e.g., system, software, etc.) and what shall be done.
* Optionally state the conditions and constraints, if any.
* Describe every input (stimulus) into the software system, every output (response) from the software system, and all functions performed by the software system in response to an input or in support of an output.
* Be verifiable (e.g., the requirement realization can be proven to the customer's satisfaction)
* Conform to agreed upon syntax, keywords, and terms.

### 3.1 External Interfaces
> This subsection defines all the inputs into and outputs requirements of the software system. Each interface defined may include the following content:
* Name of item
* Source of input or destination of output
* Valid range, accuracy, and/or tolerance
* Units of measure
* Timing
* Relationships to other inputs/outputs
* Screen formats/organization
* Window formats/organization
* Data formats
* Command formats
* End messages

#### 3.1.1 User interfaces
Define the software components for which a user interface is needed. Describe the logical characteristics of each interface between the software product and the users. This may include sample screen images, any GUI standards or product family style guides that are to be followed, screen layout constraints, standard buttons and functions (e.g., help) that will appear on every screen, keyboard shortcuts, error message display standards, and so on. Details of the user interface design should be documented in a separate user interface specification.

Could be further divided into Usability and Convenience requirements.

#### 3.1.2 Hardware interfaces
Describe the logical and physical characteristics of each interface between the software product and the hardware components of the system. This may include the supported device types, the nature of the data and control interactions between the software and the hardware, and communication protocols to be used.

#### 3.1.3 Software interfaces
Describe the connections between this product and other specific software components (name and version), including databases, operating systems, tools, libraries, and integrated commercial components. Identify the data items or messages coming into the system and going out and describe the purpose of each. Describe the services needed and the nature of communications. Refer to documents that describe detailed application programming interface protocols. Identify data that will be shared across software components. If the data sharing mechanism must be implemented in a specific way (for example, use of a global data area in a multitasking operating system), specify this as an implementation constraint.

### 3.2 Functional
> This section specifies the requirements of functional effects that the software-to-be is to have on its environment.

### 3.3 Quality of Service
> This section states additional, quality-related property requirements that the functional effects of the software should present.

#### 3.3.1 Performance
If there are performance requirements for the product under various circumstances, state them here and explain their rationale, to help the developers understand the intent and make suitable design choices. Specify the timing relationships for real time systems. Make such requirements as specific as possible. You may need to state performance requirements for individual functional requirements or features.

#### 3.3.2 Security
Specify any requirements regarding security or privacy issues surrounding use of the product or protection of the data used or created by the product. Define any user identity authentication requirements. Refer to any external policies or regulations containing security issues that affect the product. Define any security or privacy certifications that must be satisfied.

#### 3.3.3 Reliability
Specify the factors required to establish the required reliability of the software system at time of delivery.

#### 3.3.4 Availability
Specify the factors required to guarantee a defined availability level for the entire system such as checkpoint, recovery, and restart.

### 3.4 Compliance
<!-- Specify the requirements derived from existing standards or regulations, including:  
* Report format
* Data naming
* Accounting procedures
* Audit tracing

For example, this could specify the requirement for software to trace processing activity. Such traces are needed for some applications to meet minimum regulatory or financial standards. An audit trace requirement may, for example, state that all changes to a payroll database shall be recorded in a trace file with before and after values.
-->

C# code should conform to [Microsft Coding Convention.](
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)

ID             | CO-1 Type: Non-functional Requirements (Complicance)
---------------|----
PUC            | N/A
Description    | Project will adhere to Unity terms and conditions.
Rationale      | Required for legal use of software.
Fit Criterion  | All terms and conditions will be followed.
Priority       | Very High

### 3.5 Design and Implementation

#### 3.5.1 Installation
<!-- Constraints to ensure that the software-to-be will run smoothly on the target implementation platform. -->
If third-party app stores are used, such as steam, they will handle installation. Otherwise, Inno Setup can be used to create an installer for the project. In either case, the user will not have to worry about where to put files or how to remove them. Furthermore, the Installations will run smoothly because the Unity runtime is self-contained and does not require redistributables like the Visual C++ runtime library or auxiliary third party applications.

#### 3.5.2 Distribution
<!-- Constraints on software components to fit the geographically distributed structure of the host organization, the distribution of data to be processed, or the distribution of devices to be controlled. -->

Unity Distribution Portal allows game developers to distribute games to multiple app stores through a single hub. Projects can also be distributed manually by supplying users with copies of production builds.  WebGL builds can be played online through a modern browser. Other builds require a download onto the user’s hardware. The size of download will depend on the size of the code base and assets. Distribution to those without internet connections would be more difficult. However, physical media, such as spinning discs, could be used to reach those without connections.
This project does not provide any hardware or controller devices, they must be supplied by the user.

#### 3.5.3 Maintainability
<!-- Specify attributes of software that relate to the ease of maintenance of the software itself. These may include requirements for certain modularity, interfaces, or complexity limitation. Requirements should not be placed here just because they are thought to be good design practices. -->

Unity games are written in C# which is strongly typed to prevent common programming errors. C# is object oriented, which provides modularity.  The .NET standard library reduces complexity and increases maintainability. Debugging C# code can be done with Visual Studio. 

#### 3.5.4 Reusability
<!-- TODO: come up with a description -->

Game logic and player cognitive and physical analysis could be reusable in other projects. Code, which interfaces with Unity, would only be reusable with other Unity projects.

#### 3.5.5 Portability
<!-- Specify attributes of software that relate to the ease of porting the software to other host machines and/or operating systems. -->

Unity provides multi-platform support for personal computers, phones, websites and consoles. Unity also supports multiple input devices such as, keyboard, mouse, touch, joystick and game pads. C# code is portable to Windows, Mac and Linux.

#### 3.5.6 Cost
<!-- Specify monetary cost of the software product. -->
Unity is free as long as “revenue or funding is less than $100K in the last 12 months”. Editors for coding with Unity such as Visual Studio and Visual Studio Code have free community versions. Github provides free public repos. Some assets are available in the Unity Store for free. Other free assets may be found online.

#### 3.5.7 Deadline
<!-- Specify schedule for delivery of the software product. -->
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
<!-- TODO: come up with a description -->

![Image of Proof of Concept](https://i.imgur.com/j2X5dEe.png)

## 4. Verification
> This section provides the verification approaches and methods planned to qualify the software. The information items for verification are recommended to be given in a parallel manner with the requirement items in Section 3. The purpose of the verification process is to provide objective evidence that a system or system element fulfills its specified requirements and characteristics.

<!-- TODO: give more guidance, similar to section 3 -->
<!-- ieee 15288:2015 -->

## 5. Appendixes

https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions
https://unity.com/features/multiplatform
https://store.unity.com/compare-plans
https://assetstore.unity.com/top-assets/top-free
https://unity.com/products/unity-distribution-portal
https://jrsoftware.org/isinfo.php
