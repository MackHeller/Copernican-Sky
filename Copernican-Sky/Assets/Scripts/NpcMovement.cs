using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour {
    public float movementSpeed = 20f;
    public int leftBoarder =0;
    public int rightBoarder=0;
    public int upBoarder=0;
    public int downBoarder=0;
    public string pattern;
    private bool moving = true;
    private Direction currentDir = Direction.STOP;
	
	// Update is called once per frame
	void FixedUpdate() {
        if (moving) {
            setDirectionOnPattern();
            switch (currentDir)
            {
                case Direction.LEFT:
                    transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
                    break;
                case Direction.RIGHT:
                    transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                    break;
                case Direction.UP:
                    transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
                    break;
                case Direction.DOWN:
                    transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                    break;
                case Direction.STOP:
                    break;
                default:
                    throw new System.Exception("error: " + currentDir);
            }
        }
    }
    private void setDirectionOnPattern()
    {
        switch (pattern)
        {
            case "right, left":
                rightLeft();
                break;
            case "stop":
                noMovement();
                break;
            default:
                throw new System.Exception("pattern does not exist: "+ pattern);
        }
    }

    private void rightLeft()
    {
        if(currentDir == Direction.STOP || (currentDir == Direction.LEFT && this.transform.position.x<=leftBoarder))
        {
            currentDir = Direction.RIGHT;
        }
        else if(currentDir == Direction.RIGHT &&  this.transform.position.x >= rightBoarder)
        {
            currentDir = Direction.LEFT;
        }
    }
    private void noMovement()
    {
        currentDir = Direction.STOP;
    }

    public void switchMoving()
    {
        setMoving(!moving);
    }
    public void setMoving(bool set)
    {
        moving = set;
    }

    private enum Direction
    {
        LEFT,RIGHT,UP,DOWN,STOP
    }
}
