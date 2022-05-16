using UnityEngine;

public class BoardController : MonoBehaviour
{
    private BoardController() { }
    public static BoardController instance;
    private void Awake()
    {
        instance = this;
    }

    private Tile activeSelection;

    public void CheckSelectionTile(Tile tile)
    {
        if (tile.IsSelect)
        {
            DeselectionTile(tile);
        }
        else
        {
            if (!tile.Move)
            {
                if (!tile.IsSelect && activeSelection == null)
                {
                    SelectionTile(tile);
                }
                else
                {
                    if (IsTileNearby(tile))
                    {
                        SwapTiles(tile);
                        DeselectionTile(activeSelection);
                    }
                    else
                    {
                        DeselectionTile(activeSelection);
                        SelectionTile(tile);
                    }
                }
            }
        }
    }

    private bool IsTileNearby(Tile tile)
    {
        // The search for the neighboring tile is due to its actual width or height
        float xDistance = System.Math.Abs(tile.transform.localPosition.x - activeSelection.transform.localPosition.x);
        float yDistance = System.Math.Abs(tile.transform.localPosition.y - activeSelection.transform.localPosition.y);

        // Tiles should not be diagonal neighbors
        if (xDistance == 0 || yDistance == 0)
        {
            if (xDistance != 0)
            {
                xDistance = (float)System.Math.Round(xDistance, 1);
                return tile.transform.localScale.x >  xDistance - tile.transform.localScale.x;
            }

            if (yDistance != 0)
            {
                yDistance = (float)System.Math.Round(yDistance, 1);
                return tile.transform.localScale.y > yDistance - tile.transform.localScale.y;
            }
        }
        return false;

    }

    private void SelectionTile(Tile tile)
    {
        tile.IsSelect = true;
        tile.spriteRenderer.color = new Color(.5f, .5f, .5f);
        activeSelection = tile;
    }

    private void DeselectionTile(Tile tile)
    {
        tile.IsSelect = false;
        tile.spriteRenderer.color = new Color(1, 1, 1);
        activeSelection = null;
    }

    private void SwapTiles(Tile tile)
    {
        (tile.spriteRenderer.sprite, tile.ID, activeSelection.spriteRenderer.sprite, activeSelection.ID)
                = (activeSelection.spriteRenderer.sprite, activeSelection.ID, tile.spriteRenderer.sprite, tile.ID);

        tile.Move = true;
        UIGameBoard.instance.Step();
    }
}
