using UnityEngine;

public class Timer : MonoBehaviour
{
	int ticksPassed;
	bool isRunning;

	void FixedUpdate()
	{
		if (isRunning)
		{
			ticksPassed++;
		}
	}
	
	public double GetSeconds()
	{
		return ticksPassed * Time.fixedDeltaTime;
	}

	public void StartTimer()
	{
		isRunning = true;
	}

	public void StopTimer()
	{
		isRunning = false;
	}

	public void ResetTimer()
	{
		ticksPassed = 0;
	}
}
