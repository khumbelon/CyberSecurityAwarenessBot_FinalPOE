using System;

namespace CybersecurityChatbotGUI
{
    public delegate void BotResponseHandler(string message);

    /// <summary>
    /// ChatBot - Core chatbot logic with NLP and sentiment analysis
    /// Part 2 Feature: Preserved and maintained for Part 3
    /// </summary>
    public class ChatBot
    {
        public BotResponseHandler? ResponseHandler; // Nullable to avoid null warnings
        private SentimentDetector sentimentDetector = new SentimentDetector();
        private KeywordResponder keywordResponder = new KeywordResponder();
        private MemoryStore memory = new MemoryStore();

        public string ProcessInput(string userInput)
        {
            string trimmedInput = userInput.Trim();

            if (string.IsNullOrWhiteSpace(trimmedInput))
            {
                return "Please type a question about cybersecurity first.";
            }

            string normalizedInput = trimmedInput.ToLower();
            Sentiment userSentiment = sentimentDetector.DetectSentiment(normalizedInput);
            string keywordResponse = keywordResponder.GetResponse(trimmedInput, memory);

            if (string.IsNullOrEmpty(keywordResponse))
            {
                keywordResponse = "I'm not sure about that topic. Try asking about: passwords, phishing, scams, privacy, malware, safe browsing, or 2FA.";
            }

            memory.CurrentTopic = keywordResponder.GetCurrentTopic();
            string finalResponse = keywordResponse;

            if (userSentiment == Sentiment.Worried)
            {
                finalResponse = $"I understand this can feel concerning. {finalResponse}";
            }
            else if (userSentiment == Sentiment.Frustrated)
            {
                finalResponse = $"I hear your frustration. Let's work through this. {finalResponse}";
            }
            else if (userSentiment == Sentiment.Curious)
            {
                finalResponse = $"Great question! {finalResponse}";
            }

            if (!string.IsNullOrEmpty(memory.UserName))
            {
                // Personalization logic here if needed
            }

            return finalResponse;
        }

        public void SendResponse(string message)
        {
            ResponseHandler?.Invoke(message);
        }
    }
}