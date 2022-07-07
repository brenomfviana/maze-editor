using UnityEngine;

class Generator
{
    public static Maze Generate(int seed, uint width, uint height)
    {
        Random.InitState(seed);
        Maze maze = new Maze(width, height);
        for (uint i = 0; i < maze.Height; i++)
        {
            for (uint j = 0; j < maze.Width; j++)
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
        return maze;
    }
}
