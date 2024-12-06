using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace iPlanner.Infrastructure.Teams
{
    public class TeamsService : ITeamService
    {
        private readonly string _dataAccessPath;

        public TeamsService()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _dataAccessPath = Path.Combine(basePath, "iPlanner", "Data", "Teams.json");
            EnsureDirectoryExists();
            LoadData();
        }

        public ObservableCollection<TeamDTO>? Teams { get; private set; }

        public ObservableCollection<TeamDTO> LoadData()
        {
            if (File.Exists(_dataAccessPath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_dataAccessPath);
                    Teams = JsonSerializer.Deserialize<ObservableCollection<TeamDTO>>(jsonString)
                        ?? new ObservableCollection<TeamDTO>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los equipos: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Teams = new ObservableCollection<TeamDTO>();
                }
            }
            else
            {
                Teams = new ObservableCollection<TeamDTO>();
            }
            return Teams;
        }

        public void AddTeam(TeamDTO team)
        {
            if (Teams == null) return;
            Teams.Add(team);
            SaveTeams();
        }

        private void SaveTeams()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(Teams, options);
                File.WriteAllText(_dataAccessPath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los equipos: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnsureDirectoryExists()
        {
            string directoryPath = Path.GetDirectoryName(_dataAccessPath);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
