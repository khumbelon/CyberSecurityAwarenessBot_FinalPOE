using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CybersecurityChatbotGUI.Models;
using CybersecurityAwarenessBot.Data;

namespace CybersecurityChatbotGUI
{
    internal partial class MainForm : Form
    {
        private readonly VoiceGreeting voiceGreeting = new VoiceGreeting();
        private readonly ChatBot chatBot = new ChatBot();
        private readonly QuizManager quizManager = new QuizManager();
        private readonly ActivityLogger activityLogger = new ActivityLogger();
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly List<CyberTask> loadedTasks = new List<CyberTask>();
        private readonly NlpProcessor nlpProcessor = new NlpProcessor();

        private TabControl tabMain = null!;
        private TextBox txtChatInput = null!;
        private RichTextBox txtChatOutput = null!;
        private ListBox lstTasks = null!;
        private TextBox txtTaskTitle = null!;
        private TextBox txtTaskDescription = null!;
        private DateTimePicker dtpReminder = null!;
        private CheckBox chkReminder = null!;
        private Label lblQuizQuestion = null!;
        private FlowLayoutPanel pnlQuizOptions = null!;
        private Label lblQuizStatus = null!;
        private ListBox lstActivity = null!;

    

        public MainForm()
        {
            InitializeComponent();
            BuildGuiShell();
            Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            Task.Run(() => voiceGreeting.PlayWelcome());
            LoadTasks();
            RefreshActivity();

        }

