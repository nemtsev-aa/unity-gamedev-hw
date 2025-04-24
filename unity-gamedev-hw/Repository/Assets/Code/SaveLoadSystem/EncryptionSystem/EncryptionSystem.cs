using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace SaveLoadSystem {

    public class EncryptionSystem {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionSystem(byte[] key, byte[] iv) {
            if (key == null || key.Length != 32)
                throw new ArgumentException("Key must be 32 bytes (256 bits)");
            if (iv == null || iv.Length != 16)
                throw new ArgumentException("IV must be 16 bytes (128 bits)");

            _key = (byte[])key.Clone();
            _iv = (byte[])iv.Clone();
        }

        public static EncryptionSystem CreateNew() {
            using (Aes aes = Aes.Create()) {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();
                
                return new EncryptionSystem(aes.Key, aes.IV);
            }
        }

        public byte[] EncryptStringToBytes(string plainText) {
            try {
                using (Aes aes = Aes.Create()) {
                    aes.Key = _key;
                    aes.IV = _iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs)) {
                        sw.Write(plainText);
                        sw.Flush();
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
            catch (CryptographicException ex) {
                Debug.LogError($"Encryption failed: {ex.Message}");
                throw;
            }
        }

        public string DecryptToString(byte[] cipherText) {
            try {
                using (Aes aes = Aes.Create()) {
                    aes.Key = _key;
                    aes.IV = _iv;
                    aes.Padding = PaddingMode.PKCS7;

                    using (MemoryStream ms = new MemoryStream(cipherText))
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs)) {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (CryptographicException ex) {
                Debug.LogError($"Decryption failed: {ex.Message}");

                // Попробуем старый метод для совместимости с уже сохраненными файлами
                if (ex.Message.Contains("Padding"))
                    return TryLegacyDecrypt(cipherText);

                throw;
            }
        }

        private string TryLegacyDecrypt(byte[] cipherText) {
            try {
                using (Aes aes = Aes.Create()) {
                    aes.Key = _key;
                    aes.IV = _iv;
                    aes.Padding = PaddingMode.None;

                    using (MemoryStream ms = new MemoryStream(cipherText))
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs)) {
                        string result = sr.ReadToEnd();
                        // Удаляем возможные padding-байты вручную
                        int padLength = result[result.Length - 1];
                        if (padLength > 0 && padLength <= 16)
                            result = result.Substring(0, result.Length - padLength);
                        return result;
                    }
                }
            }
            catch {
                Debug.LogError("Legacy decryption attempt failed");
                throw;
            }
        }
    }

    }