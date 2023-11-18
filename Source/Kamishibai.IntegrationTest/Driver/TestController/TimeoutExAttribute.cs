namespace Driver.TestController
{
    public class TimeoutExAttribute : Attribute
    {
        public int Time { get; set; }

        public TimeoutExAttribute(int timeout)
        {
            Time = timeout;
        }
    }
}
