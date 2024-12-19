using System.IO;
using System.Text.Json;
using System.Windows;

namespace iPlanner.Infrastructure.Common
{
    public class FileService
    {
        private readonly string _basePath;

        public FileService()
        {
            _basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public string GetDataFilePath(string fileName)
        {
            return Path.Combine(_basePath, "iPlanner", "Data", fileName);
        }

        public void EnsureDirectoryExists(string filePath)
        {
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void SaveJsonData<T>(string filePath, T data)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public T? LoadJsonData<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath)) return new T();

            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return new T();
            }
        }
    }
}
