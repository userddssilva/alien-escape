using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class RotationManager : MonoBehaviour
{
    public TilemapCollider2D m_scenery;

    public CapsuleCollider2D m_playerFeet;

    public CapsuleCollider2D m_box;

    public CapsuleCollider2D m_box_2;

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
        if (
            (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.L)) &&
            (
            m_playerFeet.IsTouching(m_scenery) ||
            m_playerFeet.IsTouching(m_box) ||
            m_playerFeet.IsTouching(m_box_2)
            )
        )
        {
            CountTurn();
            GameController.instance.lastAngle = -90f;
            StartCoroutine(Flip(-90f));
        }

        if (
            (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J)) &&
            (
            m_playerFeet.IsTouching(m_scenery) ||
            m_playerFeet.IsTouching(m_box) ||
            m_playerFeet.IsTouching(m_box_2)
            )
        )
        {
            CountTurn();
            GameController.instance.lastAngle = 90f;
            StartCoroutine(Flip(90f));
        }
    }

    private void CountTurn()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "level_1") GameController.instance.level1Turns += 1;
        if (sceneName == "level_2") GameController.instance.level2Turns += 1;
    }

    private IEnumerator Flip(float angle)
    {
        OnGravityChange?.Invoke(true, angle);
        Vector3 currentPosition = _m_transform.eulerAngles;
        _m_transform.eulerAngles = currentPosition + new Vector3(0f, 0f, angle);

        //yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() =>
                    (
                    m_playerFeet.IsTouching(m_scenery) ||
                    m_playerFeet.IsTouching(m_box)
                    ));
        OnGravityChange?.Invoke(false, angle);
    }
}
