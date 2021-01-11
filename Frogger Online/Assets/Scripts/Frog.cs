using UnityEngine;

public class Frog : MonoBehaviour
{
    public int movement_space = 5;
    private Rigidbody2D rb = null;
    public Animator animator;

    AudioSource audio;

    public float limit_down = -6.0f;
    public float limit_left = -8.73f;
    public float limit_right = 6.25f;
    public float limit_up = 6.0f;

    public float jump_time = 0.5f;
    public float dist = 5.0f;
    public float v_dist = 5.0f;

    private float aux_time = 0.0f;

    public enum dir
    {
        none,
        up,
        down,
        left,
        right
    };

    private dir dir_to_move = dir.none;
    public dir cant_move_to = dir.none;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (aux_time <= 0.0f)
        {
            animator.SetFloat("speed", 0.0f);
            animator.SetFloat("h_speed", 0.0f);

            //Movement
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < limit_up && cant_move_to != dir.up)
            {
                //rb.MovePosition(rb.position + Vector2.up * movement_space);
                animator.SetFloat("h_speed", 1);
                aux_time = jump_time;
                dir_to_move = dir.up;
                audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > limit_down && cant_move_to != dir.down)
            {
                //rb.MovePosition(rb.position + Vector2.down * movement_space);
                animator.SetFloat("h_speed", -1);
                aux_time = jump_time;
                dir_to_move = dir.down;
                audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > limit_left && cant_move_to != dir.left)
            {
                //rb.MovePosition(rb.position + Vector2.left * movement_space);
                animator.SetFloat("speed", -1);
                aux_time = jump_time;
                dir_to_move = dir.left;
                audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < limit_right && cant_move_to != dir.right)
            {
                //rb.MovePosition(rb.position + Vector2.right * movement_space);
                animator.SetFloat("speed", 1);
                aux_time = jump_time;
                dir_to_move = dir.right;
                audio.Play();
            }

        }

        if(aux_time > 0.0f)
        {
            aux_time -= Time.deltaTime;

            if (dir_to_move == dir.down)
            {
                Vector2 new_pos = rb.position + Vector2.down * v_dist * Time.deltaTime;
                rb.MovePosition(new Vector2(new_pos.x, Mathf.Clamp(new_pos.y, limit_down, limit_up)));
            }
            else if (dir_to_move == dir.up)
            {
                Vector2 new_pos = rb.position + Vector2.up * v_dist * Time.deltaTime;
                rb.MovePosition(new Vector2(new_pos.x, Mathf.Clamp(new_pos.y, limit_down, limit_up)));
                Debug.Log(new_pos);
            }
            else if (dir_to_move == dir.left)
            {
                Vector2 new_pos = rb.position + Vector2.left * dist * Time.deltaTime;
                rb.MovePosition(new Vector2(Mathf.Clamp(new_pos.x, limit_left, limit_right), new_pos.y));
            }
            else if (dir_to_move == dir.right)
            {
                Vector2 new_pos = rb.position + Vector2.right * dist * Time.deltaTime;
                rb.MovePosition(new Vector2(Mathf.Clamp(new_pos.x, limit_left, limit_right), new_pos.y));
            }
            

            if (aux_time <= 0.0f)
                dir_to_move = dir.none;
        }

     
    }
}
