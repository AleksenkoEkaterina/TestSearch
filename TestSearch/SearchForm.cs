using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestSearch
{
    public partial class SearchForm : Form
    {
     
       public SearchForm()
        {

            InitializeComponent();
            binaryCheckCheck.Checked = Properties.Settings.Default.binaryCheck;
            inTextCheck.Checked = Properties.Settings.Default.inText;
            iconList.Images.Add("folder", new Bitmap(Properties.Resources.folder_open.ToBitmap()));
            iconList.Images.Add("document", new Bitmap(Properties.Resources.Generic_Document.ToBitmap()));
            dirTextBox.Text=Properties.Settings.Default.targetDirectory;
            processingFileLabel.Text = "";
            templateTextBox.Text = Properties.Settings.Default.pattern;
            if (inTextCheck.Checked == false) binaryCheckCheck.Enabled = false;
        
            
        }

        private void dirTextBox_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.targetDirectory = dirTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void dirTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings.Default.targetDirectory = dirTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private bool addToTree(string fullPath)
        {
            FileAttributes attr; //Let's validate our path
            try
            {
                 attr = File.GetAttributes(fullPath);
            }
            catch(FileNotFoundException ex)
            {
                Console.Error.WriteLine("File not found: " + fullPath);
                return false;
            }
            catch(DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine("Directory not found: " + fullPath);
                return false;
            }
            catch
            {
                Console.Error.WriteLine("Internal error: " + fullPath);
                return false;
            }

            string[] dirs = fullPath.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            string rootPath=Properties.Settings.Default.targetDirectory;
            string path=rootPath;
            string[] rootDirs =rootPath.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            int index = 0;
            bool found = false;
            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
           
            while (index<rootDirs.Length && dirs[index] == rootDirs[index] ) index++;
            index--; //Now it's root directory on both
            if (index < 0) return false; //Not a subdirectory of root
            //let's find root
            TreeNode currentNode = new TreeNode();
            foreach (TreeNode node in resultView.Nodes)
            {
                if(node.Text==dirs[index])
                {
                    found = true;
                    currentNode = node;
                    break;
                }
            }
            if(!found)//No root, eh? Let's create it!
            {
                currentNode = new TreeNode(dirs[index]);
                currentNode.Tag = new DirectoryInfo(rootPath);
                currentNode.ImageKey = "folder";
                resultView.Nodes.Add(currentNode);
                currentNode.Expand();
                
            }
            index++;
            //now let's find, where it branches
            for(; index<dirs.Length; index++)
            {
                found = false;
                foreach (TreeNode node in currentNode.Nodes)
                {
                    if(node.Text==dirs[index])
                    {
                        found = true;
                        currentNode = node;
                        path+=Path.DirectorySeparatorChar+node.Text;
                        break;
                    }
                    
                }
                if (!found) break; //Last found node is "current node" now
                //found our branch;
            }
            //adding branch
            //folders
            int shift=0;
            if (index > dirs.Length - 1) return false; //existing
            if (!isDirectory) shift = 1;
            TreeNode addNode;
            for (; index < dirs.Length-shift; index++ )
            {
                path+=Path.DirectorySeparatorChar+dirs[index];
                addNode=new TreeNode(dirs[index]);
                addNode.Tag = new DirectoryInfo(path);
                addNode.ImageKey = "folder";
                addNode.SelectedImageKey = "folder";
                currentNode.Nodes.Add(addNode);
                currentNode.Expand();
                currentNode = addNode;
            }
            //files?
            if (!isDirectory)
            {
                path += Path.DirectorySeparatorChar + dirs[index];
                addNode = new TreeNode(dirs[index]);
                addNode.Tag = new FileInfo(path);
                addNode.ImageKey = "document";
                addNode.SelectedImageKey = "document";
                currentNode.Nodes.Add(addNode);
                currentNode.Expand();
            }
            return true;
            
        }

     //   private List<string> addToTreeBuffer = new List<string>();

        private Stopwatch time = new Stopwatch();
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> obj_list = e.Argument as List<object>;
            time.Start();
            DirectoryInfo dir = obj_list[0] as DirectoryInfo;
            string template = obj_list[1] as string;
            bool? inText=obj_list[2] as bool?;
            bool? binaryCheck = obj_list[3] as bool?;
            

            
            TreeSearchBackground(dir, template, e, inText, binaryCheck);
            time.Reset();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        /*    List<string> toAdd = e.UserState as List<string>;
            foreach (string path in toAdd)
            {*/
            int state = e.ProgressPercentage;
            string path = e.UserState as string;
            if (path == @"H:\Downloads\True_Patch_Gold_FINAL_with_Hotfix7\True_Patch_Gold_FINAL_with_Hotfix7\Extras\Optional ")
            {
                int i = 0;
            }
            if (state > 0)
            {
                addToTree(path);
                resultView.Update();
            }
            if(state !=50)
            {
                processingFileLabel.Text = path;
                processingFileLabel.Update();
            }
           
           /* }*/
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          //  addToTreeBuffer.Clear();
            searchButton.Enabled = true;
        }


        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).
            Replace("\\*", ".*").
            Replace("\\?", ".") + "$";
        }
        private void TreeSearchBackground(DirectoryInfo directory, string pattern, DoWorkEventArgs e, bool? inText, bool? binaryCheck)
        {
            DirectoryInfo[] subDirs = { };
           
            try
            {
                subDirs = directory.GetDirectories();
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine("Directory not found: " + directory.Name);
            }
            catch (System.Security.SecurityException ex)
            {
                Console.Error.WriteLine("Security exception: " + directory.Name);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("No rights to access: " + directory.Name);
            }
            catch
            {
                Console.Error.WriteLine(directory.FullName);
            }
            if(backgroundWorker.CancellationPending)
            {
                e.Cancel=true;
                return;
            }
            foreach (DirectoryInfo dir in subDirs)
            {
                TreeSearchBackground(dir, pattern, e, inText, binaryCheck);
            }


            FileInfo[] matches = {};
            try
            {
                matches = directory.GetFiles(pattern);
            }
            catch(System.Security.SecurityException ex)
            {
                Console.Error.WriteLine("Security exception:" + directory.FullName);
            }
            foreach (FileInfo match in matches)
            {
                
                     //Too fast search, too slow GUI
                    //Flooded with events
                    //Won't get a faster search anyway
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }             
                if (time.ElapsedMilliseconds < 50)
                {
                    Thread.Sleep(10);
                    time.Restart();      
                }
                backgroundWorker.ReportProgress(100, match.FullName); //Show only found files, this is too fast anyway
            }
            if(inText==true)
            {
                inTextSearch(directory, pattern, binaryCheck);
            }
        }
        private bool doBinaryCheck(string str)
        {
           return str.Contains("\0\0");
        }
        private void inTextSearch(DirectoryInfo directory, string pattern, bool? binaryCheck)
        {
            string regexPattern = WildcardToRegex(pattern);
            FileInfo[] files={};
            try
            {
                files = directory.GetFiles();
            }
            catch (System.Security.SecurityException ex)
            {
                Console.Error.WriteLine("Security exception:" + directory.FullName);
            }

            foreach (FileInfo file in files)
            {
                if(time.ElapsedMilliseconds<50)
                {
                    Thread.Sleep(10);
                    time.Restart();
                }
                backgroundWorker.ReportProgress(0, file.FullName);
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    while (reader.EndOfStream == false)
                    {
                        string contents = reader.ReadLine();
                        if (binaryCheck == true && doBinaryCheck(contents) == true) break;
                        Match m = Regex.Match(contents.ToLower(), regexPattern.ToLower());
                        if (m.Success)
                        {
                            backgroundWorker.ReportProgress(50, file.FullName);
                            break;
                        }
                    }
                }
            }

        }

        private void startSearch()
        {
            string folderPath = Properties.Settings.Default.targetDirectory;
            stopButton.Enabled = true;
            resultView.Nodes.Clear();
            FileAttributes attr; //Let's validate our path
            try
            {
                attr = File.GetAttributes(folderPath);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine("File not found: " + folderPath);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine("Directory not found: " + folderPath);
                return;
            }
            catch
            {
                Console.Error.WriteLine("Internal error: " + folderPath);
                return;
            }
            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
            folderPath = folderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (!isDirectory) folderPath = folderPath.Remove(folderPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            List<object> arguments = new List<object>();
            arguments.Add(new DirectoryInfo(folderPath));
            arguments.Add(templateTextBox.Text);
            arguments.Add((bool?)inTextCheck.Checked);
            arguments.Add((bool?)binaryCheckCheck.Checked);
            while (backgroundWorker.IsBusy) { } //Wait for it
            searchButton.Enabled = false;
            backgroundWorker.RunWorkerAsync(arguments);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            startSearch();
            
         
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            searchButton.Enabled = true;
        }

        private void binaryCheckCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.binaryCheck = binaryCheckCheck.Checked;
            Properties.Settings.Default.Save();
        }

        private void inTextCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.inText= inTextCheck.Checked;
            Properties.Settings.Default.Save();
            if (inTextCheck.Checked) binaryCheckCheck.Enabled = true;
            else binaryCheckCheck.Enabled = false;
        }

        private void templateTextBox_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.pattern = templateTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void templateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Properties.Settings.Default.pattern = templateTextBox.Text;
                Properties.Settings.Default.Save();
                startSearch();
            }
        }

        private void resultView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Tag is FileInfo)
            {
                FileInfo info = e.Node.Tag as FileInfo;
                Process.Start(info.DirectoryName);
            }
            else
            {
                DirectoryInfo dinfo=e.Node.Tag as DirectoryInfo;
                Process.Start(dinfo.FullName);
                e.Node.Expand();
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = Properties.Settings.Default.targetDirectory;
                fbd.Description = "Select target directory:";
                fbd.ShowNewFolderButton = false;
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    dirTextBox.Text = fbd.SelectedPath;
                    Properties.Settings.Default.targetDirectory = dirTextBox.Text;
                    Properties.Settings.Default.Save();

                }
            }
       /*     OpenFileDialog ofl = new OpenFileDialog();
            ofl.InitialDirectory = Properties.Settings.Default.targetDirectory;
            if(ofl.ShowDialog()==DialogResult.OK)
            {
                dirTextBox.Text = ofl.FileName;
                Properties.Settings.Default.targetDirectory = dirTextBox.Text;
                Properties.Settings.Default.Save();
            }*/
        }

        

      
    }
}
