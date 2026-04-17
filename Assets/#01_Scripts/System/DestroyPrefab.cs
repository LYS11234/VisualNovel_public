using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
    [SerializeField]
    private bool isPauseMenu;

    public void DestroyParent()
    {
        Time.timeScale = 1;
        if(isPauseMenu)
        {
            SoundManager.instance.voice_.Play();
            SoundManager.instance.bgm_.Play();
        }
        Destroy(gameObject);


    }
}
