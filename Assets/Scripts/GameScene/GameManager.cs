using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardSetting
{
    public int width, height;
    public Tile prefab;
    public List<Sprite> sprites;
}

public class GameManager : Board
{
    [Header("Настройки игрового поля")]
    public BoardSetting setting;

    private Board _board;
    private BoardController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.AddComponent<BoardController>();
        _board = gameObject.AddComponent<Board>();
        _board.StartNewGame(setting);
    }

    // Update is called once per frame
    void Update()
    {
        _board.CheckBoard();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (raycastHit != false)
            {
                _controller.CheckSelectionTile(raycastHit.collider.gameObject.GetComponent<Tile>());
            }
        }
    }
}
