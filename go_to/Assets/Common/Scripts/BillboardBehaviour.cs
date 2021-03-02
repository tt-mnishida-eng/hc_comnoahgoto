using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class BillboardBehaviour : MonoBehaviour
    {
        [SerializeField] Camera camera;

        void Start()
        {
            if (camera == null)
                camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(camera.transform);
        }
    }
}