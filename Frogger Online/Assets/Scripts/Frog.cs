using UnityEngine;

public class Frog : MonoBehaviour
{
    public int movement_space = 5;
    private Rigidbody2D rb = null;

    public float limit_down = -6.0f;
    public float limit_left = -8.73f;
    public float limit_right = 6.25f;
    public float limit_up = 6.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movement
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < limit_up)
            rb.MovePosition(rb.position + Vector2.up * movement_space);

        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > limit_down)
            rb.MovePosition(rb.position + Vector2.down * movement_space);

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > limit_left)
            rb.MovePosition(rb.position + Vector2.left * movement_space);

        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < limit_right)
            rb.MovePosition(rb.position + Vector2.right * movement_space);
    
    }
}
