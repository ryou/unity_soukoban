using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class StageController : MonoBehaviour {

    enum State
    {
        wait,
        move,
        clear,
    }

    public Vector2 playerPosition;
    public List<Vector2> blockPositions;
    public List<Vector2> panelPositions;

    private Cell[,] cells = new Cell[11, 11];

    private State currentState = State.wait;
    private GameObject player;
    private List<GameObject> panels = new List<GameObject>();

	// Use this for initialization
	void Start () {
        var panelObject = (GameObject)Resources.Load("Prefabs/Panel");
        this.panelPositions.ForEach(panelPosition => {
            var panel = Instantiate(panelObject, Vector3.zero, new Quaternion(0, 0, 0, 0));
            panel.GetComponent<Panel>().SetPosition(panelPosition);
            this.panels.Add(panel);
        });

        var playerObject = (GameObject)Resources.Load("Prefabs/Player");
        this.player = Instantiate(playerObject, Vector3.zero, new Quaternion(0, 0, 0, 0));
        this.player.GetComponent<Player>().SetPosition(playerPosition);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.currentState == State.wait)
        {
            var keyDirection = GetKeyDirection();

            if (keyDirection != Direction.none)
            {
                if (player.GetComponent<Player>().CanMove(keyDirection))
                {
                    player.GetComponent<Player>().Move(keyDirection);
                    this.currentState = State.move;
                }
            }
        }
        else if (this.currentState == State.move)
        {
            if (!player.GetComponent<Player>().IsMoving()) this.currentState = State.wait;
        }
	}

    Direction GetKeyDirection()
    {
        var x = Input.GetAxis("Horizontal");
        if (x > 0)
        {
            return Direction.right;
        }
        else if (x < 0)
        {
            return Direction.left;
        }

        var y = Input.GetAxis("Vertical");
        if (y > 0)
        {
            return Direction.up;
        }
        else if (y < 0)
        {
            return Direction.down;
        }

        return Direction.none;
    }

    void UpdateArounds(BaseObject inObject)
    {

    }
}
