using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    protected int widht, height;
    protected Tile tile;
    protected List<Sprite> allSprites;
    protected Tile[,] allTile;

    public void SetValue(int widht, int height, Tile tile, List<Sprite> sprites)
    {
        this.widht = widht;
        this.height = height;
        this.tile = tile;

        // устанавливаем количество спрайтов соответственно уровню
        int value = 0;
        switch (GameLevel.intence.CurrentLevel)
        {
            case Level.Easy:
                value = 2;
                break;
            case Level.Middle:
                value = 1;
                break;
        }
        allSprites = sprites.GetRange(0,sprites.Count-value);

        CreateBoard();
    }

    protected void CreateBoard()
    {
        allTile = new Tile[widht, height];
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector2 tileSize = tile.spriteRenderer.bounds.size;
        Sprite cashSprite = null;

        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile newTile = Instantiate(tile, transform.position, Quaternion.identity);
                newTile.transform.position = new Vector3(xPos + tileSize.x * x, yPos + tileSize.y * y, 0);
                newTile.transform.parent = transform;
                allTile[x, y] = newTile;
                var tempSprites = new List<Sprite>();
                tempSprites.AddRange(allSprites);


                tempSprites.Remove(cashSprite);
                if (x > 0)
                {
                    tempSprites.Remove(allTile[x - 1, y].spriteRenderer.sprite);
                }
                newTile.spriteRenderer.sprite = tempSprites[Random.Range(0, tempSprites.Count)];
                cashSprite = newTile.spriteRenderer.sprite;
            }
        }
    }
}
