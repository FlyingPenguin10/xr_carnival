using System.Collections.Generic;
using UnityEngine;

public class GameResetManager : MonoBehaviour
{
    [System.Serializable]
    public class ResetObject
    {
        public Rigidbody rb;
        public Vector3 startPos;
        public Quaternion startRot;
    }

    private List<ResetObject> cans = new List<ResetObject>();
    private List<ResetObject> balls = new List<ResetObject>();

    void Start()
    {
        foreach (GameObject can in GameObject.FindGameObjectsWithTag("Can"))
        {
            AddResetObject(cans, can);
        }
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            AddResetObject(balls, ball);
        }
    }

    private void AddResetObject(List<ResetObject> list, GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        list.Add(new ResetObject
        {
            rb = rb,
            startPos = rb.transform.position,
            startRot = rb.transform.rotation
        });
    }

    public void ResetGame()
    {
        ResetObjects(cans);
        ResetObjects(balls);
    }

    private void ResetObjects(List<ResetObject> list)
    {
        foreach (var item in list)
        {
            item.rb.linearVelocity = Vector3.zero;
            item.rb.angularVelocity = Vector3.zero;
            item.rb.transform.SetPositionAndRotation(item.startPos, item.startRot);
        }
    }
}
