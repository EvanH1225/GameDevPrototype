using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStats : MonoBehaviour
{
    // UI elements
    public Image healthBarFill;
    public Image hungerBarFill;
    public GameObject gameOverScreen;

    // Hunger stats
    private float hungerDecreaseRate = 0.1f;

    // Player stats
    private bool isDead = false;
    private float maxHealth = 100f;
    private float maxHunger = 100f;
    private float currentHealth;
    private float currentHunger;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;

        UpdateHealthUI();
        UpdateHungerUI();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentHunger > 0)
        {
            currentHunger -= Time.deltaTime * hungerDecreaseRate;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            UpdateHungerUI();
        } else
        {
            TakeDamage(Time.deltaTime * 2f);
        }
    }


    // ---- Health Logic ------------------------------------------------------
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }


    public void TakeDamage(float amount)
    {
        if (isDead) return;

        Debug.Log("yooooooo");

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        
    }


    // ---- Hunger Logic ------------------------------------------------------
    public void DecreaseHunger(float amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        UpdateHungerUI();
    }


    public void Eat(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        UpdateHungerUI();
    }


    // ---- Update UIs --------------------------------------------------------
    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }


    private void UpdateHungerUI()
    {
        if (hungerBarFill != null)
        {
            hungerBarFill.fillAmount = currentHunger / maxHunger;
        }
    }
}
