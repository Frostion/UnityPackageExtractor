using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace UnityPackageExtractor
{
    public partial class Form1 : Form
    {
        static string extract_folder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "extract"; //extract the raw contents of the unity package here
        static string build_folder = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "build"; //recreate the file structure of the unity package here
        
        Dictionary<string, Bitmap> asset_thumbnails = new Dictionary<string, Bitmap>(); //thumbnail images for asset files
        string package_name; //name of unity package file that was extracted
        int package_asset_count = 0; //number of assets in extracted unity package

        public Form1(string path)
        {
            InitializeComponent();
            
            treeView.PathSeparator = Path.DirectorySeparatorChar.ToString();

            //check if the program was launched with "open with"
            if(path != null)
            {
                if(Path.GetExtension(path) == ".unitypackage") //only accept .unitypackage files
                {
                    extractUnityPackage(path);
                }
                else
                {
                    MessageBox.Show("The file \"" + Path.GetFileName(path) + "\" is not a Unity package.", "Open Unity Package", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }


        //====================================================================================================================================================================
        //package extraction
        //====================================================================================================================================================================


        //extract contents of a unity package file to the extract folder
        void extractUnityPackage(string package_path)
        {
            //clear and prepare UI
            package_name = Path.GetFileNameWithoutExtension(package_path);
            package_asset_count = 0;

            treeView.Nodes.Clear();
            asset_thumbnails.Clear();

            toolStripButtonOpenPackage.Enabled = false;
            toolStripButtonExtractSelected.Enabled = false;
            toolStripButtonExtractAll.Enabled = false;

            labelSelectedItem.Text = "No selected item";
            pictureBoxAssetThumbnail.Refresh();

            toolStripStatusLabel.Text = "Extracting package...";

            //extract package
            backgroundWorkerExtractPackage.RunWorkerAsync(package_path);
        }
        private void backgroundWorkerExtractPackage_DoWork(object sender, DoWorkEventArgs e)
        {
            string package_path = (string)e.Argument;
            Dictionary<string, Bitmap> assets = new Dictionary<string, Bitmap>();

            //make sure temp folders are empty before starting
            clearDirectory(extract_folder);
            clearDirectory(build_folder);

            //un-gzip then un-tar the unitypackage to extract its contents (unitypackages are just renamed .tar.gz files, sneaky)
            Stream in_stream = File.OpenRead(package_path);
            Stream gzip_stream = new GZipInputStream(in_stream);
            TarArchive tar = TarArchive.CreateInputTarArchive(gzip_stream, Encoding.Default);
            tar.ExtractContents(extract_folder);
            tar.Close();
            gzip_stream.Close();
            in_stream.Close();

            //iterate through all the extracted folders that were in the unitypackage and move the asset files to the build folder, rebuilding the file structure
            //each file in the unitypackage is stored in its own folder with a randomly-generated name which contains the following files:
            // - "asset"        the actual asset file itself (texture, model, material, etc.)
            // - "pathname"     a plaintext file that contains the original filepath and filename of the asset file
            // - "preview.png"  the thumbnail of the asset shown in the unity editor's asset browser
            // - "asset.meta"   metadata that only the unity editor cares about

            string[] folders = Directory.GetDirectories(extract_folder);
            for (int i = 0; i < folders.Length; i++)
            {
                backgroundWorkerExtractPackage.ReportProgress((int)((float)(i + 1) / folders.Length * 100));

                string folder = folders[i];
                string asset_current_path = folder + Path.DirectorySeparatorChar + "asset";
                string asset_pathname_path = folder + Path.DirectorySeparatorChar + "pathname";
                string asset_thumbnail_path = folder + Path.DirectorySeparatorChar + "preview.png";

                //not all asset folders actually describe a file - those should be ignored
                if (File.Exists(asset_current_path))
                {
                     //read the intended path and name for this asset file
                     string asset_new_path = File.ReadAllText(asset_pathname_path);

                     // Check if the path ends with "00" and remove it
                     if (asset_new_path.EndsWith("00"))
                     {
                         asset_new_path = asset_new_path.Substring(0, asset_new_path.Length - 2);
                     }
                     //the path in the "pathname" file always uses forward slashes for its directory separator char, so let's replace all of those with the system separator char to be safe
                     asset_new_path = RemoveInvalidFilePathCharacters(asset_new_path);

                     //move and rename asset to recreate the original file structure inside our build folder
                     string asset_new_path_absolute = Path.Combine(build_folder, asset_new_path);
                    Directory.CreateDirectory(Path.GetDirectoryName(asset_new_path_absolute)); //create the subfolders that the asset should be in if they don't already exist
                    File.Move(asset_current_path, asset_new_path_absolute);

                    //save the thumbnail asset so it can be used in the UI
                    Bitmap thumbnail;
                    if(File.Exists(asset_thumbnail_path)) { thumbnail = new Bitmap(asset_thumbnail_path); }
                    else { thumbnail = new Bitmap(1, 1); }
                    assets.Add(asset_new_path, new Bitmap(thumbnail));
                    thumbnail.Dispose();
                }
            }

            e.Result = assets;
        }
        
         private string RemoveInvalidFilePathCharacters(string path)
         {
             var invalidChars = Path.GetInvalidPathChars();
             var validPath = new string(path.Where(ch => !invalidChars.Contains(ch)).ToArray());
             return validPath.Trim(); // Trim to remove any leading/trailing whitespace characters
         }
         
        //update the status label to show file reading progress
        private void backgroundWorkerExtractPackage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripStatusLabel.Text = "Reading files... " + e.ProgressPercentage + "%";
            //toolStripProgressBar.Value = e.ProgressPercentage;
        }

        //update UI when extraction has finished
        private void backgroundWorkerExtractPackage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripButtonOpenPackage.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show("The Unity package file \"" + package_name + "\" could not be extracted.\n\n" + e.Error.Message, "Open Unity Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Could not extract " + package_name;
            }
            else
            {
                asset_thumbnails = (Dictionary<string, Bitmap>)e.Result;
                treeView.BeginUpdate();
                foreach(KeyValuePair<string, Bitmap> kvp in asset_thumbnails) { addTreeViewItem(kvp.Key); }
                treeView.ExpandAll();
                treeView.EndUpdate();
                package_asset_count = asset_thumbnails.Count();
                toolStripStatusLabel.Text = package_name + " • " + package_asset_count + " assets found";
                toolStripButtonExtractAll.Enabled = true;
            }
        }


        //====================================================================================================================================================================
        //other file operations
        //====================================================================================================================================================================


        //delete and re-make a directory to ensure that it's empty
        void clearDirectory(string path)
        {
            if(Directory.Exists(path)) { Directory.Delete(path, true); }
            Directory.CreateDirectory(path);
        }

        //copy a file or folder (recursively) to another location
        void copyFile(string source_path, string dest_path)
        {
            if(File.Exists(source_path))
            {
                File.Copy(source_path, dest_path, true);
            }
            else if(Directory.Exists(source_path))
            {
                Directory.CreateDirectory(dest_path);
                string[] subfolders = Directory.GetDirectories(source_path);
                foreach(string subfolder in subfolders)
                {
                    copyFile(subfolder, dest_path + Path.DirectorySeparatorChar + Path.GetFileName(subfolder));
                }
                string[] subfiles = Directory.GetFiles(source_path);
                foreach(string subfile in subfiles)
                {
                    copyFile(subfile, dest_path + Path.DirectorySeparatorChar + Path.GetFileName(subfile));
                }
            }
        }

        //====================================================================================================================================================================
        //UI interactions and events
        //====================================================================================================================================================================


        //open package button is pressed
        private void toolStripButtonOpenPackage_Click(object sender, EventArgs e)
        {
            if(openFileDialogUnityPackage.ShowDialog() == DialogResult.OK)
            {
                extractUnityPackage(openFileDialogUnityPackage.FileName);
            }
        }

        //extract selected button is pressed
        private void toolStripButtonExtractSelected_Click(object sender, EventArgs e)
        {
            string node_path = treeView.SelectedNode.FullPath;
            if(isTreeViewNodeAFolder(treeView.SelectedNode))
            {
                saveFileDialog.Filter = "Folder|folder";
            }
            else
            {
                string extension = Path.GetExtension(treeView.SelectedNode.FullPath);
                saveFileDialog.Filter = extension + " file|" + extension;
            }
            saveFileDialog.FileName = treeView.SelectedNode.Text;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                copyFile(build_folder + Path.DirectorySeparatorChar + treeView.SelectedNode.FullPath, saveFileDialog.FileName);
            }
        }

        //extract all button is pressed
        private void toolStripButtonExtractAll_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Folder|folder";
            saveFileDialog.FileName = package_name;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                copyFile(build_folder, saveFileDialog.FileName);
            }
        }

        //a file is dragged into the application window - try to load it if it's a unitypackage
        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) { e.Effect = DragDropEffects.Link; }
            else { e.Effect = DragDropEffects.None; }
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if(paths.Length == 1) //only accept one file
                {
                    if(Path.GetExtension(paths[0]) == ".unitypackage") //only accept .unitypackage files
                    {
                        extractUnityPackage(paths[0]);
                    }
                    else
                    {
                        MessageBox.Show("The file \"" + Path.GetFileName(paths[0]) + "\" is not a Unity package.", "Open Unity Package", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }

        //user is trying to drag file out of application window
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeView.SelectedNode = (TreeNode)e.Item;
            string[] filename_to_drag = { build_folder + Path.DirectorySeparatorChar + treeView.SelectedNode.FullPath };
            treeView.DoDragDrop(new DataObject(DataFormats.FileDrop, filename_to_drag), DragDropEffects.Copy);
        }

        //a node has been selected in the treeview - turn on extract selected button and show selected item details
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripButtonExtractSelected.Enabled = true;

            string fullpath = "\\" + treeView.SelectedNode.FullPath;
            labelSelectedItem.Text = fullpath.Remove(fullpath.LastIndexOf(Path.DirectorySeparatorChar)) + "\\\n" + treeView.SelectedNode.Text;
            pictureBoxAssetThumbnail.Refresh();
        }

        //add item to treeview by path
        void addTreeViewItem(string path)
        {
            string[] path_nodes = path.Split(new[] { Path.DirectorySeparatorChar });
            TreeNodeCollection node_collection = treeView.Nodes;
            foreach(string node_name in path_nodes)
            {
                TreeNode found_node = null;
                foreach(TreeNode node in node_collection)
                {
                    if(node.Text == node_name)
                    {
                        found_node = node;
                        break;
                    }
                }
                if(found_node != null) { node_collection = found_node.Nodes; }
                else { node_collection = node_collection.Add(node_name).Nodes; }
            }
        }

        //check if a node is a folder or single asset
        bool isTreeViewNodeAFolder(TreeNode node)
        {
            return node.Nodes.Count > 0;
        }

        //display thumbnail of selected asset
        private void pictureBoxAssetThumbnail_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(SystemColors.Control);
            if(treeView.SelectedNode != null && asset_thumbnails.ContainsKey(treeView.SelectedNode.FullPath))
            {
                Bitmap thumbnail = asset_thumbnails[treeView.SelectedNode.FullPath];
                g.DrawImage(thumbnail, 0, 0, pictureBoxAssetThumbnail.Width, pictureBoxAssetThumbnail.Height);
            }
        }

        //application closed - delete all temp folders
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Directory.Exists(extract_folder)) { Directory.Delete(extract_folder, true); }
            if(Directory.Exists(build_folder)) { Directory.Delete(build_folder, true); }
        }
    }
}
