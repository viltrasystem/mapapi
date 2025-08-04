namespace ViltrapportenApi.Modal
{
    public class ConsoleLogDestination : ILogDestination
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
