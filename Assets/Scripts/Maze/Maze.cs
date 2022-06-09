public class Maze {
    private uint width;
    private uint height;
    private uint start;
    private uint end;
    private Cell[] map;

    public uint Width {
        get => width;
    }

    public uint Height {
        get => height;
    }

    public uint Start {
        get => start;
    }

    public uint End {
        get => end;
    }

    public Cell[] Map {
        get => map;
    }

    public Maze(uint width_, uint height_) {
        width = width_;
        height = height_;
        BuildMaze(0, GetSize() - 1);
    }

    public Maze(uint width_, uint height_, uint start_, uint end_) {
        width = width_;
        height = height_;
        BuildMaze(start_, end_);
    }

    private void BuildMaze(uint start_, uint end_) {
        start = start_;
        end = end_;
        uint size = GetSize();
        map = new Cell[size];
        for (uint i = 0; i < size; i++) {
            map[i] = new Cell();
        }
    }

    private uint ToIndex(uint i, uint j) {
        return (i * width) + j;
    }

    public bool IsStart(uint i, uint j) {
        return ToIndex(i, j) == start;
    }

    public bool IsEnd(uint i, uint j) {
        return ToIndex(i, j) == end;
    }

    public uint GetSize() {
        return width * height;
    }

    public Cell GetCell(uint i, uint j) {
        return map[ToIndex(i, j)];
    }

    public bool IsNorthWallUp(uint i, uint j) {
        return GetCell(i, j).IsNorthWallUp();
    }

    public bool IsEastWallUp(uint i, uint j) {
        return GetCell(i, j).IsEastWallUp();
    }

    public bool IsSouthWallUp(uint i, uint j) {
        return GetCell(i, j).IsSouthWallUp();
    }

    public bool IsWestWallUp(uint i, uint j) {
        return GetCell(i, j).IsWestWallUp();
    }

    public void TearDownNorthWall(uint i, uint j) {
        if (i > 0) {
            Cell cell = GetCell(i, j);
            cell.TearDownNorthWall();
            cell = GetCell(i - 1, j);
            cell.TearDownSouthWall();
        }
    }

    public void TearDownEastWall(uint i, uint j) {
        if (j < width - 1) {
            Cell cell = GetCell(i, j);
            cell.TearDownEastWall();
            cell = GetCell(i, j + 1);
            cell.TearDownWestWall();
        }
    }

    public void TearDownSouthWall(uint i, uint j) {
        if (i < height - 1) {
            Cell cell = GetCell(i, j);
            cell.TearDownSouthWall();
            cell = GetCell(i + 1, j);
            cell.TearDownNorthWall();
        }
    }

    public void TearDownWestWall(uint i, uint j) {
        if (j > 0) {
            Cell cell = GetCell(i, j);
            cell.TearDownWestWall();
            cell = GetCell(i, j - 1);
            cell.TearDownEastWall();
        }
    }
}
