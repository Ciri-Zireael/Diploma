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

		var t = transform;
		Vector3 scale = t.localScale;

		Vector3 start = t.position;
		Vector3 end = new Vector3(start.x, start.y - scale.y / 2, start.z);
		
		yield return MoveWithTime(start, end, cooldownTime / 2);
		yield return MoveWithTime(end, start, cooldownTime / 2);

		onCooldown = false;
	}

	IEnumerator MoveWithTime(Vector3 from, Vector3 to, float speed)
	{
		float elapsedTime = 0;
		while (elapsedTime < speed)
		{
			transform.position = Vector3.Lerp(from, to, elapsedTime / speed);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
