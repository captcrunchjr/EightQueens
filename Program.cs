using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimension = 8;
            int userIn;
            string in2;
            bool repeat = true;
            bool userInputEntered = false;
            int currentConflicts = 0;
            bool isGoalState = false;
            int totalRestarts = 0;
            int totalStateChanges = 0;
            
            do
            {
                Console.WriteLine("Eight Queens");
                Console.WriteLine("------------");

                do{
                    Console.WriteLine("Please enter your desired board size as a number: ");
                    try{
                        userIn = Int32.Parse(Console.ReadLine());
                        userInputEntered = true;
                        dimension = userIn;
                    }catch(Exception){
                        Console.WriteLine("Something went wrong.");
                        userInputEntered = false;
                    }
                }while(!userInputEntered);

                var timer = System.Diagnostics.Stopwatch.StartNew();
                Board board = new(dimension);
                Queen[] queens= new Queen[dimension];
                AI aI = new AI();

                //generate Queens
                for(int i = 0; i < queens.Length; i++){
                    queens[i]=new Queen(i,dimension);
                }

                //debug for Queen Generation
                /*foreach (Queen queen in queens){
                    Console.WriteLine(queen.ToString());
                }*/

                //fill the board with queen placement
                do{
                    board.fillBoard(queens);
                    Console.WriteLine("Current State");
                    board.drawBoard();
                    currentConflicts = aI.CheckConflicts(board, queens);
                    Console.WriteLine("Conflicts = " + currentConflicts);
                    if(currentConflicts == 0){
                        isGoalState = true;
                        timer.Stop();
                        TimeSpan ts = timer.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                        Console.WriteLine("Winner!");
                        Console.WriteLine("State Changes: " + totalStateChanges);
                        Console.WriteLine("Restarts: " + totalRestarts);
                        Console.WriteLine("Time: " + elapsedTime);
                        break;
                    }
                    int[] nextMove = aI.FindBestMove(board, queens, currentConflicts);
                    //returns as [0] moves found, [1] queen to move, [2] row to move to, [3] resulting new conflicts
                    if(nextMove[0] == -1){
                        totalRestarts++;
                        aI.randomRestart(queens, dimension);
                        Console.WriteLine("No better moves, restarting...");
                    }
                    else{
                        Console.WriteLine("Moves found: " + nextMove[0]);
                        totalStateChanges++;
                        queens[nextMove[1]].setRow(nextMove[2]);
                        currentConflicts = nextMove[3];
                        Console.WriteLine("Setting new current state");
                    }
                }while(!isGoalState);
                
                Console.WriteLine("Would you like to go again? Y/N");
                in2 = Console.ReadLine();
                if(in2 == "Y" || in2 == "y"){
                    repeat = true;
                    isGoalState = false;
                    userInputEntered = false;
                    totalRestarts = 0;
                    totalStateChanges = 0;

                }
                else if(in2 == "N" || in2 == "n"){
                    repeat = false;
                }
                else{
                    Console.WriteLine("Invalid Input. Closing.");
                    repeat = false;
                }

            }while(repeat);
        }
    }
}


