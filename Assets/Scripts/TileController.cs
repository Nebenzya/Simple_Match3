using System.Collections.Generic;
using UnityEngine;

public class TileController : GameBoard
{
    protected Tile currentSelectedTile;
    protected bool isFindMantch = false;
    protected bool isShift = false; // ��������� ����������� ���� �� ����� ��������

    protected Vector2[] vectorPattern = new Vector2[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    #region protected

    // ������ ������ ���� �������� ������
    protected List<Tile> NeighborTiles()
    {
        var buffList = new List<Tile>();

        foreach (var ray in vectorPattern)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentSelectedTile.transform.position, ray);
            if (hit.collider != null)
            {
                buffList.Add(hit.collider.gameObject.GetComponent<Tile>());
            }
        }
        return buffList;
    }

    // ������� �������� ������ ������ ��������������
    protected void CheckShiftDown(int xPos, int yPos)
    {
        isShift = true;
        var buffRenderers = new List<SpriteRenderer>(); 
        int count = 0; // ���������� ������ ������ � �������

        for (int y = yPos; y < height; y++)
        {
            Tile tile = allTile[xPos, y];
            if (tile.IsEmpty)
            {
                count++;
            }
            buffRenderers.Add(tile.spriteRenderer);
        }
        for (int i = 0; i < count; i++)
        {
            ShiftDown(xPos, buffRenderers);
        }
        isShift = false;
    }

    // ������� ���������� ����� ������ ����
    protected void ShiftDown(int xPos, List<SpriteRenderer> spriteRenderers)
    {
        if (spriteRenderers.Count > 1)
        {
            for (int y = 0; y < spriteRenderers.Count - 1; y++)
            {
                spriteRenderers[y].sprite = spriteRenderers[y + 1].sprite;
                spriteRenderers[y + 1].sprite = GetNewSprite(xPos, height - 1);
            }
        }
        else
        {
            // ���� �������� ���� � ����� ������� ������, ������ ������ ����� ������
            spriteRenderers[0].sprite = GetNewSprite(xPos, height - 1);
        }

    }

    // �������� ���� �����������, ���� ������� ���������� - �������� ��������� ��������
    protected void DeleteSprite(Tile tile)
    {
        var buffList = new List<Tile>();

        foreach (var ray in vectorPattern)
        {
            buffList.AddRange(FindMatch(tile, ray));
        }

        if (buffList.Count > 1)
        {
            foreach (var item in buffList)
            {
                item.spriteRenderer.sprite = null;
                UIPlaySpace.instance.Score = 10;
            }
            isFindMantch = true;
        }
    }

    // ������ ������� ��������� �����
    protected void SwapTiles(Tile tile)
    {
        if (currentSelectedTile.spriteRenderer.sprite != tile.spriteRenderer.sprite)
        {
            (tile.spriteRenderer.sprite, currentSelectedTile.spriteRenderer.sprite) = (currentSelectedTile.spriteRenderer.sprite, tile.spriteRenderer.sprite);
        }
    }

    #endregion // protected

    #region private

    // �������� �� ���������� �� ����������� �����������
    // ���������� ������ �������� ������
    private List<Tile> FindMatch(Tile tileBegin, Vector2 dir)
    {
        var buffList = new List<Tile>();
        RaycastHit2D hit = Physics2D.Raycast(tileBegin.transform.position, dir);

        // ������ �� ������� �������� � ���������� � ������ �� ��� ��� ���� ���� ����������
        while (hit.collider != null
            && hit.collider.gameObject.GetComponent<Tile>().spriteRenderer.sprite == tileBegin.spriteRenderer.sprite)
        {
            buffList.Add(hit.collider.gameObject.GetComponent<Tile>());
            hit = Physics2D.Raycast(hit.collider.gameObject.transform.position, dir);
        }

        return buffList;
    }

    // ��������� ������ ������� ������ ����������
    // ���������� ��������� ������ ��������� �� ��������
    private Sprite GetNewSprite(int xPos, int yPos)
    {
        var sprites = new List<Sprite>();
        sprites.AddRange(allSprites);

        if (xPos > 0)
        {
            sprites.Remove(allTile[xPos - 1, yPos].spriteRenderer.sprite);
        }
        if (xPos < widht - 1)
        {
            sprites.Remove(allTile[xPos + 1, yPos].spriteRenderer.sprite);
        }
        if (yPos > 0)
        {
            sprites.Remove(allTile[xPos, yPos - 1].spriteRenderer.sprite);
        }

        return sprites[Random.Range(0, sprites.Count)];
    }

    #endregion //private
}
