
public class TrackCircuitPresence {

    private TrackCircuit tempTrack;

    public TrackCircuitPresence(TrackCircuit trackCircuit)
    {
        tempTrack = trackCircuit;
    }

	public void SetPresence(Bogey bogey, TrackCircuit trackCircuit)
    {
        if (!trackCircuit.hasCarPresence && bogey.OwnTrackCircuit == trackCircuit)
            trackCircuit.hasCarPresence = true;
        if (trackCircuit != tempTrack)
        {
            tempTrack.hasCarPresence = false;
            tempTrack = trackCircuit;
        }
    }
}
