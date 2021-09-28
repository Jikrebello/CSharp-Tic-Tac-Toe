using System;

namespace Tic_Tac_Toe
{
    class Tic_Tac_Toe
    {
        // The board for the Tic-Tac-Toe to take place.
        readonly string[,] board = new string[3, 3];
        bool gameOver = false;
        bool player1Turn = true;
        // Used to generate a number between 0 and 1 for deciding which player goes first.
        readonly Random rand = new Random();

        // ---CONSTRUCTOR---
        public Tic_Tac_Toe()
        {
            while (true)
            {
                // 1) DECIDE WHO GOES FIRST.
                CalculateTurnOrder();

                // 2) THEN START THE GAME.
                StartGame();

                if (gameOver)
                {
                    Console.WriteLine("Do you want to play again? Enter in Y for Yes or N for No.");
                    string input = Console.ReadLine();

                    if (input.ToUpper() == "Y")
                    {
                        gameOver = false;
                        CalculateTurnOrder();
                        StartGame();
                    }
                    else if (input.ToUpper() == "N")
                        break;
                    else
                        Console.WriteLine("Please enter in a valid input.");
                }
            }
        }

        // --- METHODS ---

        /// <summary>
        /// Randomly determines which player will go first.
        /// </summary>
        void CalculateTurnOrder()
        {
            int turnDecider;
            turnDecider = rand.Next(0, 2);
            if (turnDecider == 1)
                player1Turn = true;
            else
                player1Turn = false;
        }

        void StartGame()
        {
            ResetBoard();
            GetInput();
        }

        void ResetBoard()
        {
            int number = 1;
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    board[row, column] = Convert.ToString(number);
                    number++;
                }
            }
        }

        void DisplayBoard()
        {
            Console.WriteLine("     " + "|" + "     " + "|" + "     ");
            Console.WriteLine("  " + board[0, 0] + "  |  " + board[0, 1] + "  |  " + board[0, 2]);
            Console.WriteLine("_____" + "|" + "_____" + "|" + "_____");
            Console.WriteLine("     " + "|" + "     " + "|" + "     ");
            Console.WriteLine("  " + board[1, 0] + "  |  " + board[1, 1] + "  |  " + board[1, 2]);
            Console.WriteLine("_____" + "|" + "_____" + "|" + "_____");
            Console.WriteLine("     " + "|" + "     " + "|" + "     ");
            Console.WriteLine("  " + board[2, 0] + "  |  " + board[2, 1] + "  |  " + board[2, 2]);
            Console.WriteLine("     " + "|" + "     " + "|" + "     ");
            Console.WriteLine();
        }

        void GetInput()
        {
            while (!gameOver)
            {
                if (player1Turn && !IsBoardFull())
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine("Place an X for Player 1.");
                    string input = Console.ReadLine();
                    if (ReturnCoordinates(input).Item1 == -1) // ---INVALID COORDINATES FOR PLAYER 1---
                        continue;
                    else
                    {
                        board[ReturnCoordinates(input).Item1, ReturnCoordinates(input).Item2] = "X";
                        CheckAndPrintWinner();
                        player1Turn = false;
                    }
                }
                else if (!player1Turn && !IsBoardFull())
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine("Place a O for Player 2.");
                    string input = Console.ReadLine();
                    if (ReturnCoordinates(input).Item1 == -1) // ---INVALID COORDINATES FOR PLAYER 2---
                        continue;
                    else
                    {
                        board[ReturnCoordinates(input).Item1, ReturnCoordinates(input).Item2] = "O";
                        CheckAndPrintWinner();
                        player1Turn = true;
                    }
                }
                // ---BOTH PLAYERS LOSE---
                else
                {
                    Console.WriteLine("No one wins :(");
                    gameOver = true;
                }
            }
        }

        /// <summary>
        /// Checks if there are no more empty spaces left on the board.
        /// </summary>
        /// <returns></returns>
        bool IsBoardFull()
        {
            int cellCounter = 0;

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (board[row, column] == "X" || board[row, column] == "O")
                        cellCounter++;
                }
            }
            // If cellcounter == 9, then there is no more space because each cell is being held by either X or O.
            bool answer = cellCounter == 9;
            return answer;
        }

        /// <summary>
        /// Checks if the chosen numbered block is available in the board.
        /// </summary>
        /// <param name="_input">The value passed in by the player to occupy the selected block.</param>
        /// <returns>The X and Y coordinates for the board if the block is available. If not, returns -1</returns>
        Tuple<int, int> ReturnCoordinates(string _input)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (board[row, column] == _input)
                    {
                        // Create a tuple that holds the coordinates of the array where input was found and returns.
                        Tuple<int, int> foundCoordinates = new Tuple<int, int>(row, column);
                        return foundCoordinates;
                    }
                }
            }
            // If the players input was not found in the array, return a tuple with coordinates outside the bounds of the array for checks.
            Tuple<int, int> notFoundCoordinates = new Tuple<int, int>(-1, -1);
            return notFoundCoordinates;
        }

        void CheckAndPrintWinner()
        {
            // --- WIN CONDITIONS FOR CROSSES PLAYER ---
            if (CheckWinCondition("X"))
            {
                Console.WriteLine("Crosses wins!");
                gameOver = true;
            }
            // --- WIN CONDITIONS FOR NOUGHTS PLAYER ---
            else if (CheckWinCondition("O"))
            {
                Console.WriteLine("Naught's wins!");
                gameOver = true;
            }
        }

        bool CheckWinCondition(string player)
        {
            if (board[0, 0] == player && board[0, 1] == player && board[0, 2] == player ||    // TOP ROW.
                board[1, 0] == player && board[1, 1] == player && board[1, 2] == player ||    // MIDDLE ROW.
                board[2, 0] == player && board[2, 1] == player && board[2, 2] == player ||    // BOTTOM ROW.
                board[0, 0] == player && board[1, 0] == player && board[2, 0] == player ||    // LEFT COLUMN.
                board[0, 1] == player && board[1, 1] == player && board[2, 1] == player ||    // MIDDLE COLUMN.
                board[0, 0] == player && board[1, 2] == player && board[2, 2] == player ||    // RIGHT COLUMN.
                board[0, 2] == player && board[1, 1] == player && board[2, 0] == player ||    // DIAGONAL TOP RIGHT BOTTOM LEFT.
                board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)      // DIAGONAL TOP LEFT BOTTOM RIGHT.
                return true;
            else
                return false;
        }
    }
}
