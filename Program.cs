using System;

namespace noughts
{
    class Program
    {
        private enum BoardState
        {
            NOUGHTS_WIN,
            CROSSES_WIN,
            DRAW,
            NOUGHTS_TURN,
            CROSSES_TURN,
            CROSSES_MUST_START,
            BOARD_INVALID
        }

        private static BoardState GetStateOfBoard (string board)
        {
            // Assume board has been filled with X & O.
            bool isBoardFull = true;
            int crosses = 0;
            int noughts = 0;
            int emptySlots = 0;

            // CHECK BOARD IS VALID - 4 CHECKS.
            // 1 - Check if length of board does not equal 9.
            // Otherwise, return Board_Invalid.
            if (board.Length != 9)
            {
                return BoardState.BOARD_INVALID;
            }

            // 2 - Check board contains "X", "O" or "_".
            // At the same time, count the number of "X", "O" or "_".
            // Otherwise, return BOARD_INVALID.
            for (int i = 0; i < board.Length; i++)
            {
                string boardContent = Convert.ToString (board[i]);

                if (boardContent != "_" && boardContent != "X" && boardContent != "O")
                {
                    return BoardState.BOARD_INVALID;
                }

                switch (boardContent)
                {
                    case "X":
                        crosses++;
                        break;
                    case "O":
                        noughts++;
                        break;
                    default:
                        emptySlots++;
                        if (emptySlots >= 1)
                        {
                            isBoardFull = false;
                        }
                        break;
                }
            }

            // 3 - If game has not started or if "O" started the game.
            if (emptySlots == 9 || (noughts - crosses) == 1 && (emptySlots % 2) == 0)
            {
                return BoardState.CROSSES_MUST_START;
            }

            // 4 - If board has too many crosses or noughts.
            if ((noughts - crosses) > 1 || (crosses - noughts) > 1)
            {
                return BoardState.BOARD_INVALID;
            }

            // CHECK WIN CONDITIONS.
            // Define WinLines in an array
            int[, ] winLines = new int[, ]
            { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 }
            };

            // Loop through winLines
            for (int i = 0; i < winLines.GetLength (0); i++)
            {
                // Check if all elements are equal
                // NB each element is char type not string.
                if (board[winLines[i, 0]] == board[winLines[i, 1]] && board[winLines[i, 1]] == board[winLines[i, 2]])
                {
                    // Create variable to check which player won
                    // Convert element to string since current data type is char
                    string winner = Convert.ToString (board[winLines[i, 0]]);

                    if (winner != "_")
                    {
                        if (winner == "X")
                        {
                            return BoardState.CROSSES_WIN;
                        }
                        else
                        {
                            return BoardState.NOUGHTS_WIN;
                        }
                    }
                }
            }

            // Check if game is a draw or which player's turn is next.
            if (isBoardFull)
            {
                // The game is a draw
                return BoardState.DRAW;
            }
            else
            {
                // Determine next turn
                if ((emptySlots % 2) != 1)
                {
                    return BoardState.NOUGHTS_TURN;
                }
                else
                {
                    return BoardState.CROSSES_TURN;
                }
            }

        }

        static void Main (string[] args)
        {
            // leave this main method unchanged
            for (int i = 0; i < args.Length; i++)
            {
                System.Console.WriteLine (GetStateOfBoard (args[i]));
            }
        }
    }
}