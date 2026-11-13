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

### Relationships

UserProfile
  ├─→ CyberTasks (1-to-Many)
  ├─→ QuizAnswers (1-to-Many)
  └─→ ActivityLog (1-to-Many)

QuizQuestions
  └─→ QuizAnswers (1-to-Many)

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
- **Scorecard Link** (Views quiz statistics)

**Flow:**
1. User selects category
2. Quiz starts, question 1 displayed
3. User clicks answer button
4. Immediate feedback shown
5. Next button enabled
6. Repeat for all questions
7. Final scorecard with score and category breakdown

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

**Flow:**
1. User views task list
2. Click task → see details
3. Check checkbox to mark complete
4. Completion date recorded in database
5. Progress percentage updates

#### 4. Progress Tab
**Components:**
- **Quiz Statistics Section**
- Total quiz attempts
- Overall score percentage
- Breakdown by category (Password Safety: 80%, Phishing: 60%, etc.)
- Bar chart (optional visualization)
- **Task Statistics Section**
- Task completion percentage
- Tasks completed vs. total
- Timeline of completions
- **Activity Timeline** (RichTextBox or ListView)
- Chronological list of all user actions
- Format: "[timestamp] [action] [category]"
- Example: "[Nov 11, 2:45 PM] Chat Message about Phishing"
- Example: "[Nov 11, 3:00 PM] Quiz Attempt - Malware Question 3 (Correct)"
- Example: "[Nov 11, 3:15 PM] Task Completed - Create Strong Password"
- **Export Button** (Optional: save report as CSV/PDF)

**Flow:**
1. User navigates to Progress tab
2. Sees quiz stats and task progress
3. Scrolls through activity timeline
4. Optionally exports report

---

## Collections & Data Structures (Rubric Compliance)

### Dictionary<string, List<string>> Usage
**KeywordResponder.cs:**
### Dictionary<string, List<CyberTask>> Usage
**TaskRepository.cs:**
### Dictionary<string, QuizAnswerResult> Usage
**QuizRepository.cs:**
### List<QuizQuestion> Usage
**QuizManager.cs:**

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

### Step-by-Step Setup

#### 1. Clone the Repository

git clone <your-github-link>
cd CyberSecurityAwarenessBot


#### 2. Configure Database Connection

**Option A: SQL Server (Manual)**
1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your SQL Server instance
3. Open `database_setup.sql`
4. Execute the script
5. Verify tables created: UserProfile, CyberTasks, QuizQuestions, QuizAnswers, ActivityLog

**Option B: SQLite (Automatic)**
- Application auto-creates `cybersecurity_chatbot.db` on first run
- No manual setup required

**Option C: Programmatic Initialization**

// In Program.cs or MainForm.Load():
DatabaseConfig.InitializeDatabase();


#### 4. Build the Solution

Ctrl+Shift+B
Or via command line:
dotnet build
**Expected Output:**
Build succeeded. 0 Warning(s)

#### 5. Set Startup Project
- In **Solution Explorer**, right-click `CyberSecurityAwarenessBot` (NOT the console project)
- Select **Set as Startup Project**
- Verify it's bolded in the tree

#### 6. Verify Assets
- Check `Assets/greeting.wav` exists
- In **Properties**, set:
  - **Build Action**: Content
  - **Copy to Output Directory**: Copy if newer
- This ensures relative path works: `Path.Combine(Application.StartupPath, "Assets", "greeting.wav")`

#### 7. Run the Application

F5 Or **Debug** → **Start Debugging**

**Expected Behavior:**
1. Voice greeting plays on startup ✓
2. Main form opens in maximized window ✓
3. Chat tab active with ASCII art header ✓
4. User can enter name and ask questions ✓
5. Quiz tab loads questions from database ✓
6. Tasks tab shows assigned tasks ✓
7. Progress tab displays statistics ✓

---

## Cybersecurity Quiz Content Examples

### Password Safety Quiz

**Question 1 (Difficulty 1):**
- Q: What makes a strong password?
- A) At least 8 characters with numbers and symbols
- B) Your dog's name
- C) Same password for all sites
- D) Your birthday
- **Answer**: A
- **Explanation**: Strong passwords use length, complexity (numbers/symbols), and uniqueness across sites.

**Question 2 (Difficulty 2):**
- Q: What should you do if a website is breached?
- A) Change your password immediately
- B) Use the same password everywhere for consistency
- C) Ignore it—one breach doesn't matter
- D) Delete your account
- **Answer**: A
- **Explanation**: Change compromised passwords ASAP and check if other accounts use the same password.

