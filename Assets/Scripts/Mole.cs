using UnityEngine;

public class Mole : MonoBehaviour
{
    public WhackAMoleGameManager gameManager;
    public float popUpHeight = 0.3f;
    public float popUpDuration = 1.0f;

    private Vector3 startPos;
    private bool isUp = false;

    void Start()
    {
        startPos = transform.position;
    }

    public void PopUp()
    {
        if (isUp) return;
        isUp = true;
        Vector3 upPos = startPos + Vector3.up * popUpHeight;
        StartCoroutine(PopRoutine(upPos));
    }

    System.Collections.IEnumerator PopRoutine(Vector3 upPos)
    {
        transform.position = upPos;
        yield return new WaitForSeconds(popUpDuration);
        transform.position = startPos;
        isUp = false;
    }

    public void OnHit()
    {
        if (!isUp) return;
        transform.position = startPos;
        isUp = false;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
        isUp = false;
    }
}
