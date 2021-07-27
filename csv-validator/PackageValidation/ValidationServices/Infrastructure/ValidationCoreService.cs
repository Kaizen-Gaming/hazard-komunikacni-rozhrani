using System.Text;

namespace ValidationPilotServices.Infrastructure
{
    public class ValidationCoreService
    {
        private readonly StringBuilder output = new StringBuilder();

        protected readonly string error_message_template = "{0};{1};{2};{3};{4};\"{5}\"";

        protected int ErrorsCounter { get; set; } = 0;

        public bool IsValid { get; protected set; } = true;

        public string ValidationOutput => output.ToString();

        public ValidationCoreService()
        {
        }

        /// <summary>
        /// This procedure clean error messages stack.
        /// </summary>
        protected virtual void Clean()
        {
            this.IsValid = true;
            this.ErrorsCounter = 0;
        }

        protected virtual void LogError(string message)
        {
            this.IsValid = false;
            this.ErrorsCounter++;
            output.AppendLine(message);
        }

        /// <summary>
        /// This procedure writes to log the message of INFO level.
        /// </summary>
        /// <param name="message">The message to write to log file.</param>
        protected virtual void LogInfo(string message)
        {
            output.AppendLine(message);
        }

        protected virtual void LogDebug(string message)
        {
            output.AppendLine(message);
        }

        protected virtual void LogFatal(string message)
        {
            output.AppendLine(message);
        }

        protected virtual void LogWarn(string message)
        {
            output.AppendLine(message);
        }
    }
}
