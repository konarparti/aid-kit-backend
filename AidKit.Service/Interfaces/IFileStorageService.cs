using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidKit.Service.Interfaces
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Получить файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Поток файла.</returns>
        Task<Stream> GetFileAsync(string fileName, string filePath);

        /// <summary>
        /// Сохранить файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="filePath">Путь сохранения.</param>
        /// <param name="fileStream">Файл.</param>
        Task SaveFileAsync(string fileName, string filePath, Stream fileStream);
    }
}
