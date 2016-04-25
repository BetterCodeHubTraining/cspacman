namespace eu.sig.cspacman.board
{
    // This interface does not appear in the Java version.
    // It has been added in the C# version to facilitate mocking.
    public interface IBoard
    {
        int Width { get; }
        int Height { get; }

        bool Invariant();
        Square SquareAt(int x, int y);
        bool WithinBorders(int x, int y);
    }
}
