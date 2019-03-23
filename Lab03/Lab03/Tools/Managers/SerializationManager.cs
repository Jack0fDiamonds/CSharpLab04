using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab03.Tools.Managers
{
    static class SerializationManager
    {
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                if (file.CreateFolderAndCheckFileExistance())
                {
                    file.Delete();
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to serialize data to file {filePath}", ex);
            }
        }

        internal static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                if (!FileFolderHelper.CreateFolderAndCheckFileExistance(filePath))
                    throw new FileNotFoundException("File doesn't exist.");
                
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    return (TObject)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Failed to Deserialize Data From File {filePath}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Deserialize Data From File {filePath}", ex);
            }
        }
    }
}
