using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBar : MonoBehaviour
{
    public Slider slider;

    private float timeLeft;

    public void StartPotionTimer(int secondsTillEffectGone)
    {
        slider.maxValue = secondsTillEffectGone;
        timeLeft = secondsTillEffectGone;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft;
        }
    }

}
