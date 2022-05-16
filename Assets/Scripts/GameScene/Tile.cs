using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // ID is equal to Sprite index on the list (not unique)
    // this allows us to quickly test for equality two tiles
    public int ID { get; set; }
    public bool IsSelect { get; set; }
    public bool IsEmpty => spriteRenderer.sprite == null;

    private bool _move;
    public bool Move
    {
        get { return _move; }
        set
        {
            _move = value;
            if (_move == true)
            {
                if (spriteRenderer.sprite != null)
                {
                    spriteRenderer.color = new Color(.7f, .7f, .7f);
                }
                
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1);
            }
        }
    }

}
