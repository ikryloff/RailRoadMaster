using UnityEngine;

public class PathIndicationUnit : MonoBehaviour
{
    public MeshRenderer PIUMeshRend { get; set; }
    private Material colorBlocked;
    private Material colorDefault;
    private Material colorRoute;
    public TrackCircuit TrackCircuit { get; set; }

    

    private void Awake()
    {
        colorBlocked = ResourceHolder.Instance.Path_Blocked_Mat;
        colorDefault = ResourceHolder.Instance.Path_Default_Mat;
        colorRoute = ResourceHolder.Instance.Path_Route_Mat;
        GameEventManager.StartListening ("StateTrack", TrackColor);
    }

    private void OnEnable()
    {
        TrackColor (TrackCircuit);
    }

   
    private void TrackColor( TrackCircuit track )
    {
        if ( track.Equals(TrackCircuit) )
        {
            if ( gameObject.activeSelf )
            {
                if ( !TrackCircuit.IsInRoute && !TrackCircuit.IsInUse )
                {
                    if ( !TrackCircuit.HasCarPresence && PIUMeshRend.material != colorDefault )
                    {
                        PIUMeshRend.material = colorDefault;
                    }
                    else if ( TrackCircuit.HasCarPresence && PIUMeshRend.material != colorBlocked )
                    {
                        PIUMeshRend.material = colorBlocked;
                    }
                }
                else if ( TrackCircuit.IsInRoute && !TrackCircuit.IsInUse && PIUMeshRend.material != colorRoute )
                    PIUMeshRend.material = colorRoute;
                else if ( TrackCircuit.IsInRoute && TrackCircuit.IsInUse && PIUMeshRend.material != colorBlocked )
                    PIUMeshRend.material = colorBlocked;
            }
        }            
    }




}
