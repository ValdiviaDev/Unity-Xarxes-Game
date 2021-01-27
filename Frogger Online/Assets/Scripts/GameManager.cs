﻿using System;
using System.Collections;


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.Cotxe11.FroggerOnline
{
    public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region Public Variables

        [Tooltip("The prefab to use for representing the player")]
        public GameObject player1Prefab;
        public GameObject player2Prefab;

        public GameObject player1NamePrefab;
        public GameObject player2NamePrefab;

        public Transform player1Spawn;
        public Transform player2Spawn;

        bool p1Rematch = false;
        bool p2Rematch = false;

        #endregion

        private int lastLevelLoaded = 0;

        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(PhotonNetwork.IsMasterClient ? p1Rematch : p2Rematch);
            }
            else
            {
                if (!photonView.IsMine)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        p2Rematch = (bool)stream.ReceiveNext();
                    }
                    else p1Rematch = (bool)stream.ReceiveNext();
                }

            }
        }


        #endregion

        #region MonoBehaviour CallBacks

        public void Update()
        {
            if (PhotonNetwork.IsMasterClient && p1Rematch && p2Rematch)
            {
                PhotonNetwork.LoadLevel("ReMatch");
                p1Rematch = p2Rematch = false;
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

        public void Rematch()
        {
            if (PhotonNetwork.IsMasterClient) p1Rematch = true;
            else p2Rematch = true;
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

            if (lastLevelLoaded == 2)
            {
                PhotonNetwork.Destroy(Frog.LocalPlayerInstance);
            }


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        #endregion

        #region Private Methods

        private void OnLevelWasLoaded(int level)
        {
            lastLevelLoaded = level;

            if(level == 3) {
                LoadArena();
                return;
            }

            if (level != 2) return;


            if (Frog.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Instantiate(this.player1Prefab.name, player1Spawn.position, Quaternion.identity, 0);
                else
                    PhotonNetwork.Instantiate(this.player2Prefab.name, player2Spawn.position, Quaternion.identity, 0);
            }
            else
            {
                Frog.LocalPlayerInstance.transform.position = (PhotonNetwork.IsMasterClient) ? player1Spawn.position : player2Spawn.position;
            }

            GameObject uiPref = PhotonNetwork.IsMasterClient ? player1NamePrefab : player2NamePrefab;
            RectTransform preRectTransform = uiPref.GetComponent<RectTransform>();
            GameObject tmp = PhotonNetwork.Instantiate(uiPref.name, Vector3.zero, Quaternion.identity);

            // Get the source and target RectTransform components
            RectTransform rectTransform = tmp.GetComponent<RectTransform>();

            // These four properties are to be copied
            rectTransform.anchorMin = preRectTransform.anchorMin;
            rectTransform.anchorMax = preRectTransform.anchorMax;
            rectTransform.anchoredPosition = preRectTransform.anchoredPosition;
            rectTransform.sizeDelta = preRectTransform.sizeDelta;

           // tmp.GetComponent<RectTransform>().position = prePos;
            tmp.GetComponent<Text>().text = PhotonNetwork.NickName;
        }

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