### Phishing Quiz

**Question 1 (Difficulty 1):**
- Q: How to identify a phishing email?
- A) Suspicious sender, misspellings, urgency tactics
- B) All emails are phishing
- C) Phishing only happens via phone
- D) Can't identify phishing
- **Answer**: A
- **Explanation**: Phishing emails use urgency, fake sender addresses, poor grammar, and suspicious links.

**Question 2 (Difficulty 3):**
- Q: Before clicking an email link, what should you do?
- A) Check the URL by hovering over the link
- B) Click immediately—your email provider filters phishing
- C) Reply asking if it's legitimate
- D) Open the attachment to verify sender
- **Answer**: A
- **Explanation**: Hover over links to see the actual URL before clicking. Scammers hide real URLs behind text.

### Malware Quiz

**Question 1 (Difficulty 1):**
- Q: Best protection against malware?
- A) Updated antivirus + avoid untrusted downloads
- B) Disable antivirus for speed
- C) Open all attachments
- D) No protection needed
- **Answer**: A
- **Explanation**: Keep antivirus updated, only download from trusted sources, and never auto-run attachments.

**Question 2 (Difficulty 2):**
- Q: What's the risk of using public WiFi?
- A) Attackers can intercept unencrypted data
- B) Public WiFi is always safe
- C) You can't get malware from public WiFi
- D) Your device will explode
- **Answer**: A
- **Explanation**: Public WiFi is unsecured. Use VPN, avoid banking/shopping, and disable auto-connect.

### Scams Quiz

**Question 1 (Difficulty 2):**
- Q: What's a common social engineering tactic?
- A) Pretending to be IT support to get passwords
- B) Always being honest about identity
- C) Using legitimate company names only
- D) Asking permission first
- **Answer**: A
- **Explanation**: Scammers impersonate trusted entities (IT, banks, support) to manipulate victims.

### Privacy Quiz

**Question 1 (Difficulty 2):**
- Q: What should you check before installing an app?
- A) Requested permissions—only allow necessary ones
- B) Always grant all permissions requested
- C) Permissions don't matter
- D) Only check price
- **Answer**: A
- **Explanation**: Apps often request excessive permissions. Grant only what's truly needed for functionality.

---

## Edge Cases Handled

### Database Failures
- **Scenario**: SQL Server unavailable or connection fails
- **Handling**: Log warning to console, fall back to in-memory storage
- **User Experience**: "Warning: Database unavailable. Running in memory-only mode. Data will not be saved."

### Empty User Input
- **Scenario**: User presses Send with no text
- **Handling**: Display "Please enter a question..." prompt
- **Part 3 Addition**: Also applies to quiz attempts—can't submit without selecting answer

### Duplicate Task Completion
- **Scenario**: User marks same task as complete multiple times
- **Handling**: Database records each completion as separate log entry, but `Task.Completed` flag remains true
- **Workaround**: UI disables checkbox once `IsCompleted = true`

### Invalid Quiz Answers
- **Scenario**: Corrupted question in database or invalid option selected
- **Handling**: Validate QuestionId and selected answer before logging
- **Fallback**: Skip question, log error, continue to next

### Concurrent Activity Logging
- **Scenario**: Multiple operations logged simultaneously (rare in desktop app, but important)
- **Handling**: Database handles transaction locking; application uses thread-safe repositories

### Missing Audio File
- **Scenario**: `greeting.wav` not found
- **Handling**: Try-catch in `VoiceGreeting.PlayWelcome()`

### Unknown Topic in Keyword Responder
- **Scenario**: User asks about topic not in KeywordResponder dictionary
## Collections Usage Summary (Rubric Compliance)

| Collection Type | Location | Purpose | Example |
|---|---|---|---|
| `Dictionary<string, List<string>>` | `KeywordResponder.cs` | Multiple tips per category | `{ "password": ["Tip1", "Tip2", "Tip3"] }` |
| `Dictionary<string, List<CyberTask>>` | `TaskRepository.cs` | Tasks grouped by category | `{ "phishing": [Task1, Task2] }` |
| `Dictionary<string, QuizAnswerResult>` | `QuizRepository.cs` | User's latest attempts per question | `{ "1": Result, "2": Result }` |
| `List<QuizQuestion>` | `QuizManager.cs` | Question bank for category | `[Question1, Question2, Question3]` |
| `List<ActivityEntry>` | `ActivityLogger.cs` | User activity history | `[Activity1, Activity2, Activity3]` |
| `Dictionary<string, string>` | `MemoryStore.cs` | User interests (Part 2 carryover) | `{ "favorite_topic": "phishing" }` |

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

