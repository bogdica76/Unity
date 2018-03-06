using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscarePlayer : MonoBehaviour {

    public GameObject bulletPrefab;

   // Add a public field for the location of the Bullet Spawn.

public Transform bulletSpawn;

   // Add Input handling in the Update function.



//Add a “Fire” function to fire a Bullet.

void Fire()
{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 10.0f);
}








void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

    //    if (Input.GetKeyDown(KeyCode.Space))
     //   {
     //       Fire();
     //   }

    }
}
