/* 
Changes map reference according to player interaction in the world

Works Remaining:        - NA     
Problems:               - NA
 */

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { Up, Down, Left, Right }
public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary; // PolygonCollider2D to define the boundaries of the map transition
    CinemachineConfiner confiner;
    [SerializeField] Direction direction; // Enum to specify the direction of the transition
    [SerializeField] float x;
    [SerializeField] float y;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    // Triggers if player collides with a trigger event
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if colliding object is "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundary; // Update the map reference
            if (x == 0 && y == 0)
            {
                UpdatePlayerPosition(collision.gameObject); // Update the player position
            }
            else
            {
                UpdatePlayerPosition(collision.gameObject, x, y);
            }
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position; // Checks the current position of player

        // Checks the direction of player collided in and updates the value accordingly 
        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2;
                break;
            case Direction.Down:
                newPos.y -= 2;
                break;
            case Direction.Left:
                newPos.x += 2;
                break;
            case Direction.Right:
                newPos.x -= 2;
                break;
            default:
                break;
        }
        player.transform.position = newPos; // Sets playe new position
    }

    private void UpdatePlayerPosition(GameObject player, float posX, float posY)
    {
        Vector3 newPos = player.transform.position;

        newPos.y = posY;
        newPos.x = posX;

        player.transform.position = newPos;
    }
}
