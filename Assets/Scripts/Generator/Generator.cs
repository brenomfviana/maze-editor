using UnityEngine;

class Generator
{
    private int seed;
    public Maze maze;
    private uint next;
    private uint _in;
    private uint _jn;

    public Generator(int seed, uint width, uint height)
    {
        Random.InitState(seed);
        maze = new Maze(width, height);
        next = 0;
        _in = 0;
        _jn = 0;
    }

    public Maze Generate()
    {
        for (uint i = 0; i < maze.Height; i++)
        {
            for (uint j = 0; j < maze.Width; j++)
            {
                GenerateIterative(i, j);
            }
        }
        return maze;
    }

    public Maze GenerateNext()
    {
        Debug.Log(_in + " " + _jn);
        if (_jn < maze.Width && _in < maze.Height) {
            GenerateIterative(_in, _jn);
        }
        if (_jn < maze.Width)
        {
            _jn++;
        }
        if (_jn == maze.Width && _in < maze.Height) {
            _jn = 0;
            _in++;
        }
        return maze;
    }

    private void GenerateIterative(uint i, uint j)
    {
        if (i == maze.Height - 1)
        {
            maze.TearDownEastWall(i, j);
        }
        else if (j == maze.Width - 1)
        {
            maze.TearDownSouthWall(i, j);
        }
        else
        {
            bool r = Random.Range(0, 2) == 0;
            if (r && maze.IsEastWallUp(i, j))
            {
                maze.TearDownEastWall(i, j);
            }
            else if (i < maze.Height - 1)
            {
                maze.TearDownSouthWall(i, j);
            }
        }
    }
}
