# PROG6221 Programming 2A - POE Part 3

## Student Details
- Module: PROG6221 Programming 2A
- Part: POE Part 3 - Enhanced GUI with Database, Quizzes & Task Management
- IDE: Visual Studio 2022, .NET 6.0 / .NET Framework 4.7.2 Windows Forms
- Database: SQL Server / SQLite

## Project Overview
Part 3 extends the Cybersecurity Awareness Chatbot from Part 2 (GUI, Voice Greeting, Chatbot) with enterprise features: persistent data storage via SQL database, interactive quizzes with immediate feedback, task-based learning paths, activity logging, and enhanced NLP processing. The existing Part 2 chatbot logic is refactored into a Services layer while maintaining backward compatibility.

## Part 3 Features

### Core Enhancement
- **Database Integration**: Persistent storage for quizzes, tasks, user progress, activity logs
- **Quiz System**: Multiple-choice cybersecurity quizzes with immediate scoring and feedback
- **Task Management**: Guided learning tasks (e.g., "Create a strong password", "Identify phishing email") with completion tracking
- **Activity Logging**: Tracks user interactions, quiz attempts, task completions, chatbot sessions
- **NLP Enhancement**: `NlpProcessor` service provides advanced text analysis for smarter responses
- **Refactored Architecture**: Part 2 chatbot logic moved to Services layer for reusability

### Collections & Data Structures (Rubric Compliance)
- `Dictionary<string, List<CyberTask>>` - Tasks grouped by category
- `List<QuizQuestion>` - Quiz question bank
- `Dictionary<string, QuizAnswerResult>` - User quiz results tracking
- `List<ActivityEntry>` - Activity log entries

### Persistence
- All quiz attempts, task progress, user profile data, and activity logs saved to database
- Automatic schema creation via `database_setup.sql`
- Graceful fallback to in-memory storage if database unavailable

## Project Structure
CybersecurityAwarenessBot
-MainForm.cs// Main GUI 
-Navigation hub for chatbot, quizzes, tasks 
─MainForm.Designer.cs// Form designer  
─ Models
-CyberTask.cs// Task entity (Id, Title, Description, Category, Completed) 
─QuizQuestion.cs// Quiz question (Id, Text, Options[], CorrectAnswer, Category) 
─ QuizAnswerResult.cs// Quiz attempt result (UserId, QuestionId, SelectedAnswer, Correct, Timestamp) 
─ActivityEntry.cs// Activity log (UserId, Action, Category, Timestamp, Details) 
─ Data/ 
─ DatabaseConfig.cs// Connection string, schema initialization 
─ TaskRepository.cs// CRUD for CyberTask 
─ QuizRepository.cs// CRUD for QuizQuestion and QuizAnswerResult 
─ Services/ 
─ ChatBot.cs// Existing Part 2 chatbot logic (refactored from GUI) 
─ SentimentDetector.cs // Existing Part 2 sentiment analysis 
─ MemoryStore.cs// Existing Part 2 memory (user interests) 
── KeywordResponder.cs // Existing Part 2 keyword responses 
─ QuizManager.cs// Quiz orchestration, scoring, feedback        
─ ActivityLogger.cs// Log user actions to database 
─ VoiceGreeting.cs// Existing Part 2 voice greeting 
─ Assets/ 
─ greeting.wav// Voice greeting audio 
- README.md 

## Database Schema Overview

### Tables
1. **UserProfile** - UserId, UserName, InterestsJson, CreatedAt
2. **CyberTasks** - Id, Title, Description, Category, Completed, AssignedDate, CompletedDate
3. **QuizQuestions** - Id, Text, OptionA, OptionB, OptionC, OptionD, CorrectAnswer, Category, Difficulty
4. **QuizAnswers** - Id, UserId, QuestionId, SelectedAnswer, IsCorrect, AttemptDate
5. **ActivityLog** - Id, UserId, ActionType, Category, Details, Timestamp

### Key Relationships
- UserProfile → QuizAnswers (User attempts quiz)
- UserProfile → CyberTasks (User assigned tasks)
- UserProfile → ActivityLog (User actions tracked)

## Models

### CyberTask.cs

## Services

### QuizManager.cs
Handles quiz presentation, scoring, and feedback.
- `GetQuizByCategory(string category)` - Retrieves questions for category
- `SubmitAnswer(int questionId, char selectedAnswer)` - Validates answer, returns feedback
- `GetScorecard()` - Shows user's quiz performance over time

### NlpProcessor.cs
Enhances chatbot with intent detection and entity extraction.
- `DetectIntent(string userInput)` - Identifies user intent (e.g., "Learn", "Quiz", "Help")
- `ExtractEntities(string userInput)` - Extracts cybersecurity topics mentioned
- `GetContextualResponse(string input)` - Combines intent + entities for smarter responses

