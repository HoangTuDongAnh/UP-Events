namespace HTDA.Framework.Events.Samples
{
    // Example: struct event (no allocations)
    public readonly struct DamageEvent
    {
        public readonly int Amount;
        public readonly string Source;

        public DamageEvent(int amount, string source)
        {
            Amount = amount;
            Source = source;
        }
    }

    // Example: another event type
    public readonly struct PingEvent
    {
        public readonly int Count;

        public PingEvent(int count)
        {
            Count = count;
        }
    }
}