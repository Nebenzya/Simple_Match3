using System.Collections.Generic;
using UnityEngine;

public class BoardController : TileController
{
    public static BoardController instance;
    private void Awake() => instance = this;
    
    // �������� �� ������������� � ������
    public void CheckSelectionTile(Tile tile)
    {
        if (!(tile.IsEmpty || isShift))
        {
            if (tile.isSelect)
            {
                DeselectionTile(tile);
            }
            else
            {
                if (!tile.isSelect && currentSelectedTile == null)
                {
                    SelectionTile(tile);
                }
                else
                {
                    // �������� �������� �� ��������� ����� ���������
                    if (NeighborTiles().Contains(tile))
                    {
                        SwapTiles(tile);
                        CheckMathes(tile);
                        DeselectionTile(currentSelectedTile);
                    }
                    else
                    {
                        DeselectionTile(currentSelectedTile);
                        SelectionTile(tile);
                    }
                }
            }
        }
    }

    // �������� ��������� ���� 
    private void SelectionTile(Tile tile)
    {
        tile.isSelect = true;
        tile.spriteRenderer.color = new Color(.5f, .5f, .5f);
        currentSelectedTile = tile;
    }

    // ������� ��������� � ���������� ����� 
    private void DeselectionTile(Tile tile)
    {
        tile.isSelect = false;
        tile.spriteRenderer.color = new Color(1, 1, 1);
        currentSelectedTile = null;
    }

    // �������� �� ����������
    private void CheckMathes(Tile tile)
    {
        if (!tile.IsEmpty)
        {
            DeleteSprite(tile);

            if (isFindMantch)
            {
                isFindMantch = false;
                tile.spriteRenderer.sprite = null;
                SearchEmptySprite();
            }
        }
    }

    // ����� ������ ������
    protected void SearchEmptySprite()
    {
        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allTile[x, y].IsEmpty)
                {
                    // ���� ������ ������ ������ �������� ����������� ����� ����
                    // � ��������� �������� ������
                    CheckShiftDown(x, y);
                    CheckMathes(allTile[x, y]);
                }
            }
        }
    }
}
