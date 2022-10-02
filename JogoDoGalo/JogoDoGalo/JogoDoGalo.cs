using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JogoDoGalo
{
    class JogoDoGalo
    {
        public List<string> positions = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public bool gameStatus = false;
        public string userTurn;

        public JogoDoGalo()
        {
            StartGame();
        }

        public void FirstToPlay()
        {
            char[] chars = "XO".ToCharArray();
            Random r = new Random();
            int i = r.Next(chars.Length);
            userTurn = chars[i].ToString();
        }

        public void StartGame()
        {
            FirstToPlay();

            while (!gameStatus)
            {
                TableRender();
                UserChoice();
                CheckGameStatus();
                ChangeTurn();
            }
        }

        private void TableRender()
        {
            var positionsForTable = positions;
            var table = "";

            for (int i = 0; i < 3; i++)
            {
                for (int a = 0; a < 3; a++)
                {
                    table += a == 1 ? "| _" + positionsForTable[0] + "_ |" : "_" + positionsForTable[0] + "_";
                    positionsForTable = positionsForTable.Skip(1).ToList();
                }
                table += "\n";
            }
            Console.Clear();
            Console.WriteLine(table);
        }

        private void UserChoice()
        {
            int userChoice = 0;
            bool isParsable;
            bool isAvailable;

            Console.WriteLine("Jogador: " + userTurn);
            Console.WriteLine("Esolha uma casa de 1 a 9 conforme as casas disponíveis na tabela!");

            do {                 
                var choice = Console.ReadLine();
                isParsable = Int32.TryParse(choice, out userChoice);
                isAvailable = isParsable ? positions.Where(x => x == Convert.ToString(userChoice)).Any() : false;

                if (!isParsable || (userChoice <= 0 || userChoice >= 10) || !isAvailable)
                {
                    Console.WriteLine("Erro! A sua escolha não é válida!");
                }
            } while (!isParsable || (userChoice <= 0 || userChoice >= 10) || !isAvailable);

            var index = positions.IndexOf(Convert.ToString(userChoice));
            positions[index] = userTurn;

        }

        private void ChangeTurn()
        {
            userTurn = userTurn == "X" ? "O" : "X";
        }

        private void CheckGameStatus()
        {
            if (HorizontalLine() || VerticalLine() || DiagonalLine())
            {
                gameStatus = true;
                Console.WriteLine("O jogador '" + userTurn + "' venceu a partida!");
            }             
        }

        /* Check Game Lines */
        private bool HorizontalLine()
        {
            bool horizontalLine1 = positions[0] == positions[1] && positions[0] == positions[2];
            bool horizontalLine2 = positions[3] == positions[4] && positions[3] == positions[5];
            bool horizontalLine3 = positions[6] == positions[7] && positions[6] == positions[8];

            if (horizontalLine1 || horizontalLine2 || horizontalLine3)
                return true;
            else
                return false;
        }

        private bool VerticalLine()
        {
            bool verticalLine1 = positions[0] == positions[3] && positions[0] == positions[6];
            bool verticalLine2 = positions[1] == positions[4] && positions[1] == positions[7];
            bool verticalLine3 = positions[2] == positions[5] && positions[6] == positions[8];

            if (verticalLine1 || verticalLine2 || verticalLine3)
                return true;
            else
                return false;
        }

        private bool DiagonalLine()
        {
            bool diagonalLine1 = positions[0] == positions[4] && positions[0] == positions[8];
            bool diagonalLine2 = positions[2] == positions[4] && positions[2] == positions[6];
           
            if (diagonalLine1 || diagonalLine2)
                return true;
            else
                return false;
        }
    }
}
