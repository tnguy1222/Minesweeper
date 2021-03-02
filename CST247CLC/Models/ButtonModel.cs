using System;

/*Vien Nguyen
 CST-247 Enterprise Application Programming 4
 Feb 8th, 2021
 Define the Cell class with the properties of row, column, visited, live, neighbors.
 This is my own work.
*/
namespace CST247CLC.Models
{
    public class ButtonModel
    {
        //List the properties of the Cell Class.
        public int id { get; set; }
        public int row { get; set; }
        public int column { get; set; }
        public int isVisited { get; set; }
        public bool live { get; set; }
        public int neighbors { get; set; }

        public ButtonModel()
        {
            this.isVisited = 0;
        }

        public ButtonModel(int id, int row, int column, int visited, bool live, int neighbors)
        {
            this.id = id;
            this.row = row;
            this.column = column;
            this.isVisited = visited;
            this.live = live;
            this.neighbors = neighbors;
        }

        



    }

}