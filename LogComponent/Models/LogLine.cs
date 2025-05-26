using System.Text;

namespace LogComponent.Models
{
    /// <summary>
    /// This is the object that the diff. loggers (filelogger, consolelogger etc.) will operate on. The LineText() method will be called to get the text (formatted) to log
    /// </summary>
    public class LogLine
    {
        public LogLine(DateTime? timestamp = null, string? text = null)
        {
            Timestamp = timestamp ?? DateTime.Now;
            Text = text ?? string.Empty;
        }

        /// <summary>
        /// Return a formatted line
        /// </summary>
        /// <returns></returns>
        public virtual string LineText()
        {

            if (!string.IsNullOrWhiteSpace(Text))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(Text);
                sb.Append(". ");

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// The text to be display in logline
        /// </summary>
        private string Text { get; set; }

        /// <summary>
        /// The Timestamp is initialized when the log is added. Th
        /// </summary>
        public virtual DateTime Timestamp { get; private set; }
    }
}