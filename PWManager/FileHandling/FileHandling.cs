using Logging;
using System;
using System.IO;

namespace PWManager
{
    public static class FileHandling
    {
        public static string _Directory { get; set; }
        public static string _FileName { get; set; }

        /// <summary>
        /// this method returns the file path for getting the key file
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            string _FilePath = Path.Combine(_Directory, _FileName);
            return _FilePath;
        }

        /// <summary>
        /// this method resets the file path on application close or log out
        /// </summary>
        /// <returns>the reset file path</returns>
        public static string ResetFilePath()
        {
            _Directory = "";
            _FileName = "";
            string _FilePath = Path.Combine(_Directory, _FileName);
            return _FilePath;
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            try
            {
                using (Stream stream = System.IO.File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"[File Handling] [Write to Binary File] Error Writing to File! {ex}");
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
