using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public int powerAct;
    public int boostMultiplier;
    public int power;
    public int autoClickPowerAct;
    public int autoClickPower;
    public float critChance;
    public float critDMG;

    public Action OnClickEvents;
    public Action OnAutoEvents;

    // currency
    private int gold;
    public int Gold { get { return gold; } set {  gold = value; } }
    private float goldDropPercentage = 100.0f; // 100% = 100
    private int diamond;
    public int Diamond { get {  return diamond; } set {  diamond = value; } }
    private float diamondDropPercentage = 0.01f;

    public void Init()
    {
//        GameObject go = Util.FindChild(Managers.Instance.gameObject, "@GameManager");
//        if(go == null)
//        {
//            go = new GameObject { name = "@GameManager" };
//            go.transform.SetParent(Managers.Instance.gameObject.transform);
//            go.AddComponent<GameManager>();
//        }
        power = 1;
        powerAct = power;
        boostMultiplier = 2;
        autoClickPower = 10;
        autoClickPowerAct = autoClickPower;
        critChance = 0f;
        critDMG = 1f;
        OnClickEvents += DropGold;
        OnClickEvents += DropDiamond;
    }

    public void CallClickEvent(PointerEventData evt)
    {
        OnClickEvents?.Invoke();
    }
    public void CallAutoEvent(PointerEventData evt)
    {
        OnAutoEvents?.Invoke();
    }
    private void DropGold()
    {
        if (UnityEngine.Random.Range(0f, 100f) < goldDropPercentage)
            gold++;
    }
    private void DropDiamond()
    {
        if( UnityEngine.Random.Range(0f,100f) < diamondDropPercentage)
            diamond++;
    }
}
