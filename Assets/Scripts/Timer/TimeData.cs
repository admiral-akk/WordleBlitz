public class TimeUpdate {
    public float Time;

    public TimeUpdate(float time) {
        Time = time;
    }
}

public class TimeData : NewBaseData<TimeUpdate> {
    private float _time;
    public float Time {
        get => _time;
        private set {
            if (_time == value)
                return;
            Queue(new TimeUpdate(value));
            _time = value;
        }
    }

    public override void HandleUpdate(TimeUpdate update) {
        Time = update.Time;
    }
     
    public TimeUpdate Increment(float increment) {
        return new TimeUpdate(Time + increment);
    }

    public TimeUpdate Reset() {
        return new TimeUpdate(0);
    }
}