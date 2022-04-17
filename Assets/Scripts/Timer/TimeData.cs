using System.Diagnostics.Contracts;

public class TimeUpdate : BaseUpdate<TimeUpdate> {
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
            _time = value;
        }
    }

    public override void Handle(TimeUpdate update) {
        Time = update.Time;
    }
     
    [Pure]
    public TimeUpdate Increment(float increment) {
        return new TimeUpdate(Time + increment);
    }

    [Pure]
    public TimeUpdate Reset() {
        return new TimeUpdate(0);
    }
}