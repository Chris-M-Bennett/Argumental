using UnityEngine;

namespace Opponents
{
    public class DebateValuesScript : MonoBehaviour  
    {
        [Header("Mouse over field names for description of what to add")]
        [Tooltip("Name of this debater")]public string debaterName;
        [Tooltip("Level of this debater. DO NOT CHANGE ON PLAYER!")]public int debaterLevel;
        [Tooltip("Damage this debater does to their opponent's ES")]public int debaterDamage;
        [Tooltip("Maximum amount of ES this debater can have")]public int maxES;
        [HideInInspector] public int currentES = 50;
        [Tooltip("Number for the emotion this debater starts in. Happy=0 Angry=1 Sad=2 Anxiety=3." +
                 "DO NOT CHANGE ON PLAYER!")]public int emotionInt;
        [Tooltip("Thresholds Scriptable Object '{}' for this opponent. Create from Assets task menu."+
            " The player does not have one")]public OpponentThresholds emotionThresholds;
        [Tooltip("Amounts of each emotion opponent starts with. DO NOT CHANGE ON PLAYER")]public int[] emotAmounts = new int[5];
        private SpriteRenderer _spriteRenderer;


        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (CompareTag("Player")) {
                maxES = PlayerPrefs.GetInt("playerMax", 100);
                currentES = PlayerPrefs.GetInt("playerES", maxES);
                debaterLevel = PlayerPrefs.GetInt("level", 1);
                
                emotAmounts[0] = PlayerPrefs.GetInt("playerHappy", 2);
                emotAmounts[1] = PlayerPrefs.GetInt("playerSad", 2);
                emotAmounts[2] = PlayerPrefs.GetInt("playerAngry", 2);
                emotAmounts[3] = PlayerPrefs.GetInt("playerConfident", 2);
                emotAmounts[4] = PlayerPrefs.GetInt("playerAfraid", 2);
            }
            //emotionInt = Random.Range(0,3);
            if(emotionThresholds){
                GetComponent<SpriteRenderer>().sprite = emotionThresholds.emotionSprites[emotionInt];
            }
        }

        public void CheckThreshold(int prevES)
        {
            var thresh = emotionThresholds.thresholds;
            for (int i = 1; i < thresh.Count-1; i++)
            {
                if (prevES >= thresh[i] && currentES < thresh[i])
                {
                    var highest = 0;
                    var equals = 0;
                    for (int emot = 0; emot < emotAmounts.Length; emot++)
                    {
                        //Compares emotion's amount to emotion currently recorded as highest
                        if (emotAmounts[emot]>highest)
                        {
                            highest = emotAmounts[emot];
                        //Records number of emotion amounts that are equal    
                        }else if (emotAmounts[emot] == highest)
                        {
                            equals++;
                        }

                        /*int prevEmot = emotionInt;
                        while(emotionInt == prevEmot){
                            emotionInt = Random.Range(0, 3);
                        }*/
                    }
                    
                    //If two or more emotion amounts are equal, choose one at random
                    if (equals > 0)
                    {
                        highest = Random.Range(0, equals);
                    }
                        
                    //Change emotion to whichever has the highest amount
                    emotionInt = highest;
                }
            }
            _spriteRenderer.sprite = emotionThresholds.emotionSprites[emotionInt];
        }
    }
}
