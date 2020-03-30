using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class Robot : Agent
{
    public Rigidbody robot_rig, ball_rig;
    [Header("速度"), Range(1, 50)]
    public float speed;
    private void Start()
    {
        robot_rig = GetComponent<Rigidbody>();
        ball_rig = GameObject.Find("Soccer_Ball").GetComponent<Rigidbody>();

    }
    /// <summary>
    /// 每次開始時重新設定人跟球的位置
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //把加速度跟角度加速度歸零
        robot_rig.velocity = Vector3.zero;
        robot_rig.angularVelocity = Vector3.zero;
        ball_rig.velocity = Vector3.zero;
        ball_rig.angularVelocity = Vector3.zero;
        //隨機出現人的位置
        Vector3 posRobot = new Vector3(Random.Range(1f, 3f), 0.1f, Random.Range(3f, -2f));
        transform.position = posRobot;
        //隨機出現球的位置
        Vector3 posBall = new Vector3(Random.Range(1f, 3f), 0.1f, Random.Range(4f, -3f));
        ball_rig.position = posBall;

        ball.conplate = false;
    }
    /// <summary>
    /// 收集資料
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        //加入觀測資料
        sensor.AddObservation(transform.position);
        sensor.AddObservation(ball_rig.position);
        sensor.AddObservation(robot_rig.velocity.x);
        sensor.AddObservation(robot_rig.velocity.z);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        robot_rig.AddForce(control * speed);

        if (ball.conplate)
        {
            SetReward(1f);
            EndEpisode();
        }
        if (transform.position.y < 0|| ball_rig.position.y < 0)
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
