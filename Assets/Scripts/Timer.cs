using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEngine.Advertisements;
public class Timer : MonoBehaviour
{
	public static float maxTimer;
	public float degradePerc = .02f;

	public Text timerText;

	public float currentMaxTime;
	public  float currentTime;
	public bool isCountingDown;

	public GameController refer;

	
	void Update()
	{
		if (isCountingDown)
			UpdateCounter();
	}

	public void StartTimer()
	{
		currentTime = currentMaxTime;
		isCountingDown = true;
		DisplayTime();
	}

	public void ResetTimer()
	{
		currentMaxTime -= maxTimer * degradePerc;
		StartTimer();
	}

	void UpdateCounter()
	{
		currentTime -= Time.deltaTime;
		if (currentTime < 0)
		{
			refer.GameEnded();
			currentTime = 0;
			isCountingDown = false;
			//Time.timeScale = 0;
		}
		DisplayTime();
	}

	void DisplayTime()
	{	int min;
		float sec;
		float displayTimer = Mathf.Round (currentTime);
		min = (int)(displayTimer / 60);
		sec = (int)(displayTimer % 60);
		if(sec<10)
		timerText.text = min+ ":0"+sec;
		else
		timerText.text = min+ ":"+sec;
		
	}
}