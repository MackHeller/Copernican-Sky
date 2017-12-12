using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float movementSpeed = 20f;
    public int leftBoarder = 0;
    public int rightBoarder = 0;
    public int upBoarder = 0;
    public int downBoarder = 0;
    public int[] additionalCoods;
    public string pattern;
    private bool moving = true;
    public Animator animator;
    private Direction currentDir = Direction.STOP;
    // Use this for initialization

    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        animator.SetBool("Walk Right", false);
        animator.SetBool("Walk Left", false);
        animator.SetBool("Walk Up", false);
        animator.SetBool("Walk Down", false);
        if (moving)
        {
            setDirectionOnPattern();
            switch (currentDir)
            {
                case Direction.LEFT:
                    transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
                    animator.SetBool("Walk Left", true);
                    break;
                case Direction.RIGHT:
                    transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                    animator.SetBool("Walk Right", true);
                    break;
                case Direction.UP:
                    transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
                    animator.SetBool("Walk Up", true);
                    break;
                case Direction.DOWN:
                    transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
                    animator.SetBool("Walk Down", true);
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
            case "right, down, left, up":
                rightDownLeftUp();
                break;
            case "right, down, right, reverse":
                rightDownRightReverse();
                break;
            case "stop":
                noMovement();
                break;
            default:
                throw new System.Exception("pattern does not exist: " + pattern);
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
        LEFT, RIGHT, UP, DOWN, STOP
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // patterns 

    private void rightLeft()
    {
        if (currentDir == Direction.STOP || (currentDir == Direction.LEFT && this.transform.position.x <= leftBoarder))
        {
            currentDir = Direction.RIGHT;
        }
        else if (currentDir == Direction.RIGHT && this.transform.position.x >= rightBoarder)
        {
            currentDir = Direction.LEFT;
        }
    }
    private void rightDownLeftUp()
    {
        if (currentDir == Direction.STOP || (currentDir == Direction.UP && this.transform.position.y >= upBoarder))
        {
            currentDir = Direction.RIGHT;
        }
        else if (currentDir == Direction.RIGHT && this.transform.position.x >= rightBoarder)
        {
            currentDir = Direction.DOWN;
        }
        else if (currentDir == Direction.DOWN && this.transform.position.y <= downBoarder)
        {
            currentDir = Direction.LEFT;
        }
        else if (currentDir == Direction.LEFT && this.transform.position.x <= leftBoarder)
        {
            currentDir = Direction.UP;
        }
    }
    private void rightDownRightReverse()
    {
        if (currentDir == Direction.STOP || (currentDir == Direction.LEFT && this.transform.position.x <= additionalCoods[0]))
        {
            currentDir = Direction.RIGHT;
        }
        else if (currentDir == Direction.RIGHT && this.transform.position.x >= rightBoarder && this.transform.position.y > downBoarder)
        {
            currentDir = Direction.DOWN;
        }
        else if(currentDir == Direction.DOWN && this.transform.position.y <= downBoarder)
        {
            currentDir = Direction.RIGHT;
        }
        else if (currentDir == Direction.RIGHT && this.transform.position.x >= additionalCoods[1])
        {
            currentDir = Direction.LEFT;
        }
        else if (currentDir == Direction.LEFT && this.transform.position.x <= leftBoarder && this.transform.position.y < upBoarder)
        {
            currentDir = Direction.UP;
        }
        else if (currentDir == Direction.UP && this.transform.position.y >= upBoarder)
        {
            currentDir = Direction.LEFT;
        }
    }
}