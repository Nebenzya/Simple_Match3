using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardSetting
{
    public int width, height;
    public Tile tile;
    public List<Sprite> sprites; 
}

public class GameManager : MonoBehaviour
{
    public BoardSetting boardSetting;

    void Start()
    {
        BoardController.instance.SetValue(boardSetting.width, boardSetting.height, boardSetting.tile, boardSetting.sprites);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (raycastHit != false)
            {
               BoardController.instance.CheckSelectionTile(raycastHit.collider.gameObject.GetComponent<Tile>());
            }
        }
    }
}
