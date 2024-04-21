using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SoldierScriptableObject : ScriptableObject
{
    [SerializeField] public float _startingHealth;
   
    [SerializeField] public float _baseSpeed;
    
    [SerializeField] public GameObject Bullet;
}
