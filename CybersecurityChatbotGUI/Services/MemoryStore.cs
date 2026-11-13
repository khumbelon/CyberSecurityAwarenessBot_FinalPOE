using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    /// <summary>
    /// MemoryStore - Stores user context and preferences across conversation
    /// Part 2 Feature: User memory for personalization
    /// </summary>
    public class MemoryStore
    {
        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; } = string.Empty;
        public string CurrentTopic { get; set; } = string.Empty;
        public Dictionary<string, string> Interests { get; set; } = new Dictionary<string, string>();

        public MemoryStore()
        {
            ClearMemory();
        }

        public void SaveInterest(string topic, string value)
        {
            if (Interests.ContainsKey(topic))
            {
                Interests[topic] = value;
            }
            else
            {
                Interests.Add(topic, value);
            }
        }

        public string GetInterest(string topic)
        {
            return Interests.ContainsKey(topic) ? Interests[topic] : string.Empty;
        }

        public void ClearMemory()
        {
            UserName = string.Empty;
            FavouriteTopic = string.Empty;
            CurrentTopic = string.Empty;
            Interests.Clear();
        }
    }
}
