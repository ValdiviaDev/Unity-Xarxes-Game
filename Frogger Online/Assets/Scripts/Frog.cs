using UnityEngine;

public class Frog : MonoBehaviour
{
    public int movement_space = 5;
    private Rigidbody2D rb = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb.MovePosition(rb.position + Vector2.up * movement_space);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            rb.MovePosition(rb.position + Vector2.down * movement_space);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            rb.MovePosition(rb.position + Vector2.left * movement_space);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            rb.MovePosition(rb.position + Vector2.right * movement_space);
    
    }
}
