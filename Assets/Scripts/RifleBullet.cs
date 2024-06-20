using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float rifleBulletStrengh;
    [SerializeField, Range(0, 200)] private int rifleDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * rifleBulletStrengh, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<WideRobot>().ReceiveDamage(rifleDamage);
            Destroy(gameObject);
        }
    }


}
