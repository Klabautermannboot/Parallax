using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cooldown : MonoBehaviour
{
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
  public void SetMaxCooldown(float cooldown)
  {
      slider.maxValue = cooldown;
      slider.value = cooldown;
      fill.color = gradient.Evaluate(1f);

  }

  public void  SetCooldown(float cooldown)
  {
      slider.value = cooldown;
      fill.color = gradient.Evaluate(slider.normalizedValue);
  }
}