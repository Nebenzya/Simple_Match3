using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool IsSelect { get; set; }
    public int ID { get; set; }
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
