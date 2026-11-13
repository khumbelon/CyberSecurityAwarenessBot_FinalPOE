using System;

namespace CybersecurityChatbotGUI.Models
{
    public class CyberTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        // Nullable because a reminder is optional.
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}