using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System.Numerics;
public class Shop : UIBase
{
    float[] prices = new float[4] {1,10,100,200};
    float[] values = new float[4] {1,1,0.01f,0.01f};
    enum ButtonsShop
    {
        // Shop
        Upgrade=0,
        AutoClick,
        CritChance,
        CritDamage,
    }
    enum TextsShop
    {
        UpgradePrice,
        AutoClickPrice,
        CritChancePrice,
        CritDamagePrice,

        UpgradeDetail,
        AutoClickDetail,
        CritChanceDetail,
        CritDamageDetail,
    }
    public void Awake()
    {
        Managers.UI.SetDic(this);
    }
    private void Start()
    {
        Init();
        UpdateUpgradeText();
        UpdateAutoClickText();
        UpdateCritChanceText();
        UpdateCritDamageText();
    }
    public new void Init()
    {
        Bind<Button>(typeof(ButtonsShop));
        Bind<TextMeshProUGUI>(typeof(TextsShop));

        Get<Button>((int)ButtonsShop.Upgrade).gameObject.BindEvent(BuyUpgrade);
        Get<Button>((int)ButtonsShop.AutoClick).gameObject.BindEvent(BuyAutoClick);
        Get<Button>((int)ButtonsShop.CritChance).gameObject.BindEvent(BuyCritChance);
        Get<Button>((int)ButtonsShop.CritDamage).gameObject.BindEvent(BuyCritDamage);
    }
    private void BuyUpgrade(PointerEventData evt)
    {
        if(Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count < (BigInteger)prices[(int)ButtonsShop.Upgrade])
            return;
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count -= (int)prices[(int)ButtonsShop.Upgrade];
        Managers.Game.power += (int)values[(int)ButtonsShop.Upgrade];
        Managers.Game.powerAct = Managers.Game.power;
        values[(int)ButtonsShop.Upgrade] *= 2f;
        prices[(int)ButtonsShop.Upgrade] *= 3f;
        UpdateUpgradeText();

    }

    private void UpdateUpgradeText()
    {
        Get<TextMeshProUGUI>((int)TextsShop.UpgradeDetail).text = 
            "+" + values[(int)ButtonsShop.Upgrade].ToString("N0");
        Get<TextMeshProUGUI>((int)TextsShop.UpgradePrice).text = 
            prices[(int)ButtonsShop.Upgrade].ToString("N1") + " clicks";
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name)?.UpdateClick();
    }

    private void BuyAutoClick(PointerEventData data)
    {
        if (Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count < (BigInteger)prices[(int)ButtonsShop.AutoClick])
            return;
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count -= (int)prices[(int)ButtonsShop.AutoClick];
        Managers.Game.autoClickPower += (int)values[(int)ButtonsShop.AutoClick];
        Managers.Game.autoClickPowerAct += (int)values[(int)ButtonsShop.AutoClick];
        values[(int)ButtonsShop.AutoClick] *= 2f;
        prices[(int)ButtonsShop.AutoClick] *= 3f;
        
        UpdateAutoClickText();
    }

    private void UpdateAutoClickText()
    {
        Get<TextMeshProUGUI>((int)TextsShop.AutoClickDetail).text =
            "+" + values[(int)ButtonsShop.AutoClick].ToString("N0");
        Get<TextMeshProUGUI>((int)TextsShop.AutoClickPrice).text =
            prices[(int)ButtonsShop.AutoClick].ToString("N1") + " clicks";
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name)?.UpdateClick();
    }
    private void BuyCritChance(PointerEventData data)
    {
        if(Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count < (BigInteger)prices[(int)ButtonsShop.CritChance])
            return;
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count -= (int)prices[(int)ButtonsShop.CritChance];
        Managers.Game.critChance += values[(int)ButtonsShop.CritChance];
        values[(int)ButtonsShop.CritChance] *= 1.2f;
        prices[(int)ButtonsShop.CritChance] *= 1.5f;
        UpdateCritChanceText();
    }

    private void UpdateCritChanceText()
    {
        Get<TextMeshProUGUI>((int)TextsShop.CritChanceDetail).text =
            "+" + (values[(int)ButtonsShop.CritChance]).ToString("N1") + " %";
        Get<TextMeshProUGUI>((int)TextsShop.CritChancePrice).text = 
            prices[(int)ButtonsShop.CritChance].ToString("N1") + " clicks";
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name)?.UpdateClick();
    }

    private void BuyCritDamage(PointerEventData data)
    {
        if (Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count < (BigInteger)prices[(int)ButtonsShop.CritDamage])
            return;
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name).Count -= (int)prices[(int)ButtonsShop.CritDamage];
        Managers.Game.critDMG += values[(int)ButtonsShop.CritDamage];
        values[(int)ButtonsShop.CritDamage] *= 1.2f;
        prices[(int)ButtonsShop.CritDamage] *= 1.5f;
        UpdateCritDamageText();
    }

    private void UpdateCritDamageText()
    {
        
        Get<TextMeshProUGUI>((int)TextsShop.CritDamageDetail).text = 
            "+" + (values[(int)ButtonsShop.CritDamage]).ToString("N1") + " %";
        Get<TextMeshProUGUI>((int)TextsShop.CritDamagePrice).text = 
            prices[(int)ButtonsShop.CritDamage].ToString("N1") + " clicks";
        Managers.UI.GetDic<UIMain>(typeof(UIMain).Name)?.UpdateClick();
    }
}