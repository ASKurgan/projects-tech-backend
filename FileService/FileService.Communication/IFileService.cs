﻿using CSharpFunctionalExtensions;
using FileService.Contracts;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Communication
{
    public interface IFileService
    {
        /// <summary>
        /// Инициализация multipart-загрузки большого файла.
        /// </summary>
        /// <param name="request">Содержит имя, тип и размер файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ с идентификатором загрузки и URL для загрузки файла.</returns>
        Task<Result<StartMultipartUploadResponse, ErrorList>> StartMultipartUpload(
            StartMultipartUploadRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Завершение multipart-загрузки большого файла.
        /// </summary>
        /// <param name="request">Содержит данные о частях и идентификатор загрузки.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ с идентификатором файла.</returns>
        Task<Result<CompleteMultipartUploadResponse, ErrorList>> CompleteMultipartUpload(
            CompleteMultipartUploadRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Генерация предподписанной ссылки для загрузки чанка.
        /// </summary>
        /// <param name="request">Содержит данные о части файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ с URL для загрузки чанка.</returns>
        Task<Result<GetChunkUploadUrlResponse, ErrorList>> GetChunkUploadUrl(
            GetChunkUploadUrlRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Получение ссылки на скачивание файла.
        /// </summary>
        /// <param name="request">Содержит идентификатор файла и имя бакета.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ с URL для скачивания файла.</returns>
        Task<Result<GetDownloadUrlResponse, ErrorList>> GetDownloadUrl(
            GetDownloadUrlRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Получение ссылки на скачивание файла.
        /// </summary>
        /// <param name="request">Содержит идентификаторы файлов и имя бакета.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ с URL для скачивания файлов.</returns>
        Task<Result<GetDownloadUrlsResponse, ErrorList>> GetDownloadUrls(
            GetDownloadUrlsRequest request, CancellationToken cancellationToken);
    }
}
