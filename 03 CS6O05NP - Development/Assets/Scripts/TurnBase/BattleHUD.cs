using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI hP;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        playerName.text = unit.unitName;
        hP.text = unit.currentHp.ToString();
        hpSlider.maxValue = unit.maxHp;
        hpSlider.value = unit.currentHp;
    }

    public void SetHp(int hp)
    {
        hP.text = hp.ToString();
        hpSlider.value = hp;
    }
}
