using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
	//Global Variables
	public static int maxHealth = 10; // Change back to 100 after testing
	public int currentHealth;
	public HealthBar healthBar;


	public static int Rank = 1;
	public int currentRank;


	public static int maxStamina = 9; // Change back to 100 after testing
	public int currentStamina;
	public StaminaBar staminaBar;


	public static int _experienceRequired;
	public int currentXP;

	public TextMeshProUGUI RankDisplay;
	public TextMeshProUGUI HealthDisplay;
	public TextMeshProUGUI StaminaDisplay;
	public TextMeshProUGUI GameOver;
	public TextMeshProUGUI OutOfStamina;

	[SerializeField] public TextMeshProUGUI TimerDisplay;

	public GameObject GameOverPanel;
	public GameObject OutOFStaminaPanel;





	////////// END OF SCRIPT FOR STAMINA REGENERATION //////////

	private int restoreDuration = 5; // Change this to 300 seconds after testing is done.
	private DateTime nextEnergyTime;
	private DateTime lastEnergyTime;
	private bool isRestoring = false;


	private DateTime StringToDate(string datetime)
	{
		if (String.IsNullOrEmpty(datetime))
		{
			return DateTime.Now;

		} else
		{
			return DateTime.Parse(datetime);
		}
	}

	private IEnumerator RestoreStamina()
	{
		UpdateStaminaTimer();
		isRestoring = true;

		while (currentStamina < maxStamina)
		{
			DateTime currentDateTime = DateTime.Now;
			DateTime nextDateTime = DateTime.Now;
			bool isStaminaAdding = false;

			while (currentDateTime > nextDateTime)
			{
				if (currentStamina < maxStamina)
				{
					isStaminaAdding = true;
					currentStamina++;
					UpdateStamina();
					DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
					nextDateTime = AddDuration(timeToAdd, restoreDuration);
				}

				else
				{
					break;
				}
			}

			if (isStaminaAdding == true)
			{
				lastEnergyTime = DateTime.Now;
				nextEnergyTime = nextDateTime;
			}

			UpdateStaminaTimer();
			UpdateStamina();
			Save();
			yield return null;
		}

		isRestoring = false;
	}

	private DateTime AddDuration(DateTime datetime, int duration)
	{
		//return datetime.AddMinutes(duration); //Use for full game
		return datetime.AddSeconds(duration); //Used for testing
	}

	// Loads Players Stamina from Save funtion
	private void Load()
	{
		currentStamina = PlayerPrefs.GetInt("currentStamina");
		nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
		lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));

	}
	// Saves Players Stamina in order to create a timer function which will add Stamina after time has passed.
	void Save()
	{
		PlayerPrefs.SetInt("currentStamina", currentStamina);
		PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString()); //Convets DateTime in a string in order to save the PlayerPrefs
		PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString()); //Convets DateTime in a string in order to save the PlayerPrefs

	}

	void UpdateStaminaTimer()
	{
		if (currentStamina >= maxStamina)
		{
			StaminaDisplay.SetText($"{currentStamina} / {maxStamina}");
			return;
		}

		TimeSpan time = nextEnergyTime - DateTime.Now;
		string timeValue = String.Format("{0:D2}:{1:D1}", time.Minutes, time.Seconds);
		StaminaDisplay.SetText($"{timeValue}");
	}



	private void UpdateStamina()
	{
		currentStamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);
		StaminaDisplay.SetText($"{currentStamina} / {maxStamina}");
	}

	public void UseStamina()
	{
		if (currentStamina >= 1)
		{
			currentStamina--;
			UpdateStamina();
			
			if(isRestoring == false)
            {
				if(currentStamina + 1 == maxStamina)
                {
					nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
                }

				StartCoroutine(RestoreStamina());
            }
		}

		else
        {
			if (OutOFStaminaPanel != null)
			{
				OutOFStaminaPanel.SetActive(true);
				OutOfStamina.SetText($"Not Enough Stamina!");
			}

		}

	}


	////////// END OF SCRIPT FOR STAMINA REGENERATION //////////





	// Start is called before the first frame update
	void Start()
	{
		if (!PlayerPrefs.HasKey("currentStamina"))
		{
			PlayerPrefs.SetInt("currenStamina", 100);
			Load();
			StartCoroutine(RestoreStamina());
		} else
		{
			Load();
			StartCoroutine(RestoreStamina());
		}

		//Sets the Health at startup
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		HealthDisplay.SetText($"{maxHealth} / {maxHealth}");

		//Sets the Stamina at startup
		//currentStamina = maxStamina;
		//staminaBar.SetMaxStamina(maxStamina);
		//StaminaDisplay.SetText($"{currentStamina} / {maxStamina}");


		//Sets the Rank at startup

		currentRank = Rank;
		RankDisplay.SetText($"Rank: {currentRank}");

		//Sets the Experience at startup
		currentXP = _experienceRequired;
		RankDisplay.SetText($"Rank: {currentRank}");

	}

	// Missions
	public void Mission_One()
	{
		// This function will be allowed to be called until the Health or Stamina is no longer valid to complete the mission.
		if (currentHealth != 0 && currentStamina != 0)
		{
			currentHealth -= 1;
			healthBar.SetHealth(currentHealth);
			HealthDisplay.SetText($"{currentHealth} / {maxHealth}");

			currentStamina -= 1;
			staminaBar.SetStamina(currentStamina);
			StaminaDisplay.SetText($"{currentStamina} / {maxStamina}");
		}
		else 
		{
			if (GameOverPanel != null)
			{
				GameOverPanel.SetActive(true);
				GameOver.SetText($"You have been sent to the Hospital!");
			}
		}

		if (currentStamina == 0)
        {
			if(OutOFStaminaPanel != null)
            {
				OutOFStaminaPanel.SetActive(true);
				OutOfStamina.SetText($"Not Enough Stamina!");
            }
        }
	}
}






// Experience


// Ranking



// Skill Points









