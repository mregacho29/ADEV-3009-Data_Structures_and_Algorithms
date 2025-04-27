public class Coordinate
{
    public int X;
    public int Y;
    public int Z;

    public Coordinate(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    // Implementing the ShallowCopy method
    public Coordinate ShallowCopy()
    {
        return new Coordinate(this.X, this.Y, this.Z);
    }

    public static void main(String[] args)
    {
        // Using the class above
        Coordinate a = new Coordinate(1, 2, 3);
        Coordinate b = a.ShallowCopy();

        a.Z = 4;

        // Print the value of b.Z
        System.out.println(b.Z); // Expected output: 3
    }
}