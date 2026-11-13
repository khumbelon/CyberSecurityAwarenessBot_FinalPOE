# PROG6221 Programming 2A - POE Part 3
## Student Details
- **Module**: PROG6221 Programming 2A
- **Part**: POE Part 3 - Enhanced GUI with Database, Quizzes & Task Management
- **IDE**: Visual Studio 2022
- **.NET Target**: .NET 6.0 or .NET Framework 4.7.2 Windows Forms
- **Database**: SQL Server / SQLite
- **Submission Date**: 13 November 2026

## Project Overview
**Part 3** extends the Cybersecurity Awareness Chatbot from Part 2 with enterprise-grade features:
- **Persistent Data Storage**: SQL database integration for quizzes, tasks, user progress, and activity logs
- **Interactive Quiz System**: Multiple-choice cybersecurity quizzes with immediate feedback and scoring
- **Task Management**: Guided learning paths with completion tracking (e.g., "Create a strong password")
- **Activity Analytics**: Comprehensive logging of all user interactions for reporting
- **Enhanced NLP**: Advanced text processing for intent detection and entity extraction
- **Refactored Architecture**: Part 2 chatbot logic moved to Services layer for reusability and maintainability

The existing Part 2 chatbot features are **fully preserved and enhanced**:
- ✅ Voice greeting on startup
- ✅ Sentiment detection (Worried, Frustrated, Curious, frustrated)
- ✅ Memory storage (user interests and preferences)
- ✅ Keyword-based topic responses
- ✅ Conversation flow ("tell me more")
- ✅ Random response variation
- ✅ GUI with chat history and input controls

## Part 3 Core Features

1. Database Integration
2. Quiz System
3. Task Management
4. Activity Logging & Analytics
5. NLP Enhancement

## Project Structure
CyberSecurityAwarenessBot/
│
├── MainForm.cs                      // Main GUI - navigation hub for all features
├── MainForm.Designer.cs             // Windows Forms designer
│
├── Models/
│   ├── CyberTask.cs                 // Task entity (Id, Title, Description, Category, Completed, Dates)
│   ├── QuizQuestion.cs              // Quiz question (Id, Text, Options[], CorrectAnswer, Category, Difficulty)
│   ├── QuizAnswerResult.cs          // Quiz attempt (UserId, QuestionId, SelectedAnswer, IsCorrect, AttemptDate)
│   └── ActivityEntry.cs             // Activity log (UserId, ActionType, Category, Details, Timestamp)
│
├── Data/
│   ├── DatabaseConfig.cs            // Connection string config, schema initialization methods
│   ├── TaskRepository.cs            // CRUD operations for CyberTask
│   
│
├── Services/
│   ├── ChatBot.cs                   // Refactored Part 2 chatbot (ProcessInput, SendResponse)
│   ├── SentimentDetector.cs         // Sentiment analysis (Worried, Frustrated, Curious)
│   ├── MemoryStore.cs               // User memory (name, interests, current topic)
│   ├── KeywordResponder.cs          // Dictionary-based keyword responses with random selection
│   ├── QuizManager.cs               // Quiz orchestration (GetQuizByCategory, SubmitAnswer, GetScorecard)
│   ├── NlpProcessor.cs              // NLP services (DetectIntent, ExtractEntities, GetContextualResponse)
│   ├── ActivityLogger.cs            // Activity logging (LogChatMessage, LogQuizAttempt, LogTaskCompletion)
│   └── VoiceGreeting.cs             // Voice greeting on startup (SoundPlayer)
│
├── Assets/
│   └── greeting.wav                 // Voice greeting audio file (relative path via Application.StartupPath)
│
├── database_setup.sql               // SQL schema for all database tables and relationships
├── README.md                        // This file
└── .github/workflows/               // Optional: CI/CD pipeline definitions

## Database Schema Overview

### Tables

1. UserProfile
2. CyberTasks
3. QuizQuestions
4. QuizAnswers
5. ActivityLog

## Models
### CyberTask.cs
### QuizQuestion.cs
### QuizAnswerResult.cs
### ActivityEntry.cs.

## Services

### ChatBot.cs (Refactored from Part 2)
### SentimentDetector.cs (Part 2 Feature)
### MemoryStore.cs (Part 2 Feature)
### KeywordResponder.cs (Part 2 Feature)
### QuizManager.cs (Part 3 New Service)
### NlpProcessor.cs (Part 3 New Service)
### ActivityLogger.cs (Part 3 New Service)
### VoiceGreeting.cs (Part 2 Feature)

## GUI Enhancements (Part 3 Changes to Part 2)

### MainForm.cs
### Navigation Tabs
1. Chat Tab (Part 2 Chatbot)
- **ASCII Art Banner** (Consolas font for alignment)
- **Chat History** (RichTextBox, read-only)
- **Input TextBox** (accepts user queries)
- **Send Button** (or Enter key)
- **Features**:
- Voice greeting plays on startup
- Sentiment-aware responses
- Memory personalization
- "Tell me more" follow-ups
- Dynamic topic responses

