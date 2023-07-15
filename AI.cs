using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EightQueens
{
    public class AI
    {
        int h = 0;
        int lowerHMovesCount = 0;
        public AI(){

        }
        public int CheckConflicts(Board board, Queen[] queens){
            int conflicts = 0;
            conflicts += CheckRowConflicts(queens);
            conflicts += CheckDiagonalConflicts(queens);
            return conflicts;
            /*List<Task<int>> tasks = new List<Task<int>>();
            var t1 = Task<int>.Run(() => { return CheckRowConflicts(queens);});
            var t2 = Task<int>.Run(()=> { return CheckDiagonalConflicts(queens);});
            tasks.Add(t1);
            tasks.Add(t2);
            Task.WaitAll(tasks.ToArray());
            return tasks.Sum((t) => t.Result);*/
        }

        public int CheckRowConflicts(Queen[] queens){
            int rowConflicts = 0;
            for(int i = 0; i < queens.Length-1; i++){
                for(int j = i+1; j < queens.Length; j++){
                    if (queens[i].GetRow() == queens[j].GetRow()){
                        rowConflicts++;
                    }
                }
            }
            return rowConflicts;
        }
        public int CheckDiagonalConflicts(Queen[] queens){
            int diagonalConflicts = 0;
            for(int i = 0; i < queens.Length-1; i++){
                for(int j = i+1; j < queens.Length; j++){
                    int dif = j - i;
                    if ((queens[j].GetRow() == queens[i].GetRow()+dif) || (queens[j].GetRow() == queens[i].GetRow() - dif)){
                        diagonalConflicts++;
                    }
                }
            }
            return diagonalConflicts;
        }

        public Queen[] randomRestart(Queen[] queens, int dimension){
            foreach(Queen queen in queens){
                var random = new Random();
                queen.setRow(random.Next(0, dimension));
            }
            return queens;
        }

        public int[] FindBestMove(Board board, Queen[] queens, int StartConflicts){
            int[] result = new int[4];
            int bestQueen = -1;
            int bestRow = -1;
            int newConflicts = StartConflicts;
            //receive board and queens, iterate over all queens over all positions to find the best option
            //return count of better moves, choose best move, queen column/row position of best move

            for(int i = 0; i < queens.Length; i++){
                int currentRow = queens[i].GetRow();
                for(int j = 0; j < queens.Length; j++){
                    queens[i].setRow(j);
                    int tempConflicts = CheckConflicts(board, queens);
                    if(tempConflicts < StartConflicts){
                        lowerHMovesCount++;
                        if(tempConflicts < newConflicts){
                           bestQueen = i;
                            bestRow = j;
                            newConflicts = tempConflicts;
                        }
                    }
                }
                queens[i].setRow(currentRow);
            }
            if(bestQueen == -1 || bestRow == -1){
                result[0] = -1;
                //Console.WriteLine("No Better Option Found, This Is Where You Should Restart");
                return result;
            }

            result[0] = lowerHMovesCount;
            result[1] = bestQueen;
            result[2] = bestRow;
            result[3] = newConflicts;
            lowerHMovesCount = 0;
            //Console.WriteLine("Better solution found with " + newConflicts + " conflicts using Queen " + bestQueen + " assigned to row " + result[2] + " after finding " + result[0] + " better moves.");
            //returns choices, Queen (column), row to move to, newConflicts
            return result;
        }
    }
}