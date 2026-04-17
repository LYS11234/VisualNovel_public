using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCGCollections : MonoBehaviour
{
    [SerializeField]
    private Image[] cgImages;
    void Start()
    {
        for (int i = 0; i < Database.instance.userData.cg.Length; i++) {
            if (Database.instance.userData.cg[i])
                cgImages[i].color = Color.white;
            else
                cgImages[i].color = new Color(0, 0, 0, 0.4f);
        }
    }
}
