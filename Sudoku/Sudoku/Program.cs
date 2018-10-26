using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Program
    {
        public static Cell[,] gameGrid = new Cell[9, 9];
        static void Main(string[] args)
        {
            bool unSolved = true;

            //BRUTE FORCE NEEDED
            // int[,] grid = new int[,] {{ 1, 4, 0, 5, 0, 6, 3, 0, 0 },{ 3, 0, 0, 0, 0, 0, 0, 8, 0 },{ 9, 8, 2, 4, 1, 3, 0, 0, 0 },{ 0, 0, 0, 8, 0, 0, 0, 0, 9 },{ 0, 7, 6, 3, 0, 0, 1, 2, 0 },{ 8, 0, 0, 0, 0, 1, 0, 0, 0 },{ 0, 0, 0, 2, 3, 7, 8, 1, 5 },{ 0, 0, 0, 0, 0, 0, 0, 0, 6 },{ 0, 0, 8, 6, 0, 5, 0, 3, 4 },  };

            // Solved with only Naked and RowCheck
            //int[,] grid = new int[,] {{ 0, 0, 0, 0, 0, 0, 4, 0, 8 },{ 0, 6, 4, 3, 0, 8, 0, 0, 0 },{ 8, 0, 5, 0, 0, 0, 0, 2, 0 }, { 0, 3, 9, 0, 4, 0, 0, 0, 7 }, { 0, 5, 0, 2, 8, 3, 0, 1, 0 },  { 2, 0, 0, 0, 1, 0, 6, 3, 0 }, { 0, 7, 0, 0, 0, 0, 8, 0, 2 }, { 0, 0, 0, 4, 0, 1, 5, 9, 0 }, { 4, 0, 6, 0, 0, 0, 0, 0, 0 },  }; 

            // EASY PUZZLE (Just NAKED values)
            //int[,] grid = new int[,] { { 0, 0, 2, 8, 0, 0, 0, 0, 0 },{ 3, 4, 0, 5, 0, 6, 0, 0, 1 },{ 0, 0, 0, 0, 2, 3, 8, 6, 0 },{ 0, 0, 0, 0, 5, 9, 0, 0, 3 },{ 2, 9, 1, 0, 0, 0, 4, 8, 5 }, { 6, 0, 0, 4, 8, 0, 0, 0, 0 },{ 0, 1, 3, 9, 6, 0, 0, 0, 0 },{ 8, 0, 0, 3, 0, 5, 0, 9, 6 },{ 0, 0, 0, 0, 0, 1, 5, 0, 0 },  };

            // MEDIUM PUZZLE (NAKED and Just column)
            //int[,] grid = new int[,] {{ 0, 4, 0, 0, 0, 1, 0, 0, 0 },{ 1, 2, 3, 0, 7, 0, 0, 6, 0 },{ 0, 0, 9, 0, 0, 2, 0, 0, 0 }, { 6, 0, 0, 1, 2, 0, 0, 0, 9 },{ 0, 0, 1, 0, 0, 0, 2, 0, 0 },{ 5, 0, 0, 0, 9, 6, 0, 0, 4 }, { 0, 0, 0, 4, 0, 0, 1, 0, 0 }, { 0, 5, 0, 0, 1, 0, 9, 8, 3 },{ 0, 0, 0, 8, 0, 0, 0, 4, 0 },  };

            // HARD PUZZLE (Wasnt hard, NAKED)
            //int[,] grid = new int[,] {{ 2, 0, 0, 4, 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 3, 8, 0, 0, 0 },{ 9, 0, 8, 0, 0, 0, 0, 3, 0 },{ 0, 0, 9, 2, 0, 0, 6, 7, 0 },{ 0, 0, 0, 3, 0, 9, 0, 0, 0 },{ 0, 7, 1, 0, 0, 4, 9, 0, 0 },{ 0, 8, 0, 0, 0, 0, 4, 0, 6 },{ 0, 0, 0, 7, 1, 0, 0, 0, 0 },{ 0, 0, 3, 0, 0, 5, 0, 0, 2 },  };

            int[,] grid = new int[,] {
            { 0, 0, 0, 0, 0, 0, 2, 5, 3 },
            { 0, 0, 0, 5, 2, 0, 4, 0, 0 },
            { 4, 0, 0, 0, 0, 1, 0, 0, 0 },

            { 9, 0, 0, 4, 0, 0, 0, 6, 0 },
            { 0, 0, 0, 2, 6, 8, 0, 0, 0 },
            { 0, 7, 0, 0, 0, 5, 0, 0, 8 },

            { 0, 0, 0, 8, 0, 0, 0, 0, 5 },
            { 0, 0, 4, 0, 3, 6, 0, 0, 0 },
            { 6, 8, 7, 0, 0, 0, 0, 0, 0 },  };


            // Grid used to test if BLOCK is needed
            /*
            int[,] grid = new int[,] {
            { 1, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 4, 5, 6, 0, 0, 0, 0, 0, 0 },

            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 8, 0, 0, 0, 0, 0, 0 },

            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 8, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },  };*/




            /* Creates gameGrid from 2d array */
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    Cell newCell;
                    /* If the cell is not 0 then it will be created as a fixed grid cell */
                    if (grid[row, column] != 0) { newCell = new Cell(grid[row, column], true); }

                    /* Else it will be added but not fixed */
                    else { newCell = new Cell(grid[row, column], false); }

                    gameGrid[row, column] = newCell;
                }
            }

            // Fill possible numbers 
            Operations.fillPossibleValues(gameGrid);



            while (unSolved)
            {
                printGrid();
                Console.WriteLine();

                // Check for naked values in the grid. 
                // Returns TRUE when no more naked values are found
                bool noNaked = Operations.checkforNaked(gameGrid);
                bool noResult = false;

                if (noNaked)
                {
                    // If every method returns false, no more results can be found using these methods.
                    // Check all rows
                    if (!Operations.solveRow(gameGrid))
                    {
                        // Check all columns
                        if (!Operations.solveColumn(gameGrid))
                        {
                            // Check all blocks
                            //if (!Operations.solveBlock(gameGrid))

                            // Reduce possible numbers on board
                            if(!Operations.reducePossibleNumbers(gameGrid))
                                noResult = true;
                        }
                    }
                }
            }
            Console.ReadKey();
        }




        public static void printGrid()
        {
            Console.WriteLine("**************************************************\r\n");
            for (int row = 0; row < 9; row++)
            {
                Console.Write("    ");
                for (int column = 0; column < 9; column++)
                {
                    Console.Write(gameGrid[row, column].getNumber() + " ");
                    if (column == 2)
                    { Console.Write("| "); }
                    if (column == 5)
                    { Console.Write("| "); }
                }
                if (row == 2)
                { Console.WriteLine(); Console.WriteLine("    ---------------------"); }
                else if (row == 5)
                { Console.WriteLine(); Console.WriteLine("    ---------------------"); }
                else Console.WriteLine();
            }
        }
    }
}
