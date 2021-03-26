![Mactivision logo](https://github.com/BryanChiu/Mactivision/blob/master/Repo%20Assets/Mactivision.png)
CS4ZP6 2020-21 Capstone group. Mini-games

**Team Members**:
* Kunyuan Cao
* Bryan Chiu
* David Hospital
* Michael Tee
* Sijie Zhou

## Mactivision Mini-Games

Video games are experienced at many different levels; however, all games by
nature of being playable have some form of mechanical experience that serves
as first contact with the player. The mechanical experience of a game is
understood through the concepts of mechanical achievability. That is to say,
is it possible, given a player’s profile of cognitive and motor abilities,
that they can complete the game’s challenges?

The Games Scalability Environment (G-ScalE) is a research team that focuses
on understanding the fundamental science behind video games. Our capstone
team (Mactivision) has developed various cognitive tests disguised as
mini-games which will measure users’ strengths and weaknesses in performing
various atomic challenges (game tasks), each corresponding to different
cognitive and motor abilities. As they are measurements, these mini-games
cannot be won or lost; there is no scoring or feedback that would otherwise
try to encourage the user to "do better". Each test is a play-through of a
series of mini-games, called a Mini-Game Battery. 

After each mini-game, data is
recorded of the user’s gameplay, which can then be processed, by
researchers, to measure the user’s performance in the cognitive and motor
abilities being measured. Each Battery can be tailored to the researcher’s
specifications, with any combination and any number of mini-games, and thus
can be used to measure the change in a user’s
performance when using different mini-game parameters.

### Digger

Image

The digger game measures finger pressing ability. The player digs downwards by mashing a button as quickly as they can. Once the player reaches the treasure the game is over. The button pressing event module records all the button input by the user, the state of those inputs and the time each button was pressed.

### Feeder

Image

The feeder game measures updating working memory ability. The player will be asked to feed a monster some food. The list of food the monster likes and dislikes changes over time. As food items are dispensed one at a time, the player must remember the monster's preference changes and correctly feed or discard the food. The memory choice event module will record whether the player correctly remembers to feed the monster the food it wants.

### Rockstar

Image

The rockstar game measures divided attention ability. In this mini-game, the player works backstage to help the rockstar perform their show. As the rockstar performs, the player must control the spotlight, keeping it on the rockstar. At the same time a gauge will measure the excitement of the crowd. The player has to monitor and keep the crowd energy at a comfortable level by deploying an adequate amount of fireworks. The player must manage these two tasks simultaneously. The position event module will be used to record the player position and the spotlight position. The linear variable metric event module used to record the fluctuations of the crowd gauge.
