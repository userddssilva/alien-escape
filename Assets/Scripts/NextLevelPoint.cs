using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class NextLevelPoint : MonoBehaviour
{
    public string levelName;
    public CapsuleCollider2D m_playerFeet;
    public TilemapCollider2D m_scenery;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"isCollectedKey: {GameController.instance.IsCollectedKey()}");
        if (collision.gameObject.tag == "Player" &&
            GameController.instance.IsCollectedKey() &&
            m_playerFeet.IsTouching(m_scenery))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
