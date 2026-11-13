using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI
{
    public class NlpProcessor
    {
        public IntentResult DetectIntent(string rawInput)
        {
            string input = rawInput.Trim().ToLower();
            // Activity log phrases are checked early because
            // the phrase may not contain the exact words "activity log".
            if (ContainsAny(input,
                "show activity log",
                "show my activity",
                "recent actions",
                "what have you done for me"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.ShowActivityLog
                };
            }
            if (ContainsAny(input,
                "start quiz",
                "play quiz",
                "cyber quiz",
                "test my knowledge",
                "cybersecurity game"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.StartQuiz
                };
            }
            if (ContainsAny(input,
                "show tasks",
                "show my tasks",
                "view tasks",
                "what tasks do i have"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.ShowTasks
                };
            }
            // "Remind me to..." creates a task with a reminder
            // when a date/time phrase can be recognised.
            if (input.Contains("remind me to") ||
                input.Contains("set a reminder"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.AddTaskWithReminder,
                    TaskTitle = ExtractTaskTitle(input),
                    ReminderDate = ExtractReminderDate(input)
                };
            }
            if (ContainsAny(input,
                  "add task",
                  "add a task",
                  "create task",
                  "create a task",
                  "new task"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.AddTask,
                    TaskTitle = ExtractTaskTitle(input)
                };
            }
            // If none of the Part 3 commands matched, allow your
            // existing Part 2 chatbot to handle cybersecurity text.
            return new IntentResult
            {
                Intent = UserIntent.CybersecurityQuestion
            };
        }
        private bool ContainsAny(string input, params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (input.Contains(phrase))
                    return true;
            }
            return false;
        }
        public string ExtractTaskTitle(string input)
        {
            string title = input;
            // Remove common command phrases from the start.
            title = Regex.Replace(
                title,
                @"^(please\s+)?(add\s+(a\s+)?task(\s+to|\s*-\s*)?|" +
                @"create\s+(a\s+)?task(\s+to)?|new\s+task(\s+to)?|" +
                @"remind\s+me\s+to)\s*",
                "",
                RegexOptions.IgnoreCase);
            // Remove simple reminder wording from the end.
            title = Regex.Replace(
                title,
                @"\s+(tomorrow|in\s+\d+\s+days?)\.?$",
                "",
                RegexOptions.IgnoreCase);
            return title.Trim().TrimEnd('.');
        }
        public DateTime? ExtractReminderDate(string input)
        {
            if (input.Contains("tomorrow"))
                return DateTime.Today.AddDays(1);
            Match match = Regex.Match(
        input,
                @"in\s+(\d+)\s+days?",
                RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int days = Convert.ToInt32(match.Groups[1].Value);
                return DateTime.Today.AddDays(days);
            }
            return null;
        }
    }
}