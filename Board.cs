using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EightQueens
{
    public class Board
    {
        int dimension;
        string[,] boardSpace;

        public Board(int dimension){
            this.dimension = dimension;
            boardSpace = new string[dimension, dimension];
        }

        public string[,] getBoardSpace(){
            return boardSpace;
        }

        public void fillBoard(Queen[] queens){
            for (int i = 0; i < boardSpace.GetLength(0); i++){
                for(int j = 0; j < boardSpace.GetLength(1); j++){
                    if(queens[i].GetRow() == j){
                        boardSpace[i,j] = "1|";
                    }
                    else{
                        boardSpace[i,j] = "0|";
                    }
                }
            }
        }

        public void drawBoard(){
            for (int i = 0; i < boardSpace.GetLength(0); i++){
                Console.Write("|");
                for(int j = 0; j < boardSpace.GetLength(1); j++){
                    Console.Write(boardSpace[j,i]);
                }
                Console.WriteLine();
                //Console.Write(" - - - - - - - - ");
                //Console.WriteLine();
            }
        }
    }
}