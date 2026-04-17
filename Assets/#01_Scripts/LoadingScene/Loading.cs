using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Slider slider;
    private AsyncOperation op;
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Text loadPercent;
    private float currentTime;
    private float changeTime;


    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        changeTime = 0.3f;
    }

    void Update()
    {
        
        if (SceneManager.sceneCount < 2)
        {
            op = SceneManager.LoadSceneAsync(Database.instance.destination);
            op.allowSceneActivation = false;
        }

        if (SceneManager.sceneCount >= 2)
        {
            float timer = 0.0f;
            StartCoroutine(LoadNextScene(timer, op));
        }
        loadPercent.text = (slider.value * 100f).ToString("F2") + "%";
        if (currentTime < changeTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            LoadingText();
            currentTime = 0;
        }

    }

    IEnumerator LoadNextScene(float _timer, AsyncOperation _op)
    {
        while (!_op.isDone)
        {
            yield return null;
            _timer += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.value, _op.progress, _timer);
            if (_op.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(slider.value, _op.progress, _timer);
                if (slider.value >= 0.9f)
                    _timer = 0.0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, _timer);
                if (slider.value == 1f)
                {
                    _op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }

    private void LoadingText()
    {
        if (loadingText.text != "Loading...")
            loadingText.text += ".";
        else
            loadingText.text = "Loading";
    }
}