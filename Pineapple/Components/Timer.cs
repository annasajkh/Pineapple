namespace Pineapple.Components;

public sealed class Timer
{
    /// <summary>
    /// The time before OnTimeout is called
    /// </summary>
    public float WaitTime { get; set; }

    /// <summary>
    /// Set if the timer only fire once
    /// </summary>
    public bool Oneshot { get; set; }

    /// <summary>
    /// The timer time left before it called
    /// </summary>
    public float TimeLeft { get; private set; }

    /// <summary>
    /// Pause the timer if it's true run otherwise
    /// </summary>
    public bool Paused { get; private set; } = true;

    /// <summary>
    /// The event that get called when TimeLeft reach 0
    /// </summary>
    public event Action? OnTimeout;

    public Timer(float waitTime, bool isOneshot)
    {
        WaitTime = waitTime;
        Oneshot = isOneshot;
    }

    /// <summary>
    /// Start the timer
    /// </summary>
    public void Start()
    {
        Paused = false;
    }

    /// <summary>
    /// Pause the timer
    /// </summary>
    public void Stop()
    {
        Paused = true;
    }

    /// <summary>
    /// Update the timer
    /// </summary>
    /// <param name="delta"></param>
    public void Update(float delta)
    {
        if (!Paused)
        {
            TimeLeft += delta;

            if (TimeLeft >= WaitTime)
            {
                TimeLeft = 0;

                if (OnTimeout != null)
                {
                    OnTimeout();
                }

                if (Oneshot)
                {
                    Paused = true;
                }
            }
        }
    }
}