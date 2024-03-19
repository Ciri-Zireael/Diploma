using System.Collections;
using UnityEngine;

public class SlideSwitcher : MonoBehaviour
{
	[SerializeField] SlideHolder slideHolder;

	bool onCooldown;
	[SerializeField] float cooldownTime = 0.5f;
	enum Direction
	{
		Previous,
		Next
	}

	[SerializeField] Direction direction = Direction.Previous;
	void OnTriggerEnter(Collider other)
	{
		if (onCooldown) return;
		
		if (direction == Direction.Previous)
		{
			slideHolder.PrevSlide();
			print("PrevSlide");
		}

		if (direction == Direction.Next)
		{
			slideHolder.NextSlide();
			print("NextSlide");
		}

		StartCoroutine(StartCooldown());
	}

	IEnumerator StartCooldown()
	{
		onCooldown = true;

		yield return new WaitForSeconds(cooldownTime);

		onCooldown = false;
	}
}
