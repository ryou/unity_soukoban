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
    private Player player;
    private List<BaseObject> objects = new List<BaseObject>();
    private List<GameObject> panels = new List<GameObject>();

	// Use this for initialization
	void Start () {
        // load panels
        var panelTemplate = (GameObject)Resources.Load("Prefabs/Panel");
        this.panelPositions.ForEach(panelPosition => {
            var panel = Instantiate(panelTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            // TODO: ここらへんのVector2からPositionに変換する処理、なんとかならんか
            var tmpPosition = new Position((int)panelPosition.x, (int)panelPosition.y);
            panel.GetComponent<Panel>().SetPosition(tmpPosition);
            this.panels.Add(panel);
        });

        // load player
        var playerTemplate = (GameObject)Resources.Load("Prefabs/Player");
        var playerObject = Instantiate(playerTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
        this.player = playerObject.GetComponent<Player>();
        var position = new Position((int)playerPosition.x, (int)playerPosition.y);
        this.player.SetPosition(position);
        this.objects.Add(this.player);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.currentState == State.wait)
        {
            var keyDirection = GetKeyDirection();

            if (keyDirection != Direction.none)
            {
                if (this.player.CanMove(keyDirection))
                {
                    this.player.Move(keyDirection);
                    this.currentState = State.move;
                }
            }
        }
        else if (this.currentState == State.move)
        {
            if (!this.player.IsMoving()) this.currentState = State.wait;
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

    BaseObject SearchObject(Position inPosition)
    {
        return this.objects.Find(item => {
            var position = item.GetPosition();
            return (position.x == inPosition.x) && (position.y == inPosition.y);
        });
    }
}