        private void BuildGuiShell()
        {
            Text = "Cybersecurity Awareness Bot ";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new System.Drawing.Size(850, 600);
            Size = new System.Drawing.Size(1000, 700);

            RichTextBox header = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = System.Drawing.Color.FromArgb(15, 23, 42),
                ForeColor = System.Drawing.Color.FromArgb(125, 211, 252),
                Font = new System.Drawing.Font("Consolas", 11F),
                Height = 90,
                Text = @"  ______   ______  _____ ____  ____  _____ ____ 
 / ___\ \ / / __ )| ____|  _ \/ ___|| ____/ ___|
| |    \ V /|  _ \|  _| | |_) \___ \|  _|| |    
| |___  | | | |_) | |___|  _ < ___) | |__| |___ 
 \____| |_| |____/|_____|_| \_\____/|_____\____| 
            "
            };

            tabMain = new TabControl
            {
                Name = "tabMain",
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(15, 23, 42),
                ForeColor = System.Drawing.Color.White
            };

            tabMain.TabPages.Add(BuildChatTab());
            tabMain.TabPages.Add(BuildTasksTab());
            tabMain.TabPages.Add(BuildQuizTab());
            tabMain.TabPages.Add(BuildActivityTab());

            foreach (TabPage tabPage in tabMain.TabPages)
            {
                tabPage.BackColor = System.Drawing.Color.White;
                tabPage.ForeColor = System.Drawing.Color.Black;
            }

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(header, 0, 0);
            mainLayout.Controls.Add(tabMain, 0, 1);
            Controls.Add(mainLayout);

        }

        private TabPage BuildChatTab()
        {
            TabPage tab = new TabPage("Chat") { Name = "tabChat" };
            TableLayoutPanel layout = CreateLayout(2, 3);

            txtChatOutput = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = System.Drawing.Color.White
            };
            layout.Controls.Add(txtChatOutput, 0, 0);
            layout.SetColumnSpan(txtChatOutput, 2);

            txtChatInput = new TextBox { Dock = DockStyle.Fill };
            txtChatInput.KeyDown += ChatInput_KeyDown;
            layout.Controls.Add(txtChatInput, 0, 1);

            Button sendButton = new Button { Text = "Send", AutoSize = true };
            StyleButton(sendButton);
            sendButton.Click += SendChatMessage;
            layout.Controls.Add(sendButton, 1, 1);

            Label hint = new Label
            {
                Text = "Ask about passwords, phishing, privacy, malware, safe browsing, or 2FA.",
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(3)
            };
            layout.Controls.Add(hint, 0, 2);
            layout.SetColumnSpan(hint, 2);
            tab.Controls.Add(layout);
            return tab;
        }

        private TabPage BuildTasksTab()
        {
            TabPage tab = new TabPage("Tasks") { Name = "tabTasks" };
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 350
            };

            lstTasks = new ListBox { Dock = DockStyle.Fill };
            split.Panel1.Controls.Add(lstTasks);

            TableLayoutPanel editor = CreateLayout(2, 5);
            editor.Controls.Add(new Label { Text = "Title", AutoSize = true }, 0, 0);
            txtTaskTitle = new TextBox { Dock = DockStyle.Fill };
            editor.Controls.Add(txtTaskTitle, 1, 0);
            editor.Controls.Add(new Label { Text = "Description", AutoSize = true }, 0, 1);
            txtTaskDescription = new TextBox { Dock = DockStyle.Fill };
            editor.Controls.Add(txtTaskDescription, 1, 1);
            editor.Controls.Add(new Label { Text = "Reminder", AutoSize = true }, 0, 2);
            FlowLayoutPanel reminderPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
            chkReminder = new CheckBox { Text = "Set reminder" };
            dtpReminder = new DateTimePicker { Format = DateTimePickerFormat.Short, Enabled = false };
            chkReminder.CheckedChanged += (sender, e) => dtpReminder.Enabled = chkReminder.Checked;
            reminderPanel.Controls.Add(chkReminder);
            reminderPanel.Controls.Add(dtpReminder);
            editor.Controls.Add(reminderPanel, 1, 2);

            FlowLayoutPanel buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
            Button addButton = new Button { Text = "Add task", AutoSize = true };
            StyleButton(addButton);
            addButton.Click += AddTask;
            Button completeButton = new Button { Text = "Mark completed", AutoSize = true };
            StyleButton(completeButton);
            completeButton.Click += MarkTaskCompleted;
            Button deleteButton = new Button { Text = "Delete", AutoSize = true };
            StyleButton(deleteButton);
            deleteButton.Click += DeleteTask;
            Button refreshButton = new Button { Text = "Refresh", AutoSize = true };
            StyleButton(refreshButton);
            refreshButton.Click += (sender, e) => LoadTasks();
            buttons.Controls.Add(addButton);
            buttons.Controls.Add(completeButton);
            buttons.Controls.Add(deleteButton);
            buttons.Controls.Add(refreshButton);
            editor.Controls.Add(buttons, 1, 3);
            editor.SetColumnSpan(buttons, 2);
            split.Panel2.Controls.Add(editor);
            tab.Controls.Add(split);
            return tab;
        }

        private TabPage BuildQuizTab()
        {
            TabPage tab = new TabPage("Cyber Quiz") { Name = "tabQuiz" };
            TableLayoutPanel layout = CreateLayout(1, 4);
            lblQuizQuestion = new Label { Dock = DockStyle.Fill, AutoSize = false, Padding = new Padding(8) };
            layout.Controls.Add(lblQuizQuestion, 0, 0);
            pnlQuizOptions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            layout.Controls.Add(pnlQuizOptions, 0, 1);
            FlowLayoutPanel buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
            Button startButton = new Button { Text = "Start quiz", AutoSize = true };
            StyleButton(startButton);
            startButton.Click += StartQuiz;
            Button answerButton = new Button { Text = "Check answer", AutoSize = true };
            StyleButton(answerButton);
            answerButton.Click += CheckQuizAnswer;
            Button nextButton = new Button { Text = "Next question", AutoSize = true };
            StyleButton(nextButton);
            nextButton.Click += NextQuizQuestion;
            buttons.Controls.Add(startButton);
            buttons.Controls.Add(answerButton);
            buttons.Controls.Add(nextButton);
            layout.Controls.Add(buttons, 0, 2);
            lblQuizStatus = new Label { Text = "Start the quiz when you are ready.", AutoSize = true, Padding = new Padding(8) };
            layout.Controls.Add(lblQuizStatus, 0, 3);
            tab.Controls.Add(layout);
            return tab;
        }

        private static void StyleButton(Button button)
        {
            button.BackColor = System.Drawing.Color.FromArgb(14, 116, 144);
            button.ForeColor = System.Drawing.Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(8, 145, 178);
            button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(6, 148, 162);
            button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            button.Padding = new Padding(6, 3, 6, 3);
        }

        private TabPage BuildActivityTab()
        {
            TabPage tab = new TabPage("Activity Log") { Name = "tabActivity" };
            lstActivity = new ListBox { Dock = DockStyle.Fill };
            tab.Controls.Add(lstActivity);
            return tab;
        }

        private static TableLayoutPanel CreateLayout(int columns, int rows)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = columns,
                RowCount = rows,
                Padding = new Padding(8)
            };
            for (int i = 0; i < columns; i++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, i == columns - 1 ? 25 : 75));
            for (int i = 0; i < rows; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, i == 0 ? 70 : 30));
            return layout;
        }

        private void ChatInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                SendChatMessage(sender, EventArgs.Empty);
            }
        }

        private void SendChatMessage(object? sender, EventArgs e)
        {
            string input = txtChatInput.Text.Trim();
            if (input.Length == 0)
                return;
            string response = chatBot.ProcessInput(input);
            txtChatOutput.AppendText($"You: {input}{Environment.NewLine}Bot: {response}{Environment.NewLine}{Environment.NewLine}");
            txtChatInput.Clear();
            activityLogger.Log("Chat message sent");
            RefreshActivity();
        }

        private void LoadTasks()
        {
            try
            {
                loadedTasks.Clear();
                loadedTasks.AddRange(taskRepository.GetAllTasks());
                lstTasks.Items.Clear();
                foreach (CyberTask task in loadedTasks)
                {
                    string reminder = task.ReminderDate.HasValue
                        ? $" | Reminder: {task.ReminderDate.Value:d}"
                        : "";
                    string status = task.IsCompleted ? "Completed" : "Open";
                    lstTasks.Items.Add($"[{status}] {task.Title}{reminder}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load tasks: " + ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTask(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Enter a task title.", "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                taskRepository.AddTask(new CyberTask
                {
                    Title = txtTaskTitle.Text.Trim(),
                    Description = txtTaskDescription.Text.Trim(),
                    ReminderDate = chkReminder.Checked ? dtpReminder.Value : null,
                    IsCompleted = false
                });
                activityLogger.Log("Task added");
                txtTaskTitle.Clear();
                txtTaskDescription.Clear();
                chkReminder.Checked = false;
                LoadTasks();
                RefreshActivity();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add task: " + ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? SelectedTaskId()
        {
            int index = lstTasks.SelectedIndex;
            return index >= 0 && index < loadedTasks.Count ? loadedTasks[index].TaskId : null;
        }

        private void MarkTaskCompleted(object? sender, EventArgs e)
        {
            int? taskId = SelectedTaskId();
            if (!taskId.HasValue)
                return;
            try
            {
                taskRepository.MarkTaskCompleted(taskId.Value);
                activityLogger.Log("Task marked completed");
                LoadTasks();
                RefreshActivity();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update task: " + ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteTask(object? sender, EventArgs e)
        {
            int? taskId = SelectedTaskId();
            if (!taskId.HasValue)
                return;
            try
            {
                taskRepository.DeleteTask(taskId.Value);
                activityLogger.Log("Task deleted");
                LoadTasks();
                RefreshActivity();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to delete task: " + ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartQuiz(object? sender, EventArgs e)
        {
            quizManager.StartQuiz();
            DisplayQuizQuestion();
            activityLogger.Log("Quiz started");
            RefreshActivity();
        }

        private void DisplayQuizQuestion()
        {
            lblQuizQuestion.Text = quizManager.CurrentQuestion.QuestionText;
            pnlQuizOptions.Controls.Clear();
            for (int i = 0; i < quizManager.CurrentQuestion.Options.Count; i++)
            {
                pnlQuizOptions.Controls.Add(new RadioButton
                {
                    Text = quizManager.CurrentQuestion.Options[i],
                    Tag = i,
                    AutoSize = true,
                    Margin = new Padding(3)
                });
            }
            lblQuizStatus.Text = $"Question {quizManager.CurrentIndex + 1} of {quizManager.TotalQuestions} | Score: {quizManager.Score}";
        }

        private void CheckQuizAnswer(object? sender, EventArgs e)
        {
            if (!quizManager.QuizStarted)
                return;
            RadioButton? selected = pnlQuizOptions.Controls.OfType<RadioButton>().FirstOrDefault(radio => radio.Checked);
            if (selected == null)
                return;
            QuizAnswerResult result = quizManager.CheckAnswer((int)selected.Tag!);
            lblQuizStatus.Text = result.Feedback + $" Score: {quizManager.Score}";
            activityLogger.Log("Quiz answer submitted");
            RefreshActivity();
        }

        private void NextQuizQuestion(object? sender, EventArgs e)
        {
            if (!quizManager.QuizStarted)
                return;
            if (quizManager.MoveNext())
            {
                DisplayQuizQuestion();
            }
            else
            {
                lblQuizQuestion.Text = "Quiz complete.";
                pnlQuizOptions.Controls.Clear();
                lblQuizStatus.Text = $"Final score: {quizManager.Score} of {quizManager.TotalQuestions}";
            }
        }

        private void RefreshActivity()
        {
            if (lstActivity == null)
                return;
            lstActivity.Items.Clear();
            foreach (ActivityEntry entry in activityLogger.GetRecent(50))
                lstActivity.Items.Add(entry.ToString());
        }

        private void TestDatabaseConnection()
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    MessageBox.Show("Database connection successful.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
            }
        }

    }
}
