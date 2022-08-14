using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class CameraRotation : MonoBehaviour
    {
        private float x;
        private float y;
        private Vector3 rotateValue;
        private Transform _m_transform;

        private void Start()
        {
            RotationManager.OnGravityChange += ChangeRotation;
            _m_transform = GetComponent<Transform>();
        }

        private void Update()
        {

        }

        private void ChangeRotation(bool isGravityChanging, float angle)
        {
            //var actualEulerAngles = _m_transform.eulerAngles;
            //_m_transform.eulerAngles = actualEulerAngles + new Vector3(0, 0, -1 * angle);
            //Debug.Log($"Euler angles: {_m_transform.eulerAngles}");
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
