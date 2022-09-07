using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Framework.Core.Utility.Interfaces
{
    public interface IDirectoryUtility
    {
        void CopyAll(string sourceDirectory, string targetDirectory);
        Maybe<IEnumerable<string>> GetAllFilesInDirectory(string dir, string searchPattern = "*.*");
        DirectoryInfo CreateFolderIfNotExistAsync(string folderNameWithPath);
        Result Move(string sourcePath, string targetPath);
        void CopyDirectoryStructure(DirectoryInfo source, DirectoryInfo target);
        Result DeleteFile(string path);
        void DeleteFilesByPattern(string directoryPath, string searchPattern);
        void DeleteEmptyFoldersFromDirectory(string directoryPath);
        void DeleteDirectory(string path, bool isDeleteSubfolders = true);
        Task DeleteDirectoryAsync(string path, bool isDeleteSubfolders);
        Task DeleteFileAsync(string path);
    }
}