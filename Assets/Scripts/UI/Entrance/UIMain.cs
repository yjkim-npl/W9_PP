using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Numerics;
public class UIMain : UIBase
{
    // Click 
    //protected int cnt = 0; 
    [SerializeField] protected BigInteger cnt;
    //public int Count { get { return cnt; } set { cnt = value; } }
    public BigInteger Count { get { return cnt; } set { cnt = value; } }
    protected float crit = 0f;

    // Common
    private int order = 0;
    private int capA = 'A';
    enum Buttons
    {
        ClickBtn,
        PushBtn,
        PullBtn,
        AutoBtn,
        BoostBtn,
    }
    enum Texts
    {
        ClickCnt,
        AutoTxt,
        GoldTxt,
        DiamondTxt,
    }
    public virtual void Awake()
    {
        Managers.UI.SetDic(this);
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Managers.Game.OnClickEvents += Increase;
        Managers.Game.OnAutoEvents += AutoRun;

        Get<Button>((int)Buttons.ClickBtn).gameObject.BindEvent(Managers.Game.CallClickEvent);
        Get<Button>((int)Buttons.PushBtn).gameObject.BindEvent(Push);
        Get<Button>((int)Buttons.PullBtn).gameObject.BindEvent(Pull);
        Get<Button>((int)Buttons.AutoBtn).gameObject.BindEvent(Managers.Game.CallAutoEvent);
        Get<Button>((int)Buttons.BoostBtn).gameObject.BindEvent(BoostAction);
        Get<TextMeshProUGUI>((int)Texts.ClickCnt).text = $"{cnt}";
    }

    private void BoostAction(PointerEventData evt)
    {
        if (cnt < 100)
            return;
        StartCoroutine(Boost());
    }

    private IEnumerator Boost()
    {
        Get<Button>((int)Buttons.BoostBtn).transform.parent.gameObject.SetActive(false);
        Managers.Game.powerAct = Managers.Game.power * Managers.Game.boostMultiplier;
        Managers.Game.autoClickPowerAct = Managers.Game.autoClickPower * Managers.Game.boostMultiplier;
        UpdateClick();
        yield return new WaitForSeconds(10f);
        Managers.Game.powerAct = Managers.Game.power;
        Managers.Game.autoClickPowerAct = Managers.Game.autoClickPower;
        Get<Button>((int)Buttons.BoostBtn).transform.parent.gameObject.SetActive(true);
    }

    private void Increase()
    {
        if (UnityEngine.Random.Range(0f, 1f) < Managers.Game.critChance)
            cnt += (int)(Managers.Game.powerAct * Managers.Game.critDMG);
        else
            cnt += Managers.Game.powerAct;
        UpdateClick();
    }
    private void AutoRun()
    {
        if (Managers.Game.autoClickPowerAct <= 0)
            return;
        StartCoroutine(Auto());
    }
    private IEnumerator Auto()
    {
        Get<Button>((int)Buttons.AutoBtn).gameObject.SetActive(false);
        for(int i= 0; i<60; i++)
        {
            cnt += Managers.Game.autoClickPowerAct;
            UpdateClick();
            Get<TextMeshProUGUI>((int)Texts.AutoTxt).text = $"+ {(Managers.Game.autoClickPowerAct).ToString("N2")}\n clicks / s";
            yield return new WaitForSeconds(1.0f);
        }
        Get<Button>((int)Buttons.AutoBtn).gameObject.SetActive(true);
        Get<TextMeshProUGUI>((int)Texts.AutoTxt).text = $"+ 0 clicks / s";
    }
    private void Push(PointerEventData evt)
    {
        Debug.Log(cnt);
        BigInteger compBigInt = new BigInteger();
        compBigInt = (BigInteger)Mathf.Pow(10, order + 1);
        if(BigInteger.Compare(cnt,compBigInt) == 1 )
        //if (cnt / (int)Math.Pow(10,order+1) > 1)
            order++;
        UpdateClick();
    }
    private void Pull(PointerEventData evt)
    {
        if (order > 0)
            order--;
        UpdateClick();
    }

    public void UpdateClick()
    {
        if(order == 0)
            Get<TextMeshProUGUI>((int)Texts.ClickCnt).text = $"{cnt}";
        else
        {
            Get<TextMeshProUGUI>((int)Texts.ClickCnt).text = $"{(cnt/(int)(Math.Pow(10,order))).ToString("N2")} {(char)(capA+order-1)}";
        }
        Get<TextMeshProUGUI>((int)Texts.GoldTxt).text = $"{Managers.Game.Gold} G";
        Get<TextMeshProUGUI>((int)Texts.DiamondTxt).text = $"{Managers.Game.Diamond}";
    }
}