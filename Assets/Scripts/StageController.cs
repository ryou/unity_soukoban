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
    public List<Vector2> wallPositions;

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

        // load blocks
        var blockTemplate = (GameObject)Resources.Load("Prefabs/Block");
        this.blockPositions.ForEach(blockPosition => {
            var blockObject = Instantiate(blockTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)blockPosition.x, (int)blockPosition.y);
            var block = blockObject.GetComponent<Block>();
            block.SetPosition(tmpPosition);
            this.objects.Add(block);
        });

        // load walls
        var wallTemplate = (GameObject)Resources.Load("Prefabs/Wall");
        this.wallPositions.ForEach(wallPosition => {
            var wallObject = Instantiate(wallTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)wallPosition.x, (int)wallPosition.y);
            var wall = wallObject.GetComponent<Wall>();
            wall.SetPosition(tmpPosition);
            this.objects.Add(wall);
        });
        // 外壁を配置
        // TODO: 整理する
        for (var i = 0; i < 11; i++)
        {
            var wallPosition = new Position(i, 0);
            var wallObject = Instantiate(wallTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)wallPosition.x, (int)wallPosition.y);
            var wall = wallObject.GetComponent<Wall>();
            wall.SetPosition(tmpPosition);
            this.objects.Add(wall);
        }
        for (var i = 0; i < 11; i++)
        {
            var wallPosition = new Position(i, 10);
            var wallObject = Instantiate(wallTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)wallPosition.x, (int)wallPosition.y);
            var wall = wallObject.GetComponent<Wall>();
            wall.SetPosition(tmpPosition);
            this.objects.Add(wall);
        }
        for (var i = 1; i < 10; i++)
        {
            var wallPosition = new Position(0, i);
            var wallObject = Instantiate(wallTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)wallPosition.x, (int)wallPosition.y);
            var wall = wallObject.GetComponent<Wall>();
            wall.SetPosition(tmpPosition);
            this.objects.Add(wall);
        }
        for (var i = 1; i < 10; i++)
        {
            var wallPosition = new Position(10, i);
            var wallObject = Instantiate(wallTemplate, Vector3.zero, new Quaternion(0, 0, 0, 0));
            var tmpPosition = new Position((int)wallPosition.x, (int)wallPosition.y);
            var wall = wallObject.GetComponent<Wall>();
            wall.SetPosition(tmpPosition);
            this.objects.Add(wall);
        }

        UpdateArounds();
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

                    UpdateArounds();
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

    void UpdateArounds()
    {
        this.objects.ForEach(item => {
            UpdateAround(item);
        });
    }

    void UpdateAround(BaseObject inObject)
    {
        var position = inObject.GetPosition();
        var around = new Dictionary<Direction, BaseObject>();

        // TODO: 以下、かなり雑な実装なので整理すること
        // up
        {
            var targetPosition = new Position(position.x, position.y + 1);
            var obj = SearchObject(targetPosition);
            around.Add(Direction.up, obj);
        }

        // right
        {
            var targetPosition = new Position(position.x + 1, position.y);
            var obj = SearchObject(targetPosition);
            around.Add(Direction.right, obj);
        }
        
        // down
        {
            var targetPosition = new Position(position.x, position.y - 1);
            var obj = SearchObject(targetPosition);
            around.Add(Direction.down, obj);
        }
        
        // left
        {
            var targetPosition = new Position(position.x - 1, position.y);
            var obj = SearchObject(targetPosition);
            around.Add(Direction.left, obj);
        }

        inObject.SetAround(around);
    }

    BaseObject SearchObject(Position inPosition)
    {
        return this.objects.Find(item => {
            var position = item.GetPosition();
            return (position.x == inPosition.x) && (position.y == inPosition.y);
        });
    }
}
