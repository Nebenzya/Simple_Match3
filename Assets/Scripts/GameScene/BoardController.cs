using UnityEngine;

public class BoardController : MonoBehaviour
{
    private Tile activeSelection;

    public void CheckSelectionTile(Tile tile)
    {
        if (tile.IsSelect)
        {
            DeselectionTile(tile);
        }
        else
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

    private bool IsTileNearby(Tile tile)
    {
        var xdistans = System.Math.Abs(tile.transform.localPosition.x - activeSelection.transform.localPosition.x);
        var ydistans = System.Math.Abs(tile.transform.localPosition.y - activeSelection.transform.localPosition.y);
        return xdistans <= tile.transform.localScale.x || ydistans <= tile.transform.localScale.y;
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

    private void SwapTiles(Tile tile) => (tile.spriteRenderer.sprite, tile.ID, activeSelection.spriteRenderer.sprite, activeSelection.ID)
        = (activeSelection.spriteRenderer.sprite, activeSelection.ID, tile.spriteRenderer.sprite, tile.ID);
}
