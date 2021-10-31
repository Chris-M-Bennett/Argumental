﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameObject currentOpponent;
    [HideInInspector] public static bool newGame;

    public static Dictionary<int, int> EmotionStrengths = new Dictionary<int, int>();
    // Start is called before the first frame update
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
        
        EmotionStrengths.Add(0,1);
        EmotionStrengths.Add(1,2);
        EmotionStrengths.Add(2,3);
        EmotionStrengths.Add(3,4);
        EmotionStrengths.Add(4,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