#### 2. Quiz Tab
**Components:**
- **Category Dropdown** - Select topic for quiz
- Options: "Password Safety", "Phishing", "Malware", "Scams", "Privacy", "All Categories"
- **Question Display Panel** (Label or TextBox)
- Shows current question text
- Shows question number (e.g., "Question 1 of 10")
- **Answer Option Buttons** (4 buttons for A, B, C, D)
- Buttons disabled until user clicks one
- **Feedback Label** (Shows explanation and score)
- **Progress Bar** (Shows % of quiz completed)
- **Next Button** (Proceeds to next question)

#### 3. Tasks Tab
**Components:**
- **Tasks List** (ListView or ListBox)
- Shows all assigned tasks
- Checkbox for each task (editable)
- Task title and category
- **Task Details Panel** (TextBox/RichTextBox)
- Full task description
- Step-by-step instructions
- Assigned date
- Completed date (if finished)
- **Completion Percentage** (Label)
- Shows "X/Y tasks completed"
- Progress bar visualization

## Delegate Usage (Part 2 Compliance)

### BotResponseHandler Delegate
**Defined in ChatBot.cs:**
public delegate void BotResponseHandler(string message);
**Assigned in MainForm.Load():**
**Invoked in ChatBot.SendResponse():**
**Callback Implementation in MainForm:**
**Extensions in Part 3:**
- Delegate can notify Activity Logger
- Delegate can update UI progress indicators
- Delegate can trigger quiz feedback display


## How to Setup & Run Part 3

### Prerequisites
- **Visual Studio 2022** (Professional, Community, or Enterprise)
- **.NET 6.0 SDK** or **.NET Framework 4.7.2 Runtime**
- **SQL Server Express 2019+** OR **SQLite**
- **Windows 10/11** with Windows Forms support


## Testing Checklist

### Database Testing
- [ ] **Schema Creation**: `database_setup.sql` successfully creates all 5 tables
- [ ] **Connection String**: Application connects to SQL Server or SQLite without errors
- [ ] **Fallback Logic**: Application displays warning and continues if database unavailable
- [ ] **Data Persistence**: Quiz scores persist after app restart
- [ ] **Task Tracking**: Completed tasks marked as complete in database
- [ ] **Activity Log**: All actions logged with correct timestamps

### Chat Tab Testing
- [ ] **Voice Greeting**: Audio plays on startup (or error message if file missing)
- [ ] **ASCII Art**: Header displays correctly with Consolas font
- [ ] **Name Capture**: User name captured on first input
- [ ] **Sentiment Detection**: "Worried" responses include empathy prefix
- [ ] **Keyword Responses**: Asking about "passwords" returns password tips
- [ ] **Random Responses**: Asking same topic twice returns different tips
- [ ] **"Tell me more"**: Continues previous topic
- [ ] **Unknown Topics**: Returns default helpful message
- [ ] **Empty Input**: Prompts user to enter question

### Quiz Tab Testing
- [ ] **Category Selection**: Dropdown loads all 5 categories
- [ ] **Questions Load**: Clicking category displays first question
- [ ] **Answer Options**: All 4 options (A, B, C, D) display correctly
- [ ] **Immediate Feedback**: Correct answer shows green, incorrect shows red
- [ ] **Scoring**: Answer logged to database as correct/incorrect
- [ ] **Progress Bar**: Shows % of quiz completed
- [ ] **Next Button**: Advances to next question
- [ ] **Final Scorecard**: Shows total score, category breakdown
- [ ] **Multiple Attempts**: User can retake quiz and scores tracked

### Tasks Tab Testing
- [ ] **Task List Loads**: All assigned tasks display
- [ ] **Task Details**: Clicking task shows full description
- [ ] **Checkbox Marking**: Checking box marks task complete
- [ ] **Completion Date**: Completed timestamp recorded in database
- [ ] **Progress Percentage**: Shows "X/Y completed"
- [ ] **Multiple Completions**: User can mark same task complete again (tracked separately)

### Progress Tab Testing
- [ ] **Quiz Statistics**: Shows attempts, score %, category breakdown
- [ ] **Task Statistics**: Shows completion % and count
- [ ] **Activity Timeline**: Lists all actions in chronological order
- [ ] **Timestamps**: All entries show correct date/time
- [ ] **Action Types**: Differentiates ChatMessage, QuizAttempt, TaskCompleted, VoiceGreeting
- [ ] **Export Report**: (Optional) Generates CSV/PDF with all data

### NLP Testing
- [ ] **Intent Detection**: "I want a quiz" detected as Quiz intent
- [ ] **Intent Detection**: "Tell me about passwords" detected as Learn intent
- [ ] **Entity Extraction**: Extracts topic from user input (e.g., "phishing")
- [ ] **Contextual Responses**: Combines intent + entities for smart reply

