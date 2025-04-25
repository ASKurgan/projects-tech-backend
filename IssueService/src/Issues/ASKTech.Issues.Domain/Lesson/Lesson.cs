﻿using ASKTech.Issues.Domain.Issue.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Lesson
{
    public class Lesson : Entity<LessonId>, ISoftDeletable
    {
        // EF CORE
        private Lesson(LessonId id)
            : base(id)
        {
        }

        public Guid ModuleId { get; private set; }

        public Title Title { get; private set; }

        public Description Description { get; private set; }

        public Experience Experience { get; private set; }

        public Guid? PreviewId { get; private set; }

        public Guid[] Tags { get; private set; }

        public Guid[] Issues { get; private set; }

        public Video Video { get; private set; } = Video.None;

        public bool IsDeleted { get; private set; }

        public DateTime? DeletionDate { get; private set; }

        public Lesson(
            LessonId id,
            Guid moduleId,
            Title title,
            Description description,
            Experience experience,
            Guid[] tags,
            Guid[] issues)
            : base(id)
        {
            ModuleId = moduleId;
            Title = title;
            Description = description;
            Experience = experience;
            Tags = tags;
            Issues = issues;
        }

        /// <summary>
        /// Метод, который полностью обновляет урок.
        /// </summary>
        /// <param name="title">Название.</param>
        /// <param name="description">Описание.</param>
        /// <param name="experience">Опыт за урок.</param>
        /// <param name="video">Ссылка на видео.</param>
        /// <param name="fileId">Ссылка на файл.</param>
        /// <param name="tags">Список тегов к уроку.</param>
        /// <param name="issues">Список задач к уроку.</param>
        public void Update(
            Title title,
            Description description,
            Experience experience,
            Video video,
            Guid fileId,
            Guid[] tags,
            Guid[] issues)
        {
            Title = title;
            Description = description;
            Experience = experience;
            Video = video;
            PreviewId = fileId;
            Tags = tags;
            Issues = issues;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletionDate = null;
        }

        /// <summary>
        /// Добавить видео к уроку.
        /// </summary>
        /// <param name="video">Видео.</param>
        /// <returns>Выполненную операцию либо ошибку, если видео есть.</returns>
        public UnitResult<Error> AddVideo(Video video)
        {
            if (Video != Video.None)
                return Errors.General.AlreadyExist();

            Video = video;
            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Добавить задачку к уроку.
        /// </summary>
        /// <param name="issueId">Ссылка на задачу.</param>
        /// <returns>Выполненную операцию либо ошибку, что такая задача уже есть.</returns>
        public UnitResult<Error> AddIssue(Guid issueId)
        {
            if (Issues.Contains(issueId))
                return Errors.General.AlreadyExist();

            Issues = Issues.Append(issueId).ToArray();
            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Добавить тег к уроку.
        /// </summary>
        /// <param name="tagId">Ссылка на тег.</param>
        /// <returns>Выполненную операцию либо ошибку, что такой тег уже есть.</returns>
        public UnitResult<Error> AddTag(Guid tagId)
        {
            if (Tags.Contains(tagId))
                return Errors.General.AlreadyExist();

            Tags = Tags.Append(tagId).ToArray();
            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Удалить тег у урока.
        /// </summary>
        /// <param name="tagId">Ссылка на тег.</param>
        /// <returns>Выполненную операцию либо ошибку, что такой тег отсутствует.</returns>
        public UnitResult<Error> RemoveTag(Guid tagId)
        {
            if (!Tags.Contains(tagId))
                return Errors.General.NotFound(tagId, "tag");

            Tags = Tags.Where(id => id != tagId).ToArray();
            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Удалить задачу у урока.
        /// </summary>
        /// <param name="issueId">Ссылка на задачу.</param>
        /// <returns>Выполненную операцию либо ошибку, что такая задача отсутствует.</returns>
        public UnitResult<Error> RemoveIssue(Guid issueId)
        {
            if (!Issues.Contains(issueId))
                return Errors.General.NotFound(issueId, "tag");

            Issues = Issues.Where(id => id != issueId).ToArray();
            return UnitResult.Success<Error>();
        }
    }
}
