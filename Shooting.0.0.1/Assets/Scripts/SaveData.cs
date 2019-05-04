using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static int Score;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }


    public static void setScore(int input)
    {
        Score = input;
    }

    public static int getScore()
    {
        return Score;
    }
}
