using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GlobalStateManager : MonoBehaviour
{
    public List<GameObject> Players = new List<GameObject>();

    List<int> PlayerScores = new List<int>();
    public List<Text> Texts = new List<Text>();

    void Start()
    {
        PlayerScores.Add(10);
        PlayerScores.Add(10);
        PlayerScores.Add(10);
        PlayerScores.Add(10);
    }

    public bool PlayerDied(int playerNumber)
    {
        PlayerScores[playerNumber] = PlayerScores[playerNumber] - 1 < 0 ? 0 : PlayerScores[playerNumber] - 1;
        Texts[playerNumber].text = PlayerScores[playerNumber].ToString();
        return PlayerScores[playerNumber] == 0;
    }
    public void PlayerLife(int playerNumber)
    {
        PlayerScores[playerNumber] = PlayerScores[playerNumber] + 1;
        Texts[playerNumber].text = PlayerScores[playerNumber].ToString();
    }

}
