public class TrackEnd : TrafficLight
{
    protected override void Awake()
    {
        base.Awake ();
    }
    void Start()
    {
        GetPositionX = gameObject.transform.position.x;
        IsClosed = true;
    }

}
