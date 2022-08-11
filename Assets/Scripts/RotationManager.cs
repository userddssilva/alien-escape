using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RotationManager : MonoBehaviour
{
    public TilemapCollider2D m_scenery;
    public CapsuleCollider2D m_playerFeet;
    public static event Action<bool, float> OnGravityChange;

    private Transform _m_transform;

    private void Start()
    {
        _m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ChangeRotation();
    }

    private void ChangeRotation()
    {
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.L)) && !GameController.instance.IsTurning() && m_playerFeet.IsTouching(m_scenery))
            StartCoroutine(Flip(-90f));

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J)) && !GameController.instance.IsTurning() && m_playerFeet.IsTouching(m_scenery))
            StartCoroutine(Flip(90f));
    }

    private IEnumerator Flip(float angle)
    {
        OnGravityChange?.Invoke(true, angle);
        Vector3 currentPosition = _m_transform.eulerAngles;
        _m_transform.eulerAngles = currentPosition + new Vector3(0f, 0f, angle);
        //yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => m_playerFeet.IsTouching(m_scenery));
        OnGravityChange?.Invoke(false, angle);
    }
}
