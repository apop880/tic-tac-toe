namespace TicTacToe.Board;

public class Box
{
    public int xLoc { get; }
    public int yLoc { get; }
    public char symbol { get; set; }   

    public Box(int x, int y)
    {
        xLoc = x;
        yLoc = y;
        symbol = (char) 0;
    }
}