### Performance Testing
- [ ] **App Startup**: Loads within 2 seconds
- [ ] **Quiz Loading**: Questions display within 1 second
- [ ] **Report Generation**: Progress report loads within 2 seconds
- [ ] **Database Queries**: No noticeable lag during CRUD operations

### Error Handling Testing
- [ ] **Missing Audio File**: Graceful error message, app continues
- [ ] **Database Down**: Warning shown, in-memory mode activated
- [ ] **Corrupted Data**: Invalid records skipped with logged error
- [ ] **Concurrent Operations**: No data corruption or crashes

  ## Releases
- v1.0-part1
- v2.0-part2
- v3.0-final
## Video Presentation
[https://youtu.be/uTS3uLHMaMc].

 ## Appendix B. Suggested evidence screenshots
 <img width="790" height="265" alt="CI Workflow" src="https://github.com/user-attachments/assets/a724a519-08cc-45bd-bd7a-1b1a090a5283" /> --CI workflow
 <img width="984" height="681" alt="Chat screen" src="https://github.com/user-attachments/assets/f480a697-041d-4804-9cb5-dd9549f240f7" />  --Chat screen
 <img width="1251" height="649" alt="Commits history" src="https://github.com/user-attachments/assets/77a4f19c-8d76-429e-800b-f3071c79b508" /> Commits history
 <img width="977" height="611" alt="final quiz score" src="https://github.com/user-attachments/assets/5144e3e0-99b2-4104-9f3e-aee1a0bbeb67" /> finalQuiz
<img width="292" height="345" alt="MySQL Workbench" src="https://github.com/user-attachments/assets/b0d0a227-fc97-4ab2-ab41-cea0ebc59669" /> My SQL workbench
<img width="284" height="228" alt="Releases" src="https://github.com/user-attachments/assets/a7cb1299-5879-46a5-86cd-67c18255d835" /> Releases
<img width="981" height="686" alt="quiz question and feedback" src="https://github.com/user-attachments/assets/4da9fb4c-992e-408c-94d0-611ac2dc4623" /> ---quiz question and feedback
<img width="984" height="689" alt="Tasks" src="https://github.com/user-attachments/assets/82769b61-6028-4264-b8f9-4c43169ae8e9" /> Task
<img width="899" height="1599" alt="IMG-20261112-WA0145" src="https://github.com/user-attachments/assets/da2e7641-5163-47a3-91bc-c608be79d833" /> Activity.
 
 ## References & Related Projects

### Part 1: CyberSecurityAwarenessBot (Console)
- **Location**: `CyberSecurityAwarenessBot/` folder in solution
- **Features**: Basic chatbot, console-only, no GUI
- **Files**: `Program.cs`, `Chatbot.cs`, `UserProfile.cs`, `ResponseHandler.cs`
- **Purpose**: Foundation for Part 2 and Part 3

### Part 2: CybersecurityChatbotGUI (GUI with Voice)
- **Location**: `CybersecurityChatbotGUI/` folder in solution
- **Features**: Windows Forms GUI, voice greeting, sentiment, memory
- **Files**: `ChatbotForm.cs`, `ChatBot.cs`, `SentimentDetector.cs`, `MemoryStore.cs`, `KeywordResponder.cs`, `VoiceGreeting.cs`
- **Purpose**: Enhanced UX with voice and graphical chat interface

### Part 3: CyberSecurityAwarenessBot (Database + Quizzes + Tasks)
- **Location**: `CyberSecurityAwarenessBot/` folder (refactored)
- **Features**: Database persistence, quizzes, task management, activity analytics
- **New Files**: `database_setup.sql`, Models/, Data/, enhanced Services/
- **Purpose**: Enterprise features with persistent data storage

---

## Notes for Assessors

### Part 2 Preservation
- ✅ **All Part 2 features intact**:
  - Chatbot logic (sentiment, keywords, memory) preserved in `Services/ChatBot.cs`
  - Voice greeting functionality
  - Random response variation
  - Conversation flow ("tell me more")
  - GUI with chat history
  - Delegate pattern (`BotResponseHandler`)

- ✅ **No breaking changes**: Existing Part 2 behavior fully functional in Part 3 Chat tab

### Part 3 Enhancements
- ✅ **Database Integration**: All user data persisted in SQL schema
- ✅ **Quiz System**: Fully functional quiz with scoring and feedback
- ✅ **Task Management**: Assigned tasks with completion tracking
- ✅ **Activity Logging**: Comprehensive audit trail
- ✅ **Collections Usage**: `Dictionary<string, List<T>>` and `List<T>` throughout codebase
- ✅ **Delegate Pattern**: `BotResponseHandler` maintained and extended
- ✅ **Error Handling**: Graceful fallback for database failures

## License & Attribution
- Project: PROG6221 Programming 2A - POE
- Institution: [Rosebank Collage]
- Academic Integrity: This is an academic submission. Do not plagiarize or use for unauthorized purposes.

**Last Updated**: November 13, 2026 
**Version**: Part 3 - Final  
**Status**: Ready for Assessment


