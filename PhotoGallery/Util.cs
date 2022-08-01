using PhotoGallery.Properties;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoGallery
{
    public static class FileUtil
    {

        #region File Explorer Utils
        /// <summary>
        /// Creates an instance of the File Explorer object <see cref="OpenFileDialog"/>
        /// </summary>
        /// <returns>File Explorer object</returns>
        public static OpenFileDialog CreateFileExplorerObject()
        {
            OpenFileDialog usingDialogAndIsDisposable = new OpenFileDialog();
            usingDialogAndIsDisposable.Filter = "Images Files (*.png, *.jpg, *.jpeg, *.svg) | *.png; *.jpg; *.jpeg; *.svg";
            return usingDialogAndIsDisposable;
        }

        /// <summary>
        /// Displays the a File Explorer to the user and waits for the user to either select a file or cancel <see cref="DialogResult"/>
        /// </summary>
        /// <param name="ofd">File Explorer object</param>
        /// <returns>True if the user selected a file and false if the user cancelled</returns>
        public static bool ShowFileExplorerAndGetResult(OpenFileDialog ofd)
        {
            return ofd.ShowDialog() == DialogResult.OK;
        }

        /// <summary>
        /// Validates that the user's selected file does not reside in the application's selected target folder
        /// </summary>
        /// <param name="ofd">File Explorer object</param>
        /// <returns>True if the selected file is inside the target folder. Otherwise, returns false</returns>
        public static bool IsSelectedFileFromTargetFolder(OpenFileDialog ofd)
        {
            return ofd.FileName.Replace("\\" + ofd.SafeFileName, "") == Settings.Default.TargetFolder;
        }

        /// <summary>
        /// Gets the user-selected file's name including the extension. Does not include the path.
        /// </summary>
        /// <param name="ofd">File Explorer object</param>
        /// <returns>The name of the file the user selected</returns>
        public static string GetSelectedFileName(OpenFileDialog ofd)
        {
            return ofd.SafeFileName;
        }

        /// <summary>
        /// Gets the user-selected file's absolute path. Extension inclusion can be configured <see cref="FileDialog.AddExtension"/>
        /// </summary>
        /// <param name="ofd">File Explorer object</param>
        /// <returns>The path of the file the user selected</returns>
        public static string GetSelectedFileAbsolutePath(OpenFileDialog ofd)
        {
            return ofd.FileName;
        }
        #endregion

        #region Folder Explorer Utils
        /// <summary>
        /// Creates an instance of the Folder Explorer object <see cref="FolderBrowserDialog"/>
        /// </summary>
        /// <returns>Folder Explorere object</returns>
        public static FolderBrowserDialog CreateFolderExplorerObject()
        {
            FolderBrowserDialog usingDialogAndIsDisposable = new FolderBrowserDialog();
            usingDialogAndIsDisposable.ShowNewFolderButton = true;
            return usingDialogAndIsDisposable;
        }

        /// <summary>
        /// Displays the a Folder Explorer to the user and waits for the user to either select a folder or cancel <see cref="DialogResult"/>
        /// </summary>
        /// <param name="ffd">Folder Explorer object</param>
        /// <returns>True if the user selected a folder and false if the user cancelled</returns>
        public static bool ShowFolderExplorerAndGetResult(FolderBrowserDialog ffd)
        {
            return ffd.ShowDialog() == DialogResult.OK;
        }
        #endregion

        #region File Actions/Info

        /// <summary>
        /// Deletes a specified image file from the file system
        /// </summary>
        /// <param name="fileNameWithExtension">File path</param>
        public static void DeleteImageFile(string fileNameWithExtension)
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete($"{Settings.Default.TargetFolder}\\{fileNameWithExtension}");
        }

        /// <summary>
        /// Moves a specified image file in the file system to a specified destination. Optional potential object using file to dispose file references prior to moving it
        /// </summary>
        /// <param name="originalPath">File's current location including name and extension</param>
        /// <param name="destinationPath">Destination location including name and extension</param>
        /// <param name="objUsingFile">Optional parameter potentially using the specified file</param>
        public static void MoveImageFile(string originalPath, string destinationPath, IDisposable objUsingFile = null)
        {
            if (objUsingFile != null)
            {
                objUsingFile.Dispose();
            }
            File.Move(originalPath, destinationPath);
        }

        /// <summary>
        /// Retrieves an Array of FileInfo objects <see cref="FileInfo"/> representing the files in the specified directory
        /// </summary>
        /// <param name="dirPath">Directorty path</param>
        /// <returns>Array of files</returns>
        public static FileInfo[] GetFilesFromDirectory(string dirPath)
        {
            return new DirectoryInfo(dirPath).GetFiles();
        }
        #endregion
    }
}
