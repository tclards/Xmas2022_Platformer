using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelartCollectablesShowcaseAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Animator iAnim = transform.GetChild(i).GetComponent<Animator>();
            iAnim.SetBool("Continue", true);
            iAnim.SetInteger("Collectable", i);
        }
    }
}
