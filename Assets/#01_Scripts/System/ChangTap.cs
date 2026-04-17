using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangTap : MonoBehaviour
{
    [SerializeField]
    private GameObject cgTap;
    [SerializeField]
    private Image cgTapButton;
    [SerializeField]
    private GameObject collectionsTap;
    [SerializeField]
    private Image collectionTapButton;

    [SerializeField]
    private Color openColor;
    [SerializeField]
    private Color closeColor;

    public void OpenCGTap()
    {
        collectionsTap.SetActive(false);
        cgTap.SetActive(true);
        cgTapButton.color = openColor;
        collectionTapButton.color = closeColor;

    }

    public void OpenColletionsTap()
    {
        collectionsTap.SetActive(true);
        cgTap.SetActive(false);
        collectionTapButton.color = openColor;
        cgTapButton.color = closeColor;
    }

    public void returnToMain()
    {
        Database.instance.destination = "#01_LobbyScene";
        SceneManager.LoadScene(Database.instance.destination);
    }
}
