Battleship

To play this game you will need at least 2 configuration files
thay must be located in the next folder and it must be named
as player1.txt and player2.txt

 <your route>\BattleShipSimulator\bin\Debug\netcoreapp3.1

Configuration File

The configuration file has to have an specific format as shown below:

	x,y
	n	
	x,y,H or O,Size
where each line stands for:
	x,y = dimensions of the board. Both players must have the same board sizes.
	n = number of ships. Both players must have the number of ships.
	x,y,H Or O, size = initial X,Y location of ship #n, horizontal o vertical, how many squares does the ship must occupy 
		NOTE: if horizontal is choosed, the ship will be placed at x,y location and will occupay "size" squares from left to right. 
		             Same rules apply with vertical except the ship will be positioned from top to bottom. If 5 is the ship's number then
		            there must be 5 lines with every single ship configuration.
Board:

"!" = segment of a ship
"*" = location succesfully attacked

At the end of every round the players are given the option to continue playing until one of them gets destroyed or quit the game.
	 