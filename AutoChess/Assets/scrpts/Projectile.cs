using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float duration;
    private GameObject target;
    private bool isMoving = false;


    public void Init(GameObject value)
    {
        target = value;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if(target == null)
            {
                Destroy(this.gameObject);
                return;
            }
            // 相对位置
            Vector3 relativePos = target.transform.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            this.transform.rotation = rotation;

            Vector3 targetPosion = target.transform.position + new Vector3(0, 1, 0);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosion, step);

            float distance = Vector3.Distance(this.transform.position, targetPosion);
            if(distance < 0.2f)
            {
                this.transform.parent = target.transform;
                Destroy(this.gameObject, duration);
            }
        }
    }
}
