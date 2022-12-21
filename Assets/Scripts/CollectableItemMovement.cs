using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CollectableItemMovement : MonoBehaviour
{

    [Range(0, 5)][SerializeField] private float fMoveDistance;  // distance at which item will jiggle
    [Range(0, 5)][SerializeField] private float fMoveSpeed;     // speed at which item will jiggle
    [SerializeField] float fStartPos;                           // records starting POS of item from which to center movement

    // Start is called before the first frame update
    void Start()
    {
        fStartPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        Loop();
    }

    private void Loop()
    {
        float yPos = Mathf.PingPong(Time.time * fMoveSpeed, 1) * fMoveDistance;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
