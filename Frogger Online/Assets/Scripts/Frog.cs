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

    private Vector2 new_pos;

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
                //Movement
                dir_to_move = dir.up;
                new_pos = rb.position + Vector2.up * v_dist;

                //Animation
                animator.SetFloat("h_speed", 1);
                aux_time = jump_time;

                //Audio
                audio.Play();
                
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > limit_down && cant_move_to != dir.down)
            {
                //Movement
                dir_to_move = dir.down;
                new_pos = rb.position + Vector2.down * v_dist;

                //Animation
                animator.SetFloat("h_speed", -1);
                aux_time = jump_time;

                //Audio
                audio.Play();

            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > limit_left && cant_move_to != dir.left)
            {
                //Movement
                dir_to_move = dir.left;
                new_pos = rb.position + Vector2.left * dist;

                //Animation
                animator.SetFloat("speed", -1);
                aux_time = jump_time;
                
                //Audio
                audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < limit_right && cant_move_to != dir.right)
            {
                //Movement
                dir_to_move = dir.right;
                new_pos = rb.position + Vector2.right * dist;

                //Animation
                animator.SetFloat("speed", 1);
                aux_time = jump_time;
                
                //Audio
                audio.Play();
            }

        }

        if(aux_time > 0.0f)
        {
            aux_time -= Time.deltaTime;

            float time_norm = Mathf.Lerp(jump_time, 0.0f, aux_time);
            Vector2 interPos = Vector2.Lerp(transform.position, new_pos, time_norm);

            if (dir_to_move == dir.down || dir_to_move == dir.up)
            {
                rb.MovePosition(new Vector2(interPos.x, Mathf.Clamp(interPos.y, limit_down, limit_up)));
            }

            else if (dir_to_move == dir.left || dir_to_move == dir.right)
            {
                rb.MovePosition(new Vector2(Mathf.Clamp(interPos.x, limit_left, limit_right), interPos.y));
            }
     
            if (aux_time <= 0.0f)
                dir_to_move = dir.none;
        }

     
    }
}
