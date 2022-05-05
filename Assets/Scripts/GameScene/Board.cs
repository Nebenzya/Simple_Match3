using System.Collections.Generic;
using UnityEngine;

public enum VectorDirection
{
    // TODO addition of a diagonal direction
    Horizontal,
    Vertical
}

public class Board : MonoBehaviour
{
    private Board() { } // To save the "Singleton"
    public static Board instance;
    private void Awake()
    {
        instance = this;
    }

    private static int _width, _height, _xLastIndex, _yLastIndex;
    private static Tile _prefab;
    private static Tile[,] _allTiles;
    private static List<Sprite> _sprites;

    #region Start Game
    public void StartNewGame(BoardSetting setting)
    {
        _width = setting.width;
        _height = setting.height;
        _prefab = setting.prefab;
        _sprites = setting.sprites;
        _xLastIndex = _width - 1;
        _yLastIndex = _height - 1;

        CreateBoard();
    }

    public void CreateBoard()
    {
        _allTiles = new Tile[_width, _height];
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector2 tileSize = _prefab.spriteRenderer.bounds.size;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile newTile = Instantiate(_prefab, transform.position, Quaternion.identity);
                newTile.transform.position = new Vector3(xPos + tileSize.x * x, yPos + tileSize.y * y, 0);
                newTile.transform.parent = transform;
                AddSprite(newTile);

                _allTiles[x, y] = newTile;
            }
        }
    }

    private void AddSprite(Tile tile)
    {
        int value = Random.Range(0, _sprites.Count);
        tile.ID = value;
        tile.spriteRenderer.sprite = _sprites[value];
    }
    #endregion // Start Game

    #region Search match or empty tile
    public void CheckBoard()
    {
        VectorDirection[] _allVectors = { VectorDirection.Horizontal, VectorDirection.Vertical };
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                // Looking in all directions and searching for matches
                foreach (var vector in _allVectors)
                {
                    SearchMatch(x, y, vector);
                }
            }
        }
        SearchEmpty();
    }

    private void SearchMatch(int xPos, int yPos, VectorDirection vector)
    {
        int Start = 0, End = 0, xStep = 0, yStep = 0;
        bool isStartMove = false;

        if (vector == VectorDirection.Horizontal)
            xStep = 1;
        else
            yStep = 1;

        bool toSelect = xStep > yStep; // replaces the VectorDirection

        if (toSelect ? xPos < _xLastIndex : yPos < _yLastIndex)
        {
            while (_allTiles[xPos, yPos].ID == _allTiles[xPos + xStep, yPos + yStep].ID)
            {
                if (!isStartMove)
                {
                    Start = toSelect ? xPos : yPos;
                    isStartMove = true;
                }

                _ = toSelect ? ++xPos : ++yPos;

                if (!(toSelect ? xPos < _xLastIndex : yPos < _yLastIndex)) break;
            }
        }
        else return;

        if (isStartMove) End = toSelect ? xPos : yPos;

        if (End - Start > 1)
        {
            var tileBuffer = new List<Tile>();

            for (int i = Start; i <= End; i++)
            {
                if (toSelect)
                {
                    tileBuffer.Add(_allTiles[i, yPos]);
                }
                else
                {
                    tileBuffer.Add(_allTiles[xPos, i]);
                }

            }
            DeleteSprite(tileBuffer);
        }
    }

    private void DeleteSprite(List<Tile> tileBuffer)
    {
        if (tileBuffer.Count > 2)
        {
            UIGameBoard.instance.Score = tileBuffer.Count;
            foreach (var tile in tileBuffer)
            {
                tile.spriteRenderer.sprite = null;
                tile.Move = false;
            }
        }
    }

    private void SearchEmpty()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_allTiles[x, y].IsEmpty)
                {
                    GoingDown(x, y);
                }
            }
        }
        if (CheckEmpty())
        {
            SearchEmpty();
        }
    }

    // Return true if empty tile is found
    public bool CheckEmpty()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_allTiles[x, y].IsEmpty)
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion // Search match or empty tile

    private void GoingDown(int xPos, int yPos)
    {
        while (yPos <= _yLastIndex)
        {
            if (yPos < _yLastIndex)
            {
                _allTiles[xPos, yPos].spriteRenderer.sprite = _allTiles[xPos, yPos + 1].spriteRenderer.sprite;
                _allTiles[xPos, yPos].ID = _allTiles[xPos, yPos + 1].ID;
                _allTiles[xPos, yPos].Move = _allTiles[xPos, yPos + 1].Move;
            }
            else if (yPos == _yLastIndex)
            {
                AddSprite(_allTiles[xPos, yPos]);
            }
            yPos++;
        }
    }
}


