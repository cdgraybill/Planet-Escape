using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rocketRotation = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip engineShutdown;

    Rigidbody rocketRigidbody;
    AudioSource thrustSound;
    AudioSource thrustShutdownSound;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
        thrustShutdownSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessThrustSound();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessThrustSound()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrustSound.isPlaying)
            {
                thrustSound.PlayOneShot(mainEngine);
            }
        }
        else
        {
            thrustSound.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rocketRotation);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rocketRotation);
        }
    }

    void ApplyRotation(float rotation)
    {
        rocketRigidbody.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotation * Time.deltaTime);
        rocketRigidbody.freezeRotation = false;
    }
}
