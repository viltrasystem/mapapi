namespace ViltrapportenApi.Modal
{
    public class FileLogDestination : ILogDestination
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public FileLogDestination(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                File.AppendAllText(_filePath, message + Environment.NewLine);
            }
        }
    }

}
