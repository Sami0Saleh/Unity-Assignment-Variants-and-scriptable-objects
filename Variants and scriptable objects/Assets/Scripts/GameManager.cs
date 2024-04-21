using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // soldiers refs
    [SerializeField] private SoldierScript _soldierScriptA;
    [SerializeField] private SoldierScript _soldierScriptB;
    // KD Text
    [SerializeField] private TextMeshProUGUI _text_A_KD;
    [SerializeField] private TextMeshProUGUI _text_B_KD;
    // cameras 
    [SerializeField] private Camera _cameraA;
    [SerializeField] private Camera _cameraB;


    // private fields 
    private int _ADeathCount = 0;
    private int _BDeathCount = 0;
    private int _AKillCount = 0;
    private int _BKillCount = 0;

     void Update()
    {
        UpdateKD();
    }

    private void UpdateKD()
    {
        _ADeathCount = _soldierScriptA.deathCount;
        _BDeathCount = _soldierScriptB.deathCount;
        _AKillCount = _soldierScriptB.deathCount;
        _BKillCount = _soldierScriptA.deathCount;
        _text_A_KD.text = $"Veriant A  Kills {_AKillCount} Deaths {_ADeathCount}";
        _text_B_KD.text = $"Veriant B  Kills {_BKillCount} Deaths {_BDeathCount}";
    }

    // gets called by switch camera button
    public void SwitchCamera()
    {
        if(_cameraA.isActiveAndEnabled == true)
        {
            _cameraA.gameObject.SetActive(false);
            _cameraB.gameObject.SetActive(true);
        }
        else if (_cameraB.isActiveAndEnabled == true)
        {
            _cameraB.gameObject.SetActive(false);
            _cameraA.gameObject.SetActive(true);
        }
    }
}
