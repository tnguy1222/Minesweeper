using CST247CLC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST247CLC.Controllers
{
    public class MinesweeperController : Controller
    {
      // static List<ButtonModel> buttons = new List<ButtonModel>();

        static GameBoard gameBoard = new GameBoard(10);
  
        public IActionResult Index()
        {

            //Send the button list to the "Index" page
            return View("Index", gameBoard.buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);
            gameBoard.buttons.ElementAt(bN).isVisited = 1;
            List<int> btns = gameBoard.GetChangedButton(gameBoard.buttons.ElementAt(bN).row, gameBoard.buttons.ElementAt(bN).column);
            foreach (var item in btns)
            {
                gameBoard.buttons.ElementAt(item).isVisited = 1;
            }
           
            return View("Index", gameBoard.buttons);
        }
    }
}
