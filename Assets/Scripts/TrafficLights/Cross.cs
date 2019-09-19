public class Cross : TrafficLight
{

    void Start()
    {
        GetPositionX = gameObject.transform.position.x;
        IsClosed = true;
    }
}
