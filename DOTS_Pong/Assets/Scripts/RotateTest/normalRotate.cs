using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalRotate : MonoBehaviour
{
    public int RotateSpeed;
    public float moveSpeed = 100;
    public float angel;
    private float startY;
   // Start is called before the first frame update
   void Start()
   {
        startY = transform.position.y;
        angel = transform.position.x * 10;
    }

   // Update is called once per frame
   void Update()
   {
        angel = angel + Time.deltaTime * moveSpeed;
        transform.Rotate(Vector3.up, RotateSpeed*Time.deltaTime);
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(Mathf.Deg2Rad * angel) * 10, transform.position.z);
   }

}
