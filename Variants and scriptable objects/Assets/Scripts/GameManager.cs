using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] SoldierScript SoldierScriptA;
    [SerializeField] SoldierScript SoldierScriptB;
    [SerializeField] TextMeshProUGUI A_KD;
    [SerializeField] TextMeshProUGUI B_KD;

    [SerializeField] Camera CameraA;
    [SerializeField] Camera CameraB;

    private int _ADeathCount = 0;
    private int _BDeathCount = 0;

    private int _AKillCount = 0;
    private int _BKillCount = 0;

 
    // Update is called once per frame
    void Update()
    {
        _ADeathCount = SoldierScriptA.deathCount;
        _BDeathCount = SoldierScriptB.deathCount;
        _AKillCount = SoldierScriptB.deathCount;
        _BKillCount = SoldierScriptA.deathCount;
        A_KD.text = $"Veriant A  Kills {_AKillCount} Deaths {_ADeathCount}";
        B_KD.text = $"Veriant B  Kills {_BKillCount} Deaths {_BDeathCount}";
    }

    public void SwitchCamera()
    {
        if(CameraA.isActiveAndEnabled == true)
        {
            CameraA.gameObject.SetActive(false);
            CameraB.gameObject.SetActive(true);
        }
        else if (CameraB.isActiveAndEnabled == true)
        {
            CameraB.gameObject.SetActive(false);
            CameraA.gameObject.SetActive(true);
        }
    }
}
