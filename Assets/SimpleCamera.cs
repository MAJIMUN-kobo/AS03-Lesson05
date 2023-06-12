using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    // フィールド
    public Vector3 velocity;    // 移動速度

    void Start()
    {
        
    }

    void Update()
    {
        // this.transform.Rotate(0, 90 * Time.deltaTime, 0);

        this.InputController(); // 入力処理を実行
    }


    // === 入力を処理する
    void InputController( )
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.I) == true)
        {
            this.transform.Rotate(15 * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(KeyCode.J) == true)
        {
            this.transform.Rotate(0, -15 * Time.deltaTime, 0);
        }

        if(Input.GetKey(KeyCode.K) == true)
        {
            this.transform.Rotate(-15 * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(KeyCode.L) == true)
        {
            this.transform.Rotate(0, 15 * Time.deltaTime, 0);
        }

        //this.transform.position += velocity * Time.deltaTime;
        this.transform.Translate(velocity * Time.deltaTime);
    }
}
