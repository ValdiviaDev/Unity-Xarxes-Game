using UnityEngine;
using Photon.Pun;

public class Frog : MonoBehaviour
{
    public int movement_space = 5;
    private Rigidbody2D rb = null;
    private Animator animator;

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
    private dir cant_move_to = dir.none;

    private Vector2 new_pos;

    public LayerMask layer;
    public float ray_length = 2.0f;


    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            Frog.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (aux_time <= 0.0f)
        {
            animator.SetFloat("speed", 0.0f);
            animator.SetFloat("h_speed", 0.0f);

            //Movement
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < limit_up && cant_move_to != dir.up)
            {
                //Raycast to look for other frog
                RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.up, ray_length, layer.value);

                if (!ray)
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
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > limit_down && cant_move_to != dir.down)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, ray_length, layer.value);

                if (!ray)
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
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > limit_left && cant_move_to != dir.left)
            {
                //Raycast to look for other frog
                RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, ray_length, layer.value);

                if (!ray)
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
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < limit_right && cant_move_to != dir.right)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, ray_length, layer.value);

                if (!ray)
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

        }


    }

    void FixedUpdate()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if(aux_time > 0.0f)
        {
            aux_time -= Time.fixedDeltaTime;

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
