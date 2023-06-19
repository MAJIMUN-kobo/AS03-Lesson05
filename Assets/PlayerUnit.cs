using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    // フィールド
    public Vector3 inputAxis;       // 入力ベクトル
    public Vector3 velocity;        // 移動ベクトル 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();     // 移動関数の実行
    }
    
    public void Movement()
    {
        inputAxis = new Vector3(
                                x: Input.GetAxis("Horizontal"),
                                y: 0,
                                z: Input.GetAxis("Vertical")
                                );

        Vector3 moveVelocity = new Vector3(0, 0, 0);
        Transform adCamera = GameObject.Find("WorldPoint").transform;
        if(adCamera != null)
        {   // カメラがある時に実行
            Vector3 cameraForward =
                Vector3.Scale(
                              adCamera.transform.forward,
                              new Vector3(1, 0, 1)
                              ).normalized;

            moveVelocity = inputAxis.x * adCamera.right + inputAxis.z * cameraForward;
        }

        velocity = moveVelocity;
        this.transform.Translate(
                                 velocity * 10 * Time.deltaTime,
                                 Space.World
                                 );
    }
}
