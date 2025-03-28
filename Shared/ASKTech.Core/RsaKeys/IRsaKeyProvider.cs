using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.RsaKeys
{
    public interface IRsaKeyProvider
    {
        /// <summary>
        /// Возвращает RSA, инициализированный приватным ключом.
        /// </summary>
        /// <returns>RSA private key.</returns>
        RSA GetPrivateRsa();

        /// <summary>
        /// Возвращает RSA, инициализированный публичным ключом.
        /// </summary>
        /// <returns>RSA public key.</returns>
        RSA GetPublicRsa();
    }
}
