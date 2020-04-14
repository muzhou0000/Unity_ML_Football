using UnityEngine;

public class ball : MonoBehaviour
{/// <summary>
/// 足球是否進入球門
/// </summary>
    public static bool conplate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "進球感應區")
        {
            conplate = true;
        }
    }
}
