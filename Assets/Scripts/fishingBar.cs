using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingBar : MonoBehaviour
{

    public Slider fishingSlider;
    
    public void SetHealth(float playerHealth)
    {
        fishingSlider.value = playerHealth;
    }
}