---

## Project Dependency Graph

┌─────────────────────────────────────────────────┐
│ MainForm (GUI)                                  │
├─────────────────────────────────────────────────┤
│ - ChatBot (Chat tab)                            │
│ - QuizManager (Quiz tab)                        │
│ - TaskRepository (Tasks tab)                    │
│ - ActivityLogger (Progress tab)                 │
└─────────────────────────────────────────────────┘
         ↓
    ┌────────────────────────────────┐
    │ Services Layer                 │
    ├────────────────────────────────┤
    │ - ChatBot                      │
    │ - SentimentDetector            │
    │ - MemoryStore                  │
    │ - KeywordResponder             │
    │ - QuizManager                  │
    │ - NlpProcessor                 │
    │ - ActivityLogger               │
    │ - VoiceGreeting                │
    └────────────────────────────────┘
         ↓
    ┌────────────────────────────────┐
    │ Data/Repository Layer          │
    ├────────────────────────────────┤
    │ - DatabaseConfig               │
    │ - TaskRepository               │
    │ - QuizRepository               │
    └────────────────────────────────┘
         ↓
    ┌────────────────────────────────┐
    │ Database                       │
    ├────────────────────────────────┤
    │ - UserProfile                  │
    │ - CyberTasks                   │
    │ - QuizQuestions                │
    │ - QuizAnswers                  │
    │ - ActivityLog                  │
    └────────────────────────────────┘



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

### Code Quality
- ✅ **Single Responsibility Principle**: Each service has one purpose
- ✅ **Dependency Injection Pattern**: Services injected into repositories/managers
- ✅ **Separation of Concerns**: Models, Data, Services, UI layers clearly separated
- ✅ **XML Documentation**: Methods documented with summary tags
- ✅ **Exception Handling**: Try-catch blocks for network/database errors
- ✅ **Null Safety**: Null-coalescing operators and null checks throughout

### Rubric Compliance
- ✅ **Collections**: Multiple Dictionary and List instances as required
- ✅ **Delegate**: BotResponseHandler implemented and used meaningfully
- ✅ **Database**: SQL schema with proper relationships and constraints
- ✅ **GUI**: Windows Forms with tabs, labels, buttons, lists, progress bars
- ✅ **Voice Greeting**: SoundPlayer implementation preserved
- ✅ **ASCII Art**: Monospaced font (Consolas) for visual alignment
- ✅ **Edge Cases**: Handled empty input, unknown topics, missing files, database failures
- ✅ **Memory/Personalization**: User name and interests stored and recalled

---

## Troubleshooting

### Issue: "Database unavailable" warning on startup
**Solution:**
1. Check SQL Server is running (Services → SQL Server)
2. Verify `ConnectionString` in `DatabaseConfig.cs` is correct
3. Ensure database exists: `CREATE DATABASE CybersecurityChatbot`
4. Confirm user has permissions to create/modify tables

### Issue: Voice greeting doesn't play
**Cause:** `greeting.wav` not found or audio device muted
**Solution:**
1. Check `Assets/greeting.wav` exists in project
2. Verify **Build Action** = **Content**
3. Verify **Copy to Output** = **Copy if newer**
4. Check Windows volume is not muted
5. Test with another audio player

### Issue: Quiz questions don't load
**Cause:** QuizQuestions table empty or database connection failed
**Solution:**
1. Run `database_setup.sql` to populate sample questions
2. Verify database connection with test query
3. Check QuizRepository.GetByCategory() implementation

### Issue: Tasks tab shows empty list
**Cause:** No tasks assigned to current user
**Solution:**
1. Manually insert test data into CyberTasks table:

   INSERT INTO CyberTasks (Title, Description, Category, Completed, AssignedDate)
   VALUES ('Create a Strong Password', '...', 'password', 0, GETDATE());

2. OR create UI button to auto-generate sample tasks

### Issue: Progress tab shows no activity history
**Cause:** ActivityLog table empty (no actions logged yet)
**Solution:**
1. Use chat, quiz, or tasks to generate actions
2. Each action automatically logged to ActivityLog
3. Refresh Progress tab to see updates
---

## License & Attribution
- Project: PROG6221 Programming 2A - POE
- Institution: [Rosebank Collage]
- Academic Integrity: This is an academic submission. Do not plagiarize or use for unauthorized purposes.

**Last Updated**: November 13, 2026 
**Version**: Part 3 - Final  
**Status**: Ready for Assessment


