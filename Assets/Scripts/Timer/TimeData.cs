
public  class TimeUpdate : NewUpdateType
{
    public float Time;

    public TimeUpdate(float time)
    {
        Time = time;
    }
}

public class TimeData : NewBaseData<TimeUpdate>
{
    private float _time;

    public 
}