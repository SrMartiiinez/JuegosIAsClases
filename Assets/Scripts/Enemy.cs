using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent agent;
    public Player player;

    public float idleTime = 3f;

    public float detectRange = 5f;
    public LayerMask detectLayer;

    public Transform[] patrolPoints;

    public bool hasVision = true;

    // Campo de visión del NPC
    public float visionAngle = 90f;
    public Transform angleHelperR, angleHelperL;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    public void DetectPlayer()
    {
        Collider[] _targets = Physics.OverlapSphere(transform.position, detectRange, detectLayer);

        if (_targets.Length > 0)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                if (_targets[i].CompareTag("Player"))
                {
                    Vector3 _dirToPlayer = _targets[i].transform.position - transform.position;

                    if (hasVision)
                    {
                        // Comprueba si el jugador está dentro del campo de visión
                        if (Vector3.Angle(transform.forward, _dirToPlayer) <= visionAngle / 2f)
                        {
                            // El jugador está a la vista
                            if (Physics.Raycast(transform.position, _dirToPlayer, out RaycastHit _hit, detectRange))
                            {
                                if (_hit.transform.CompareTag("Player"))
                                {
                                    // Detecta al jugador
                                    player = _targets[i].GetComponent<Player>();

                                    // Iniciamos el Game Over
                                    player.GameOver();

                                    Debug.Log("Jugador detectado con visión");
                                }
                                else
                                {
                                    player = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Detecta al jugador si no hay obstáculos entre ellos
                        if (Physics.Raycast(transform.position, _dirToPlayer, out RaycastHit _hit, detectRange))
                        {
                            if (_hit.transform.CompareTag("Player"))
                            {
                                // Detecta al jugador
                                player = _targets[i].GetComponent<Player>();

                                // Si el jugador no está "corriendo más despacio", iniciamos el Game Over
                                if (!player.movement.IsRunning)
                                {
                                    // Iniciamos el Game Over
                                    player.GameOver();
                                }

                                Debug.Log("Jugador detectado con sonido");
                            }
                            else
                            {
                                player = null;
                            }
                        }
                    }
                    return;
                }
            }
        }
        else
        {
            player = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        if (hasVision)
        {
            angleHelperR.localEulerAngles = new Vector3(0, visionAngle / 2f, 0);
            angleHelperL.localEulerAngles = new Vector3(0, -visionAngle / 2f, 0);
            Gizmos.DrawRay(angleHelperR.position, angleHelperR.forward * detectRange);
            Gizmos.DrawRay(angleHelperL.position, angleHelperL.forward * detectRange);
        }

    }
}