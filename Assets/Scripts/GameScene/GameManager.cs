using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardSetting
{
    public int width, height;
    public Tile prefab;
    public List<Sprite> sprites;
}

public class GameManager : MonoBehaviour
{
    [Header("Game Setting")]
    public BoardSetting setting;

    // Start is called before the first frame update
    void Start()
    {
        Board.instance.StartNewGame(setting);
    }

    // Update is called once per frame
    void Update()
    {
        Board.instance.CheckBoard();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (raycastHit != false && !UIGameBoard.instance.gameOverPanel.activeSelf)
            {
                BoardController.instance.CheckSelectionTile(raycastHit.collider.gameObject.GetComponent<Tile>());
            }
        }
    }
}
