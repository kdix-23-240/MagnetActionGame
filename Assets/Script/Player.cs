using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;//Rigidbody
    private MagnetManager magnetManager;
    private AreaCheck areaCheck;
    private int magnetCheckNum;
    private Gravity gravity = new Gravity();
    private int wallCheckNum = 0;

    [SerializeField] private float jumpPower;
    [SerializeField] private float movePower;

    private Animator animator;
    private bool hasSpeedX = false;
    private bool hasSpeedY = false;

    private bool isRight = true;

    public void ManualStart() //ゲームが動く時に一回だけ動く
    {
        rb = GetComponent<Rigidbody2D>();
        magnetManager = MagnetManager.GetInstance(); //マネージャーを生成するのを一つにするためシングルトン
        areaCheck = AreaCheck.GetInstance();
        //Debug.Log(lastCheckPoint.GetLastCheckPointPos().x);
        LastCheckPointPos.SetFirstPoint(this.gameObject.transform.position);
        magnetManager.SetStartNS();
        if (LastCheckPointPos.GetLastPoint().x == 0 && LastCheckPointPos.GetLastPoint().y == 0)
        {
            LastCheckPointPos.SetLastPoint(this.gameObject.transform.position);
        }
        else
        {
            this.gameObject.transform.position = LastCheckPointPos.GetLastPoint();
            //lastCheckPoint.GetLastCheckPointPos();
        }
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        //Animator
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void ManualUpdate() //毎フレーム動く
    {
        Jump();


        ObserveHasSpeedX();
        ObserveHasSpeedY();

        AllAnimations();
    }

    public void ManualFixedUpdate() //一定時間ごとに呼ばれる
    {
        Move();
        ObserveIsRight();
        AddMoveForce();

    }

    public void AddMoveForce()
    {
        wallCheckNum = 0;
        magnetCheckNum = -1;
        if (areaCheck.GetIsInArea())
        {
            magnetCheckNum = magnetManager.NSChecker();
            if ((magnetCheckNum == 0 && areaCheck.GetJudgSquare() == 1) || (magnetCheckNum == 1 && areaCheck.GetJudgSquare() == 2))
            {
                rb.AddForce(gravity.AddUpForce());
            }
            else if (areaCheck.GetJudgSquare() == 3)
            {
                wallCheckNum = 1;
                if (magnetCheckNum == 1)
                {
                    rb.AddForce(gravity.AddLeftForce());
                    rb.AddForce(gravity.AntiGravity());
                }
                else if (magnetCheckNum == 0)
                {
                    rb.AddForce(gravity.AddRightForce());
                    rb.AddForce(gravity.AntiGravity());
                }

            }
            else if (areaCheck.GetJudgSquare() == 4)
            {
                wallCheckNum = 1;
                if (magnetCheckNum == 1)
                {
                    rb.AddForce(gravity.AddRightForce());
                    rb.AddForce(gravity.AntiGravity());
                }
                else if (magnetCheckNum == 0)
                {
                    rb.AddForce(gravity.AddLeftForce());
                    rb.AddForce(gravity.AntiGravity());
                }
            }
        }

        ResetAllAnimations();
    }

    public void Move() //横移動のメソッド
    {
        Vector3 pos = this.transform.position;
        if (wallCheckNum == 0)
        {

            if (Input.GetKey(KeyCode.A))
            {
                pos.x -= movePower;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                pos.x += movePower;
            }
        }
        else if (wallCheckNum == 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos.y += movePower;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                pos.y -= movePower;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                if (magnetCheckNum == 1)
                {
                    pos.y += movePower;
                }
            }

        }

        this.transform.position = pos;


    }

    public void Jump()
    {
        //int magnetCheckNum = magnetManager.NSChecker(); //今はどこにいてもチェックしてる（後で変えやなあかん）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (magnetCheckNum == 1 || this.rb.velocity.y != 0)
            {
                return;
            } //違う極ならジャンプせずにreturn
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode2D.Impulse); //勝手に浮くようにしたので大ジャンプ無くした
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GOAL")
        {
            LastCheckPointPos.SetLastPoint(LastCheckPointPos.GetFirstPoint());
            SceneManager.LoadScene("Clear");
        }

        if (collision.gameObject.tag == "DEATH")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            LastCheckPointPos.SetLastPoint(this.gameObject.transform.position);
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// オブジェクトがx軸方向に速度も持っていればtrueを返す、なければfalseを返すメソッド
    /// </summary>
    /// <returns>速度を持っているかどうか</returns>

    public bool ObserveHasSpeedX()
    {
        if (this.rb.velocity.x == 0)
        {
            this.hasSpeedX = false;
        }
        else
        {
            this.hasSpeedX = true;
        }
        return this.hasSpeedX;
    }

    /// <summary>
    /// y軸方向にスピードがあるかどうかを監視
    /// </summary>
    /// <returns></returns>
    public bool ObserveHasSpeedY()
    {
        if (this.rb.velocity.y <= 0.1 && this.rb.velocity.y >= -0.1)
        {
            this.hasSpeedY = false;
            Debug.Log("速度なし");
        }
        else
        {
            this.hasSpeedY = true;
        }
        return this.hasSpeedY;
    }

    /// <summary>
    /// プレイヤーが右に向いているかどうか監視
    /// </summary>
    public bool ObserveIsRight()
    {
        // if (!ObserveHasSpeedY())
        // {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            //Debug.Log("左向き");
            isRight = false;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.Space) && magnetCheckNum == 1))
        {
            //Debug.Log("右向き");
            isRight = true;
        }
        //}
        return isRight;
    }

    /// <summary>
    /// 全アニメーションリセット
    /// 通常Idle状態に戻る
    /// </summary>
    public void ResetAllAnimations()
    {
        animator.SetBool("RightWallMove", false);
        animator.SetBool("ReRightWallMove", false);
        animator.SetBool("RightWallIdle", false);
        animator.SetBool("ReRightWallIdle", false);

        animator.SetBool("RightWallIdle", false);
        animator.SetBool("LeftWallMove", false);
        animator.SetBool("LeftWallIdle", false);
        animator.SetBool("ReLeftWallIdle", false);
        animator.SetBool("UpsideDownMove", false);
        animator.SetBool("ReUpsideDownMove", false);
        animator.SetBool("ReUpsideDownIdle", false);
        animator.SetBool("UpsideDownIdle", false);
        animator.SetBool("ReJump", false);
        animator.SetBool("ReMove", false);
        animator.SetBool("Jump", false);
        animator.SetBool("ReIdle", false);
        animator.SetBool("Move", false);
    }

    public void AnimationIdle()
    {
        if (ObserveIsRight())
        {
            this.ResetAllAnimations();
        }
        else
        {
            this.ResetAllAnimations();
            animator.SetBool("ReIdle", true);
            //Debug.Log("反対むき");
        }
    }

    public void AnimationCeilingIdle()
    {
        if (ObserveIsRight())
        {
            animator.SetBool("UpsideDownMove", false);
            animator.SetBool("UpsideDownIdle", true);
        }
        else
        {
            animator.SetBool("ReUpsideDownMove", false);
            animator.SetBool("ReUpsideDownIdle", true);
            //Debug.Log("逆さま");
        }
    }

    public void AnimationLeftWallIdle()
    {
        if (ObserveIsRight())
        {
            animator.SetBool("LeftWallIdle", true);
            animator.SetBool("RightWallIdle", false);
        }
        else
        {
            animator.SetBool("ReLeftWallIdle", true);
        }
    }
    public void AnimationRightWallIdle()
    {
        if (ObserveIsRight())
        {
            animator.SetBool("RightWallIdle", true);
            animator.SetBool("LeftWallIdle", false);
        }
        else
        {
            animator.SetBool("ReRightWallIdle", true);
        }
    }

    public void AnimationMove()
    {
        //Debug.Log("成功");
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("ReMove", false);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Move", false);
            animator.SetBool("ReMove", true);
        }
    }

    public void AnimationCeilingMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("ReUpsideDownMove", false);
            animator.SetBool("UpsideDownMove", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("UpsideDownMove", false);
            animator.SetBool("ReUpsideDownMove", true);
        }
    }

    public void AnimationLeftWallMove()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("ReLeftWallMove", false);
            animator.SetBool("LeftWallMove", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("LeftWallMove", false);
            animator.SetBool("ReLeftWallMove", true);
        }
    }

    public void AnimationRightWallMove()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("ReRightWallMove", false);
            animator.SetBool("RightWallMove", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("RightWallMove", false);
            animator.SetBool("ReRightWallMove", true);
        }
    }

    public void AnimationJump()
    {
        if (this.ObserveHasSpeedY())
        {
            ResetAllAnimations();
            if (ObserveIsRight())
            {
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("ReIdle", true);
                animator.SetBool("ReJump", true);
            }
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("ReJump", false);
        }

    }

    public void AnimationFloor()
    {
        AnimationJump();
        if (GetComponent<Rigidbody2D>().IsSleeping())
        {
            //Debug.Log("-------------------止まれ---------------------");
            this.AnimationIdle();
        }
        else
        {
            //Debug.Log("--------------------進め--------------------");
            this.AnimationMove();
        }
    }

    public void AnimationCeiling()
    {
        this.AnimationCeilingIdle();
        if (GetComponent<Rigidbody2D>().IsSleeping())
        {
            this.AnimationCeilingIdle();
        }
        else
        {
            this.AnimationCeilingMove();
        }
    }

    public void AnimationLeftWall()
    {
        animator.SetBool("Move", false);
        animator.SetBool("ReMove", false);
        animator.SetBool("LeftWallIdle", false);
        animator.SetBool("ReLeftWallIdle", false);
        animator.SetBool("LeftWallMove", false);
        animator.SetBool("ReLeftWallMove", false);
        this.AnimationLeftWallIdle();
        if (GetComponent<Rigidbody2D>().IsSleeping())
        {
            this.AnimationLeftWallIdle();
        }
        else
        {
            this.AnimationLeftWallMove();
        }
    }

    public void AnimationRightWall()
    {
        animator.SetBool("Move", false);
        animator.SetBool("ReMove", false);
        animator.SetBool("ReRightWallIdle", false);
        animator.SetBool("RightWallMove", false);
        animator.SetBool("ReRightWallMove", false);
        this.AnimationRightWallIdle();
        if (GetComponent<Rigidbody2D>().IsSleeping())
        {
            this.AnimationRightWallIdle();
        }
        else
        {
            this.AnimationRightWallMove();
        }
    }

    /// <summary>
    /// 各エリアごとのアニメーションを指定
    /// </summary>
    public void AllAnimations()
    {
        if (this.areaCheck.GetIsInArea())
        {
            switch (this.areaCheck.GetJudgSquare())
            {
                case 1:
                    ResetAllAnimations();
                    AnimationFloor();
                    break;
                case 2:
                    AnimationCeiling();
                    break;
                case 3:
                    AnimationLeftWall();
                    break;
                case 4:
                    AnimationRightWall();
                    break;
                default:
                    AnimationFloor();
                    break;
            }
        }
        else
        {
            ResetAllAnimations();
            AnimationFloor();
            if (!GetComponent<Rigidbody2D>().IsSleeping())
            {
                AnimationJump();
            }
        }
    }
}
