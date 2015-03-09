# PiDarts

   PiDarts is a small project I decided to start after attending a sports bar and seeing one of those fancy dartboards in the corner. I was really impressed, so I looked up how much one would cost to buy for my home. Well, it turns out these dartboards sell for [thousands of dollars](http://www.sears.com/game-room-guys-arachnid-galaxy-3-electronic-home/p-SPM10116485920?hlSellerId=4023&sid=IDx20110310x00001i&kpid=SPM10116485920&kispla=SPM10116485920&kpid=SPM10116485920&mktRedirect=y). The software behind these boards can't be too difficult to build, especially if I just want something to track my stats & home league games. So this project is a part of my effort to build a fully open source implementation of stat and league tracking software. My goals for this project are as follows:

  * Learn more about microcontrollers.
  * Learn more about serial communication between the Raspberry Pi & the Arduino.
  * Create stat tracking software for my home dartboard. Eventually play a statistically accurate game against myself :)
  * Create league tracking software to record games against friends.

## Hardware

   The hardware works by interfacing the dartboard with an Arduino. A dartboard has exactly 62 different sections: 1-20 (single,double,triple) & bullseye (single,double). This means we need to be able to represent at least 62 unique values. The way a dartboard detects where a dart has landed is pretty simple. I will upload pictures later, but basically there are two thin peices of plastic with one on top of the other that reside behind the dartboard field. These two pieces of plastic each have a series contact points that line up. When a dart hits the board, the section it hit presses down on the two pieces of plastic which pinches the contacts and closes a circuit. By reading which pin sent the signal, and which pin received the signal, you can determine which section was hit. For this particulat dartboard, one piece of plastic has 7 input lines and the other has 10 input lines. This allows us to make a 7x10 matrix, giving us a total of 70 unique sections that can be represented.


## Software

   The solution uses MonoGame to control the game flow. The solution contains four projects:

1. PiDarts Portable Library
  * Represents the logic to play darts. It is meant to be fully portable. Eventually I plan to make an iPad application that connects to the dartbard via bluetooth.
2. PiDarts Tests
  * Testing for the portable library.
3. PiDarts UI for Windows (WPF)
  * Windows UI for viewing live scores during a dart game.
4. PiDartsContent
  * Store resources for game.

## The plan

   The plan for devlopment is as follows:

1. Build and test the logic for a basic 301 game. (PiDarts Portable)
2. Build Cricket. Refactor where necessary to ensure I have the proper abstractions.
3. Build a player-tracking system.
4. Build a stat-tracking system.
5. Build a league-tracking system.
 
