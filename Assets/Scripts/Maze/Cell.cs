public class Cell {
    private bool north; // Up wall (if true, the wall is up)
    private bool east;  // Left wall (if true, the wall is up)
    private bool south; // Bottom wall (if true, the wall is up)
    private bool west;  // Right wall (if true, the wall is up)

    public Cell() {
        north = true;
        east = true;
        south = true;
        west = true;
    }

    public bool IsNorthWallUp() {
        return north;
    }

    public bool IsEastWallUp() {
        return east;
    }

    public bool IsSouthWallUp() {
        return south;
    }

    public bool IsWestWallUp() {
        return west;
    }

    public void TearDownNorthWall() {
        north = false;
    }

    public void TearDownEastWall() {
        east = false;
    }

    public void TearDownSouthWall() {
        south = false;
    }

    public void TearDownWestWall() {
        west = false;
    }
}
