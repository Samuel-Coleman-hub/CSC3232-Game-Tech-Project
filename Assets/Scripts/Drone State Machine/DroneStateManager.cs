using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStateManager : MonoBehaviour
{
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    [HideInInspector] public Collider droneCollider;

    //Patrol Variables
    [Header("Patrol Settings")]
    public Rigidbody rb;
    public float flyPointRange;
    public float sightScaleMultiplier;
    public float sightHeightMultiplier;

    [HideInInspector] public Vector3 boxCastScale;
    [HideInInspector] public Vector3 boxCastPosition;

    //Attack Variables
    [Header("Attack Settings")]
    public Transform playerPosition;
    public GameObject bulletPrefab;
    public Transform gunTip;
    public GameObject bulletContainer;
    public float shotForce;
    public float gunCooldown;
    


    public DroneBaseState currentState;
    public DronePatrolState dronePatrolState = new();
    public DroneAttackState droneAttackState = new();
    public DroneDeadState droneDeadState = new();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        droneCollider = GetComponent<Collider>();
        boxCastScale = new Vector3(transform.localScale.x, transform.localScale.y * sightScaleMultiplier
            , transform.localScale.z);
        currentState = dronePatrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }

    public void SwitchState(DroneBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, flyPointRange);

        //Draw a Ray forward from GameObject toward the maximum distance
        Gizmos.DrawRay(transform.position, transform.forward * 10f);
        //Draw a cube at the maximum distance
        boxCastPosition = new Vector3(transform.position.x, transform.position.y * sightHeightMultiplier
            , transform.position.z);
        Gizmos.DrawWireCube(boxCastPosition + -transform.up* 10f, boxCastScale);
    }
}
