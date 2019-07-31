public class TrackEnd : TrafficLight
{
    void Start()
    {
        GetPositionX = gameObject.transform.position.x;
        IsClosed = true;
    }

}
