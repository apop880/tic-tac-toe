namespace TicTacToe.Board;

public class GameBoard {
    private string[] grid = new string[]
    {
        "           ",
        " 1 │ 2 │ 3 ",
        "───┼───┼───",
        " 4 │ 5 │ 6 ",
        "───┼───┼───",
        " 7 │ 8 │ 9 ",
        "            "
    };

    private List<Box> boxes;
    private List<List<Box>> lines;

    private Box box1 = new(1, 3);
    private Box box2 = new(5, 3);
    private Box box3 = new(9, 3);
    private Box box4 = new(1, 5);
    private Box box5 = new(5, 5);
    private Box box6 = new(9, 5);
    private Box box7 = new(1, 7);
    private Box box8 = new(5, 7);
    private Box box9 = new(9, 7);

    public GameBoard()
    {
        boxes = new List<Box>()
        {
            box1, box2, box3, box4, box5, box6, box7, box8, box9
        };

        var row1 = new List<Box>()
        {
            box1, box2, box3
        };
        var row2 = new List<Box>()
        {
            box4, box5, box6
        };
        var row3 = new List<Box>()
        {
            box7, box8, box9
        };

        var col1 = new List<Box>()
        {
            box1, box4, box7
        };
        var col2 = new List<Box>()
        {
            box2, box5, box8
        };
        var col3 = new List<Box>()
        {
            box3, box6, box9
        };

        var diag1 = new List<Box>()
        {
            box1, box5, box9
        };
        var diag2 = new List<Box>()
        {
            box3, box5, box7
        };

        lines = new List<List<Box>>()
        {
            row1, row2, row3, col1, col2, col3, diag1, diag2
        };
    }

    public void DrawBoard()
    {
        for (var i = 0; i < grid.Length; i++)
        {
            System.Console.WriteLine(grid[i]);
        }
    }

    public bool FillBoxX(ConsoleKeyInfo key)
    {
        int theBox = key.Key switch
        {
            ConsoleKey.NumPad7 or ConsoleKey.D1 => 0,
            ConsoleKey.NumPad8 or ConsoleKey.D2 => 1,
            ConsoleKey.NumPad9 or ConsoleKey.D3 => 2,
            ConsoleKey.NumPad4 or ConsoleKey.D4 => 3,
            ConsoleKey.NumPad5 or ConsoleKey.D5 => 4,
            ConsoleKey.NumPad6 or ConsoleKey.D6 => 5,
            ConsoleKey.NumPad1 or ConsoleKey.D7 => 6,
            ConsoleKey.NumPad2 or ConsoleKey.D8 => 7,
            ConsoleKey.NumPad3 or ConsoleKey.D9 => 8,
            _ => 10,
        };

        if (theBox < 10 && boxes[theBox].symbol == (char) 0)
        {
            Console.SetCursorPosition(boxes[theBox].xLoc, boxes[theBox].yLoc);
            Console.WriteLine("X");
            boxes[theBox].symbol = 'X';
            return true;
        }

        return false;
    }

    public void FillBoxO(Box? theBox)
    {
        if (theBox is not null)
        {
            Console.SetCursorPosition(theBox.xLoc, theBox.yLoc);
            Console.WriteLine("O");
            theBox.symbol = 'O';
        }
    }

    public bool GetWinningMove()
    {
        foreach (var line in lines)
        {
            if (line.Count(box => box.symbol == 'O') == 2 && line.Count(box => box.symbol == 'X') == 0)
            {
                FillBoxO((line.Find(box => box.symbol == (char) 0)));
                return true;
            }
        }
        return false;
    }

    public bool GetDefensiveMove()
    {
        List<Box> defensiveBoxes = new();
        foreach (var line in lines)
        {
            if (line.Count(box => box.symbol == 'X') == 2 && line.Count(box => box.symbol == 'O') == 0)
            {
                defensiveBoxes.Add(line.Find(box => box.symbol == (char) 0));
            }
        }

        if (defensiveBoxes.Count > 0)
        {
            var getBest = defensiveBoxes.GroupBy(box => box).ToDictionary(box => box.Key, box => box.Count());
            FillBoxO((getBest.MaxBy(box => box.Value).Key));
            return true;
        }
        return false;
    }

    public void GetRandomMove()
    {
        foreach (var line in lines)
        {
            if (line.Count(box => box.symbol == 'O') == 1 && line.Count(box => box.symbol == 'X') == 0)
            {
                FillBoxO((line.Find(box => box.symbol == (char) 0)));
                return;
            }
        }
        var open = GetEmptyBoxes().ToList();
        FillBoxO(open[Random.Shared.Next(0, open.Count)]);
    }

    public (bool endGame, char? winner) WinnerCheck()
    {
        foreach (var line in lines)
        {
            if (line.All(box => box.symbol == 'X') is true)
            {
                return (true, 'X');
            }
            else if (line.All(box => box.symbol == 'O') is true)
            {
                return (true, 'O');
            }
        }

        if (GetEmptyBoxes().Any() is false)
        {
            return (true, null);
        }

        return (false, null);

    }

    public IEnumerable<Box> GetEmptyBoxes() => boxes.Where(box => box.symbol == (char) 0);
}