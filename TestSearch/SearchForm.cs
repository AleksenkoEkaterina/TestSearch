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
        Stopwatch elapsedTimeWatch = new Stopwatch();
        
        private int f_num;
        public SearchForm()  
        {
            InitializeComponent();
            binaryCheckCheck.Checked = Properties.Settings.Default.binaryCheck;
            inTextCheck.Checked = Properties.Settings.Default.inText;
            iconList.Images.Add("folder", new Bitmap(Properties.Resources.folder_open.ToBitmap()));
            iconList.Images.Add("document", new Bitmap(Properties.Resources.Generic_Document.ToBitmap()));
            dirTextBox.Text = Properties.Settings.Default.targetDirectory;
            processingFileLabel.Text = "";
            elapsedTime.Text = new TimeSpan(0, 0, 0, 0, 0).ToString(@"hh\:mm\:ss\.fff");
         //   p_num = 0;
            f_num = 0;
            processedNum.Text = 0.ToString();
            foundNum.Text = 0.ToString();
            templateTextBox.Text = Properties.Settings.Default.pattern;
            if (inTextCheck.Checked == false) binaryCheckCheck.Enabled = false;
            wildRadioButton.Checked = Properties.Settings.Default.wildcards;
            regRadioButton.Checked = !Properties.Settings.Default.wildcards;
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

        private Stopwatch time = new Stopwatch();
        private int p_num;
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> obj_list = e.Argument as List<object>;
            time.Start();
            DirectoryInfo dir = obj_list[0] as DirectoryInfo;
            string template = obj_list[1] as string;
            bool? inText=obj_list[2] as bool?;
            bool? binaryCheck = obj_list[3] as bool?;
            bool? regexp = obj_list[4] as bool?;
            if (regexp == false) template = WildcardToRegex(template);
            p_num = 0;
            TreeSearchBackground(dir, template, e, inText, binaryCheck);
            backgroundWorker.ReportProgress(50, (int?)p_num);
            time.Reset();
        }

        private string shortenPath(string path, int maxChars)
        {
            if (path.Length < maxChars) return path;
            string result;
            string[] dirs = path.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            result = dirs[dirs.Length-1];
            int index=dirs.Length-2;
            while(result.Length<(maxChars-dirs[0].Length)&&index>0)
            {
                result = dirs[index] + Path.DirectorySeparatorChar + result;
                index--;
            }
            if (index == 0)
                result = path;
            else result = dirs[0] + Path.DirectorySeparatorChar + "..." + Path.DirectorySeparatorChar + result;
            return result;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int state = e.ProgressPercentage;
            string path;
            if (state == 100)
            {
                path = e.UserState as string;
                if (addToTree(path)) //Can be duplicate
                {
                    f_num++;
                    foundNum.Text = f_num.ToString();
                    resultView.Update();
                    foundNum.Update();
                }
            }
            else if(state == 0)
            {
                object[] ar = e.UserState as object[];
                path = ar[0] as string;
                int? num = ar[1] as int?;
                processingFileLabel.Text = shortenPath(path, 64);
                processedNum.Text = num.ToString();
                processingFileLabel.Update();
                processedNum.Update();
            }
            else
            {
                int? num = e.UserState as int?;
                processedNum.Text = num.ToString();
                processedNum.Update();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            searchButton.Enabled = true;
            elapsedTimeWatch.Reset();
            elapsedTimeTimer.Stop();
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

            FileInfo[] files = { };
            try
            {
                files = directory.GetFiles();
            }
            catch (System.Security.SecurityException ex)
            {
                Console.Error.WriteLine("Security exception:" + directory.FullName);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("No access:" + directory.FullName);
            }
            catch
            {
                Console.Error.WriteLine("Internal error:" + directory.FullName);
            }
            foreach (FileInfo file in files)
            {
                p_num++;
                if (time.ElapsedMilliseconds > 2)
                {
                    object[] ar = new object[] {file.FullName, (int?)p_num};
                    backgroundWorker.ReportProgress(0, ar); //We'll lose some filenames here...
                    //...but no one can read this fast anyway
                    time.Restart();
                }
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Match m = Regex.Match(file.Name, pattern);
                if (m.Success)
                {
                    backgroundWorker.ReportProgress(100, file.FullName);
                    time.Restart();
                }
                if(inText==true)inTextSearch(file, e, pattern, binaryCheck);
            }
              
            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        private bool doBinaryCheck(string str)
        {
           return str.Contains("\0\0"); //Quite simple
        }
        private void inTextSearch(FileInfo file, DoWorkEventArgs e, string pattern, bool? binaryCheck)
        {
            using (StreamReader reader = new StreamReader(file.FullName, Encoding.UTF8, true, 8192))
            {
                char[] buffer = new char[8192];
                while (reader.EndOfStream == false)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    int read = reader.Read(buffer, 0, 8192);
                    string contents = Regex.Escape(new string(buffer, 0, read));
                    if (binaryCheck == true && doBinaryCheck(contents) == true) break;
                    Match m = Regex.Match(contents.ToLower(), pattern.ToLower());
                    if (m.Success)
                    {
                        backgroundWorker.ReportProgress(100, file.FullName);
                        time.Restart();
                        break;
                    }
                }
            }
        }

        private void startSearch()
        {
            string folderPath = Properties.Settings.Default.targetDirectory;
            
            resultView.Nodes.Clear();
            FileAttributes attr; //Let's validate our path
            try
            {
                attr = File.GetAttributes(folderPath);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine("File not found: " + folderPath);
                MessageBox.Show(this, "File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine("Directory not found: " + folderPath);
                MessageBox.Show(this, "Directory not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch
            {
                Console.Error.WriteLine("Internal error: " + folderPath);
                return;
            }
            //Let's validate pattern
            if(regRadioButton.Checked)
            {
                try
                {
                    new Regex(templateTextBox.Text);
                }
                catch
                {
                    Console.Error.WriteLine("Regex not valid: " + templateTextBox.Text);
                    MessageBox.Show(this, "The regular expression is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
            folderPath = folderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (!isDirectory) folderPath = folderPath.Remove(folderPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);

            List<object> arguments = new List<object>();
            arguments.Add(new DirectoryInfo(folderPath));
            arguments.Add(templateTextBox.Text);
            arguments.Add((bool?)inTextCheck.Checked);
            arguments.Add((bool?)binaryCheckCheck.Checked);
            arguments.Add((bool?)regRadioButton.Checked);
            while (backgroundWorker.IsBusy) { } //Wait for it
            searchButton.Enabled = false;
            stopButton.Enabled = true;
            p_num = 0;
            f_num = 0;
            processedNum.Text = p_num.ToString();
            foundNum.Text = f_num.ToString();
            elapsedTimeWatch.Start();
            elapsedTimeTimer.Start();
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
        }

        private void dirTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dirTextBox.Text != "") searchButton.Enabled = true;
            else searchButton.Enabled = false;
        }

        private void templateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (templateTextBox.Text != "") searchButton.Enabled = true;
            else searchButton.Enabled = false;
        }

        private void wildRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.wildcards = wildRadioButton.Checked;
            Properties.Settings.Default.Save();
        }

        private void elapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime.Text = elapsedTimeWatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");
            elapsedTime.Update();
        }

        

      
    }
}
