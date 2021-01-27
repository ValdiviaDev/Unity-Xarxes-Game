using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FrogDeath : MonoBehaviourPunCallbacks, IPunObservable
{
    private Frog f;
    private Animator animator;
    private bool dead = false;
    private bool floor = false;
    private bool water = false;
    private bool dying = false;
    private bool JustDiedCar = false;
    private bool JustDiedWater = false;

    private int num_of_floors = 0; //Counts num of floor colliders the frog collides with. If <=0 and touching water, then the frog dies

    [SerializeField] private AudioSource death_car;
    [SerializeField] private AudioSource death_water;

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(JustDiedCar);
            JustDiedCar = false;

            stream.SendNext(JustDiedWater);
            JustDiedWater = false;
        }
        else
        {
            if (!photonView.IsMine)
            {
                if ((bool)stream.ReceiveNext())
                    death_car.Play();

                if ((bool)stream.ReceiveNext())
                    death_water.Play();
            }
              
        }
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
       f = GetComponent<Frog>();
       animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (dead || (water && !floor && num_of_floors <= 0))
            {
                //Debug.Log("Dead, num floors, "+ num_of_floors);
                animator.SetBool("dead", true);
                dying = true;
                if (f)
                    f.FrogDie();
                else
                    Debug.Log("Can't find Frog component!!!");

                if (water)
                {
                    death_water.Play();
                    JustDiedWater = true;
                }

            }
        }
        
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyCol"))
        {
            death_car.Play();
            JustDiedCar = dead = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("WaterCol"))
        {
            water = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorCol"))
        {
            floor = true;
          
            num_of_floors++;
            Debug.Log("enter floor, " + num_of_floors);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyCol"))
        {
            dead = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("WaterCol"))
        {
            water = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorCol"))
        {
            floor = false;
            num_of_floors--;
            Debug.Log("leave floor, " + num_of_floors);
        }

    }


    public void SetDying(bool set)
    {
        dying = set;
    }
}
