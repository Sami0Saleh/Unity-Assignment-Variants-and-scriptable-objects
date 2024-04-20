using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadSceneAsyncOperation;
    [SerializeField] GameObject WheelSprite;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI ButtonText;
    [SerializeField] GameObject ReadyText;

    public float _rotationSpeed = 10;

    private bool userClickedOnSpin = false;
    private bool userClickedOK = false;
    private bool _canStartNextScene = false;
    private int _framesToLoad;

    private void Update()
    {
        if (userClickedOK)
        {
            SlowTheWheel();

            if (_rotationSpeed > 0)
            {   _rotationSpeed -= 0.001f;
            Debug.Log(_rotationSpeed); }
        else loadSceneAsyncOperation.allowSceneActivation = true; ;
        }
        else
        {
            SpinTheWheel();
            if (loadSceneAsyncOperation != null)
            {
                _rotationSpeed = loadSceneAsyncOperation.progress; Debug.Log(_rotationSpeed);
                slider.value = loadSceneAsyncOperation.progress;
            }
        }

        if (_canStartNextScene && Input.anyKeyDown)
        {
            userClickedOK = true;
           
            if (_rotationSpeed <= 0) { loadSceneAsyncOperation.allowSceneActivation = true; }
            
        }
    }
    public void LoadNextScene()
    {
        if (!userClickedOnSpin)
        {
            userClickedOnSpin = true;
            loadSceneAsyncOperation = SceneManager.LoadSceneAsync("Scene B");
            loadSceneAsyncOperation.allowSceneActivation = false;
            StartCoroutine(LoadAsyncScene());
        }
    }

    IEnumerator LoadAsyncScene()
    {

        yield return new WaitUntil(() => loadSceneAsyncOperation.progress >= 0.9f);
        _canStartNextScene = true;
        ReadyText.SetActive(true);
        ButtonText.text = "Start Next Scene";
    }

    private void SpinTheWheel()
    {
        WheelSprite.transform.Rotate(0, 0, _rotationSpeed);
    }

    private void SlowTheWheel()
    {
        WheelSprite.transform.Rotate(0, 0, _rotationSpeed);
    }
    }
