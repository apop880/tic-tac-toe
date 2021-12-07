using TicTacToe.Board;

namespace TicTacToe;

public class Game
{
    public GameBoard Board = new GameBoard();

    public bool playerTurn;

    public void Setup()
    {
        Board.DrawBoard();

        var random = new Random();
        playerTurn = random.NextDouble() > 0.5;
    }

    public void GameLoop()
    {
        Console.SetCursorPosition(0, 9);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, 9);
        (bool endGame, char? winner) status;
        bool result = false;
        while(true)
        {
            status = Board.WinnerCheck();
            if(status.endGame)
            {
                switch (status.winner)
                {
                    case 'X':
                        Console.WriteLine("You win!");
                        break;
                    case 'O':
                        Console.WriteLine("You lost!");
                        break;
                    default:
                        Console.WriteLine("Tie!");
                        break;
                }
                break;
            }

            if (playerTurn)
            {
                Console.WriteLine("User's Turn: Enter a space 1-9");
                while(result == false)
                {
                    var key = Console.ReadKey(true);
                    result = Board.FillBoxX(key);
                }                
            }
            else {
                result = Board.GetWinningMove();
                if (result == false)
                {
                    result = Board.GetDefensiveMove();
                }
                if (result == false)
                {
                    Board.GetRandomMove();
                }
            }
            Console.SetCursorPosition(0, 9);
            Console.Write(new string(' ', 80));
            Console.SetCursorPosition(0, 9);
            result = false;
            playerTurn = !playerTurn;
        }


    }
}