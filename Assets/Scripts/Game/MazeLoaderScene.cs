using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeLoaderScene : MonoBehaviour
{
    public Camera mainCamera;
    public Sprite[] sprites;
    public Grid grid;
    public Tilemap tilemap;
    public GameObject cell;

    void Awake()
    {
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        Maze maze = Generator.Generate(Random.Range(0, 1000), 6, 6);
        Vector3Int crtCell = tilemap.WorldToCell(transform.position);
        int ix = crtCell.x;
        Vector3? center = new Vector3(0, 0, 0);
        for (uint i = 0; i < maze.Height; i++)
        {
            for (uint j = 0; j < maze.Width; j++)
            {
                Vector3 spawnPoint = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position)) + crtCell;
                GameObject tile = Instantiate(cell, spawnPoint, Quaternion.identity, grid.transform);
                TearDownWalls(tile, maze, i, j);
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                sr.sprite = GetSprite(maze, i, j);
                Vector3? aux = GetMazeCenter(maze, i, j, spawnPoint);
                center = aux != null ? FixMazeCenter((Vector3)aux, i, j) : center;
                crtCell.x += 1;
            }
            crtCell.x = ix;
            crtCell.y -= 1;
        }
        mainCamera.Move((Vector3)center);
    }

    private void TearDownWalls(GameObject cell, Maze maze, uint i, uint j)
    {
        GameObject wall = cell.transform.GetChild(0).gameObject;
        if (!maze.IsNorthWallUp(i, j))
        {
            Destroy(wall.transform.GetChild((int)Direction.North).gameObject);
        }
        if (!maze.IsEastWallUp(i, j))
        {
            Destroy(wall.transform.GetChild((int)Direction.East).gameObject);
        }
        if (!maze.IsSouthWallUp(i, j))
        {
            Destroy(wall.transform.GetChild((int)Direction.South).gameObject);
        }
        if (!maze.IsWestWallUp(i, j))
        {
            Destroy(wall.transform.GetChild((int)Direction.West).gameObject);
        }
    }

    private Sprite GetSprite(Maze maze, uint i, uint j)
    {
        if (maze.IsStart(i, j))
        {
            return sprites[1];
        }
        else if (maze.IsEnd(i, j))
        {
            return sprites[2];
        }
        return sprites[0];
    }

    private Vector3? GetMazeCenter(Maze maze, uint i, uint j, Vector3 point)
    {
        if (i == maze.Height / 2 && j == maze.Width / 2)
        {
            return point;
        }
        return null;
    }

    private Vector3? FixMazeCenter(Vector3 center, uint i, uint j)
    {
        if (i % 2 == 1)
        {
            center.x -= 0.5f;
        }
        if (j % 2 == 1)
        {
            center.y += 0.5f;
        }
        return center;
    }
}
