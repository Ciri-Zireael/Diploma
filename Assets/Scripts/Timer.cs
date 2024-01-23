using UnityEngine;

public class Timer : MonoBehaviour
{
	int ticksPassed;

	void FixedUpdate()
	{
		ticksPassed++;
	}
	
	public double getSeconds()
	{
		return ticksPassed * Time.fixedDeltaTime;
	}
}
