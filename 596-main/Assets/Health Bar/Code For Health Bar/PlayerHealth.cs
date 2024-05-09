using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // The current health of the player
    private float health;

    // Timer for lerping the health bar
    private float lerpTimer;

    // The maximum health of the player
    public float maxHealth = 100;

    // The speed at which the health bar lerps
    public float chipSpeed = 2f;

    // Reference to the front health bar image
    public Image frontHealthBar;

    // Reference to the back health bar image
    public Image backHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial health to the maximum health
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Clamp the health value between 0 and the maximum health
        health = Mathf.Clamp(health, 0, maxHealth);

        // Update the health bar UI
        UpdateHealthUI();

        // For testing: Take damage when 'A' key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10));
        }

        // For testing: Restore health when 'S' key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    // Update the health bar UI
    public void UpdateHealthUI()
    {

        // Get the fill amounts of the front and back health bars
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;

        // Calculate the fraction of health remaining
        float hFraction = health / maxHealth;

        // If the back health bar fill is greater than the health fraction
        if (fillB > hFraction)
        {
            // Set the front health bar fill to the health fraction
            frontHealthBar.fillAmount = hFraction;

            // Set the back health bar color to red
            backHealthBar.color = Color.red;

            // Increment the lerp timer
            lerpTimer += Time.deltaTime;

            // Calculate the percent complete based on the lerp timer and chip speed
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            // Lerp the back health bar fill to the health fraction
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        // If the front health bar fill is less than the health fraction
        if (fillF < hFraction)
        {
            // Set the back health bar color to green
            backHealthBar.color = Color.green;

            // Set the back health bar fill to the health fraction
            backHealthBar.fillAmount = hFraction;

            // Increment the lerp timer
            lerpTimer += Time.deltaTime;

            // Calculate the percent complete based on the lerp timer and chip speed
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            // Lerp the front health bar fill to the back health bar fill
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        // Reduce the health by the damage amount
        health -= damage;

        // Reset the lerp timer
        lerpTimer = 0f;
    }

    // Method to restore health
    public void RestoreHealth(float healAmount)
    {
        // Increase the health by the heal amount
        health += healAmount;

        // Reset the lerp timer
        lerpTimer = 0f;
    }
}