namespace TicTacToe;

internal class Program {
    private static void Main() {
        Console.Clear();
        Console.WriteLine("Welcome to TIC-TAC-TOE");
        Console.WriteLine("X: Human; O: Computer");

        Game game = new Game();
        game.Setup();
        game.GameLoop();

        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }
}