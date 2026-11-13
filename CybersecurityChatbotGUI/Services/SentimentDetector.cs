using System;// to use basic system functionalities
using System.Text;// to use StringBuilder


namespace CybersecurityChatbotGUI// namespace of the project
{ 
public enum Sentiment// enum to represent different sentiment types
{
    Neutral,
    Worried,
    Curious,
    Frustrated
}
class SentimentDetector// class for detecting sentiment in user input
    {
    public Sentiment DetectSentiment(string input) //class for detecting sentiment in user input
    {
        if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
        {
            return Sentiment.Worried;
        }
        if (input.Contains("curious") || input.Contains("interested"))
        {
            return Sentiment.Curious;
        }
        if (input.Contains("frustrated") || input.Contains("annoyed"))
        {
            return Sentiment.Frustrated;
        }
        return Sentiment.Neutral;// return neutral sentiment if no specific sentiment keywords are found
        }
}
}
