using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Animator chestAnimator;
    public GameObject prizePrefab;
    public Transform spawnPoint;
    private bool isOpen = false;

    public void ToggleChest()
    {
        if (isOpen)
        {
            chestAnimator.SetTrigger("CloseTrigger");
        }
        else
        {
            chestAnimator.SetTrigger("OpenTrigger");
            SpawnPrize();
        }
        isOpen = !isOpen;
    }

    void SpawnPrize()
    {
        Instantiate(prizePrefab, spawnPoint.position, Quaternion.identity);
    }
}