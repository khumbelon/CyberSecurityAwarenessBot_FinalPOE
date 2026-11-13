using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CybersecurityChatbotGUI
{
    public class VoiceGreeting
    {
        private bool audioAvailable = false;

        public VoiceGreeting()
        {
            string audioPath = GetAudioPath();
            audioAvailable = !string.IsNullOrEmpty(audioPath) && File.Exists(audioPath);
        }

        /// <summary>
        /// Get path to greeting audio file (Audio folder, not Assets)
        /// </summary>
        private string? GetAudioPath()
        {
            try
            {
                string[] possiblePaths = new[]
                {
                    Path.Combine(Application.StartupPath, "Audio", "greeting.wav"),  // ✅ Audio folder
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "greeting.wav"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Audio", "greeting.wav")
                };

                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Play welcome greeting
        /// </summary>
        public void PlayWelcome()
        {
            try
            {
                if (!audioAvailable)
                {
                    return;
                }

                string? audioPath = GetAudioPath();
                if (string.IsNullOrEmpty(audioPath) || !File.Exists(audioPath))
                {
                    return;
                }

                using (var player = new SoundPlayer(audioPath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VoiceGreeting] Error: {ex.Message}");
            }
        }
    }
}