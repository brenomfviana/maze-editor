using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeLoaderScene : MonoBehaviour
{
    public Sprite[] sprites;
    public Grid grid;
    public Tilemap tilemap;
    public GameObject cell;

    void Awake()
    {
        this.LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        Maze maze = Generator.Generate(Random.Range(0, 1000), 5, 5);
        Vector3Int crtCell = tilemap.WorldToCell(this.transform.position);
        int ix = crtCell.x;
        for (uint i = 0; i < maze.Height; i++)
        {
            for (uint j = 0; j < maze.Width; j++)
            {
                Vector3 spawnPoint = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position)) + crtCell;
                GameObject tile = Instantiate(cell, spawnPoint, Quaternion.identity, grid.transform);
                TearDownWalls(tile, maze, i, j);
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (maze.IsStart(i, j))
                {
                    sr.sprite = sprites[1];
                }
                else if (maze.IsEnd(i, j))
                {
                    sr.sprite = sprites[2];
                }
                else
                {
                    sr.sprite = sprites[0];
                }
                crtCell.x += 1;
            }
            crtCell.x = ix;
            crtCell.y -= 1;
        }
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
}
