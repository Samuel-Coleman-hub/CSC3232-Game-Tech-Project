using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStateManager : MonoBehaviour
{
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    [HideInInspector] public Collider droneCollider;

    [Header("GameObject Settings")]
    public GameObject leftPropellorObj;
    public GameObject rightPropellorObj;
    public GameObject eyeObj;

    //Patrol Variables
    [Header("Patrol Settings")]
    public GameObject patrolLightObj;
    public Light patrolLight;
    public Color lightAttackColour;
    public Color lightFriendlyColour;
    public Material lightAttackMaterial;
    public Material lightFriendlyMaterial;
    public MeshRenderer patrolLightMeshRenderer;

    public float patrolMaxDistanceToPlayer;
    public float patrolMinDistanceToPlayer;
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

    //Off State Variables
    [Header("Explosion Settings")]
    public float timeTillExplode;
    public float explosionRadius;
    public float explosionForce;
    public float explosionLift;

    [Header("Friendly Settings")]
    public DroneRemote droneRemote;
    public LayerMask whatIsNavMesh;
    public Transform lastSeenEnemy;

    //Concrete States
    public DroneBaseState currentState;
    public DroneEnemyIdleState enemyIdleState;
    public DronePatrolState patrolState;
    public DroneAttackState attackState;

    public DroneOffIdleState offIdleState;
    public DroneOffExplodeState offExplodeState;

    public DronePlayerTurnOnState playerIdleState;
    public DronePlayerTargetState playerTargetState;
    public DronePlayerAttackState playerAttackState;

    private void Awake()
    {
        //Concrete States
        enemyIdleState = new DroneEnemyIdleState(this);
        patrolState = new DronePatrolState(this);
        attackState = new DroneAttackState(this);


        offIdleState = new DroneOffIdleState(this);
        offExplodeState = new DroneOffExplodeState(this);

        playerIdleState = new DronePlayerTurnOnState(this);
        playerTargetState = new DronePlayerTargetState(this);
        playerAttackState = new DronePlayerAttackState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        droneCollider = GetComponent<Collider>();
        boxCastScale = new Vector3(transform.localScale.x, transform.localScale.y * sightScaleMultiplier
            , transform.localScale.z);
        currentState = patrolState;
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType , GameObject objHit)
    {
        currentState.OnCollisionChildEnter(hitBoxType, objHit);
    }

    public void HitByDroneRemote()
    {
        currentState.HitByDroneRemote();
    }

    public void SwitchState(DroneBaseState state)
    {
        currentState = state;
        state.EnterState();
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
