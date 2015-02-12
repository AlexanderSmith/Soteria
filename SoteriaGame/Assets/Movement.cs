using UnityEngine;
using System.Collections;

public class Movement {

    public void Move(float inputX, float inputY, float inputZ, float speed, Transform whatToTransform)
    {
        whatToTransform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed));
    }
}
