using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour
{
    public WhackAMoleGameManager gameManager;
    public float popUpHeight = 0.3f;
    public float popUpDuration = 1.0f;
    public float moveSpeed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isUp = false;
    private bool isMoving = false;
    private Rigidbody rb;

    private void Start()
    {
        startPos = transform.position;
        gameObject.tag = "Mole";
        
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                transform.position, 
                targetPos, 
                moveSpeed * Time.fixedDeltaTime
            );

            if (rb != null)
            {
                rb.MovePosition(newPosition);
            }
            else
            {
                transform.position = newPosition;
            }

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                if (rb != null)
                {
                    rb.MovePosition(targetPos);
                }
                else
                {
                    transform.position = targetPos;
                }
                isMoving = false;
            }
        }
    }

    public void PopUp()
    {
        if (isUp) return;
        isUp = true;
        targetPos = startPos + Vector3.up * popUpHeight;
        isMoving = true;
        StartCoroutine(PopRoutine());
    }

    private IEnumerator PopRoutine()
    {
        yield return new WaitForSeconds(popUpDuration);
        if (isUp)
        {
            targetPos = startPos;
            isMoving = true;
            isUp = false;
        }
    }

    public void OnHit()
    {
        if (!isUp) return;
        
        if (gameManager != null)
        {
            gameManager.OnMoleHit();
        }

        StopAllCoroutines();
        targetPos = startPos;
        isMoving = true;
        isUp = false;
    }

    public void ResetPosition()
    {
        StopAllCoroutines();
        transform.position = startPos;
        isUp = false;
        isMoving = false;
    }
}
