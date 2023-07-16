using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : BaseStateManager
{
    public int playerId = 1;

    public float range = 3f;
    public float minIncline = 0f;
    public float maxIncline = 7f;
    public float aimSpeed = 5f;
    public float moveSpeed = 40f;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject HeartPrefab;
    public GameObject PointPrefab;
    public GameObject ThrowPoint;
    public CapsuleCollider2D capsule;
    public SpriteRenderer sprite;

    public int numberOfPoints = 10;
    public GameObject[] Points;

    public BaseState PlayerPauseState;
    public BaseState HumanRoamState;
    public BaseState HumanAimState;
    public BaseState HumanAttackedState;
    public BaseState GhostRoamState;
    public BaseState GhostMasterState;

    public IInteractable interactable;
    [HideInInspector] public MovementUtils utils;

    void Awake()
    {
        PlayerPauseState = new PlayerPauseState(this);
        HumanRoamState = new PlayerHumanRoamState(this);
        HumanAimState = new PlayerHumanAimState(this);
        HumanAttackedState = new PlayerHumanAttackedState(this);
        GhostRoamState = new PlayerGhostRoamState(this);
        GhostMasterState = new PlayerGhostMasterState(this);

        utils = GetComponent<MovementUtils>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Points = new GameObject[numberOfPoints];

        for(int i=0; i<numberOfPoints; i++)
        {
            Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
            Points[i].transform.localScale = Points[i].transform.localScale * (1 - (i / 10));
            Points[i].SetActive(false);
        }
    }

    public override BaseState GetInitialState()
    {
        if (playerId == 1)
            return HumanRoamState;
        else
            return GhostRoamState;
    }

}
