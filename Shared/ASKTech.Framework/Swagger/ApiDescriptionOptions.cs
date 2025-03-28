﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Swagger
{
    public class ApiDescriptionOptions
    {
        /// <summary>
        /// Заголовок API.
        /// </summary>
        public string Title { get; set; } = "My API";

        /// <summary>
        /// Версия API.
        /// </summary>
        public string Version { get; set; } = "v1";

        /// <summary>
        /// Описание для сервиса.
        /// </summary>
        public string Description { get; set; } = "Описание для сервиса.";

        /// <summary>
        /// Контактная информация.
        /// </summary>
        public ApiContactOptions Contact { get; set; } = new ApiContactOptions();
    }

    /// <summary>
    /// Представляет контактную информацию для Swagger.
    /// </summary>
    public class ApiContactOptions
    {
        /// <summary>
        /// Имя контактного лица или команды.
        /// </summary>
        public string Name { get; set; } = "Support Team";

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; } = "support@example.com";

        /// <summary>
        /// URL контакта.
        /// </summary>
        public string Url { get; set; } = "https://example.com";
    }
}
