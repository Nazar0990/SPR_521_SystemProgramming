using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Timer = System.Timers.Timer;

namespace _02_TaskManager
{
    public partial class MainWindow : Window
    {
        private Timer? updateTimer;
        private int updateInterval = 1000; 
        private bool isPaused = false;

        public MainWindow()
        {
            InitializeComponent();
            intervalComboBox.SelectionChanged += IntervalComboBox_SelectionChanged;
            StartAutoUpdate();
            UpdateProcesses();
        }

        private void IntervalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (intervalComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string text = selectedItem.Content.ToString() ?? "";

                if (text == "Пауза")
                {
                    isPaused = true;
                    StopTimer();
                    statusText.Text = "Оновлення призупинено";
                }
                else
                {
                    isPaused = false;
                    int seconds = int.Parse(text.Split(' ')[0]);
                    updateInterval = seconds * 1000;
                    StartAutoUpdate();
                    statusText.Text = $"Оновлення кожні {seconds} сек";
                }
            }
        }

        private void StartAutoUpdate()
        {
            StopTimer();
            updateTimer = new Timer(updateInterval);
            updateTimer.Elapsed += (s, e) =>
            {
                Dispatcher.Invoke(UpdateProcesses);
            };
            updateTimer.AutoReset = true;
            updateTimer.Start();
        }

        private void StopTimer()
        {
            updateTimer?.Stop();
            updateTimer?.Dispose();
        }

        private void UpdateProcesses()
        {
            try
            {
                var processes = Process.GetProcesses()
                    .Select(p => new ProcessInfo(p))
                    .OrderBy(pi => pi.ProcessName)
                    .ToList();

                processDataGrid.ItemsSource = processes;
                statusText.Text = $"Оновлено: {processes.Count} процесів";
            }
            catch (Exception ex)
            {
                statusText.Text = $"Помилка: {ex.Message}";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateProcesses();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (processDataGrid.SelectedItem is ProcessInfo selectedProcess)
            {
                try
                {
                    Process process = Process.GetProcessById(selectedProcess.Id);
                    process.Kill();
                    process.WaitForExit();
                    statusText.Text = $"Процес "{ selectedProcess.ProcessName} " завершено";
                    UpdateProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося завершити процес:{ ex.Message}", 
                        "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Виберіть процес зі списку", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*",
                Title = "Вибрати файл для запуску"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    Process.Start(dialog.FileName);
                    statusText.Text =$"Процес запущено: {dialog.FileName}";
                    UpdateProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося запустити процес:{ ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if 
            (processDataGrid.SelectedItem is
            ProcessInfo selectedProcess)
            {
                string details = $"Назва: " +$"{selectedProcess.ProcessName}" + $"ID процесу: {selectedProcess.Id}" +$"Час запуску: {selectedProcess.StartTime:dd.MM.yyyy HH:mm:ss}" +$"Стан: {selectedProcess.Status}" +$"Пріоритет: {selectedProcess.Priority}" +$"CPU: {selectedProcess.CPU}%" +$"Пам'ять: {selectedProcess.WorkingSet64 / (1024 * 1024):N0} MB"+$"Шлях: {selectedProcess.Path}";

                MessageBox.Show(details, $"Інформація про процес: {selectedProcess.ProcessName}",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Виберіть процес зі списку", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            StopTimer();
            base.OnClosed(e);
        }
    }

    public class ProcessInfo
    {
        public string ProcessName { get; set; }
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public double CPU { get; set; }
        public long WorkingSet64 { get; set; }
        public string Path { get; set; }

        public ProcessInfo(Process process)
        {
            ProcessName = process.ProcessName;
            Id = process.Id;
            StartTime = process.StartTime;
            WorkingSet64 = process.WorkingSet64;

            try
            {
                Path = process.MainModule?.FileName ?? "Невідомо";
            }
            catch
            {
                Path = "Доступ заборонено";
            }

            try
            {
                Status = process.HasExited ? "Завершено" : "Виконується";
                Priority = process.PriorityClass.ToString();
                CPU = 0.0;
            }
            catch
            {
                Status = "Невідомо";
                Priority = "Невідомо";
            }
        }
    }
}