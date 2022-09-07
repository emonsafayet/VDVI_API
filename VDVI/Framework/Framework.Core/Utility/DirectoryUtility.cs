using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Framework.Core.Utility.Interfaces;
using Serilog;

namespace Framework.Core.Utility
{
    public class DirectoryUtility : IDirectoryUtility
    {
        private const string SystemVolumeInformation = "System Volume Information";

        private readonly ILogger _logger;

        public DirectoryUtility(ILogger logger)
        {
            _logger = logger;
        }

        public void CopyAll(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            Copy(diSource, diTarget);
        }

        private void Copy(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists; if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                //Console.WriteLine(fi.Name);
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each sub directory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                //Console.WriteLine(diSourceSubDir.Name);
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                Copy(diSourceSubDir, nextTargetSubDir);
            }
        }

        public Maybe<IEnumerable<string>> GetAllFilesInDirectory(string dir, string searchPattern = "*.*")
        {
            return Directory.Exists(dir)
                ? Directory.GetFiles(dir, searchPattern, SearchOption.AllDirectories)
                : null;
        }

        public DirectoryInfo CreateFolderIfNotExistAsync(string folderNameWithPath)
        {
            if (string.IsNullOrWhiteSpace(folderNameWithPath))
                throw new ArgumentException("Path is null or empty.", nameof(folderNameWithPath));

            DirectoryInfo createdDirectory;

            try
            {
                createdDirectory = Directory.CreateDirectory(folderNameWithPath);
            }
            catch (Exception ex)
            {
                createdDirectory = null;
                _logger.Error(ex, $"Failed to create directory from method:{nameof(CreateFolderIfNotExistAsync)}");
            }

            return createdDirectory;
        }

        public Result Move(string sourcePath, string targetPath)
        {
            try
            {

                Directory.Move(sourcePath, targetPath);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }

        public void CopyDirectoryStructure(DirectoryInfo source, DirectoryInfo target)
        {
            if (!source.Exists)
                return;

            Directory.CreateDirectory(target.FullName);

            foreach (var fi in source.GetFiles())
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);

            foreach (var diSourceSubDir in source.GetDirectories())
                CopyDirectoryStructure(diSourceSubDir, target.CreateSubdirectory(diSourceSubDir.Name));
        }


        public Result DeleteFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Result.Failure($"{nameof(path)} is null/empty");

            if (!File.Exists(path))
                return Result.Success($"[{path}] is already deleted");

            try
            {
                File.Delete(path);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }

        public void DeleteFilesByPattern(string directoryPath, string searchPattern)
        {
            foreach (var filePath in Directory.EnumerateFiles(directoryPath, searchPattern))
            {
                File.Delete(filePath);
            }
        }

        public void DeleteEmptyFoldersFromDirectory(string directoryPath)
        {
            if (directoryPath.Contains(SystemVolumeInformation))
                return;

            var directories = Directory.GetDirectories(directoryPath);

            if (!directories.Any())
                return;

            foreach (var dir in directories)
            {
                DeleteEmptyFoldersFromDirectory(dir);

                if (!dir.Contains(SystemVolumeInformation) && Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
                    Directory.Delete(dir, false);
            }
        }

        public void DeleteDirectory(string path, bool isDeleteSubfolders = true)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, isDeleteSubfolders);
        }

        public async Task DeleteDirectoryAsync(string path, bool isDeleteSubfolders)
        {
            await Task.Run(() =>
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, isDeleteSubfolders);
            });
        }

        public async Task DeleteFileAsync(string path)
        {
            await Task.Run(() =>
            {
                if (File.Exists(path))
                    File.Delete(path);
            });
        }
    }
}
