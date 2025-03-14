using UnityEngine;
using UnityEngine.UI;

public enum DamageType { Fire, Ice, Normal, Lightning, Dark}

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]

//Inherits everything from ItemSO, then has everythin within.
public class WeaponSO : ItemSO
{
    //Base Values, can be changed
    public float minDagamge = 1;
    public float maxDagamge = 20;
    public DamageType damageType = DamageType.Normal;
}

