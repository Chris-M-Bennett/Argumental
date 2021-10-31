﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player{
    public class PlayerOverworldControls : MonoBehaviour
    {
        private Vector3 _currentPosition;
        private bool _isRunning = false;
        [SerializeField] private float moveSpeed = 0.001f;
        [SerializeField] private float runSpeedDif = 0.004f;
        [SerializeField] private LayerMask opponentMask;
        [SerializeField] private Vector2 startPosition;
        // Start is called before the first frame update
        private void Start()
        {
            if (GameManager.newGame)
            {
                transform.position = startPosition;
                GameManager.newGame = false;
            }
            else
            {
                _currentPosition = transform.position;
                transform.position = new Vector2(PlayerPrefs.GetFloat("playerXPos", _currentPosition.x), 
                    PlayerPrefs.GetFloat("playerYPos", _currentPosition.y));
            }
        }

        // Update is called once per frame
        private void Update()
        {
            _currentPosition = transform.position;
            if (Input.GetAxis("Vertical") != 0)
            {//Moves the player on the vertical axis in the direction of the input
                _currentPosition.y += moveSpeed*Input.GetAxis("Vertical");
            }
            if (Input.GetAxis("Horizontal") != 0)
            {//Moves the player on the horizontal axis in the direction of the input
                _currentPosition.x += moveSpeed*Input.GetAxis("Horizontal");
            }
            if(Input.GetButtonDown("Run")){
                if(_isRunning){
                    moveSpeed -= runSpeedDif;
                }
                else{moveSpeed += runSpeedDif;}
                _isRunning = !_isRunning;
            }
            transform.position = _currentPosition;

            Collider2D hit = Physics2D.OverlapCircle(_currentPosition, 0.5f, opponentMask);
            if (Input.GetButtonDown("Activate") && hit){
                GameManager.currentOpponent = hit.gameObject;
                PlayerPrefs.SetFloat("playerXPos", _currentPosition.x);
                PlayerPrefs.SetFloat("playerYPos", _currentPosition.y);
                SceneManager.LoadSceneAsync("Debate");
            }
        }
    }
}
