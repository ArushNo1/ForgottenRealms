using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public float currentHealth;
    public Slider slider;
    public Gradient colorGradient;
    public Image fillBar;
    public bool heartBar = false;
    public Sprite[] heartImages;
    public List<Image> hearts;
    private float maxHealth;
    private bool setAlr = false;
    public GameObject heartContainer;
    public Transform heartBarTransform;

    public void SetMaxHealth(float amount)
    {
        if (heartBar)
        {
            MaxHeartBar(amount);
        }
        else
        {
            MaxHealthBar(amount);
        }
    }

    private void MaxHeartBar(float amount)
    {
        if (!setAlr)
        {
            SetHealth(amount);
            setAlr = true;
        }
        maxHealth = amount;

        int numHearts = Mathf.CeilToInt((maxHealth / 20));

        while(hearts.Count < numHearts){
            hearts.Add(Instantiate(heartContainer, heartBarTransform).GetComponent<Image>());
        }
        while(hearts.Count > numHearts){
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }
        
        UpdateHeart();
    }

    public void SetHealth(float amount)
    {
        currentHealth = amount;
        UpdateHeart();
    }

    private void MaxHealthBar(float amount)
    {
        slider.maxValue = amount;
        currentHealth = amount;
        slider.value = currentHealth;
        fillBar.color = colorGradient.Evaluate(1f);
    }

    public void ChangeHealth(float amount)
    {
        if (heartBar)
        {
            ChangeHeartBar(amount);
        }
        else
        {
            ChangeHealthBar(amount);
        }
    }

    private void ChangeHeartBar(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHeart();
    }

    private void ChangeHealthBar(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, slider.maxValue);
        slider.value = currentHealth;
        fillBar.color = colorGradient.Evaluate(slider.normalizedValue);
    }

    void UpdateHeart()
    {
        int healthAsInt = (int)currentHealth / 10;
        foreach(Image heart in hearts)
        {
            if (healthAsInt > 1)
            {
                heart.sprite = heartImages[2];
            }
            else
            {
                heart.sprite = heartImages[healthAsInt];
            }
            healthAsInt -= 2;
            if (healthAsInt < 0)
            {
                healthAsInt = 0;
            }
        }
    }
}
