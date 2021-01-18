using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.Cotxe11.FroggerOnline
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Public Variables


        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public Transform player1Spawn;
        public Transform player2Spawn;

        public LayerMask layerPlayer1;
        public LayerMask layerPlayer2;

        public Material player1Material;
        public Material player2Material;
        #endregion

        #region MonoBehaviour CallBacks

        private void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (Frog.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    GameObject tmp;
                    if (PhotonNetwork.IsMasterClient)
                    {
                        (tmp=PhotonNetwork.Instantiate(this.playerPrefab.name, player1Spawn.position, Quaternion.identity, 0)).GetComponent<Frog>().layer = layerPlayer2;
                        tmp.GetComponent<SpriteRenderer>().material = player1Material;
                        tmp.layer = layerPlayer1.value;
                    }
                    else
                    {
                        (tmp=PhotonNetwork.Instantiate(this.playerPrefab.name, player2Spawn.position, Quaternion.identity, 0)).GetComponent<Frog>().layer = layerPlayer1;
                        tmp.GetComponent<SpriteRenderer>().material = player2Material;
                        tmp.layer = layerPlayer2.value;
                    }
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        #endregion



        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Photon Callbacks


        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }


        #endregion
    }
}