using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AidKit.Service.Interfaces;
using Minio;
using Minio.Exceptions;

namespace AidKit.Service.Implemetions
{
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly MinioClient _minioClient;

        public MinioFileStorageService(string minioHost, string minioAccessKey, string minioSecretKey, bool useHttps)
        {
            if (string.IsNullOrWhiteSpace(minioHost))
            {
                throw new ArgumentException($"\"{nameof(minioHost)}\" не может быть пустым или содержать только пробел.", nameof(minioHost));
            }

            if (string.IsNullOrWhiteSpace(minioAccessKey))
            {
                throw new ArgumentException($"\"{nameof(minioAccessKey)}\" не может быть пустым или содержать только пробел.", nameof(minioAccessKey));
            }

            if (string.IsNullOrWhiteSpace(minioSecretKey))
            {
                throw new ArgumentException($"\"{nameof(minioSecretKey)}\" не может быть пустым или содержать только пробел.", nameof(minioSecretKey));
            }

            _minioClient = new MinioClient(minioHost, minioAccessKey, minioSecretKey);
            if (useHttps)
            {
                _minioClient.WithSSL();
            }
        }

        public async Task<Stream> GetFileAsync(string fileName, string filePath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"\"{nameof(fileName)}\" не может быть пустым или содержать только пробел.", nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"\"{nameof(filePath)}\" не может быть пустым или содержать только пробел.", nameof(filePath));
            }

            try
            {
                await _minioClient.StatObjectAsync(filePath, fileName);
            }
            catch (BucketNotFoundException exc)
            {
                throw new InvalidOperationException("Хранилище с таким именем не найдено.", exc);
            }

            var stream = new MemoryStream();

            await _minioClient.GetObjectAsync(filePath, fileName,
                (str) =>
                {
                    str.CopyTo(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                });

            return stream;
        }

        public async Task SaveFileAsync(string fileName, string filePath, Stream fileStream)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"\"{nameof(fileName)}\" не может быть пустым или содержать только пробел.", nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"\"{nameof(filePath)}\" не может быть пустым или содержать только пробел.", nameof(filePath));
            }

            if (fileStream is null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            await CheckExistenceOrCreateBucketAsync(filePath);
            try
            {
                //При повторении имени, заменяет один файл - другим
                await _minioClient.PutObjectAsync(filePath, fileName, fileStream, fileStream.Length);
            }
            catch
            {
                throw new InvalidOperationException("Ошибка при сохранении картинки");
            }
        }

        private async Task CheckExistenceOrCreateBucketAsync(string bucketName)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
            {
                throw new ArgumentException(
                    $"\"{nameof(bucketName)}\" не может быть пустым или содержать только пробел.", nameof(bucketName));
            }

            var isExists = await _minioClient.BucketExistsAsync(bucketName);

            if (!isExists)
            {
                await _minioClient.MakeBucketAsync(bucketName);
            }
        }
    }
}
