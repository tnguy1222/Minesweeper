using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*Vien Nguyen, Adam Bender
CST-247 Enterprise Application Programming 3
02/09/2021
Define the board for the game.
This is our work.
*/

namespace CST247CLC.Models
{
    public class GameBoard
    {
        //Create ramdom number from 1 to 100. it the generated number is less than the game level,
        //The button will contains a bomb.
        public Random rd = new Random();

        //List all the board's properties
        public int size { get; set; }

        public int gameLevel { get; set; }

        public List<ButtonModel> buttons { get; set; }

        public ButtonModel[,] grid { get; set; }

        public GameBoard(int size)
        {
            //Initiate the gamboard properties
            this.size = size;
            //Assign the difficult level of the game, hard code the number for now.
            this.gameLevel = 10;
            //Initiate the list buttons on the game board and assign the bomb button.
            buttons = new List<ButtonModel>();

            grid = new ButtonModel[size, size];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new ButtonModel();
                    grid[i, j].id = index;
                    grid[i, j].live = IsBomb(gameLevel);
                    grid[i, j].row = i;
                    grid[i, j].column = j;
                    index++;
                }
            }
            //Set the bomb button
            SetBombNeighborsToButtons();
            //Initiate the button list buttons 
            for (int i = 0; i < Math.Pow(size, 2); i++)
            {
                ButtonModel btn = new ButtonModel();
                buttons.Add(btn);
            }
            //Copy items from 2D array into the button list buttons
            int idx = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    buttons[idx] = grid[i, j];
                    idx++;
                }
            }
        }

        //Determines if the ramdom number is less than or equals the game level, that button is a bomb.
        public bool IsBomb(int gameLevel)
        {
            int number = rd.Next(0, 100);
            return number <= gameLevel;
        }

        //Calculate the live neighbor function. it will look for the neighbors around it and count them if they are bombs. 
        //Set bomb neighbor to buttons
        public void SetBombNeighborsToButtons()
        {
            //Copy the bombs neighor from 2D array back the buttons list.
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j].neighbors = CalculateLiveNeighbors(i, j);
                }
            }
        }
        public int CalculateLiveNeighbors(int row, int column)
        {
            //Live neighes of a cell are the neighbor one cell over the left, right, above and below.
            int selfCellLive = 0;
            //the the cell is a live cell, it also be counted as a neighbor
            if (grid[row, column].live == true)
                selfCellLive = selfCellLive + 1;
            //sum up all the neighbors of the cell.
            return CountHorizontal(row, column) + CountVertical(row, column) + CountPrimary(row, column) + CountSub(row, column) + selfCellLive;
        }
        public int CountHorizontal(int row, int column)
        {   //Check and count neighbor on the right, wont't count if the neighbor is out of bound.
            int liveNeighbors = 0;
            if (column + 1 < size)
            {
                if (grid[row, column + 1].live)
                    liveNeighbors++;
            }
            //Check and count neighbor on the left, wont't count if the neighbor is out of bound.
            if (column - 1 >= 0)
            {
                if (grid[row, column - 1].live)
                    liveNeighbors++;
            }
            return liveNeighbors;
        }

        public int CountVertical(int row, int column)
        {
            int liveNeighbors = 0;
            //Check and count neighbor on the top of the cell, wont't count if the neighbor is out of bound.
            if (row - 1 >= 0)
            {
                if (grid[row - 1, column].live)
                    liveNeighbors++;
            }
            //Check and count neighbor on the bottom, wont't count if the neighbor is out of bound.
            if (row + 1 < size)
            {
                if (grid[row + 1, column].live)
                    liveNeighbors++;
            }
            return liveNeighbors;
        }

        public int CountPrimary(int row, int column)
        {
            //Check and count neighbor on the below left, wont't count if the neighbor is out of bound.
            int liveNeighbors = 0;
            if (!(row - 1 < 0 || column + 1 >= size))
            {
                if (grid[row - 1, column + 1].live)
                {
                    liveNeighbors++;
                }
            }

            //Check and count neighbor on the above right, wont't count if the neighbor is out of bound.
            if (!(row + 1 >= size || column - 1 < 0))
            {
                if (grid[row + 1, column - 1].live)
                {
                    liveNeighbors++;
                }
            }

            return liveNeighbors;
        }

        public int CountSub(int row, int column)
        {
            //Check and count neighbor on the above left, wont't count if the neighbor is out of bound.
            int liveNeighbors = 0;
            if (!(row - 1 < 0 || column - 1 < 0))
            {
                if (grid[row - 1, column - 1].live)
                {
                    liveNeighbors++;
                }
            }
            //Check and count neighbor on the below right, wont't count if the neighbor is out of bound.
            if (!(size <= row + 1 || size <= column + 1))
            {
                if (grid[row + 1, column + 1].live)
                {
                    liveNeighbors++;
                }
            }
            return liveNeighbors;
        }

        public List<int> GetChangedButton(int row, int colm)
        {
            List<int> idArr = new List<int>();
            FloodFill(row, colm, idArr);
            return idArr;
        }

        public void FloodFill(int row, int colm, List<int> idArr)
        {
            //If the cell is within the board
            if (IsSafe(row, colm))
            {
                //Repeat until the neighbor is next to the live cells
                if (grid[row, colm].neighbors == 0)
                {
                    //Set cell as is visited
                    grid[row, colm].isVisited = 1;
                    idArr.Add(grid[row, colm].id);
                    //Using recursion to check the cell on the left
                    if (IsSafe(row, colm - 1) && grid[row, colm - 1].isVisited == 0)
                    {
                        //Repeat recursion with the cell shift left one unit
                        FloodFill(row, colm - 1, idArr);
                    }
                }
            }
            //If the cell is within the board
            if (IsSafe(row, colm))
            {
                //Repeat until the neighbor is next to the live cells
                if (grid[row, colm].neighbors == 0)
                {
                    //Set cell as is visited
                    grid[row, colm].isVisited = 1;
                    idArr.Add(grid[row, colm].id);
                    //Using recursion to check the cell on the right
                    if (IsSafe(row, colm + 1) && grid[row, colm + 1].isVisited == 0)
                    {
                        //Repeat recursion with the cell shift right one unit
                        FloodFill(row, colm + 1, idArr);
                    }
                }
            }
            //If the cell is within the board
            if (IsSafe(row, colm))
            {
                //Repeat until the neighbor is next to the live cells
                if (grid[row, colm].neighbors == 0)
                {
                    //Set cell as is visited
                    grid[row, colm].isVisited = 1;
                    idArr.Add(grid[row, colm].id);
                    //Using recursion to check the cell on the top
                    if (IsSafe(row - 1, colm) && grid[row - 1, colm].isVisited == 0)
                    {
                        //Repeat recursion with the cell shift up one unit
                        FloodFill(row - 1, colm, idArr);
                    }
                }
            }
            //If the cell is within the board
            if (IsSafe(row, colm))
            {
                //Repeat until the neighbor is next to the live cells
                if (grid[row, colm].neighbors == 0)
                {
                    //Set cell as is visited
                    grid[row, colm].isVisited = 1;
                    idArr.Add(grid[row, colm].id);
                    //Using recursion to check the cell on the bottom
                    if (IsSafe(row + 1, colm) && grid[row + 1, colm].isVisited == 0)
                    {
                        //Repeat recursion with the cell shift down one unit
                        FloodFill(row + 1, colm, idArr);
                    }
                }
            }
        }

        public bool IsSafe(int r, int c)
        {
            bool safe = true;
            if (r < 0 || c < 0 || r > size-1 || c > size-1)
            {
                return false;
            }
            return safe;
        }
    }
}
