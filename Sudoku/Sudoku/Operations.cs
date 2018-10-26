using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Operations
    {
        public struct Coordinate
        {
            public int x;
            public int y;
        } 

        /* METHODS */

        /* Checks the current row for a number */
        public static bool CheckRow(int row, int column, int number, Cell[,] grid)
        {
            /* Checks each row for the number in question */
            for (int columnIndex = 0; columnIndex < 9; columnIndex++)
            {
                if (columnIndex == column)
                    continue;
                else if (grid[row, columnIndex].getNumber() == number)
                    return false;
            }
            return true;
        }

        /* Checks the current row for a number */
        public static bool CheckColumn(int row, int column, int number, Cell[,] grid)
        {
            /* Checks each row for the number in question */
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                if (rowIndex == row)
                    continue;
                else if (grid[rowIndex, column].getNumber() == number)
                    return false;
            }
            return true;
        }

        /* Checks the current block for a number */
        public static bool CheckBlock(int row, int column, int number, Cell[,] grid)
        {
            int[] block1 = new int[] { 0, 1, 2 };
            int[] block2 = new int[] { 3, 4, 5 };
            int[] block3 = new int[] { 6, 7, 8 };

            int[] blockRow = new int[] { 0, 0, 0 };
            int[] blockColumn = new int[] { 0, 0, 0 };

            /* Checks to see what block the current cell is */
            if (0 <= row && row < 3)
                blockRow = block1;
            if (3 <= row && row < 6)
                blockRow = block2;
            if (6 <= row && row < 9)
                blockRow = block3;

            if (0 <= column && column < 3)
                blockColumn = block1;
            if (3 <= column && column < 6)
                blockColumn = block2;
            if (6 <= column && column < 9)
                blockColumn = block3;

            // I know what block its in with blockRow and blockColumn
            // Depending on which block, i have to check different grid cells. Work this out.

            foreach (int rowIndex in blockRow)
            {
                foreach (int columnIndex in blockColumn)
                {
                    if (rowIndex != row && columnIndex != column)
                        if (grid[rowIndex, columnIndex].getNumber() == number)
                            return false;
                }
            }

            return true;
        }

        /* Updates cells around a newly solved cell */
        public static void updatePossibleNumbers(int row, int column, int solvedNumber, Cell[,] grid)
        {
            /* Checks each row */
            for (int columnIndex = 0; columnIndex < 9; columnIndex++)
            {
                if (!grid[row, columnIndex].isFixed())
                {
                    if (grid[row, columnIndex].getPossibleNumbers().Contains(solvedNumber))
                    {
                        grid[row, columnIndex].removePossibleNumber(solvedNumber);
                    }
                }
            }

            /* Checks each column */
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                if (!grid[rowIndex, column].isFixed())
                {
                    if (grid[rowIndex, column].getPossibleNumbers().Contains(solvedNumber))
                    {
                        grid[rowIndex, column].removePossibleNumber(solvedNumber);
                    }
                }
            }

            /* Cells needed to check for current block */
            int[] block1 = new int[] { 0, 1, 2 };
            int[] block2 = new int[] { 3, 4, 5 };
            int[] block3 = new int[] { 6, 7, 8 };

            int[] blockRow = new int[] { 0, 0, 0 };
            int[] blockColumn = new int[] { 0, 0, 0 };

            /* Checks to see what block the current cell is */
            if (0 <= row && row < 3)
                blockRow = block1;
            if (3 <= row && row < 6)
                blockRow = block2;
            if (6 <= row && row < 9)
                blockRow = block3;

            if (0 <= column && column < 3)
                blockColumn = block1;
            if (3 <= column && column < 6)
                blockColumn = block2;
            if (6 <= column && column < 9)
                blockColumn = block3;

            foreach (int rowIndex in blockRow)
            {
                foreach (int columnIndex in blockColumn)
                {
                    if (!grid[rowIndex, columnIndex].isFixed())
                    {
                        if (grid[rowIndex, columnIndex].getPossibleNumbers().Contains(solvedNumber))
                        {
                            grid[rowIndex, columnIndex].removePossibleNumber(solvedNumber);
                        }
                    }
                }
            }
        }
       
        /* Fill grid with possible values */
        public static void fillPossibleValues(Cell[,] gameGrid)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    /* If the current grid is not a fixed value */
                    if (!gameGrid[row, column].isFixed())
                    {
                        List<int> possibleNumbers = new List<int>();
                        /* FOR EACH CELL */
                        /* Check to see what numbers are legal in the current cell */
                        for (int i = 1; i < 10; i++)
                        {
                            bool possible = true;
                            /* Checks row */
                            possible = Operations.CheckRow(row, column, i, gameGrid);

                            if (possible)
                            {
                                /* Checks column */
                                possible = Operations.CheckColumn(row, column, i, gameGrid);

                                /* Checks block */
                                if (possible)
                                    possible = Operations.CheckBlock(row, column, i, gameGrid);
                            }
                            if (possible)
                                possibleNumbers.Add(i);
                        }
                        gameGrid[row, column].addPossibleNumber(possibleNumbers);
                    }
                }
            }
        }

        /* Checks the grid for naked values and solves them cells */
        public static bool checkforNaked(Cell[,] gameGrid)
        {
            bool noNaked = true;
            /* Check naked values */
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    // Check if the current cell is a fixed cell AND the current cell only has one possible value AND the current cell is not already solved
                    if ((!gameGrid[row, column].isFixed()) && (gameGrid[row, column].getPossibleNumbers().Count() == 1) && gameGrid[row,column].getNumber()==0)
                    {
                        int solvedNumber = gameGrid[row, column].getOnlyNumber();
                        gameGrid[row, column].setNumber(solvedNumber);
                        updatePossibleNumbers(row, column, solvedNumber, gameGrid);
                        noNaked = false;
                        Console.WriteLine("--NAKED VALUE " + solvedNumber + " at (" + column + "," + row);
                    }
                }
            }
            return noNaked;
        }

        // Returns true if a value is found
        public static bool solveRow (Cell[,] gameGrid)
        {
            bool result = false;
            // Implement it checking for solved values, and not check their number in the row and see if its quicker?
            // Check rows for single possible values 
            for (int row = 0; row < 9; row++)
            {
                // Checks each number 
                for (int index = 1; index < 10; index++)
                {
                    int numberInstance = 0;
                    int columnIndex = 10;
                    // Check each column 
                    for (int column = 0; column < 9; column++)
                    {
                        if (gameGrid[row, column].getNumber() != 0)
                            continue;
                        // If the current cell is not solved 
                        else if (gameGrid[row, column].getNumber() == 0)
                        {
                            if (gameGrid[row, column].getPossibleNumbers().Contains(index))
                            {
                                // if the same number is found more than once break out 
                                if (numberInstance == 1)
                                {
                                    numberInstance++;
                                    break;
                                }
                                else
                                {
                                    numberInstance++;
                                    columnIndex = column;
                                }

                            }
                        }
                    }
                    if (numberInstance == 1)
                    {
                        result = true;
                        gameGrid[row, columnIndex].setNumber(index);
                        updatePossibleNumbers(row, columnIndex, index, gameGrid);
                        Console.WriteLine("--ROW VALUE " + index + " at (" + columnIndex + "," + row);
                    }
                }
            }
            return result;
        }

        // Returns true if a value is found
        public static bool solveColumn (Cell[,] gameGrid)
        {
            bool result = false;
            // Implement it checking for solved values, and not check their number in the row and see if its quicker?
            // Check rows for single possible values 
            for (int column = 0; column < 9; column++)
            {
                // Checks each number 
                for (int index = 1; index < 10; index++)
                {
                    int numberInstance = 0;
                    int rowIndex = 10;
                    // Check each column 
                    for (int row = 0; row < 9; row++)
                    {
                        if (gameGrid[row, column].getNumber() != 0)
                            continue;
                        // If the current cell is not solved 
                        else if (gameGrid[row, column].getNumber() == 0)
                        {
                            if (gameGrid[row, column].getPossibleNumbers().Contains(index))
                            {
                                // If the same number is found more than once break out 
                                if (numberInstance == 1)
                                {
                                    numberInstance++;
                                    break;
                                }
                                else
                                {
                                    numberInstance++;
                                    rowIndex = row;
                                }
                            }
                        }
                    }
                    if (numberInstance == 1)
                    {
                        result = true;
                        gameGrid[rowIndex, column].setNumber(index);
                        updatePossibleNumbers(rowIndex, column, index, gameGrid);
                        Console.WriteLine("--COLUMN VALUE " + index + " at (" + rowIndex + "," + column);
                    }
                }

            }
            return result;
        }

        // Returns true if a value is found
        public static bool solveBlock (Cell[,] gameGrid)
        {
            bool result = false;
            // EACH NUMBER
            for (int index = 1; index <10; index++)
            {
                int numberInstance = 0;
                int rowIndex = 10;
                int columnIndex = 10;
                bool indexFound = false;
                // EACH ROW
                for (int row = 0; row<3; row++)
                {
                    // IF COLUMN HAS BEEN BROKEN OUT OF AND THE CURRENT NUMBER IS SOLVED
                    if (!indexFound)
                    {
                        // EACH COLUMN
                        for (int column = 0; column < 3; column++)
                        {
                            if (gameGrid[row, column].isFixed() || gameGrid[row, column].getNumber() != 0)
                                continue;
                            else if (gameGrid[row, column].getPossibleNumbers().Contains(index))
                            {
                                if (numberInstance == 1)
                                {
                                    indexFound = true;
                                    numberInstance++;
                                    break;
                                }
                                else
                                {
                                    numberInstance++;
                                    rowIndex = row;
                                    columnIndex = column;
                                }
                            }
                        }
                    }
                }
                if (numberInstance == 1)
                {
                    result = true;
                    gameGrid[rowIndex, columnIndex].setNumber(index);
                    updatePossibleNumbers(rowIndex, columnIndex, index, gameGrid);
                    Console.WriteLine("--BLOCK SOLVE " + index + " at (" + rowIndex + "," + columnIndex + ")");
                }
            }
            return result;
        }

        public static bool reducePossibleNumbers (Cell[,] gameGrid)
        {
            bool successful = false;    

            // Check for possible numbers that exist in a single row or column of a block.
            // For each number
            for (int index = 1; index < 10; index++)
            {
                // For each block
                if (rpnSingleRowOrColumn(gameGrid, index, 0, 0))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 0, 3))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 0, 6))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 3, 0))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 3, 3))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 3, 6))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 6, 0))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 6, 3))
                    successful = true;
                if (rpnSingleRowOrColumn(gameGrid, index, 6, 6))
                    successful = true;
            }
            if (successful)
            {
                Console.WriteLine("Reduced possible numbers. By scenario 1");
                return successful;
            }
            return successful;
        }

        private static bool rpnSingleRowOrColumn(Cell[,] gameGrid, int index, int row, int column)
        {
            bool successful = false;

            // Gathers a list of coordinates of cells, that contain the current index as a possible number
            // Later this list is checked. If in a block a possible number only exists in a single row or column, it must exist there and can be taken throughout the other blocks.
            bool indexFound = false;
            bool hit = false;
            List<Coordinate> coords = new List<Coordinate>();
            for (int rowIndex = row; rowIndex < row+3; rowIndex++)
            {
                // IF COLUMN HAS BEEN BROKEN OUT OF AND THE CURRENT NUMBER IS SOLVED
                if (!indexFound)
                {
                    // EACH COLUMN
                    for (int columnIndex = column; columnIndex < column+3; columnIndex++)
                    {
                        // If Solved
                        if (gameGrid[rowIndex, columnIndex].isFixed() || gameGrid[rowIndex, columnIndex].getNumber() != 0)
                            continue;
                        else if (gameGrid[rowIndex, columnIndex].getNumber() == index)
                        {
                            indexFound = true;
                            break;
                        }
                        else if (gameGrid[rowIndex, columnIndex].getPossibleNumbers().Contains(index))
                        {
                            Coordinate currentCoord;
                            currentCoord.x = columnIndex;
                            currentCoord.y = rowIndex;
                            coords.Add(currentCoord);
                            hit = true;
                        }
                    }
                }
                else break;
            }

            bool xMatch = true;
            bool yMatch = true;
            // Set x & y to first coord in list.
            // If all x's do not equal the same, we can not presume the current index can only exist in that column etc.
            if (hit)
            {
                int x = coords[0].x;
                int y = coords[0].y;

                // Check each x & y equal first
                foreach (var coord in coords)
                {
                    if (coord.x != x)
                        xMatch = false;

                    if (coord.y != y)
                        yMatch = false;
                }
                
                if (xMatch)
                {
                    // Checks each value in a column
                    for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                    {
                        if (!gameGrid[rowIndex, x].isFixed())
                        {
                            if (rowIndex != row && rowIndex != row+1 && rowIndex != row+2)
                            {
                                if (gameGrid[rowIndex, x].getPossibleNumbers().Contains(index))
                                {
                                    gameGrid[rowIndex, x].removePossibleNumber(index);
                                    successful = true;
                                }
                            }
                        }
                    }
                }

                if (yMatch)
                {
                    // Checks each value in a row
                    for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                    {
                        if (!gameGrid[y, columnIndex].isFixed())
                        {
                            if (columnIndex != column && columnIndex != column+1 && columnIndex != column+2)
                            {
                                if (gameGrid[y, columnIndex].getPossibleNumbers().Contains(index))
                                {
                                    gameGrid[y, columnIndex].removePossibleNumber(index);
                                    successful = true;
                                }
                            }
                        }
                    }
                }
            }
            return successful;
        }
    }
}
