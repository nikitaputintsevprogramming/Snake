using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private AudioSource AudioEating;
    [SerializeField] private ParticleSystem Boom;
    [SerializeField] List<Transform> Tails;
    [Range(0, 3)]
    [SerializeField] float BonesDistance;
    [SerializeField] GameObject BonePrefab;
    [Range(0,4)]
    [SerializeField] float Speed;
    private int count;
    [SerializeField] Text countText;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        count = 0;
        SetCountText ();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);

        float angle = Input.GetAxis("Horizontal");
        _transform.Rotate(0, angle, 0);
    }        

    private void MoveSnake(Vector3 newPosition) 
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }
            else
            {
                break;
            }
        }
        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);

            GameObject bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);
            count++;
            SetCountText();
            AudioEating.Play();
            Boom.Play();
        }
        else if (collision.gameObject.CompareTag("NoFood"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(2);
        }
    }


    void SetCountText ()
    {
        countText.text = "Food: " + count.ToString();
    }
}
