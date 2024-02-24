using UnityEngine;

public class SlideSwitcher : MonoBehaviour
{
	[SerializeField] SlideHolder slideHolder;
	enum Direction
	{
		Previous,
		Next
	}

	[SerializeField] Direction direction = Direction.Previous;
	void OnTriggerEnter(Collider other)
	{
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
	}
}
