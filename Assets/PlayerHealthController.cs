using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    private void Awake()
    {
        instance = this;
    }


    public int maxHealth, currentHealth;
    public float invicibleLength = 1f;
    private float invicCounter;
   
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth -50;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleLength > 0)
        {
            invicCounter -= Time.deltaTime;
        }
    }


    public void DamagePlayer(int damageAmount)
    {
        if (invicCounter <= 0)
        {

            currentHealth -= damageAmount;
            invicCounter = invicibleLength;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (UIController.instance.level == 1)
                {

                    SceneManager.LoadScene("GameOver1");
                }else if (UIController.instance.level == 2)
                {

                    SceneManager.LoadScene("GameOver2");
                }
                else if (UIController.instance.level == 3)
                {

                    SceneManager.LoadScene("GameOver3");
                }
            }
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
        }

    }

    public void HealthIncrease(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        if (currentHealth == 0)
        {
            currentHealth = 0;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;

    }
}
