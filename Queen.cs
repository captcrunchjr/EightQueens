using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EightQueens
{
    public class Queen
    {
        int CurRow;
        int CurColumn;

        public Queen(int col, int dimension){
            CurColumn = col;
            CurRow = GenerateStartingRow(dimension);
        }

        int GenerateStartingRow(int dimension){
            var random = new Random();
            int row = random.Next(0,dimension);

            return row;
        }

        public int GetRow(){
            return CurRow;
        }

        public void setRow(int val){
            CurRow = val;
        }

        public int GetColumn(){
            return CurColumn;
        }

        public string ToString(){
            return "Column = " + CurColumn + " Row: " + CurRow;
        }

    }
}