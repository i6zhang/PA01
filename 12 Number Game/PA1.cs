using System;
using static System.Console;

namespace Bme121
{
    class Program
    {

        static void Main( )
        {
            Random rGen = new Random( );

            // Try to figure out which form of the board to display.
            // Non-Windows machines seem to not have the box-drawing characters.

            bool useU0000 = false;
            if( Environment.OSVersion.Platform == PlatformID.MacOSX ) useU0000 = true;
            if( Environment.OSVersion.Platform == PlatformID.Unix ) useU0000 = true;

            // Initialize the game board in the solved puzzle state.
            // The zero value represents a hole.

            int[ , ] board =
            {
                {  0,  1,  2,  0 },
                {  3,  4,  5,  6 },
                {  7,  8,  9, 10 },
                {  0, 11, 12,  0 }
            };

            // Dimensions of the game board are extracted into variables for convenience.

            int rows = board.GetLength( 0 );
            int cols = board.GetLength( 1 );
            int length = board.Length;

            // This is the main game-playing loop.
            // Each iteration is either performing one random move (as part of scrambling)
            // or one move entered by the user.

            bool quit = false;
            int randomMoves = 0;
            while( ! quit )
            {
                int move = 0;

                // Either generate a random move or display the game board and ask the user for a move.

                if( randomMoves > 0 )
                {
                    move = rGen.Next( 1, 13 );

                    randomMoves --;
                }
                else
                {
                    // Extract the game-board values into an array of displayed game-board strings.
                    // This is done so the strings can be of width 3 which makes the game-board
                    // display code below express very clearly.

                    string[ ] map = new string[ length ];
                    for( int i = 0; i < length; i ++ )
                    {
                        int value = board[ i / cols, i % cols ];
                        if( value == 0 ) map[ i ] = "   ";
                        else map[ i ] = $" {value:x} ";
                    }

                    // Display the game board.

                    Clear( );
                    WriteLine( );
                    WriteLine( " Welcome to the double-play game!" );
                    WriteLine( " Tiles slide in pairs by pushing towards a hole." );
                    WriteLine( " Scramble, then arrange back in order by sliding." );
                    WriteLine( );

                    if( useU0000 )
                    {
                        // Use Unicode 'C0 Controls and Basic Latin' range 0000–007f.

                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  0 ], map[  1 ], map[  2 ], map[  3 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  4 ], map[  5 ], map[  6 ], map[  7 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[  8 ], map[  9 ], map[ 10 ], map[ 11 ] );
                        WriteLine( " +---+---+---+---+" );
                        WriteLine( " |{0}|{1}|{2}|{3}|", map[ 12 ], map[ 13 ], map[ 14 ], map[ 15 ] );
                        WriteLine( " +---+---+---+---+" );
                    }
                    else
                    {
                        // Use Unicode 'Box Drawing' range 2500–257f.

                        WriteLine( " ╔═══╦═══╦═══╦═══╗" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  0 ], map[  1 ], map[  2 ], map[  3 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  4 ], map[  5 ], map[  6 ], map[  7 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[  8 ], map[  9 ], map[ 10 ], map[ 11 ] );
                        WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                        WriteLine( " ║{0}║{1}║{2}║{3}║", map[ 12 ], map[ 13 ], map[ 14 ], map[ 15 ] );
                        WriteLine( " ╚═══╩═══╩═══╩═══╝" );
                    }
                    WriteLine( );

                    // Interpret the user's desired move.

                    Write( " Tile to push (s to scramble, q to quit): " );
                    string response = ReadKey( intercept: true ).KeyChar.ToString( );
                    WriteLine( );

                    switch( response )
                    {
                        case "s": randomMoves = 100_000; break;

                        case "1": move =  1; break;
                        case "2": move =  2; break;
                        case "3": move =  3; break;
                        case "4": move =  4; break;
                        case "5": move =  5; break;
                        case "6": move =  6; break;
                        case "7": move =  7; break;
                        case "8": move =  8; break;
                        case "9": move =  9; break;
                        case "a": move = 10; break;
                        case "b": move = 11; break;
                        case "c": move = 12; break;

                        case "q": quit = true; break;
                    }
                }

                // If a move is possible, adjust the game board to make the move.

                if( move > 0 )
                {
                    // TO DO: Update the game board to show the effect of the move
                    // (Find the pushed tile, determine what direction it can move, make the move).
					
					//initialize variables 
                    int column = 0; 
                    int row = 0; 
                    bool up = true;
                    bool down = true; 
                    bool right = true; 
                    bool left = true; 
					
					//check which row and column the input is currently in
                    for (int i = 0; i < 4; i++) {
						for (int j = 0; j < 4; j++) {
							if (board [ i ,j ] == move){
								row = i;
								column = j;
							}	
						}	
					}
					
					//check if the move is out of bounds or not 
					if (row == 0 || row == 1){
						up = false;
					}	
					
					if (row == 2 || row == 3){
						down = false;
					}	
					
					if (column == 0 || column == 1){
						left = false;
					}
					
					if (column == 2 || column == 3){ 
						right = false; 
					}	
					
					//check if the move is valid, move the piece 
					
					if (up){
						if ( board [row - 1, column] != 0 && board [row - 2, column] == 0 ){ 
							board [row - 2, column] = board [row - 1, column]; 
							board [row - 1, column] = board [row, column]; 
							board [row, column] = 0; 
						}
					}			
							
					if (down){
						if ( board [row + 1, column] != 0 && board [row + 2, column] == 0 ){ 
							board [row + 2, column] = board [row + 1, column]; 
							board [row + 1, column] = board [row, column]; 
							board [row, column] = 0;
						}
					}		
					
					if (right){
						if ( board [row, column + 1] != 0 && board [row, column + 2] == 0 ){ 
							board [row, column + 2] = board [row, column + 1]; 
							board [row, column +1] = board [row, column]; 
							board [row, column] = 0;
						}
                    }
					
					if (left){
						if ( board [row, column - 1] != 0 && board [row, column - 2] == 0 ){ 
							board [row, column - 2] = board [row, column - 1]; 
							board [row, column - 1] = board [row, column]; 
							board [row, column] = 0;	
						}
					}					
                    
                }
            }

            WriteLine( " Thanks for playing!" );
            WriteLine( );
        }
    }
}
