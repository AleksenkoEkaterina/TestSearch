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
using System.Xml;

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
            f_num = 0;
            processedNum.Text = 0.ToString();
            foundNum.Text = 0.ToString();
            templateTextBox.Text = Properties.Settings.Default.pattern;
            if (inTextCheck.Checked == false) binaryCheckCheck.Enabled = false;
            wildRadioButton.Checked = Properties.Settings.Default.wildcards;
            regRadioButton.Checked = !Properties.Settings.Default.wildcards;
            folderCheck.Checked = Properties.Settings.Default.folders;
            caseCheck.Checked = Properties.Settings.Default.caseSensitive;

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
        private Stopwatch addingtime = new Stopwatch();
        private int p_num;
        private int added = 0;
        string last_name="";
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> obj_list = e.Argument as List<object>;
            DirectoryInfo dir = obj_list[0] as DirectoryInfo;
            string template = obj_list[1] as string;
            bool? inText=obj_list[2] as bool?;
            bool? binaryCheck = obj_list[3] as bool?;
            bool? regexp = obj_list[4] as bool?;
            bool? folders = obj_list[5] as bool?;
            bool? caseSensitive = obj_list[6] as bool?;
            if (regexp == false) template = WildcardToRegex(template);
            p_num = 0;
            time.Start();
            addingtime.Start();
           
            TreeSearchBackground(dir, template, e, inText, binaryCheck, folders, caseSensitive);
            if(last_name!="")
                backgroundWorker.ReportProgress(0, new object[]{last_name, (int?)p_num});
            time.Reset();
            addingtime.Reset();
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
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            setInterfaceEnabled(true);

            elapsedTimeWatch.Reset();
            elapsedTimeTimer.Stop();
        }


        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
        }

        private void TreeSearchBackground(DirectoryInfo directory, string pattern, DoWorkEventArgs e, bool? inText, bool? binaryCheck, bool? folders, bool? caseSensitive)
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
                if (folders == true)
                {
                    Match m;
                    if(caseSensitive==false) m = Regex.Match(dir.Name, pattern, RegexOptions.IgnoreCase);
                    else m = Regex.Match(dir.Name, pattern);
                    if (m.Success)
                    {
                        if (addingtime.ElapsedMilliseconds < 5) Thread.Sleep(5);
                        backgroundWorker.ReportProgress(100, dir.FullName);
                        addingtime.Restart();
                        time.Restart();
                    }
                }
                TreeSearchBackground(dir, pattern, e, inText, binaryCheck, folders, caseSensitive);
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
                    object[] ar = new object[] { file.FullName, (int?)p_num };
                    backgroundWorker.ReportProgress(0, ar); //We'll lose some filenames here...
                    //...but no one can read this fast anyway
                    time.Restart();
                }
                last_name = file.FullName; //For the last report
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
              
                Match m;
                if (caseSensitive == false) m = Regex.Match(file.Name, pattern, RegexOptions.IgnoreCase);
                else m = Regex.Match(file.Name, pattern);
                if (m.Success)
                {
                    if (addingtime.ElapsedMilliseconds < 5) Thread.Sleep(5);
                    backgroundWorker.ReportProgress(100, file.FullName);
                    time.Restart();
                    addingtime.Restart();
                }
                if(inText==true)inTextSearch(file, e, pattern, binaryCheck, caseSensitive);
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

        Encoding detectXML(string test)
        {
            if (test[0]!='<') return Encoding.Default;
            else
            {
                Match m=Regex.Match(test, "^<\\?xml.+?encoding=[\"']([^\"']+)[\"'].*?\\?>");           
                if (m.Success)
                {
                    return Encoding.UTF8;
                    //Other unicode will be detected by BOM
                    //Other codepages won't be detected anyway
                }
            }
            return Encoding.Default;
        }
        private void inTextSearch(FileInfo file, DoWorkEventArgs e, string pattern, bool? binaryCheck, bool? caseSensitive)
        {
            bool detectedUTF = false;
            Encoding enc = Encoding.Default;
            try
            {
                using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                {
                    using (StreamReader testreader = new StreamReader(fs, Encoding.UTF8))
                    {
                        string test = testreader.ReadLine();

                        if (testreader.EndOfStream == false && test.Length > 0)
                            enc = detectXML(test);

                        fs.Position = 0;
                        if (enc != Encoding.Default) detectedUTF = true;
                        using (StreamReader reader = new StreamReader(fs, enc, !detectedUTF, 8192))
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
                                string contents = new string(buffer, 0, read);
                                if (reader.CurrentEncoding == Encoding.UTF8) detectedUTF = true;
                                if (binaryCheck == true && doBinaryCheck(contents) == true) break;
                                Match m;
                                if (caseSensitive == false) m = Regex.Match(contents, pattern, RegexOptions.IgnoreCase);
                                else m = Regex.Match(contents, pattern);
                                if (m.Success)
                                {
                                    if (addingtime.ElapsedMilliseconds < 5) Thread.Sleep(5);
                                    backgroundWorker.ReportProgress(100, file.FullName);
                                    time.Restart();
                                    addingtime.Restart();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch(System.Security.SecurityException ex)
            {
                Console.Error.WriteLine("Security exception: " + file.FullName);
            }
            catch(IOException ex)
            {
                Console.Error.WriteLine("Cannot access file: " + file.FullName);
            }
            catch(NotSupportedException ex)
            {
                Console.Error.WriteLine("Not supported: " + file.FullName);
            }
            catch
            {
                Console.Error.WriteLine("Internal error: " + file.FullName);
            }
        }

        private void setInterfaceEnabled(bool value)
        {
            searchButton.Enabled = value;
            browseButton.Enabled = value;
            stopButton.Enabled = !value;

            dirTextBox.Enabled = value;
            templateTextBox.Enabled = value;

            caseCheck.Enabled = value;
            if (!value || !inTextCheck.Checked) binaryCheckCheck.Enabled = false;
            else binaryCheckCheck.Enabled = true;
            folderCheck.Enabled = value;
            inTextCheck.Enabled = value;

            wildRadioButton.Enabled = value;
            regRadioButton.Enabled = value;
            
            

        }
        private void startSearch()
        {
            string folderPath = Properties.Settings.Default.targetDirectory;
            
            
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
           // folderPath = folderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (!isDirectory)
            {
                MessageBox.Show("The path is not a directory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }//folderPath = folderPath.Remove(folderPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            List<object> arguments = new List<object>();
            arguments.Add(new DirectoryInfo(folderPath));
            arguments.Add(templateTextBox.Text);
            arguments.Add((bool?)inTextCheck.Checked);
            arguments.Add((bool?)binaryCheckCheck.Checked);
            arguments.Add((bool?)regRadioButton.Checked);
            arguments.Add((bool?)folderCheck.Checked);
            arguments.Add((bool?)caseCheck.Checked);
            while (backgroundWorker.IsBusy) { } //Wait for it
            setInterfaceEnabled(false);
            resultView.Nodes.Clear();
            f_num = 0;
            processedNum.Text = 0.ToString();
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

        private void folderCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.folders = folderCheck.Checked;
            Properties.Settings.Default.Save();
        }

        private void caseCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.caseSensitive = caseCheck.Checked;
            Properties.Settings.Default.Save();
        }

        

      
    }
}
