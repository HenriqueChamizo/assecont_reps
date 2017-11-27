using BioproxC.RWTech.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BioproxC.RWTech.Criptografia
{
    public static class AES
    {
        public static int QTD_BYTES_BLOCO = 16;
        public static int QTD_CARACTERES_CHAVE = 64;

        private static String ALGORITHM = "AES";
	    //private static String BLOCK_MODE = "ECB";
	    //private static String PADDING = "NoPadding";
	    private static CipherMode cipher = CipherMode.ECB;
        private static byte[] IV = new byte[16];

        public static byte[] getKey(String chave) 
        {
            try
            {
                return Conversor.hexStringToByteArray(chave);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static CipherMode getInstanceCipher()
        {
		    return cipher;
	    }
	
	    private static byte[] encrypt(byte[] pacoteOriginal, byte[] key)
        {
            try
            {
                using (Aes aes = Aes.Create(ALGORITHM))
                {
                    aes.Mode = cipher;
                    aes.Padding = PaddingMode.None;
                    aes.Key = key;
                    //ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, IV);

                    //MemoryStream ms = new MemoryStream();
                    //CryptoStream cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
                    //cryptoStream.Write(pacoteOriginal, 0, pacoteOriginal.Length);
                    
                    return EncryptStringToBytes_Aes(pacoteOriginal, aes.Key, aes.IV);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.None;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        //using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        //{

                        //    //Write all data to the stream.
                        //    swEncrypt.Write(plainText);
                        //}
                        csEncrypt.Write(plainText, 0, plainText.Length);
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static byte[] criptografar(byte[] bufferNaoCriptografado, String hash)
        {
            int indiceBufferCru = 0;
            int tamanhoPacoteOriginal = bufferNaoCriptografado.Length;
            int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
            if (tamanhoPacoteOriginal % 16 > 0)
                tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
            byte[] bufferCriptografado = new byte[tamanhoPacoteCriptografado];
            try
            {
                byte[] bufferCru = new byte[AES.QTD_BYTES_BLOCO];
                byte[] blocoBufferCriptografado;
                byte[] key = AES.getKey(hash);
                int ultimoIndiceBufferCriptografado = 0;
                for (int i = 0; i < tamanhoPacoteOriginal; i++)
                {
                    bufferCru[indiceBufferCru] = bufferNaoCriptografado[i];
                    indiceBufferCru++;
                    if (indiceBufferCru == AES.QTD_BYTES_BLOCO)
                    {
                        indiceBufferCru = 0;
                        blocoBufferCriptografado = AES.encrypt(bufferCru, key);
                        for (int j = 0; j < blocoBufferCriptografado.Length; j++)
                        {
                            bufferCriptografado[ultimoIndiceBufferCriptografado] = blocoBufferCriptografado[j];
                            ultimoIndiceBufferCriptografado++;
                        }
                    }
                }

                if (indiceBufferCru > 0)
                {
                    while (indiceBufferCru < AES.QTD_BYTES_BLOCO)
                    {
                        bufferCru[indiceBufferCru] = (byte)0xFF;
                        indiceBufferCru++;
                    }
                    indiceBufferCru = 0;
                    blocoBufferCriptografado = AES.encrypt(bufferCru, key);
                    for (int j = 0, i = ultimoIndiceBufferCriptografado; j < blocoBufferCriptografado.Length; j++, i++)
                    {
                        try
                        {
                            bufferCriptografado[i] = blocoBufferCriptografado[j];
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
		    return bufferCriptografado;
	    }
	
	    public static int defineTamanhoPacote(int tamanhoPacoteOriginal)
        {
            int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
            if (tamanhoPacoteOriginal % AES.QTD_BYTES_BLOCO > 0)
            {
                tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
            }
            return tamanhoPacoteCriptografado;
        }

        public static byte[] descriptografar(byte[] bufferCriptografado, String hash)
        {
            int indiceBufferCru = 0;
            int tamanhoPacoteOriginal = bufferCriptografado.Length;
            int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
            if (tamanhoPacoteOriginal % AES.QTD_BYTES_BLOCO > 0)
                tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
            byte[] bufferDescriptografado = new byte[tamanhoPacoteCriptografado];
            try
            {
                byte[] bufferCru = new byte[AES.QTD_BYTES_BLOCO];
                byte[] blocoBufferDescriptografado;
                byte[] key = AES.getKey(hash);
                int ultimoIndiceBufferDescriptografado = 0;
                for (int i = 0; i < tamanhoPacoteOriginal; i++)
                {
                    bufferCru[indiceBufferCru] = bufferCriptografado[i];
                    indiceBufferCru++;
                    if (indiceBufferCru == AES.QTD_BYTES_BLOCO)
                    {
                        indiceBufferCru = 0;
                        blocoBufferDescriptografado = AES.decrypt(bufferCru, key);
                        for (int j = 0; j < blocoBufferDescriptografado.Length; j++)
                        {
                            bufferDescriptografado[ultimoIndiceBufferDescriptografado] = blocoBufferDescriptografado[j];
                            ultimoIndiceBufferDescriptografado++;
                        }
                    }
                }

                if (indiceBufferCru > 0)
                {
                    while (indiceBufferCru < AES.QTD_BYTES_BLOCO)
                    {
                        bufferCru[indiceBufferCru] = (byte)0xFF;
                        indiceBufferCru++;
                    }
                    indiceBufferCru = 0;
                    blocoBufferDescriptografado = AES.decrypt(bufferCru, key);
                    for (int j = 0, i = ultimoIndiceBufferDescriptografado; j < blocoBufferDescriptografado.Length; j++, i++)
                    {
                        try
                        {
                            bufferDescriptografado[i] = blocoBufferDescriptografado[j];
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
		    return bufferDescriptografado;
	    }
	
	    public static byte[] decrypt(byte[] pacoteEncriptado, byte[] key)
        {
            try
            {
                using (Aes aes = Aes.Create(ALGORITHM))
                {
                    aes.Mode = cipher;
                    aes.Padding = PaddingMode.None;
                    aes.Key = key;
                    //ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, IV);

                    //MemoryStream ms = new MemoryStream();
                    //CryptoStream cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
                    //cryptoStream.Write(pacoteEncriptado, 0, pacoteEncriptado.Length);

                    return DecryptStringFromBytes_Aes(pacoteEncriptado, aes.Key, aes.IV);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static byte[] DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            byte[] decrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.None;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting  stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                        decrypted = msDecrypt.ToArray();
                    }
                }
            }
            return decrypted;
        }
    }
}
