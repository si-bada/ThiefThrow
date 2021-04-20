using UnityEngine;
public enum PoliceType
{
    Easy,
    Medium,
    Hard,
    Extreme
}
public class PoliceController : MonoBehaviour
{
    private int mDirection = 1;
    public int Speed;
    public PoliceType type;
    void Update()
    {
        transform.position += new Vector3(Speed * Time.deltaTime, 0, 0) * mDirection;
    }

    void GetEdge()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < Screen.width * 0.2f)
        {
            Debug.LogWarning("Lower edge ");
        }
        else if (screenPos.x > Screen.width * 0.8f)
        {
            Debug.LogWarning("Higher edge ");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("wall"))
        {
            mDirection *= -1;
        }
        if(other.tag.Equals("ball"))
        {
            Debug.LogWarning("Lost");
        }
    }
}
