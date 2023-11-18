using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //Waypoints die die plattform ansteuert. Einstellbar im editor
    [SerializeField] private GameObject[] waypoint;
    private int currentWaypoint;

    //Geschwindigkeit der Plattform in Units pro Sekunde
    [SerializeField]
    private float speed = 2f;

    //Toggle für Teleport vom letzten zum ersten Waypoint
    [SerializeField]
    private bool reset = false;

    //Toggle um zu aktivieren das der Spieler vom Objekt mitgezogen wird
    [SerializeField]
    private bool PlayerStick = false;

    private void Start()
    {
        //setzt array index auf einen unter maximum damit als erstes der letzte wegpunkt angesteuert wird
        if(reset==true)
        {
            currentWaypoint = waypoint.Length-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checkt ob die Plattform bereits auf dem wegpunkt ist indem die entfernung zwischen eigener und waypoint Position miteinander verglichen wird
        if (Vector2.Distance(waypoint[currentWaypoint].transform.position, transform.position) < .1f)
        {
            //Wenn plattform auf Waypoint wird der nächste waypoint angesteuert
            currentWaypoint++;
            if(currentWaypoint>=waypoint.Length)
            {
                currentWaypoint = 0;

                if (reset == true)
                {
                    //Springe zu erstem waypoint wenn letzter waypoint erreicht wurde
                    transform.position = Vector2.MoveTowards(transform.position, waypoint[currentWaypoint].transform.position, 10000f);
                }
            }
        }
        //Verringere distanz zwischen selbst und aktuellem ziel mit speed pro sekunde
        transform.position = Vector2.MoveTowards(transform.position, waypoint[currentWaypoint].transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Wenn eine Kollision stattfindet und das Objekt den Spieler Tag hat und PlayerStick auf true ist setze selbst als Parent vom Spieler
        if(collision.gameObject.CompareTag("Player") && PlayerStick==true)
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Wenn eine Kollision verlassen wird und das Objekt den Spieler Tag hat und PlayerStick auf true ist setze nichts als Parent vom Spieler
        if (collision.gameObject.CompareTag("Player") && PlayerStick == true)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}