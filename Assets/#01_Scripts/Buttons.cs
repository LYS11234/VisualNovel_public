using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private int loveScore;



    //Choice System
    public void ChoiceSystemForFirstHeroin()
    {
        Database.instance.nowPlayer.affection_first += loveScore;
        Debug.LogError($"First Heroin Loves you {Database.instance.nowPlayer.affection_first}!");
    }

    public void ChoiceSystemForSecondHeroin() 
    {
        Database.instance.nowPlayer.affection_second += loveScore;
    }

    public void ChoiceSystemForThirdHeroin()
    {
        Database.instance.nowPlayer.affection_third += loveScore;
    }
}
