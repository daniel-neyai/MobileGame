using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_LevelManager : MonoBehaviour {

    public bool SHOW_COLLIDER = true; // $$

    public static LS_LevelManager instance { set; get; }

    // Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100.0f; //Distance the player sees before segments spawned.
    private const int INITIAL_SEGMENTS = 10; // Segments when game starts.
    private const int INITIAL_TRANSITION_SEGMENTS = 2; // Transition Segments.
    private const int MAX_SEGMENTS_ON_SCREEN = 15; // How many segments we have on screen before we start despawning old segments.
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel; // Are we on a longblock or on the ground?
    private int y1, y2, y3; // Is our lane where?

    // List of pieces
    public List<LS_Piece> ramps = new List<LS_Piece>();
    public List<LS_Piece> longblocks = new List<LS_Piece>();
    public List<LS_Piece> jumps = new List<LS_Piece>();

    [HideInInspector]
    public List<LS_Piece> pieces = new List<LS_Piece>();
    // All the pieces in the pool

    // List of segments
    public List<LS_Segment> availableSegments = new List<LS_Segment>();
    public List<LS_Segment> availableTransitions = new List<LS_Segment>();

    [HideInInspector]
    public List<LS_Segment> segments = new List<LS_Segment>();

    // Gameplay
    private bool isMoving = false;

    private void Awake()
    {
        instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
    }

    private void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
            if (i < INITIAL_TRANSITION_SEGMENTS)
                SpawnTransition();
            else
                GenerateSegment();
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
            GenerateSegment();


        if (amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
    }

    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f, 1f) < (continiousSegments * 0.25f))
        {
            // Spawnt transition segment
            continiousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continiousSegments++;
        }
    }

    private void SpawnSegment()
    {
        List<LS_Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        LS_Segment s = GetLS_Segment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnTransition()
    {
        List<LS_Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        LS_Segment s = GetLS_Segment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }
    public LS_Segment GetLS_Segment(int id, bool transition)
    {
        LS_Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<LS_Segment>();
            
            s.SegId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
    }

    public LS_Piece GetPiece(PieceType pt, int visualIndex)
    {
        LS_Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if(p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
                go = ramps[visualIndex].gameObject;
            else if (pt == PieceType.longblock)
                go = longblocks[visualIndex].gameObject;
            else if (pt == PieceType.jump)
                go = jumps[visualIndex].gameObject;

            go = Instantiate(go);
            p = go.GetComponent<LS_Piece>();
            pieces.Add(p);
        }

        return p;
    }
}
