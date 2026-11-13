using System;

namespace CybersecurityChatbotGUI.Models
{
    public class IntentResult
    {
        public UserIntent Intent { get; set; }
        public string TaskTitle { get; set; } = "";
        public DateTime? ReminderDate { get; set; }
    }
}