### ActivityLogger.cs
Persists user interactions for analytics.
- `LogChatMessage(string userId, string category)` - Log chatbot interaction
- `LogQuizAttempt(string userId, int questionId, bool correct)` - Log quiz attempt
- `LogTaskCompletion(string userId, int taskId)` - Log task completed
- `GetUserActivityReport(string userId)` - Retrieve activity history

## GUI Changes from Part 2

### MainForm Enhancements
- **Navigation Tabs**: Chat | Quiz | Tasks | Progress
- **Chat Tab** (existing Part 2 chatbot)
- **Quiz Tab**
  - Category dropdown (Password Safety, Phishing, Malware, Scams, Privacy)
  - Display current question and options as buttons
  - Real-time feedback: Green (correct), Red (incorrect), explanation
  - Progress bar showing quiz completion
- **Tasks Tab**
  - List of assigned tasks with checkboxes
  - Task details panel with description and category
  - Completion date tracking
- **Progress Tab**
  - Quiz scorecard (attempts, scores, categories)
  - Task completion percentage
  - Activity timeline (chatbot interactions, quiz attempts, tasks completed)

## How to Setup & Run Part 3

### Prerequisites
- Visual Studio 2022
- .NET 6.0 or .NET Framework 4.7.2
- SQL Server Express (or SQLite for local development)

### Setup Steps
1. **Clone the repository**:

2. **Configure Database**:
- Open `DatabaseConfig.cs`
- Update `ConnectionString` for your SQL Server instance or SQLite file path
- Example for SQL Server:
  ```csharp
  public static string ConnectionString = "Server=localhost;Database=CybersecurityChatbot;Integrated Security=true;";
  ```
- Example for SQLite:
  ```csharp
  public static string ConnectionString = "Data Source=cybersecurity_chatbot.db;Version=3;";
  ```

3. **Initialize Database**:
- Run `database_setup.sql` in SQL Server Management Studio (SQL Server)
- OR let the app auto-create SQLite database on first run
- Script creates tables: UserProfile, CyberTasks, QuizQuestions, QuizAnswers, ActivityLog

4. **Build the Solution**:

## Cybersecurity Quiz Content Examples

### Password Safety Quiz
- Q1: What makes a strong password?
- A) At least 8 characters with numbers and symbols
- B) Your dog's name
- C) Same password for all sites
- D) Birthday

### Phishing Quiz
- Q1: How to identify a phishing email?
- A) Suspicious sender, misspellings, urgency
- B) All emails are phishing
- C) Phishing is only via phone
- D) Can't identify phishing

### Malware Quiz
- Q1: Best protection against malware?
- A) Updated antivirus + avoid untrusted downloads
- B) Disable antivirus for speed
- C) Open all attachments
- D) No protection needed

## Delegate Usage (Part 2 Compliance)
- `BotResponseHandler` delegate continues in Part 3
- Assigned in `MainForm.Load()` for chat tab updates
- Can be extended for Activity Logger notifications or Quiz feedback display

## Edge Cases Handled
- Empty quiz attempts (validation before submit)
- Network/database unavailable (fallback to in-memory storage with warning)
- User completes task multiple times (tracked in database)
- Concurrent activity logging (thread-safe database writes)
- Invalid SQL data (try-catch in repositories with graceful error messaging)

## Testing Checklist
- [ ] Database initialization and schema creation successful
- [ ] Quiz attempts logged to database
- [ ] Task completion tracked correctly
- [ ] Activity log shows all user actions with timestamps
- [ ] NLP processor identifies intents (e.g., "I want to quiz" → Quiz intent)
- [ ] Voice greeting plays on app startup
- [ ] Chat, Quiz, Tasks, Progress tabs navigate smoothly
- [ ] Sentiment detection works with new NLP processor
- [ ] Graceful fallback if database unavailable
- [ ] All Collections (Dictionary, List) used as per rubric

## References
- Part 1: `CyberSecurityAwarenessBot` (console chatbot)
- Part 2: `CybersecurityChatbotGUI` (GUI chatbot - this is the baseline)
- Part 3: `CybersecurityAwarenessBot` (enhanced with database + features)

## Notes for Assessors
- Part 2 chatbot refactored into `Services/ChatBot.cs` for reusability
- All Part 2 features (sentiment, memory, voice, random responses) preserved
- New Part 3 requirements addressed: database persistence, quizzes, tasks, activity logging
- Meaningful Collections usage: `Dictionary<string, List<T>>`, `List<T>` per rubric
- Delegate: `BotResponseHandler` maintained for UI updates
- Graceful error handling for database failures