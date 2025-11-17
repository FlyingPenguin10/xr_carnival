using UnityEngine;

public class JackInTheBox : MonoBehaviour
{
    public Animator anim;
    public GameObject lid;
    public Rigidbody prizeRB;

    bool opened = false;

    public void OnClick()
    {
        if (opened) return;
        opened = true;

        anim.SetTrigger("Open");
    }

    // Called by animation event at end of lid slide
    public void HideLid()
    {
        lid.SetActive(false);
    }

    // Called by animation event after lid hides
    public void PopPrize()
    {
        prizeRB.gameObject.SetActive(true);
        prizeRB.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
    }
}
